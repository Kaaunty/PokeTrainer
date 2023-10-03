using Gtk;

namespace NowComesGtk.Screens
{
    public class BasePokemonWindow : BaseWindow
    {
        public BasePokemonWindow(string pokemonName, string pokemonDex, int width, int height) : base("", 800, 500)
        {
            Fixed fix = new Fixed();
            string title = $"PokéTrainer© // Pokémons tipo - Água // Pokémons - {pokemonName} [#{pokemonDex}]";
            SetIconFromFile("Images/poketrainer_icon.png");

            Title = title;
            Resizable = false;
            SetPosition(WindowPosition.Center);
            SetDefaultSize(width, height);
        }
    }
}
