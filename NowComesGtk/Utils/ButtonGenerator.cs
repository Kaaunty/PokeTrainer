using Gtk;

namespace NowComesGtk.Reusable_components
{
    public class ButtonGenerator : Button
    {
        public ButtonGenerator(string imagePath, int width, int height) : base()
        {
            Image image = new(imagePath);
            SetSizeRequest(width, height);
            Relief = ReliefStyle.None;
            Image = image;
        }
    }
}