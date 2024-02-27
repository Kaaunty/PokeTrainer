using Gtk;
using System.Drawing.Text;

namespace NowComesGtk
{
    public class PokeTrainer
    {
        public static void Main(string[] args)
        {
            Environment.CurrentDirectory = System.IO.Directory.GetCurrentDirectory().Replace("bin\\Debug\\net7.0", "");
            InstallFont("Fonts/Pixeloid.ttf");
            Application.Init();
            new PokemonLoad();
            Application.Run();
        }

        private static void InstallFont(string fontFile)
        {
            PrivateFontCollection fontCollection = new PrivateFontCollection();
            fontCollection.AddFontFile(fontFile);
        }
    }
}