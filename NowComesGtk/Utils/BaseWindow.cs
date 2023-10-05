using Gtk;

namespace NowComesGtk.Utils
{
    public class BaseWindow : Window
    {
        public BaseWindow(string title, int width, int height) : base(WindowType.Toplevel)
        {
            Title = title;
            Resizable = false;
            SetPosition(WindowPosition.Center);
            SetDefaultSize(width, height);
            SetIconFromFile("Images/poketrainer_icon.png");
        }
    }
}
