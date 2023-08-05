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
        static extern short GetAsyncKeyState(Keys vKey);

        public static Keys Key;
        private static bool Pressed = false;

        void IMod.Init()
        {
            SetSaveFolder();
            Key = SaveAndLoad.Load();

            InjectionPoints.PreTickEvent += Tick;
        }

        private static void SetSaveFolder()
        {
            string assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            SaveAndLoad.PathToSave = Path.Combine(assemblyFolder, "Key.json");
        }

        private static void Tick(GooseEntity goose)
        {
            if (GetAsyncKeyState(Key) != 0 && !Pressed)
            {
                Pressed = true;
                API.Goose.playHonckSound();
                return;
            }
            Pressed = false;
        }
    }
}
