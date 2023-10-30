using NowComesGtk.Reusable_components;
using PokeApi.BackEnd.Service;
using NowComesGtk.Utils;
using PokeApiNet;
using GLib;
using Gtk;

namespace NowComesGtk.Screens
{
    public class PoketrainerMainScreen : BaseWindow
    {
#nullable disable

        private SeparateMethods _separeteMethods = new();
        private ApiRequest _apiRequest = new();
        private Fixed _fix = new();

        public PoketrainerMainScreen() : base("PokéTrainer©", 800, 500)
        {
            Image poketrainerBackground = new Image("Images/pokemon_homescreen/homescreen.png");
            _fix.Put(poketrainerBackground, 0, 0);

            #region Buttons Pokédex

            Button btnWaterType = new ButtonGenerator("", 50, 70);
            btnWaterType.Image = new Image("Images/buttons_type/WaterIcon.png");
            btnWaterType.Data["type"] = "water";
            btnWaterType.Clicked += BtnTypePokedexScreen;
            _fix.Put(btnWaterType, 72, 215);

            Button btnDarkType = new ButtonGenerator("", 50, 70);
            btnDarkType.Image = new Image("Images/buttons_type/DarkIcon.png");
            btnDarkType.Data["type"] = "dark";
            btnDarkType.Clicked += BtnTypePokedexScreen;
            _fix.Put(btnDarkType, 132, 215);

            Button btnDragonType = new ButtonGenerator("", 50, 70);
            btnDragonType.Image = new Image("Images/buttons_type/DragonIcon.png");
            btnDragonType.Data["type"] = "dragon";
            btnDragonType.Clicked += BtnTypePokedexScreen;
            _fix.Put(btnDragonType, 192, 215);

            Button btnElectricType = new ButtonGenerator("", 50, 70);
            btnElectricType.Image = new Image("Images/buttons_type/ElectricIcon.png");
            btnElectricType.Data["type"] = "electric";
            btnElectricType.Clicked += BtnTypePokedexScreen;
            _fix.Put(btnElectricType, 252, 215);

            Button btnFairyType = new ButtonGenerator("", 50, 70);
            btnFairyType.Image = new Image("Images/buttons_type/FairyIcon.png");
            btnFairyType.Data["type"] = "fairy";
            btnFairyType.Clicked += BtnTypePokedexScreen;
            _fix.Put(btnFairyType, 312, 215);

            Button btnFightingType = new ButtonGenerator("", 50, 70);
            btnFightingType.Image = new Image("Images/buttons_type/FightingIcon.png");
            btnFightingType.Data["type"] = "fighting";
            btnFightingType.Clicked += BtnTypePokedexScreen;
            _fix.Put(btnFightingType, 372, 215);

            Button btnFireType = new ButtonGenerator("", 50, 70);
            btnFireType.Image = new Image("Images/buttons_type/FireIcon.png");
            btnFireType.Data["type"] = "fire";
            btnFireType.Clicked += BtnTypePokedexScreen;
            _fix.Put(btnFireType, 432, 215);

            Button btnFlyingType = new ButtonGenerator("", 50, 70);
            btnFlyingType.Image = new Image("Images/buttons_type/FlyingIcon.png");
            btnFlyingType.Data["type"] = "flying";
            btnFlyingType.Clicked += BtnTypePokedexScreen;
            _fix.Put(btnFlyingType, 492, 215);

            Button btnGhostType = new ButtonGenerator("", 50, 70);
            btnGhostType.Image = new Image("Images/buttons_type/GhostIcon.png");
            btnGhostType.Data["type"] = "ghost";
            btnGhostType.Clicked += BtnTypePokedexScreen;
            _fix.Put(btnGhostType, 552, 215);

            Button btnGrassType = new ButtonGenerator("", 50, 70);
            btnGrassType.Image = new Image("Images/buttons_type/GrassIcon.png");
            btnGrassType.Data["type"] = "grass";
            btnGrassType.Clicked += BtnTypePokedexScreen;
            _fix.Put(btnGrassType, 72, 295);

            Button btnGroundType = new ButtonGenerator("", 50, 70);
            btnGroundType.Image = new Image("Images/buttons_type/GroundIcon.png");
            btnGroundType.Data["type"] = "ground";
            btnGroundType.Clicked += BtnTypePokedexScreen;
            _fix.Put(btnGroundType, 132, 295);

            Button btnIceType = new ButtonGenerator("", 50, 70);
            btnIceType.Image = new Image("Images/buttons_type/IceIcon.png");
            btnIceType.Data["type"] = "ice";
            btnIceType.Clicked += BtnTypePokedexScreen;
            _fix.Put(btnIceType, 192, 295);

            Button btnNormalType = new ButtonGenerator("", 50, 70);
            btnNormalType.Image = new Image("Images/buttons_type/NormalIcon.png");
            btnNormalType.Data["type"] = "normal";
            btnNormalType.Clicked += BtnTypePokedexScreen;
            _fix.Put(btnNormalType, 252, 295);

            Button btnPoisonType = new ButtonGenerator("", 50, 70);
            btnPoisonType.Image = new Image("Images/buttons_type/PoisonIcon.png");
            btnPoisonType.Data["type"] = "poison";
            btnPoisonType.Clicked += BtnTypePokedexScreen;
            _fix.Put(btnPoisonType, 312, 295);

            Button btnPsychicType = new ButtonGenerator("", 50, 70);
            btnPsychicType.Image = new Image("Images/buttons_type/PsychicIcon.png");
            btnPsychicType.Data["type"] = "psychic";
            btnPsychicType.Clicked += BtnTypePokedexScreen;
            _fix.Put(btnPsychicType, 372, 295);

            Button btnRockType = new ButtonGenerator("", 50, 70);
            btnRockType.Image = new Image("Images/buttons_type/RockIcon.png");
            btnRockType.Data["type"] = "rock";
            btnRockType.Clicked += BtnTypePokedexScreen;
            _fix.Put(btnRockType, 432, 295);

            Button btnSteelType = new ButtonGenerator("", 50, 70);
            btnSteelType.Image = new Image("Images/buttons_type/SteelIcon.png");
            btnSteelType.Data["type"] = "steel";
            btnSteelType.Clicked += BtnTypePokedexScreen;
            _fix.Put(btnSteelType, 492, 295);

            Button btnBugType = new ButtonGenerator("", 50, 70);
            btnBugType.Image = new Image("Images/buttons_type/BugIcon.png");
            btnBugType.Data["type"] = "bug";
            btnBugType.Clicked += BtnTypePokedexScreen;
            _fix.Put(btnBugType, 552, 295);

            #endregion Buttons Pokédex

            Button btnWelcome = new ButtonGenerator("", 70, 40);
            btnWelcome.Image = new Image("Images/buttons/btnWelcome.png");
            btnWelcome.TooltipMarkup = "Olá...";
            btnWelcome.Clicked += Dialog_Start;
            _fix.Put(btnWelcome, 620, 75);

            Button btnAllPokemons = new ButtonGenerator("", 50, 60);
            btnAllPokemons.Image = new Image("Images/buttons_type/AllPokémons.png");
            btnAllPokemons.TooltipMarkup = "Todos os Pokémons";
            btnAllPokemons.Clicked += BtnTypePokedexScreen;
            _fix.Put(btnAllPokemons, 67, 90);

            Button btnGitHub = new ButtonGenerator("", 40, 40);
            btnGitHub.Image = new Image("Images/buttons/btnGitHub.png");
            btnGitHub.TooltipMarkup = "Clique para abrir o gitHub do projeto.";
            btnGitHub.Clicked += GitHub_Open;
            _fix.Put(btnGitHub, 735, 435);

            Button BtnTest = new ButtonGenerator("Teste", 100, 50);
            BtnTest.Image = new Image("Images/buttons/btnTeste.png");
            BtnTest.Clicked += btnPokemonTest;
            _fix.Put(BtnTest, 640, 300);
            Button BtnTestLegendary = new ButtonGenerator("Teste", 100, 50);
            BtnTestLegendary.Image = new Image("Images/buttons/btnTeste.png");
            BtnTestLegendary.Clicked += btnPokemonTestLegendary;
            _fix.Put(BtnTestLegendary, 640, 360);
            Button BtnTestMythical = new ButtonGenerator("Teste", 100, 50);
            BtnTestMythical.Image = new Image("Images/buttons/btnTeste.png");
            BtnTestMythical.Clicked += btnPokemonTestMythical;
            _fix.Put(BtnTestMythical, 640, 420);
            DeleteEvent += delegate { Gtk.Application.Quit(); };

            Add(_fix);
            ShowAll();
        }

