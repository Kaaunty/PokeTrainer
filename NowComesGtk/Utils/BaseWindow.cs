using Gtk;

namespace NowComesGtk.Utils
{
    public class BaseWindow : Window
    {
        public BaseWindow(string title, int width, int height) : base(WindowType.Toplevel)
        {
            try
            {
                Title = title;
                Resizable = false;

                SetPosition(WindowPosition.Center);
                SetDefaultSize(width, height);
                SetIconFromFile("Images/pokeball.png");
            }
            catch (System.Exception e)
            {
                GLib.ExceptionManager.RaiseUnhandledException(e, true);
            }
        }
    }
}