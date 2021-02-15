using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Devices;
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

namespace Launch_Soundboard
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Button[,] launchpadButtonDefinitions = new Button[9, 9];
        public static bool editMode = false;
        LaunchpadManager lpMngr;

        public MainWindow()
        {
            InitializeComponent();

            // Generate Grid Buttons
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    Button btn = new Button();
                    btn.Click += LaunchGridButton_OnClick;
                    Grid.SetRow(btn, y + 1);
                    Grid.SetColumn(btn, x);
                    launchGrid.Children.Add(btn);

                    launchpadButtonDefinitions[x, y] = btn;
                }
            }


            // Generate Toolbar Buttons
            for (int y = 0; y < 8; y++)
            {
                Button btn = new Button();
                btn.Click += LaunchGridButton_OnClick;
                btn.BorderThickness = new Thickness(5);
                btn.Margin = new Thickness(2);
                btn.BorderBrush = new SolidColorBrush(Color.FromRgb(51, 51, 51));
                Grid.SetRow(btn, y + 1);
                Grid.SetColumn(btn, 8);
                launchGrid.Children.Add(btn);

                launchpadButtonDefinitions[8, y] = btn;
            }


            // Edit Button
            editButton.Click += (a, b) =>
            {
                editMode = !editMode;

                if (editMode)
                {
                    editButton.Visibility = Visibility.Collapsed;
                    saveButton.Visibility = Visibility.Visible;
                    cancelButton.Visibility = Visibility.Visible;
                }
                else
                {
                    editButton.Visibility = Visibility.Visible;
                    saveButton.Visibility = Visibility.Collapsed;
                    cancelButton.Visibility = Visibility.Collapsed;
                }
            };

            // Setup Launchpad
            lpMngr = new LaunchpadManager();

            // Setup Start/Stop Button
            setColor_BarRight(8, LaunchpadButtonColor.Red);



            /*
            //COLOR EXAMPLE 
            setColor(1, 1, LaunchpadButtonColor.Red);
            setColor(1, 2, LaunchpadButtonColor.Orange);
            setColor(1, 3, LaunchpadButtonColor.Yellow);
            setColor(1, 4, LaunchpadButtonColor.Pink);
            setColor(1, 5, LaunchpadButtonColor.Lila);

            setColor(2, 1, LaunchpadButtonColor.DarkBlue);
            setColor(2, 2, LaunchpadButtonColor.LightBlue);
            setColor(2, 3, LaunchpadButtonColor.Purple);

            setColor(3, 1, LaunchpadButtonColor.Oliv);
            setColor(3, 2, LaunchpadButtonColor.Green);
            setColor(3, 3, LaunchpadButtonColor.LightGreen);


            setColor(4, 1, LaunchpadButtonColor.White);
            setColor(4, 2, LaunchpadButtonColor.Gray);

            setColor(4, 2, LaunchpadButtonColor.Gray);

            setColor_BarRight(3, LaunchpadButtonColor.Oliv);
            setColor_BarRight(4, LaunchpadButtonColor.Gray);
            setColor_BarRight(5, LaunchpadButtonColor.Red);
            */


        }

        /************************
         *        Events
         ************************/

        public void LaunchGridButton_OnClick(object sender, EventArgs args)
        {
            if (editMode)
            {
                EditForm editWnd = new EditForm();
                editWnd.ShowDialog();
            }
            else
            {

            }
        }



        /************************
         * Public Update Buttons
         ************************/

        public void LaunchGridButton_Press(int row, int col)
        {
            ((Button)launchpadButtonDefinitions[col, row]).RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        }

        public void setColor(int row, int col, LaunchpadButtonColor color)
        {
            // Update GUI
            ((Button)launchpadButtonDefinitions[col - 1, row - 1]).Background = new SolidColorBrush(lpMngr.enum2color(color));

            // Update launchpad
            lpMngr.setColor_Grid(row, col, color);
        }

        public void setColor_BarRight(int nr, LaunchpadButtonColor color)
        {
            // Update GUI
            ((Button)launchpadButtonDefinitions[8, nr - 1]).BorderBrush = new SolidColorBrush(lpMngr.enum2color(color));

            // Update launchpad
            lpMngr.setColor_BarRight(nr, color);
        }
    }
}
