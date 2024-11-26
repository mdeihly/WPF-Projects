using DataLibrary.Models;
using GUI.ViewModels;
using SqlDataManager.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ViewModel _viewModel;

        #region constructor
        public MainWindow()
        {
            InitializeComponent();

            _viewModel = new ViewModel();
            DataContext = _viewModel;
        }
        #endregion

        #region events
        private async void searchBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Get the search criteria from the TextBox
                string lastName = txtBox.Text;

                // Fetch the list of users asynchronously from the database
                var list = await UserData.getPeople(txtBox.Text);
                //List<UserModel> list = UserData.getPeople(txtBox.Text);

                // Update the Users property in the ViewModel
                _viewModel.SetUsers(list.ToList()); // This will clear and add the new users to the ObservableCollection
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            };
        }
        #endregion
    }
}
