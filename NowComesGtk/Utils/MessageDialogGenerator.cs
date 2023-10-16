namespace NowComesGtk.Utils
{
    public class MessageDialogGenerator : Gtk.MessageDialog
    {
        public static void ShowMessageDialog(string message)
        {
            var dialog = new Gtk.MessageDialog(null, Gtk.DialogFlags.Modal, Gtk.MessageType.Info, Gtk.ButtonsType.Ok, message);
            dialog.Run();
            dialog.Destroy();
        }
    }
}
