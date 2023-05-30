using System.Runtime.InteropServices;
using System.Windows.Forms;
using GooseShared;

namespace Honk
{
    public class Main : IMod
    {
        [DllImport("user32.dll")]
        static extern short GetAsyncKeyState(Keys vKey);

        static Keys Key = Keys.F;
        static bool pressed = false;

        void IMod.Init()
        {
            InjectionPoints.PreTickEvent += Tick;
        }

        public void Tick(GooseEntity goose)
        {
            if (GetAsyncKeyState(Main.Key) != 0 && !pressed)
            {
                pressed = true;
                API.Goose.playHonckSound();
                return;
            }
            pressed = false;
        }
    }
}
