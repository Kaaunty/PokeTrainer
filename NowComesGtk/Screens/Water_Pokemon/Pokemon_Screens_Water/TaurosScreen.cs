using Gtk;

namespace NowComesGtk.Screens.Water_Pokemon.Pokemon_Screens_Water
{
    public class TaurosScreen : BasePokemonWindow
    {
        public TaurosScreen() : base("Tauros", "0128", 800, 500)
        {
            Fixed fix = new Fixed();
            Image pokemonWaterBackground = new Image("Images/pokemon_water/pokemonWater_backgroung.png");
            fix.Put(pokemonWaterBackground, 0, 0);
            Image pokemonPic = new Image();
            fix.Put(pokemonPic, 75, 100);

            DeleteEvent += delegate { Simulate_Return(); };

            Add(fix);
            ShowAll();
        }
        private void Simulate_Return()
        {
            Close();
            PokemonsWaterScreen pokemonsWater = new PokemonsWaterScreen();
            pokemonsWater.Show();
        }
    }
}
