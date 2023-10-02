using NowComesGtk.Reusable_components;
using Gtk;

namespace NowComesGtk.Screens.Water_Pokemon
{
    public class PokemonWaterMainScreen : BaseWindow
    {
        public PokemonWaterMainScreen() : base("PokéTrainer© // Pokémons tipo - Água", 800, 500)
        {
            Fixed fix = new Fixed();
            Image pokemonWaterBackground = new Image("Images/pokemon_water/background_pokemonWater_homescreen.png");
            fix.Put(pokemonWaterBackground, 0, 0);

            // Pokémons button:
            Button btnPokemonsWater = new ButtonGenerator("Images/pokemon_water/btn_pokemon_water.png", 150, 175);
            fix.Put(btnPokemonsWater, 250, 50);
            btnPokemonsWater.Clicked += Pokemons_Water_Click;

            Add(fix);
            ShowAll();
        }
        private void Pokemons_Water_Click(object? sender, EventArgs e)
        {
            PokemonsWaterScreen pokemonsWater = new PokemonsWaterScreen();
            pokemonsWater.Show();
        }
    }
}
