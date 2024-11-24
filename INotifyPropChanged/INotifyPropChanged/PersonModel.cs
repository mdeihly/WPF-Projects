using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace INotifyPropChanged
{
    public class PersonModel : INotifyPropertyChanged
    {

        private string _name;

        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged(nameof(Name));  // Notify that Name has changed
                }
            }
        }

        public PersonModel()
        {
            Task.Run(() =>
            {
                while(true)
                {
                    Random r = new Random();
                    Name = r.Next(1, 1000).ToString();
                    Debug.WriteLine($"Name: {Name}");
                    Thread.Sleep(1000);
                }
            });
        }

        // INotifyPropertyChanged event
        public event PropertyChangedEventHandler PropertyChanged;

        // Method to raise the PropertyChanged event
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}



