using Gtk;

namespace NowComesGtk.Utils
{
    public class MessageDialogGenerator : MessageDialog
    {
        public static void ShowMessageDialog(string message)
        {
            var messageDialog = new MessageDialog(null, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, message);
            string icon = "Images/pokeball.png";
            messageDialog.WindowPosition = WindowPosition.Center;
            messageDialog.SetIconFromFile(icon);
            messageDialog.Run();
            messageDialog.Destroy();
        }
    }
}