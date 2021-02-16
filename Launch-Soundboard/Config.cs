using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Launch_Soundboard
{
    public class Config
    {
        public SoundSet[] sets = new SoundSet[7];
        public string inputDevice = "MIDIIN2 (LPMiniMK3 MIDI)";
        public string outputDevice = "MIDIOUT2 (LPMiniMK3 MIDI)";
        public int audioOutputDeviceID;
        public string audioOutputDeviceName;
        public static Config config;

        public Config()
        {
            // Create Empty array
            for (int x = 0; x < 7; x++)
            {
                sets[x] = new SoundSet();
            }
        }

        public static void save()
        {
            string filePath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\config.json";
            string fileContent = JsonConvert.SerializeObject(Config.config, Formatting.Indented);

            File.WriteAllText(filePath, fileContent);
        }

        public static void load()
        {
            string filePath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\config.json";
            if (File.Exists(filePath))
            {
                string fileContent = File.ReadAllText(filePath);

                Config.config = JsonConvert.DeserializeObject<Config>(fileContent);
            } else
            {
                Config.config = new Config();
            }
        }
    }

    public class SoundSet
    {
        public SoundButton[,] buttons = new SoundButton[8, 8];

        public SoundSet()
        {
            // Create Empty array
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    buttons[x, y] = new SoundButton() { col = x, row = y, color = LaunchpadButtonColor.Off, volume = 1.0 };
                }
            }
        }
    }

    public class SoundButton
    {
        public int row { get; set; }
        public int col { get; set; }
        public string sound { get; set; }
        public double volume { get; set; }
        public LaunchpadButtonColor color { get; set; }

        public SoundButton()
        {
            volume = 1;
        }

    }
}
