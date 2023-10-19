using NowComesGtk.Reusable_components;
using PokeApi.BackEnd.Service;
using NowComesGtk.Utils;
using PokeApiNet;
using Gtk;

namespace NowComesGtk.Screens
{
    public class PoketrainerMainScreen : BaseWindow
    {
#nullable disable

        private ApiRequest _apiRequest = new();
        private Fixed fix = new Fixed();

        public PoketrainerMainScreen() : base("PokéTrainer©", 800, 500)
        {
            Image poketrainerBackground = new Image("Images/pokemon_homescreen/homescreen.png");
            fix.Put(poketrainerBackground, 0, 0);
            var fontDescription = Pango.FontDescription.FromString("RetroPix Regular 15");

            #region Buttons Pokédex

            Button btnWaterType = new ButtonGenerator("", 50, 70);
            btnWaterType.Image = new Image("Images/buttons_type/WaterIcon.png");
            btnWaterType.Data["type"] = "water";
            btnWaterType.Clicked += BtnTypePokedexScreen;
            fix.Put(btnWaterType, 40, 75);

            Button btnDarkType = new ButtonGenerator("", 50, 70);
            btnDarkType.Image = new Image("Images/buttons_type/DarkIcon.png");
            btnDarkType.Data["type"] = "dark";
            btnDarkType.Clicked += BtnTypePokedexScreen;
            fix.Put(btnDarkType, 100, 75);

            Button btnDragonType = new ButtonGenerator("", 50, 70);
            btnDragonType.Image = new Image("Images/buttons_type/DragonIcon.png");
            btnDragonType.Data["type"] = "dragon";
            btnDragonType.Clicked += BtnTypePokedexScreen;
            fix.Put(btnDragonType, 160, 75);

            Button btnElectricType = new ButtonGenerator("", 50, 70);
            btnElectricType.Image = new Image("Images/buttons_type/ElectricIcon.png");
            btnElectricType.Data["type"] = "electric";
            btnElectricType.Clicked += BtnTypePokedexScreen;
            fix.Put(btnElectricType, 220, 75);

            Button btnFairyType = new ButtonGenerator("", 50, 70);
            btnFairyType.Image = new Image("Images/buttons_type/FairyIcon.png");
            btnFairyType.Data["type"] = "fairy";
            btnFairyType.Clicked += BtnTypePokedexScreen;
            fix.Put(btnFairyType, 280, 75);

            Button btnFightingType = new ButtonGenerator("", 50, 70);
            btnFightingType.Image = new Image("Images/buttons_type/FightingIcon.png");
            btnFightingType.Data["type"] = "fighting";
            btnFightingType.Clicked += BtnTypePokedexScreen;
            fix.Put(btnFightingType, 340, 75);

            Button btnFireType = new ButtonGenerator("", 50, 70);
            btnFireType.Image = new Image("Images/buttons_type/FireIcon.png");
            btnFireType.Data["type"] = "fire";
            btnFireType.Clicked += BtnTypePokedexScreen;
            fix.Put(btnFireType, 400, 75);

            Button btnFlyingType = new ButtonGenerator("", 50, 70);
            btnFlyingType.Image = new Image("Images/buttons_type/FlyingIcon.png");
            btnFlyingType.Data["type"] = "flying";
            btnFlyingType.Clicked += BtnTypePokedexScreen;
            fix.Put(btnFlyingType, 460, 75);

            Button btnGhostType = new ButtonGenerator("", 50, 70);
            btnGhostType.Image = new Image("Images/buttons_type/GhostIcon.png");
            btnGhostType.Data["type"] = "ghost";
            btnGhostType.Clicked += BtnTypePokedexScreen;
            fix.Put(btnGhostType, 520, 75);

            Button btnGrassType = new ButtonGenerator("", 50, 70);
            btnGrassType.Image = new Image("Images/buttons_type/GrassIcon.png");
            btnGrassType.Data["type"] = "grass";
            btnGrassType.Clicked += BtnTypePokedexScreen;
            fix.Put(btnGrassType, 40, 170);

            Button btnGroundType = new ButtonGenerator("", 50, 70);
            btnGroundType.Image = new Image("Images/buttons_type/GroundIcon.png");
            btnGroundType.Data["type"] = "ground";
            btnGroundType.Clicked += BtnTypePokedexScreen;
            fix.Put(btnGroundType, 100, 170);

            Button btnIceType = new ButtonGenerator("", 50, 70);
            btnIceType.Image = new Image("Images/buttons_type/IceIcon.png");
            btnIceType.Data["type"] = "ice";
            btnIceType.Clicked += BtnTypePokedexScreen;
            fix.Put(btnIceType, 160, 170);

            Button btnNormalType = new ButtonGenerator("", 50, 70);
            btnNormalType.Image = new Image("Images/buttons_type/NormalIcon.png");
            btnNormalType.Data["type"] = "normal";
            btnNormalType.Clicked += BtnTypePokedexScreen;
            fix.Put(btnNormalType, 220, 170);

            Button btnPoisonType = new ButtonGenerator("", 50, 70);
            btnPoisonType.Image = new Image("Images/buttons_type/PoisonIcon.png");
            btnPoisonType.Data["type"] = "poison";
            btnPoisonType.Clicked += BtnTypePokedexScreen;
            fix.Put(btnPoisonType, 280, 170);

            Button btnPsychicType = new ButtonGenerator("", 50, 70);
            btnPsychicType.Image = new Image("Images/buttons_type/PsychicIcon.png");
            btnPsychicType.Data["type"] = "psychic";
            btnPsychicType.Clicked += BtnTypePokedexScreen;
            fix.Put(btnPsychicType, 340, 170);

            Button btnRockType = new ButtonGenerator("", 50, 70);
            btnRockType.Image = new Image("Images/buttons_type/RockIcon.png");
            btnRockType.Data["type"] = "rock";
            btnRockType.Clicked += BtnTypePokedexScreen;
            fix.Put(btnRockType, 400, 170);

            Button btnSteelType = new ButtonGenerator("", 50, 70);
            btnSteelType.Image = new Image("Images/buttons_type/SteelIcon.png");
            btnSteelType.Data["type"] = "steel";
            btnSteelType.Clicked += BtnTypePokedexScreen;
            fix.Put(btnSteelType, 460, 170);

            Button btnBugType = new ButtonGenerator("", 50, 70);
            btnBugType.Image = new Image("Images/buttons_type/BugIcon.png");
            btnBugType.Data["type"] = "bug";
            btnBugType.Clicked += BtnTypePokedexScreen;
            fix.Put(btnBugType, 520, 170);

            #endregion Buttons Pokédex

            Button btnWelcome = new ButtonGenerator("", 70, 40);
            btnWelcome.Image = new Image("Images/buttons/btnWelcome.png");
            //btnDialog.Clicked += btnPokemonTest;
            fix.Put(btnWelcome, 620, 75);

            Button btnGitHub = new ButtonGenerator("", 40, 40);
            btnGitHub.Image = new Image("Images/buttons/btnGitHub.png");
            //btnDialog.Clicked += btnPokemonTest;
            fix.Put(btnGitHub, 735, 435);

            Button BtnTest = new ButtonGenerator("Teste", 100, 50);
            BtnTest.Image = new Image("Images/buttons/btnTeste.png");
            BtnTest.Clicked += btnPokemonTest;
            fix.Put(BtnTest, 640, 300);

            Button BtnTestLegendary = new ButtonGenerator("Teste", 100, 50);
            BtnTestLegendary.Image = new Image("Images/buttons/btnTeste.png");
            BtnTestLegendary.Clicked += btnPokemonTestLegendary;
            fix.Put(BtnTestLegendary, 640, 340);
            Button BtnTestMythical = new ButtonGenerator("Teste", 100, 50);
            BtnTestMythical.Image = new Image("Images/buttons/btnTeste.png");
            BtnTestMythical.Clicked += btnPokemonTestMythical;
            fix.Put(BtnTestMythical, 640, 380);

            DeleteEvent += delegate { Application.Quit(); };
            Add(fix);
            ShowAll();
        }

        private async void btnPokemonTest(object sender, EventArgs e)
        {
            Pokemon pokemon = await _apiRequest.GetPokemonAsync("charizard");

            PokemonScreen pokemonScreen = new(pokemon);
            pokemonScreen.Show();
        }

        private async void btnPokemonTestLegendary(object sender, EventArgs e)
        {
            Pokemon pokemon = await _apiRequest.GetPokemonAsync("rayquaza");

            PokemonScreen pokemonScreen = new(pokemon);
            pokemonScreen.Show();
        }

        private async void btnPokemonTestMythical(object sender, EventArgs e)
        {
            Pokemon pokemon = await _apiRequest.GetPokemonAsync("hoopa");

            PokemonScreen pokemonScreen = new(pokemon);
            pokemonScreen.Show();
        }

        private void BtnTypePokedexScreen(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string type = btn.Data["type"].ToString();

            PokedexScreen pokemonsWater = new(type);
            pokemonsWater.ShowAll();
        }
    }
}