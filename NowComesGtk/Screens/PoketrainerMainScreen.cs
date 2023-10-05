using NowComesGtk.Reusable_components;
using NowComesGtk.Utils;
using PokeApiNet;
using Gtk;

namespace NowComesGtk.Screens
{
    public class PoketrainerMainScreen : BaseWindow
    {
        Pokemon pokemon;

        public PoketrainerMainScreen() : base("PokéTrainer©", 800, 500)
        {
            Fixed fix = new Fixed();
            Image poketrainerBackground = new Image("Images/background_homescreen.png");
            fix.Put(poketrainerBackground, 0, 0);
            DeleteEvent += delegate { Application.Quit(); };

            #region Menubar Settings

            VBox vbMb = new(false, 0);
            MenuBar mb = new MenuBar();
            vbMb.PackStart(mb, false, false, 0);

            Menu pokemonsMenu = new Menu();
            MenuItem pokemonsMI = new MenuItem("Pokémons");

            MenuItem waterMenuItem = new MenuItemGenerator("Água", "Images/pokemon_water/WaterIcon.png", WaterMenuItem_Actived);
            pokemonsMenu.Append(waterMenuItem);

            #endregion Menubar Settings 

            //Botão teste do Kauã ------> Esse vai direto para a tela de pokémons tipo água!
            Button btnPokedex = new ButtonGenerator("", 150, 175);
            fix.Put(btnPokedex, 250, 50);
            btnPokedex.Clicked += btnPokedexTest;

            //Botão teste do Kauã ------> Esse vai direto para a tela do pokémon Squirtle!
            Button btnPokemonScreen = new ButtonGenerator("", 40, 40);
            fix.Put(btnPokemonScreen, 250, 250);
            btnPokemonScreen.Clicked += btnPokemonScreenTest;

            mb.Append(pokemonsMI);
            pokemonsMI.Submenu = pokemonsMenu;
            pokemonsMenu.Append(waterMenuItem);

            fix.Add(vbMb);
            Add(fix);
            ShowAll();
        }
        private void WaterMenuItem_Actived(object? sender, EventArgs e)
        {
            TypeMainScreen pokemonWater = new TypeMainScreen();
            pokemonWater.Show();
        }

        [Obsolete]
        private void btnPokedexTest(object? sender, EventArgs e)
        {
            PokedexScreen pokemonsWater = new();
            pokemonsWater.Show();
        }

        [Obsolete]
        private void btnPokemonScreenTest(object? sender, EventArgs e)
        {
            PokemonScreen pokemonScreen = new();
            pokemonScreen.Show();
        }

    }
}
