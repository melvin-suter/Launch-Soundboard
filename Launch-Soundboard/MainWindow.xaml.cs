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
        int currentSet = 0;

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
            editButton.Click += toggleEditMode;
            saveButton.Click += toggleEditMode;

            // Load Config
            Config.load();
            this.Closing += (a, b) => Config.save(); // Save on exit

            // Setup Launchpad
            lpMngr = new LaunchpadManager();

            // Setup Start/Stop Button
            setColor_BarRight(8, LaunchpadButtonColor.Red);

            // Setup Load Grid
            loadSoundSet();

            // Setup Launchpad Events
            lpMngr.buttonPressed_BarRight += (a, e) =>
            {
                if (e.nr < 8)
                {
                    currentSet = e.nr - 1;
                    Dispatcher.Invoke(() =>
                    {
                        loadSoundSet();
                    });
                }
                else
                {
                    SoundManager.stopAll();
                }
            };
            lpMngr.buttonPressed_Grid += (a, e) => GridButtonClick(e.col - 1, e.row - 1);

            // Setup Sound Manager Events
            SoundManager.playingSound += (a, b) => Dispatcher.Invoke(() => setColor_BarRight(8, LaunchpadButtonColor.Green));
            SoundManager.stoppedSound += (a, b) => Dispatcher.Invoke(() => setColor_BarRight(8, LaunchpadButtonColor.Red));

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

        public void toggleEditMode(object sender, EventArgs args)
        {
            editMode = !editMode;

            if (editMode)
            {
                editButton.Visibility = Visibility.Collapsed;
                saveButton.Visibility = Visibility.Visible;
            }
            else
            {
                editButton.Visibility = Visibility.Visible;
                saveButton.Visibility = Visibility.Collapsed;
                Config.save();
            }
        }

        public void LaunchGridButton_OnClick(object sender, EventArgs args)
        {
            Button btn = (Button)sender;
            int col = Grid.GetColumn((UIElement)btn);
            int row = Grid.GetRow((UIElement)btn) - 1;

            if (col < 8)
            {
                if (editMode)
                {
                    EditForm editWnd = new EditForm();
                    editWnd.color = Config.config.sets[currentSet].buttons[col, row].color.ToString();
                    editWnd.volume = Config.config.sets[currentSet].buttons[col, row].volume.ToString();
                    editWnd.sound = Config.config.sets[currentSet].buttons[col, row].sound;
                    editWnd.ShowDialog();

                    if (editWnd.result == true)
                    {

                        Config.config.sets[currentSet].buttons[col, row].color = (LaunchpadButtonColor)Enum.Parse(typeof(LaunchpadButtonColor), editWnd.color, true);
                        Config.config.sets[currentSet].buttons[col, row].sound = editWnd.sound;
                        Config.config.sets[currentSet].buttons[col, row].volume = Convert.ToDouble(editWnd.volume);
                        loadSoundSet();
                    }
                }
                else
                {
                    GridButtonClick(col, row);
                }
            }
            else
            {
                if (row < 7)
                {
                    currentSet = row ;
                    Dispatcher.Invoke(() =>
                    {
                        loadSoundSet();
                    });
                }
                else
                {
                    SoundManager.stopAll();
                }
            }
        }

        public void GridButtonClick(int col, int row)
        {
            SoundButton btn = Config.config.sets[currentSet].buttons[col, row];
            SoundManager.playSong(btn.sound, (float)btn.volume);

        }



        /************************
         * Public Update Buttons
         ************************/

        public void loadSoundSet()
        {
            for (int row = 1; row <= 8; row++)
            {
                for (int col = 1; col <= 8; col++)
                {
                    SoundButton btn = Config.config.sets[currentSet].buttons[col - 1, row - 1];
                    setText(row, col, System.IO.Path.GetFileNameWithoutExtension(btn.sound), lpMngr.enum2foreground(btn.color));
                    setColor(row, col, btn.color);
                }

                if (row < 8)
                    setColor_BarRight(row, row - 1 == currentSet ? LaunchpadButtonColor.Green : LaunchpadButtonColor.Gray);
            }

        }

        public void LaunchGridButton_Press(int row, int col)
        {
            ((Button)launchpadButtonDefinitions[col, row]).RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        }

        public void setText(int row, int col, string text, Color color)
        {
            ((Button)launchpadButtonDefinitions[col - 1, row - 1]).Content = text;
            ((Button)launchpadButtonDefinitions[col - 1, row - 1]).Foreground = new SolidColorBrush(color);
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
