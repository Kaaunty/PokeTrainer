using Gdk;
using Gtk;

namespace NowComesGtk.Reusable_components
{
    public class ButtonGenerator : Button
    {
        public ButtonGenerator(string imagePath, int width, int height) : base()
        {
            Image imgPokeball = new Image(imagePath);
            SetSizeRequest(width, height);
            Relief = ReliefStyle.None;
            Image = imgPokeball;
            //// Use ModifyBg para definir a cor de fundo da seleção como transparente (alfa 0)
            //ModifyBg(StateType.Selected, new Color(0, 0, 0, 0));
        }


    }
}