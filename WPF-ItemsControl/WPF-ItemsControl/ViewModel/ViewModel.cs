using DemoLibrary;
using System;
using System.Collections.ObjectModel;

namespace WPF_ItemsControl.ViewModel
{
    public class ViewModel
    {
        public ObservableCollection<PersonModel> ModelList { get; set; }

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
                    PersonId = 1,
                    FirstName = "Alex",
                    LastName = "Joe",
                    Country = "Canada",
                    IsAvailable = true

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
