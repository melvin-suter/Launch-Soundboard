using Microsoft.Win32;
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

            foreach (LaunchpadButtonColor clr in (LaunchpadButtonColor[])Enum.GetValues(typeof(LaunchpadButtonColor)))
            {
                colorBox.Items.Add(clr);
            }
        
            
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

        private void selectFile(object sender, RoutedEventArgs e)
        {
            string folderPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\sounds\\";
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Audio Files|*.mp3;*.wav";
            fileDialog.CheckFileExists = true;
            fileDialog.InitialDirectory = folderPath;

            
            if( fileDialog.ShowDialog() == true)
            {
                if(fileDialog.FileName.StartsWith(folderPath))
                {
                    soundBox.Text = fileDialog.FileName.Substring(folderPath.Length);
                }
            }
        }

        private void DeleteClick(object sender, RoutedEventArgs e)
        {
            volume = "1";
            sound = "";
            color = "Off";
            result = true;
            this.Close();
        }
    }
}
