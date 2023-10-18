using Gtk;

namespace NowComesGtk
{
    public class PokeTrainer
    {
        public static void Main(string[] args)
        {
            string caminhoOriginal = System.IO.Directory.GetCurrentDirectory().Replace("bin\\Debug\\net7.0", "");
            Environment.CurrentDirectory = caminhoOriginal;

            Application.Init();
            new PokemonLoad();
            Application.Run();
        }
    }
}