        private void GitHub_Open(object sender, EventArgs e)
        {
            _separeteMethods.GitHubOpen();
        }

        private void Dialog_Start(object sender, EventArgs e)
        {
            _separeteMethods.DialogWithXamuca();
        }

        private async void btnPokemonTest(object sender, EventArgs e)
        {
            //Pokemon pokemon = await _apiRequest.GetPokemonAsync("scizor");
            Pokemon poke = await _apiRequest.GetPokemon("rayquaza");

            Console.WriteLine(poke.Name);
            Console.WriteLine(poke.Types[0].Type.Name);

            //PokemonScreen pokemonScreen = new(pokemon);
            //pokemonScreen.Show();
        }

        private async void btnPokemonTestLegendary(object sender, EventArgs e)
        {
            Pokemon pokemon = await _apiRequest.GetPokemon("basculin-red-striped");
            PokemonScreen pokemonScreen = new(pokemon);
            pokemonScreen.Show();
        }

        private async void btnPokemonTestMythical(object sender, EventArgs e)
        {
            Pokemon pokemon = await _apiRequest.GetPokemon("arceus");
            PokemonScreen pokemonScreen = new(pokemon);
            pokemonScreen.Show();
        }

        private void BtnTypePokedexScreen(object sender, EventArgs e)
        {
            try
            {
                Button btn = (Button)sender;
                string type = btn.Data["type"].ToString();

                PokedexScreen pokemonsWater = new(type);
                pokemonsWater.ShowAll();
            }
            catch (Exception ex)
            {
                MessageDialogGenerator.ShowMessageDialog("Erro ao carregar a lista de Pokémon:" + ex);
                ExceptionManager.RaiseUnhandledException(ex, true);
            }
        }
    }
}