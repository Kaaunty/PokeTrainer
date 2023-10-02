using Gtk;

namespace NowComesGtk.Reusable_components
{
    public class MenuItemGenerator : MenuItem
    {
        public MenuItemGenerator(string label, string imagePath, EventHandler activatedHandler)
        {
            EventBox eventBox = new EventBox();
            HBox hbox = new HBox(false, 0);
            Label labelWidget = new Label();
            Image image = new Image();

            labelWidget.Markup = $"<span font_desc='Sans 9'>{label}</span>";
            image.Pixbuf = new Gdk.Pixbuf(imagePath);
            hbox.PackStart(image, false, false, 2);
            hbox.PackStart(labelWidget, false, false, 2);
            eventBox.Add(hbox);
            Child = eventBox;

            if (activatedHandler != null)
            {
                Activated += activatedHandler;
            }
        }
    }
}
