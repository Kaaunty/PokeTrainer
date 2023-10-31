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

        private SeparatedMethods _separetedMethods = new();
        private ApiRequest _apiRequest = new();
        private Fixed _fix = new();

        public PoketrainerMainScreen() : base("PokéTrainer©", 800, 500)
        {
            DeleteEvent += delegate { Gtk.Application.Quit(); };
            Image poketrainerBackground = new Image("Images/pokemon_homescreen/homescreen.png");
            _fix.Put(poketrainerBackground, 0, 0);

            #region Buttons Pokédex

            Button btnWaterType = new ButtonGenerator("", 50, 70);
            btnWaterType.Image = new Image("Images/buttons_type/WaterIcon.png");
            btnWaterType.Data["_type"] = "water";
            btnWaterType.Clicked += BtnTypePokedexScreen;
            _fix.Put(btnWaterType, 72, 215);

            Button btnDarkType = new ButtonGenerator("", 50, 70);
            btnDarkType.Image = new Image("Images/buttons_type/DarkIcon.png");
            btnDarkType.Data["_type"] = "dark";
            btnDarkType.Clicked += BtnTypePokedexScreen;
            _fix.Put(btnDarkType, 132, 215);

            Button btnDragonType = new ButtonGenerator("", 50, 70);
            btnDragonType.Image = new Image("Images/buttons_type/DragonIcon.png");
            btnDragonType.Data["_type"] = "dragon";
            btnDragonType.Clicked += BtnTypePokedexScreen;
            _fix.Put(btnDragonType, 192, 215);

            Button btnElectricType = new ButtonGenerator("", 50, 70);
            btnElectricType.Image = new Image("Images/buttons_type/ElectricIcon.png");
            btnElectricType.Data["_type"] = "electric";
            btnElectricType.Clicked += BtnTypePokedexScreen;
            _fix.Put(btnElectricType, 252, 215);

            Button btnFairyType = new ButtonGenerator("", 50, 70);
            btnFairyType.Image = new Image("Images/buttons_type/FairyIcon.png");
            btnFairyType.Data["_type"] = "fairy";
            btnFairyType.Clicked += BtnTypePokedexScreen;
            _fix.Put(btnFairyType, 312, 215);

            Button btnFightingType = new ButtonGenerator("", 50, 70);
            btnFightingType.Image = new Image("Images/buttons_type/FightingIcon.png");
            btnFightingType.Data["_type"] = "fighting";
            btnFightingType.Clicked += BtnTypePokedexScreen;
            _fix.Put(btnFightingType, 372, 215);

            Button btnFireType = new ButtonGenerator("", 50, 70);
            btnFireType.Image = new Image("Images/buttons_type/FireIcon.png");
            btnFireType.Data["_type"] = "fire";
            btnFireType.Clicked += BtnTypePokedexScreen;
            _fix.Put(btnFireType, 432, 215);

            Button btnFlyingType = new ButtonGenerator("", 50, 70);
            btnFlyingType.Image = new Image("Images/buttons_type/FlyingIcon.png");
            btnFlyingType.Data["_type"] = "flying";
            btnFlyingType.Clicked += BtnTypePokedexScreen;
            _fix.Put(btnFlyingType, 492, 215);

            Button btnGhostType = new ButtonGenerator("", 50, 70);
            btnGhostType.Image = new Image("Images/buttons_type/GhostIcon.png");
            btnGhostType.Data["_type"] = "ghost";
            btnGhostType.Clicked += BtnTypePokedexScreen;
            _fix.Put(btnGhostType, 552, 215);

            Button btnGrassType = new ButtonGenerator("", 50, 70);
            btnGrassType.Image = new Image("Images/buttons_type/GrassIcon.png");
            btnGrassType.Data["_type"] = "grass";
            btnGrassType.Clicked += BtnTypePokedexScreen;
            _fix.Put(btnGrassType, 72, 295);

            Button btnGroundType = new ButtonGenerator("", 50, 70);
            btnGroundType.Image = new Image("Images/buttons_type/GroundIcon.png");
            btnGroundType.Data["_type"] = "ground";
            btnGroundType.Clicked += BtnTypePokedexScreen;
            _fix.Put(btnGroundType, 132, 295);

            Button btnIceType = new ButtonGenerator("", 50, 70);
            btnIceType.Image = new Image("Images/buttons_type/IceIcon.png");
            btnIceType.Data["_type"] = "ice";
            btnIceType.Clicked += BtnTypePokedexScreen;
            _fix.Put(btnIceType, 192, 295);

            Button btnNormalType = new ButtonGenerator("", 50, 70);
            btnNormalType.Image = new Image("Images/buttons_type/NormalIcon.png");
            btnNormalType.Data["_type"] = "normal";
            btnNormalType.Clicked += BtnTypePokedexScreen;
            _fix.Put(btnNormalType, 252, 295);

            Button btnPoisonType = new ButtonGenerator("", 50, 70);
            btnPoisonType.Image = new Image("Images/buttons_type/PoisonIcon.png");
            btnPoisonType.Data["_type"] = "poison";
            btnPoisonType.Clicked += BtnTypePokedexScreen;
            _fix.Put(btnPoisonType, 312, 295);

            Button btnPsychicType = new ButtonGenerator("", 50, 70);
            btnPsychicType.Image = new Image("Images/buttons_type/PsychicIcon.png");
            btnPsychicType.Data["_type"] = "psychic";
            btnPsychicType.Clicked += BtnTypePokedexScreen;
            _fix.Put(btnPsychicType, 372, 295);

            Button btnRockType = new ButtonGenerator("", 50, 70);
            btnRockType.Image = new Image("Images/buttons_type/RockIcon.png");
            btnRockType.Data["_type"] = "rock";
            btnRockType.Clicked += BtnTypePokedexScreen;
            _fix.Put(btnRockType, 432, 295);

            Button btnSteelType = new ButtonGenerator("", 50, 70);
            btnSteelType.Image = new Image("Images/buttons_type/SteelIcon.png");
            btnSteelType.Data["_type"] = "steel";
            btnSteelType.Clicked += BtnTypePokedexScreen;
            _fix.Put(btnSteelType, 492, 295);

            Button btnBugType = new ButtonGenerator("", 50, 70);
            btnBugType.Image = new Image("Images/buttons_type/BugIcon.png");
            btnBugType.Data["_type"] = "bug";
            btnBugType.Clicked += BtnTypePokedexScreen;
            _fix.Put(btnBugType, 552, 295);

            #endregion Buttons Pokédex

            Button btnWelcome = new ButtonGenerator("", 70, 40);
            btnWelcome.Image = new Image("Images/buttons/btnWelcome.png");
            btnWelcome.TooltipMarkup = "Olá...";
            btnWelcome.Clicked += Dialog_Start;
            _fix.Put(btnWelcome, 620, 75);

            Image openBag = new Image("Images/buttons_type/AllPokémonsOpen.png");
            Image closedBag = new Image("Images/buttons_type/AllPokémons.png");

            Button btnAllPokemons = new ButtonGenerator("", 50, 60);
            btnAllPokemons.Image = new Image("Images/buttons_type/AllPokémons.png");
            btnAllPokemons.FocusInEvent += delegate { btnAllPokemons.Image = closedBag; };
            btnAllPokemons.FocusOutEvent += delegate { btnAllPokemons.Image = openBag; };
            btnAllPokemons.TooltipMarkup = "Todos os Pokémons";
            btnAllPokemons.Data["_type"] = "all";
            btnAllPokemons.Clicked += BtnTypePokedexScreen;
            _fix.Put(btnAllPokemons, 67, 90);

            Button btnGitHub = new ButtonGenerator("", 40, 40);

            btnGitHub.Image = new Image("Images/buttons/btnGitHub.png");
            btnGitHub.TooltipMarkup = "Clique para abrir o gitHub do projeto.";
            btnGitHub.Clicked += GitHub_Open;
            _fix.Put(btnGitHub, 735, 435);

            DeleteEvent += delegate { Gtk.Application.Quit(); };

            Add(_fix);
            ShowAll();
        }

        private void GitHub_Open(object sender, EventArgs e)
        {
            _separetedMethods.GitHubOpen();
        }

        private void Dialog_Start(object sender, EventArgs e)
        {
            _separetedMethods.DialogWithXamuca();
        }

        private void BtnTypePokedexScreen(object sender, EventArgs e)
        {
            try
            {
                Button btn = (Button)sender;
                string type = btn.Data["_type"].ToString();

                PokedexScreen pokedexScreen = new(type);
                pokedexScreen.ShowAll();
            }
            catch (Exception ex)
            {
                MessageDialogGenerator.ShowMessageDialog("Erro ao carregar a lista de Pokémon:" + ex);
                ExceptionManager.RaiseUnhandledException(ex, true);
            }
        }
    }
}