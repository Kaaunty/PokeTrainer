using NowComesGtk.Screens.Water_Pokemon.Pokemon_Screens_Water;
using NowComesGtk.Screens.Water_Pokemon;
using NowComesGtk.Reusable_components;
using Gtk;

namespace NowComesGtk.Screens
{
    public class PoketrainerMainScreen : BaseWindow
    {
        public PoketrainerMainScreen() : base("PokéTrainer©", 800, 500)
        {
            Fixed fix = new Fixed();
            Image poketrainerBackground = new Image("Images/background_homescreen.png");
            fix.Put(poketrainerBackground, 0, 0);
            DeleteEvent += delegate { Application.Quit(); };

            #region Menubar Settings

            VBox vbMb = new VBox(false, 0);
            MenuBar mb = new MenuBar();
            vbMb.PackStart(mb, false, false, 0);

            Menu pokemonsMenu = new Menu();
            MenuItem pokemonsMI = new MenuItem("Pokémons");

            MenuItem waterMenuItem = new MenuItemGenerator("Água", "Images/pokemon_water/WaterIcon.png", WaterMenuItem_Actived);
            pokemonsMenu.Append(waterMenuItem);

            #endregion Menubar Settings 

            //Botão teste do Kauã ------> Esse vai direto para a tela de pokémons tipo água!
            Button btnPokemonsWater = new ButtonGenerator("Images/pokemon_water/btn_pokemon_water.png", 150, 175);
            fix.Put(btnPokemonsWater, 250, 50);
            btnPokemonsWater.Clicked += Pokemons_Water_Click;

            //Botão teste do Kauã ------> Esse vai direto para a tela do pokémon Squirtle!
            Button btnSquirtle = new ButtonGenerator("Images/pokemon_water/pure_pokemon/0007_squirtle.png", 40, 40);
            fix.Put(btnSquirtle, 250, 250);
            btnSquirtle.Clicked += Squirtle_Click;

            mb.Append(pokemonsMI);
            pokemonsMI.Submenu = pokemonsMenu;
            pokemonsMenu.Append(waterMenuItem);

            fix.Add(vbMb);
            Add(fix);
            ShowAll();
        }
        private void WaterMenuItem_Actived (object? sender, EventArgs e)
        {
            PokemonWaterMainScreen pokemonWater = new PokemonWaterMainScreen();
            pokemonWater.Show();
        }
        private void Pokemons_Water_Click(object? sender, EventArgs e)
        {
            PokemonsWaterScreen pokemonsWater = new PokemonsWaterScreen();
            pokemonsWater.Show();
        }
        private void Squirtle_Click(object? sender, EventArgs e)
        {
            SquirtleScreen squirtleCreen = new SquirtleScreen();
            squirtleCreen.Show();
        }
    }
}
