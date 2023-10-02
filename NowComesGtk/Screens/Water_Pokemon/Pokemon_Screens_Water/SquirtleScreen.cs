using Gtk;
using NowComesGtk.Reusable_components;

namespace NowComesGtk.Screens.Water_Pokemon.Pokemon_Screens_Water
{
    public class SquirtleScreen : BasePokemonWindow
    {
        public SquirtleScreen() : base("Squirtle", "0007", 800, 500)
        {
            Fixed fix = new Fixed();
            Image pokemonWaterBackground = new Image("Images/pokemon_water/pokemonWater_backgroung.png");
            fix.Put(pokemonWaterBackground, 0, 0);
            Image pokemonPic = new Image(); 
            fix.Put(pokemonPic, 75, 100);


            Button btnMoves = new ButtonGenerator("Images/pokemon_water/Sem nome (75 × 50 px).png", 75, 50);
            fix.Put(btnMoves, 584, 410);

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
