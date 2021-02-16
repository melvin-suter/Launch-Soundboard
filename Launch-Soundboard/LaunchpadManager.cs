using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Launch_Soundboard
{
    public enum LaunchpadButtonColor
    {
        LightBlue,
        DarkBlue,
        Purple,
        LightGreen,
        Green,
        Oliv,
        Yellow,
        Orange,
        Red,
        Lila,
        Pink,        
        White,
        Gray,
        Off
    }

    public class LaunchpadManager
    {
        public delegate void GridEventHandler(object sender, GridEventArgs e);
        public delegate void BarEventHandler(object sender, BarEventArgs e);
        public event GridEventHandler buttonPressed_Grid;
        public event BarEventHandler buttonPressed_BarRight;
        public event BarEventHandler buttonPressed_BarTop;


        OutputDevice outDev;
        InputDevice inDev;
        List<int> pressedButtons = new List<int>();
        Dictionary<SevenBitNumber, LaunchpadButtonColor> colorPair = new Dictionary<SevenBitNumber, LaunchpadButtonColor>();

        public LaunchpadManager()
        {

            // Get Device
            SelectDevices wnd = new SelectDevices();
            wnd.ShowDialog();

            outDev = OutputDevice.GetByName(Config.config.outputDevice);
            inDev = InputDevice.GetByName(Config.config.inputDevice);

            // Setup Event Handler
            buttonPressed_Grid = new GridEventHandler((a, b) => { });
            buttonPressed_BarRight = new BarEventHandler((a, b) => { });
            buttonPressed_BarTop = new BarEventHandler((a, b) => { });

            // Handle Input Events
            inDev.EventReceived += (a, e) =>
            {
                var midiDevice = (MidiDevice)a;

                // is on sidebar
                if (e.Event.EventType == MidiEventType.ControlChange)
                {
                    var midiEvent = (ControlChangeEvent)e.Event;

                    // Button Down
                    if (midiEvent.ControlValue == 127)
                    {
                        int row = 9 - (midiEvent.ControlNumber / 10);
                        int col = midiEvent.ControlNumber - ((midiEvent.ControlNumber / 10) * 10);

                        // Right
                        if (col == 9)
                        {
                            buttonPressed_BarRight.Invoke(new object(), new BarEventArgs(row));
                        }

                        // On Top
                        if (row == 0)
                        {
                            buttonPressed_BarRight.Invoke(new object(), new BarEventArgs(col));
                        }

                        // Add to the currently pressed buttons
                        pressedButtons.Add(midiEvent.ControlNumber);
                    }

                    // Button Up
                    if (midiEvent.ControlValue == 0)
                    {
                        // Add to the currently pressed buttons
                        pressedButtons.Remove(midiEvent.ControlNumber);
                    }
                }

                // Is on grid
                if (e.Event.EventType == MidiEventType.NoteOn)
                {
                    var midiEvent = (NoteOnEvent)e.Event;

                    // Button Down
                    if (midiEvent.Velocity == 127)
                    {
                        // Add to the currently pressed buttons
                        pressedButtons.Add(midiEvent.NoteNumber);

                        // Caluclate row/col
                        int row = 9 - (midiEvent.NoteNumber / 10);
                        int col = midiEvent.NoteNumber - ((midiEvent.NoteNumber / 10) * 10);

                        // Raid Grid Event
                        buttonPressed_Grid.Invoke(new object(), new GridEventArgs(row, col));
                    }

                    // Button Up
                    if (midiEvent.Velocity == 0)
                    {
                        pressedButtons.Remove(midiEvent.NoteNumber);
                    }
                }
            };

            inDev.StartEventsListening();

            outDev.TurnAllNotesOff();
        }

        protected virtual void Dispose(bool disposing)
        {
            (inDev as IDisposable)?.Dispose();
            outDev.Dispose();
        }

        #region Launchpad Interactions

        public bool isPressed_Grid(int row, int col)
        {
            // Calculate Index
            SevenBitNumber index = (SevenBitNumber)((9 - row) * 10 + col);

            return pressedButtons.Contains(index);
        }

        public bool isPressed_BarRight(int nr)
        {
            // Calculate Index
            SevenBitNumber index = (SevenBitNumber)((9 - nr) * 10 + 9);

            return pressedButtons.Contains(index);
        }

        public bool isPressed_BarTop(int nr)
        {
            // Calculate Index
            SevenBitNumber index = (SevenBitNumber)nr;

            return pressedButtons.Contains(index);
        }

        #endregion

        #region Color Update Functions

        public void setColor_Grid(int row, int col, LaunchpadButtonColor color, int channel = 0)
        {
            // Calculate Index
            SevenBitNumber index = (SevenBitNumber)((9 - row) * 10 + col);

            // Send Event
            setColor(index, color, channel);
        }

        public void setColor_BarTop(int nr, LaunchpadButtonColor color, int channel = 0)
        {
            // Calculate INdex
            SevenBitNumber index = (SevenBitNumber)nr;

            setColor(index, color, channel);
        }

        public void setColor_BarRight(int nr, LaunchpadButtonColor color, int channel = 0)
        {
            // Calculate INdex
            SevenBitNumber index = (SevenBitNumber)((9 - nr) * 10 + 9);

            setColor(index, color, channel);
        }

        void setColor(SevenBitNumber index, LaunchpadButtonColor color, int channel)
        {
            // Update Color Pairs
            if (colorPair.Keys.Contains(index))
                colorPair[index] = color;
            else
                colorPair.Add(index, color);

            // Create Event
            var midiEvent = new NoteOnEvent(index, enum2nr(color));
            midiEvent.Channel = (FourBitNumber)channel;

            // Send Data
            outDev.SendEvent(midiEvent);
        }

        #endregion

        #region Enumerator Converters

        public SevenBitNumber enum2nr(LaunchpadButtonColor color)
        {
            switch (color)
            {
                case LaunchpadButtonColor.LightGreen:
                    return ((SevenBitNumber)16);
                    break;

                case LaunchpadButtonColor.Lila:
                    return ((SevenBitNumber)57);
                    break;

                case LaunchpadButtonColor.Pink:
                    return ((SevenBitNumber)53);
                    break;

                case LaunchpadButtonColor.Red:
                    return ((SevenBitNumber)5);
                    break;

                case LaunchpadButtonColor.DarkBlue:
                    return ((SevenBitNumber)67);
                    break;

                case LaunchpadButtonColor.LightBlue:
                    return ((SevenBitNumber)32);
                    break;

                case LaunchpadButtonColor.Purple:
                    return ((SevenBitNumber)49);
                    break;

                case LaunchpadButtonColor.Oliv:
                    return ((SevenBitNumber)63);
                    break;

                case LaunchpadButtonColor.Green:
                    return ((SevenBitNumber)18);
                    break;

                case LaunchpadButtonColor.Yellow:
                    return ((SevenBitNumber)13);
                    break;

                case LaunchpadButtonColor.Orange:
                    return ((SevenBitNumber)9);
                    break;

                case LaunchpadButtonColor.White:
                    return ((SevenBitNumber)3);
                    break;

                case LaunchpadButtonColor.Gray:
                    return ((SevenBitNumber)118);
                    break;
            }

            return ((SevenBitNumber)0);
        }

        public System.Windows.Media.Color enum2color(LaunchpadButtonColor color)
        {
            switch (color)
            {
                case LaunchpadButtonColor.Red:
                    return System.Windows.Media.Color.FromRgb(255, 0, 0);
                    break;

                case LaunchpadButtonColor.Orange:
                    return System.Windows.Media.Color.FromRgb(255, 154, 0);
                    break;

                case LaunchpadButtonColor.Yellow:
                    return System.Windows.Media.Color.FromRgb(255, 222, 0);
                    break;

                case LaunchpadButtonColor.Pink:
                    return System.Windows.Media.Color.FromRgb(255, 90, 255);
                    break;

                case LaunchpadButtonColor.Lila:
                    return System.Windows.Media.Color.FromRgb(255, 0, 170);
                    break;

                case LaunchpadButtonColor.DarkBlue:
                    return System.Windows.Media.Color.FromRgb(0, 0, 255);
                    break;

                case LaunchpadButtonColor.LightBlue:
                    return System.Windows.Media.Color.FromRgb(30, 240, 255);
                    break;

                case LaunchpadButtonColor.Purple:
                    return System.Windows.Media.Color.FromRgb(150, 0, 255);
                    break;

                case LaunchpadButtonColor.Oliv:
                    return System.Windows.Media.Color.FromRgb(84, 110, 52);
                    break;

                case LaunchpadButtonColor.Green:
                    return System.Windows.Media.Color.FromRgb(0, 255, 0);
                    break;

                case LaunchpadButtonColor.LightGreen:
                    return System.Windows.Media.Color.FromRgb(210, 255, 160);
                    break;

                case LaunchpadButtonColor.White:
                    return System.Windows.Media.Color.FromRgb(230, 230, 230);
                    break;

                case LaunchpadButtonColor.Gray:
                    return System.Windows.Media.Color.FromRgb(100, 100, 100);
                    break;


                default:
                    return System.Windows.Media.Color.FromRgb(51, 51, 51);
                    break;
            }
        }


        public System.Windows.Media.Color enum2foreground(LaunchpadButtonColor color)
        {
            switch (color)
            {
                case LaunchpadButtonColor.Red:
                case LaunchpadButtonColor.Orange:
                case LaunchpadButtonColor.Pink:
                case LaunchpadButtonColor.Lila:
                case LaunchpadButtonColor.DarkBlue:
                case LaunchpadButtonColor.Purple:
                case LaunchpadButtonColor.Oliv:
                case LaunchpadButtonColor.Gray:
                    return System.Windows.Media.Colors.White;
                    break;

                case LaunchpadButtonColor.Yellow:
                case LaunchpadButtonColor.White:
                case LaunchpadButtonColor.LightBlue:
                case LaunchpadButtonColor.LightGreen:
                case LaunchpadButtonColor.Green:
                    return System.Windows.Media.Colors.Black;
                    break;


                default:
                    return System.Windows.Media.Color.FromRgb(51, 51, 51);
                    break;
            }
        }

        #endregion
    }



    public class BarEventArgs : EventArgs
    {
        public int nr { get; set; }

        public BarEventArgs(int _nr)
        {
            nr = _nr;
        }
    }

    public class GridEventArgs : EventArgs
    {
        public int row { get; set; }
        public int col { get; set; }

        public GridEventArgs(int _row, int _col)
        {
            row = _row;
            col = _col;
        }
    }
}
