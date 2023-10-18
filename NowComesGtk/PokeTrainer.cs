using Gtk;

namespace NowComesGtk
{
    public class PokeTrainer
    {
        public static void Main(string[] args)
        {
            string caminhoOriginal = System.IO.Directory.GetCurrentDirectory();
            string caminho = caminhoOriginal.Replace("bin\\Debug\\net7.0", "");
            Console.WriteLine(caminho);
            Environment.CurrentDirectory = caminho;

            Application.Init();
            new PokemonLoad();
            Application.Run();
        }
    }
}