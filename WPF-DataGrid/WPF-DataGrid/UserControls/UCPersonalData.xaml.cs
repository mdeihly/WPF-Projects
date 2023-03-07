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

namespace WPF_DataGrid.UserControls
{
    /// <summary>
    /// Logica di interazione per UCPersonalData.xaml
    /// </summary>
    public partial class UCPersonalData : UserControl
    {
        public object TextBlockText
        {
            get { return (object)GetValue(TextBlockTextProperty); }
            set { SetValue(TextBlockTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TextBlockProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextBlockTextProperty =
            DependencyProperty.Register("TextBlockText", typeof(object), typeof(UCPersonalData), new PropertyMetadata(0));

        public object TextBoxText
        {
            get { return (object)GetValue(TextBoxTextProperty); }
            set { SetValue(TextBoxTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TextBlockProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextBoxTextProperty =
            DependencyProperty.Register("TextBoxText", typeof(object), typeof(UCPersonalData), new PropertyMetadata(0));

        public UCPersonalData()
        {
            InitializeComponent();
        }
    }
}
