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

namespace DarkLight_Themes
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            loadGurrentTheme();
        }

        private void loadGurrentTheme()
        {
            // Check saved theme and set the corresponding RadioButton
            string savedTheme = Properties.Settings.Default.ColorMode;

            switch (savedTheme)
            {
                case "Dark":
                    darkModeBtn.IsChecked = true;
                    break;
                case "Light":
                    lightModeBtn.IsChecked = true;
                    break;
                default:
                    // Set a default if no saved value
                    lightModeBtn.IsChecked = true;
                    break;
            }
        }
        #region events
        private void darkModeBtn_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.ColorMode = "Dark";
            Properties.Settings.Default.Save();
        }

        private void lightModeBtn_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.ColorMode = "Light";
            Properties.Settings.Default.Save();
        }
        #endregion
    }
}
