﻿//  ---------------------------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//
//  The MIT License (MIT)
//
//  Permission is hereby granted, free of charge, to any person obtaining a copy
//  of this software and associated documentation files (the "Software"), to deal
//  in the Software without restriction, including without limitation the rights
//  to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//  copies of the Software, and to permit persons to whom the Software is
//  furnished to do so, subject to the following conditions:
//
//  The above copyright notice and this permission notice shall be included in
//  all copies or substantial portions of the Software.
//
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//  OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//  THE SOFTWARE.
//  ---------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ViewModel.Common {

    /// <summary>
    /// An <see cref="ICommand"/> whose delegates can be attached for <see cref="Execute"/> and <see cref="CanExecute"/>.
    /// </summary>
    /// <typeparam name="T">Parameter type.</typeparam>
    /// <remarks>
    /// The constructor deliberately prevents the use of value types.
    /// Because ICommand takes an object, having a value type for T would cause unexpected behavior when CanExecute(null) is called during XAML initialization for command bindings.
    /// Using default(T) was considered and rejected as a solution because the implementor would not be able to distinguish between a valid and defaulted values.
    /// <para/>
    /// Instead, callers should support a value type by using a nullable value type and checking the HasValue property before using the Value property.
    /// <example>
    ///     <code>
    /// public MyClass()
    /// {
    ///     this.submitCommand = new DelegateCommand&lt;int?&gt;(this.Submit, this.CanSubmit);
    /// }
    ///
    /// private bool CanSubmit(int? customerId)
    /// {
    ///     return (customerId.HasValue &amp;&amp; customers.Contains(customerId.Value));
    /// }
    ///     </code>
    /// </example>
    /// https://github.com/microsoft/Windows-appsample-networkhelper/blob/master/DemoApps/QuizGame/Common/DelegateCommand.cs
    /// </remarks>
    public class DelegateCommand<T> : DelegateCommandBase {

        /// <summary>
        /// Initializes a new instance of <see cref="DelegateCommand{T}"/>.
        /// </summary>
        /// <param name="executeMethod">Delegate to execute when Execute is called on the command. This can be null to just hook up a CanExecute delegate.</param>
        /// <remarks><seealso cref="CanExecute"/> will always return true.</remarks>
        public DelegateCommand(Action<T?> executeMethod)
            : this(executeMethod, (o) => true) { }

        /// <summary>
        /// Initializes a new instance of <see cref="DelegateCommand{T}"/>.
        /// </summary>
        /// <param name="executeMethod">Delegate to execute when Execute is called on the command. This can be null to just hook up a CanExecute delegate.</param>
        /// <param name="canExecuteMethod">Delegate to execute when CanExecute is called on the command. This can be null.</param>
        /// <exception cref="ArgumentNullException">When both <paramref name="executeMethod"/> and <paramref name="canExecuteMethod"/> ar <see langword="null" />.</exception>
        public DelegateCommand(Action<T?> executeMethod, Predicate<T?> canExecuteMethod)
            : base((o) => executeMethod((T?)o), (o) => canExecuteMethod((T?)o)) {
            if (executeMethod is null || canExecuteMethod is null) {
                throw new ArgumentNullException(nameof(executeMethod));
            }
        }

        /// <summary>
        /// Factory method to create a new instance of <see cref="DelegateCommand{T}"/> from an awaitable handler method.
        /// </summary>
        /// <param name="executeMethod">Delegate to execute when Execute is called on the command.</param>
        /// <returns>Constructed instance of <see cref="DelegateCommand{T}"/></returns>
        public static DelegateCommand<T> FromAsyncHandler(Func<T?, Task> executeMethod) {
            return new DelegateCommand<T>(executeMethod);
        }

        public static DelegateCommand<T> FromAsyncHandler(Func<T?, Task> executeMethod, Predicate<T?> canExecuteMethod) {
            return new DelegateCommand<T>(executeMethod, canExecuteMethod);
        }

        ///<summary>
        ///Determines if the command can execute by invoked the <see cref="Func{T,Bool}"/> provided during construction.
        ///</summary>
        ///<param name="parameter">Data used by the command to determine if it can execute.</param>
        ///<returns>
        ///<see langword="true" /> if this command can be executed; otherwise, <see langword="false" />.
        ///</returns>
        public Boolean CanExecute(T parameter) {
            return base.CanExecute(parameter);
        }

        ///<summary>
        ///Executes the command and invokes the <see cref="Action{T}"/> provided during construction.
        ///</summary>
        ///<param name="parameter">Data used by the command.</param>
        public async Task Execute(T parameter) {
            await base.Execute(parameter);
        }

        private DelegateCommand(Func<T?, Task> executeMethod)
            : this(executeMethod, (o) => true) { }

        private DelegateCommand(Func<T?, Task> executeMethod, Predicate<T?> canExecuteMethod)
            : base((o) => executeMethod((T?)o), (o) => canExecuteMethod((T?)o)) {
            if (executeMethod is null || canExecuteMethod is null) {
                throw new ArgumentNullException(nameof(executeMethod));
            }
        }
    }

    /// <summary>
    /// An <see cref="ICommand"/> whose delegates do not take any parameters for <see cref="Execute"/> and <see cref="CanExecute"/>.
    /// </summary>
    /// <seealso cref="DelegateCommandBase"/>
    /// <seealso cref="DelegateCommand{T}"/>
    public class DelegateCommand : DelegateCommandBase {

        /// <summary>
        /// Creates a new instance of <see cref="DelegateCommand"/> with the <see cref="Action"/> to invoke on execution.
        /// </summary>
        /// <param name="executeMethod">The <see cref="Action"/> to invoke when <see cref="ICommand.Execute"/> is called.</param>
        public DelegateCommand(Action executeMethod)
            : this(executeMethod, () => true) { }

        /// <summary>
        /// Creates a new instance of <see cref="DelegateCommand"/> with the <see cref="Action"/> to invoke on execution
        /// and a <see langword="Func" /> to query for determining if the command can execute.
        /// </summary>
        /// <param name="executeMethod">The <see cref="Action"/> to invoke when <see cref="ICommand.Execute"/> is called.</param>
        /// <param name="canExecuteMethod">The <see cref="Func{TResult}"/> to invoke when <see cref="ICommand.CanExecute"/> is called</param>
        public DelegateCommand(Action executeMethod, Func<Boolean> canExecuteMethod)
            : base((o) => executeMethod(), (o) => canExecuteMethod()) {
            if (executeMethod is null || canExecuteMethod is null) {
                throw new ArgumentNullException(nameof(executeMethod));
            }
        }

        /// <summary>
        /// Factory method to create a new instance of <see cref="DelegateCommand"/> from an awaitable handler method.
        /// </summary>
        /// <param name="executeMethod">Delegate to execute when Execute is called on the command.</param>
        /// <returns>Constructed instance of <see cref="DelegateCommand"/></returns>
        public static DelegateCommand FromAsyncHandler(Func<Task> executeMethod) {
            return new DelegateCommand(executeMethod);
        }

        /// <summary>
        /// Factory method to create a new instance of <see cref="DelegateCommand"/> from an awaitable handler method.
        /// </summary>
        /// <param name="executeMethod">Delegate to execute when Execute is called on the command. This can be null to just hook up a CanExecute delegate.</param>
        /// <param name="canExecuteMethod">Delegate to execute when CanExecute is called on the command. This can be null.</param>
        /// <returns>Constructed instance of <see cref="DelegateCommand"/></returns>
        public static DelegateCommand FromAsyncHandler(Func<Task> executeMethod, Func<bool> canExecuteMethod) {
            return new DelegateCommand(executeMethod, canExecuteMethod);
        }

        ///<summary>
        /// Executes the command.
        ///</summary>
        public async Task Execute() {
            await this.Execute(null);
        }

        /// <summary>
        /// Determines if the command can be executed.
        /// </summary>
        /// <returns>Returns <see langword="true"/> if the command can execute, otherwise returns <see langword="false"/>.</returns>
        public Boolean CanExecute() {
            return this.CanExecute(null);
        }

        private DelegateCommand(Func<Task> executeMethod)
            : this(executeMethod, () => true) { }

        private DelegateCommand(Func<Task> executeMethod, Func<Boolean> canExecuteMethod)
            : base((o) => executeMethod(), (o) => canExecuteMethod()) {
            if (executeMethod == null || canExecuteMethod == null)
                throw new ArgumentNullException(nameof(executeMethod));
        }
    }

    /// <summary>
    /// An <see cref="ICommand"/> whose delegates can be attached for <see cref="Execute"/> and <see cref="CanExecute"/>.
    /// </summary>
    public abstract class DelegateCommandBase : ICommand {
        private readonly Func<Object?, Task> _executeMethod;
        private readonly Predicate<Object?> _canExecuteMethod;

        /// <summary>
        /// Creates a new instance of a <see cref="DelegateCommandBase"/>, specifying both the execute action and the can execute function.
        /// </summary>
        /// <param name="executeMethod">The <see cref="Action"/> to execute when <see cref="ICommand.Execute"/> is invoked.</param>
        /// <param name="canExecuteMethod">The <see cref="Predicate{Object}"/> to invoked when <see cref="ICommand.CanExecute"/> is invoked.</param>
        /// <exception cref="ArgumentNullException"></exception>
        protected DelegateCommandBase(Action<Object?> executeMethod, Predicate<Object?> canExecuteMethod) {
            if (executeMethod is null || canExecuteMethod is null) {
                throw new ArgumentNullException(nameof(executeMethod));
            }
            this._executeMethod = (arg) => {
                executeMethod(arg);
                return Task.Delay(0);
            };
            this._canExecuteMethod = canExecuteMethod;
        }

        /// <summary>
        /// Creates a new instance of a <see cref="DelegateCommandBase"/>, specifying both the Execute action as an awaitable Task and the CanExecute function.
        /// </summary>
        /// <param name="executeMethod">The <see cref="Func{Object,Task}"/> to execute when <see cref="ICommand.Execute"/> is invoked.</param>
        /// <param name="canExecuteMethod">The <see cref="Predicate{Object}"/> to invoked when <see cref="ICommand.CanExecute"/> is invoked.</param>
        protected DelegateCommandBase(Func<Object?, Task> executeMethod, Predicate<Object?> canExecuteMethod) {
            if (executeMethod is null || canExecuteMethod is null) {
                throw new ArgumentNullException(nameof(executeMethod));
            }
            this._executeMethod = executeMethod;
            this._canExecuteMethod = canExecuteMethod;
        }

        /// <summary>
        /// Raises <see cref="ICommand.CanExecuteChanged"/> on the UI thread so every
        /// command invoker can requery <see cref="ICommand.CanExecute"/>.
        /// </summary>
        protected virtual void OnCanExecuteChanged() {
            //var handlers = this.CanExecuteChanged;
            //if (handlers is not null) {
            //    handlers(this, EventArgs.Empty);
            //}
            this.CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Raises <see cref="DelegateCommandBase.CanExecuteChanged"/> on the UI thread so every command invoker
        /// can requery to check if the command can execute.
        /// <remarks>Note that this will trigger the execution of <see cref="DelegateCommandBase.CanExecute"/> once for each invoker.</remarks>
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1030:UseEventsWhereAppropriate")]
        public void RaiseCanExecuteChanged() {
            this.OnCanExecuteChanged();
        }

        /// <summary>
        /// Executes the command with the provided parameter by invoking the <see cref="Action{Object}"/> supplied during construction.
        /// </summary>
        /// <param name="parameter"></param>
        protected async Task Execute(Object? parameter) {
            await this._executeMethod(parameter);
        }

        /// <summary>
        /// Determines if the command can execute with the provided parameter by invoking the <see cref="Func{Object,Bool}"/> supplied during construction.
        /// </summary>
        /// <param name="parameter">The parameter to use when determining if this command can execute.</param>
        /// <returns>Returns <see langword="true"/> if the command can execute.  <see langword="False"/> otherwise.</returns>
        protected Boolean CanExecute(Object? parameter) {
            return this._canExecuteMethod(parameter);
        }

        #region ICommand

        async void ICommand.Execute(Object? parameter) {
            await this.Execute(parameter);
        }

        Boolean ICommand.CanExecute(Object? parameter) {
            return this.CanExecute(parameter);
        }

        public event EventHandler? CanExecuteChanged;

        #endregion ICommand
    }
}
