using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.ViewModels
{
    public  class ViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<UserModel> _users;
        public ObservableCollection<UserModel> Users
        {
            get => _users;
            set
            {
                _users = value;
                OnPropertyChanged(nameof(Users));
            }
        }

        #region constructor
        public ViewModel()
        {
            _users = new ObservableCollection<UserModel>();

        }
        #endregion

        #region functions
        // update Users with new data
        public void SetUsers(List<UserModel> userList)
        {
            Users.Clear();  // Clear any previous users
            foreach (var user in userList)
            {
                Users.Add(user);  // Add the new users
            }
        }
        #endregion


        #region Inotify interface

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
