using System.Windows.Forms;

namespace Honk
{
    public class HonkKey
    {
        public Keys Key { get; private set; }

        public HonkKey()
        {
            this.Key = Keys.F;
        }
    }
}
