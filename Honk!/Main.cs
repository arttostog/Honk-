using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using GooseShared;

namespace Honk
{
    public class Main : IMod
    {
        [DllImport("user32.dll")]
        private static extern short GetAsyncKeyState(Keys vKey);

        private HonkKey Key;
        
        private bool Pressed = false;

        void IMod.Init()
        {
            SaveAndLoad<HonkKey> saveAndLoad = new SaveAndLoad<HonkKey>(GetSaveFolder());
            Key = saveAndLoad.Load(true);

            InjectionPoints.PreTickEvent += Tick;
        }

        private string GetSaveFolder()
        {
            string assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            return Path.Combine(assemblyFolder, "Key.json");
        }

        private void Tick(GooseEntity goose)
        {
            if (GetAsyncKeyState(Key.Key) != 0)
            {
                if (!Pressed)
                {
                    API.Goose.playHonckSound();
                }
                Pressed = true;
                return;
            }
            Pressed = false;
        }
    }
}
