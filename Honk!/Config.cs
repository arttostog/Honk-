using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace Honk
{
    public class Config
    {
        private static readonly StringEnumConverter _stringEnumConverter = new StringEnumConverter();
        private static readonly string _pathToSave = Path.Combine(
            Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
            "Key.json"
        );

        public static void SaveKey(Keys key)
        {
            using (StreamWriter writer = File.CreateText(_pathToSave))
                writer.WriteLine(JsonConvert.SerializeObject(key, _stringEnumConverter));
        }

        public static Keys LoadKey()
        {
            try
            {
                using (StreamReader reader = new StreamReader(new FileStream(_pathToSave, FileMode.Open)))
                    return JsonConvert.DeserializeObject<Keys>(reader.ReadLine(), _stringEnumConverter);
            }
            catch
            {
                return Keys.None;
            }
        }
    }
}
