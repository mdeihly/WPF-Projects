using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfBasics
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ApplyButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"Description is: {this.DescriptionText.Text}");
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox chkBox = (CheckBox)sender;
            this.LengthTextBox.Text += chkBox.Content;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(this.NoteTextBox == null)
            {
                return;
            }
            ComboBox cb = (ComboBox)sender;
            var item = cb.SelectedItem as ComboBoxItem;
            NoteTextBox.Text = item.Content.ToString();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ComboBox_SelectionChanged(this.FinishCb, null);
        }

        private void SupplierName_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.SupplierCode.Text = (sender as TextBox).Text;

        }
    }
}
