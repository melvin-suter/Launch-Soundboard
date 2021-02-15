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
using System.Windows.Shapes;

namespace Launch_Soundboard
{
    /// <summary>
    /// Interaction logic for EditForm.xaml
    /// </summary>
    public partial class EditForm : Window
    {
        public string volume { get { return volumeBox.Text; } set { volumeBox.Text = value; } }
        public string sound { get { return soundBox.Text; } set { soundBox.Text = value; } }
        public string color { get { return colorBox.Text; } set { colorBox.Text = value; } }
        public bool result = false;

        public EditForm()
        {
            InitializeComponent();
        }
        private void SaveClick(object sender, RoutedEventArgs e)
        {
            result = true;
            this.Close();
        }

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
