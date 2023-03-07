using DemoLibrary.Model;
using ViewModel.Common;

using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using WPF_DataGrid;
using DemoLibrary.Models;

namespace ViewModel
{
    /// <summary>
    /// the view model is the component that is responsible for handling the application's presentation logic and state
    /// </summary>
    public class MainWindowViewModel : BindableBase
    {
        private const String ApplicationDefaultLanguage = "en-US";
        
        private Window _view;
        public MainWindowViewModel(Window view)
        {
            // get the main window
            _view = view;

            // Load language optional item;
            this._languageCollection = new ObservableCollection<LanguageTypeInfo> {
                new("en-US", "English"),
                new("it-IT", "Italiano"),
                new("fr-FR", "Français")
            };
            
            // set default languaged
            this.SelectedLanguage = ApplicationDefaultLanguage;

            //_people = new ObservableCollection<PersonModel>();
            ////_people.Add(new PersonModel()
            ////{
            ////    FirstName = "hussein",
            ////    LastName = "mdeihly",
            ////    Available = true
            ////});
            //_person = new PersonModel();
        }
               

        #region BindingSource

        private ObservableCollection<LanguageTypeInfo> _languageCollection;

        /// <summary>
        /// The Collection of optional language.
        /// </summary>
        public ObservableCollection<LanguageTypeInfo> LanguageCollection
        {
            get => this._languageCollection;
            set => this._languageCollection = value;
        }

        private String? _selectedLanguage;

        /// <summary>
        /// The Language which was selected.
        /// </summary>
        public String? SelectedLanguage
        {
            get => this._selectedLanguage;
            set
            {
                if (this.SetProperty(ref this._selectedLanguage, value))
                {
                    this.ApplyLanguage.RaiseCanExecuteChanged();
                }
            }
        }

        public PersonModel Person { get; set; } = new PersonModel();

         public ObservableCollection<PersonModel> People { get; set; } = new ObservableCollection<PersonModel>();

        
        private DelegateCommand? _addPerson;
        public DelegateCommand AddPerson => _addPerson ?? (_addPerson = new(() =>
        {
            // Add to datagrid
            People.Add(Person);

        }));


        private DelegateCommand? _applyLanguage;

        /// <summary>
        /// Apply language change.
        /// If the activity language is the same as
        /// <see cref="MainWindowViewModel.SelectedLanguage"/>, Application
        /// is not allowed.
        /// </summary>
        /// <remarks>
        /// https://stackoverflow.com/questions/68572536/icommand-canexecutechanged-is-always-null
        /// </remarks>
        public DelegateCommand ApplyLanguage => _applyLanguage ?? (_applyLanguage = new(() => {
            this.UpdateApplicationLanguage();
            this.ApplyLanguage.RaiseCanExecuteChanged();
        }, () =>
        {
            var dictionaryResources = Application.Current.Resources;
            if (dictionaryResources["Language_Code"] is String lang)
            {
                return SelectedLanguage != lang;
            }
            return false;
        }));


        // HUSSEIN
        private DelegateCommand? _closeWindowCommand;
        public DelegateCommand CloseWindowCommand => _closeWindowCommand ?? (_closeWindowCommand = new(() =>
        {
            // close main window
            _view.Close();
        }));

        #endregion BindingSource

        /// <summary>
        /// Get the <see cref="ResourceDictionary"/> of language.
        /// </summary>
        /// <param name="lang">Language Name, pass <see langword="null"/> to get the default language.</param>
        /// <returns>a <see cref="ResourceDictionary"/> which path is
        /// <c>$"/Resource/Language/{<paramref name="lang"/>}.xaml"</c>.
        /// if it does not exist, return default language one.</returns>
        private static ResourceDictionary? LoadLanguageResourceDictionary(String? lang = null)
        {
            try
            {
                // if is null or blank string, set lang as default.
                lang = String.IsNullOrWhiteSpace(lang) ? ApplicationDefaultLanguage : lang;
                var langUri = new Uri($@"\Dictionaries\Languages\{lang}.xaml", UriKind.Relative);
                return Application.LoadComponent(langUri) as ResourceDictionary;
            }
            // The file does not exist.
            catch (IOException)
            {
                return null;
            }
        }

        /// <summary>
        /// Update the Application Language to <see cref="MainWindowViewModel.SelectedLanguage"/>.
        /// </summary>
        private void UpdateApplicationLanguage()
        {
            ResourceDictionary? langResource = LoadLanguageResourceDictionary(this.SelectedLanguage) ??
                                               LoadLanguageResourceDictionary();
            
            Application.Current.Resources.MergedDictionaries.Clear();
            // Add new language.
            Application.Current.Resources.MergedDictionaries.Add(langResource);
        }
    }
}
