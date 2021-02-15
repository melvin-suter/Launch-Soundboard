using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Launch_Soundboard
{
    class GridSetConfig
    {
        List<GridSet> sets = new List<GridSet>();

        public static GridSetConfig create(){
            GridSetConfig config = new GridSetConfig();
            string path = Environment.ExpandEnvironmentVariables("%appdata%\\launch-soundboard");

            foreach (string file in GridSet.getSets())
            {
                config.sets.Add(GridSet.load(file));
            }

            return config;
        }

    }

    class GridSet
    {
        GridButtonConfig[,] buttons { get; set; }


        public static List<string> getSets()
        {
            string path = Environment.ExpandEnvironmentVariables("%appdata%\\launch-soundboard");
            List<string> res = new List<string>();

            foreach (string file in Directory.EnumerateFiles(path))
            {
                res.Add(Path.GetFileNameWithoutExtension(file));
            }

            return res;
        }

        public void save()
        {
            string path = Environment.ExpandEnvironmentVariables("%appdata%\\launch-soundboard");

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            XmlSerializer serializer = new XmlSerializer(typeof(GridSet));
            using (var writer = new StreamWriter(path))
            {
                serializer.Serialize(writer, this);
            }
        }

        public static GridSet load(string fileName)
        {
            string path = Environment.ExpandEnvironmentVariables("%appdata%\\launch-soundboard");
            GridSet gridSetConfig = new GridSet();

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            XmlSerializer serializer = new XmlSerializer(typeof(GridSet));
            using (var reader = new StreamReader(path + "\\" + fileName + ".xml"))
            {
                gridSetConfig = (GridSet)serializer.Deserialize(reader);
            }

            return gridSetConfig;
        }
    }

    class GridButtonConfig
    {
        public string sound { get; set; }
        public double volume { get; set; }
        public LaunchpadButtonColor color { get; set; }
    }
}
