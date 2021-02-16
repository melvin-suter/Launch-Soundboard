using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Melanchall.DryWetMidi.Devices;
using NAudio.Wave;

namespace Launch_Soundboard
{
    /// <summary>
    /// Interaction logic for SelectDevices.xaml
    /// </summary>
    public partial class SelectDevices : Window
    {

        public SelectDevices()
        {
            InitializeComponent();

            foreach (var dev in InputDevice.GetAll())
            {
                inputDevice.Items.Add(dev.Name);
            }
            inputDevice.SelectedItem = Config.config.inputDevice;

            foreach (var dev in OutputDevice.GetAll())
            {
                outputDevice.Items.Add(dev.Name);
            }
            outputDevice.SelectedItem = Config.config.outputDevice;

            for(int i = 0; i < WaveOut.DeviceCount; i++)
            {
                audioDevice.Items.Add(WaveOut.GetCapabilities(i).ProductName);
            }
            audioDevice.SelectedItem = Config.config.audioOutputDeviceName;

            Timer t = new Timer((a) =>
            {
                Dispatcher.Invoke(() => this.Close());
            }, null, 15000, 15000);

            this.Closed += (a,b) => {
                Config.config.outputDevice = outputDevice.Text;
                Config.config.inputDevice = inputDevice.Text;
                Config.config.audioOutputDeviceID = audioDevice.SelectedIndex;
                Config.config.audioOutputDeviceName = audioDevice.Text;
            };
        }

        private void SaveClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
