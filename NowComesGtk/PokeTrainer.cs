using Gtk;

namespace NowComesGtk
{
    public class PokeTrainer
    {
        public static void Main(string[] args)
        {
            Application.Init();
            new PokemonLoader();
            Application.Run();
        }
    }
}