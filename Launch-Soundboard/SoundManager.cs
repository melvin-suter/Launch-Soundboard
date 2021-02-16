using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;

namespace Launch_Soundboard
{
    class SoundManager
    {
        public static List<WaveOut> activeWaveOuts = new List<WaveOut>();
        public delegate void SoundEventHandler(object sender, EventArgs e);
        public static event SoundEventHandler playingSound;
        public static event SoundEventHandler stoppedSound;

        public static void playSong(string filePath, float volume)
        {
            string filepath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\sounds\\" + filePath;

            if (System.IO.File.Exists(filepath))
            {
                IWaveProvider waveReader;

                if (filepath.Split('.').Last() == "mp3")
                    waveReader = new NAudio.Wave.Mp3FileReader(filepath);
                else
                    waveReader = new NAudio.Wave.WaveFileReader(filepath);

                var waveOut = new NAudio.Wave.WaveOut();
                waveOut.DeviceNumber = Config.config.audioOutputDeviceID;
                waveOut.Volume = volume;
                waveOut.Init(waveReader);
                waveOut.Play();

                activeWaveOuts.Add(waveOut);
                raiseEvents();

                waveOut.PlaybackStopped += (a, b) =>
                {
                    activeWaveOuts.Remove(waveOut);
                    raiseEvents();
                };
            }
        }

        public static void stopAll()
        {
            foreach (WaveOut wave in activeWaveOuts.ToArray())
            {
                wave.Stop();
            }
        }


        public static void raiseEvents()
        {
            if (activeWaveOuts.Count() > 0)
                playingSound.Invoke(new object(), new EventArgs());
            else
                stoppedSound.Invoke(new object(), new EventArgs());
        }
    }



    
}
