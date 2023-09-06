using Newtonsoft.Json;
using System;
using System.IO;

namespace Honk
{
    public class SaveAndLoad<T> where T : new()
    {
        private string PathToSave;

        public SaveAndLoad(String PathToSave)
        {
            this.PathToSave = PathToSave;
        }

        public T Save(T Object)
        {
            using (StreamWriter StreamWriter = File.CreateText(PathToSave)) StreamWriter.WriteLine(JsonConvert.SerializeObject(Object));
            return Object;
        }

        public T Load(bool NewSaveIfError)
        {
            try
            {
                using (TextReader TextReader = new StreamReader(new FileStream(PathToSave, FileMode.Open)))
                {
                    return JsonConvert.DeserializeObject<T>(TextReader.ReadLine());
                }
            }
            catch
            {
                if (NewSaveIfError)
                {
                    return Save(new T());
                }
                return default(T);
            }
        }
    }
}
