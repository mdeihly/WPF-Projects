using DemoLibrary;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Xml.Linq;

namespace ComboBox.ViewModel
{
    public class ViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<PersonModel> ModelList { get; set; }

        private PersonModel _selectedPerson;

        // INotifyPropertyChanged event
        public event PropertyChangedEventHandler PropertyChanged;

        // Method to raise the PropertyChanged event
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public PersonModel SelectedPerson
        {
            get => _selectedPerson; 
            set 
            {
                if (_selectedPerson != value)
                {
                    _selectedPerson = value;
                    OnPropertyChanged(nameof(SelectedPerson));
                }
            }
        }


        public ViewModel()
        {
            loadModelList();
        }

        private void loadModelList()
        {
            ModelList = new ObservableCollection<PersonModel>()
            {
                new PersonModel()
                {
                    PersonId = 1,
                    FirstName = "Alex",
                    LastName = "Joe",
                    Country = "Canada"

                },

                new PersonModel()
                {
                    PersonId = 2,
                    FirstName = "John",
                    LastName = "Mae",
                    Country = "USA",
                    IsAvailable = false

                },

                new PersonModel()
                {
                    PersonId = 3,
                    FirstName = "Mike",
                    LastName = "Ted",
                    Country = "United Kingdom",
                    IsAvailable = false
                },

                new PersonModel()
                {
                    PersonId = 4,
                    FirstName = "James",
                    LastName = "Qui",
                    Country = "Brazil",
                    IsAvailable= true
                },

                new PersonModel()
                {
                    PersonId = 5,
                    FirstName = "Leo",
                    LastName = "Lo",
                    Country = "Argentina",
                    IsAvailable = true

                }

            };
        }

    }
}
