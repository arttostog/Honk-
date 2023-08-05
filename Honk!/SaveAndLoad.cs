using Newtonsoft.Json;
using System.IO;
using System.Windows.Forms;

namespace Honk
{
    public class SaveAndLoad
    {
        public static string PathToSave;

        public static Keys Save(Keys Key)
        {
            using (StreamWriter StreamWriter = File.CreateText(PathToSave)) StreamWriter.WriteLine(JsonConvert.SerializeObject(Key));
            return Key;
        }

        public static Keys Load()
        {
            try
            {
                using (TextReader TextReader = new StreamReader(new FileStream(PathToSave, FileMode.Open)))
                {
                    return JsonConvert.DeserializeObject<Keys>(TextReader.ReadLine());
                }
            }
            catch
            {
                return Save(Keys.F);
            }
        }
    }
}
