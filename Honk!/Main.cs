using System.Runtime.InteropServices;
using System.Windows.Forms;
using GooseShared;

namespace Honk
{
    public class Main : IMod
    {
        private Keys _honkKey;
        private bool _honkKeyIsPressed = false;

        [DllImport("user32.dll")]
        private static extern short GetAsyncKeyState(Keys key);

        void IMod.Init()
        {
            InitHonkKey();
            InjectionPoints.PreTickEvent += Tick;
        }

        private void InitHonkKey()
        {
            if ((_honkKey = Config.LoadKey()) == Keys.None)
                Config.SaveKey(_honkKey = Keys.F);
        }

        private void Tick(GooseEntity goose)
        {
            if (GetAsyncKeyState(_honkKey) != 0)
            {
                if (!_honkKeyIsPressed)
                {
                    API.Goose.playHonckSound();
                    _honkKeyIsPressed = true;
                }
                return;
            }
            _honkKeyIsPressed = false;
        }
    }
}
