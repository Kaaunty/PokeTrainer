using NowComesGtk.Reusable_components;
using NowComesGtk.Utils;
using PokeApiNet;
using Gtk;

namespace NowComesGtk.Screens
{
    public class PokemonScreen : BaseWindow
    {
        public string pokemonDex = "#0123";
        public string pokemonName = "Pokemon";
        public string pokemonType = "Leandrinha";

        public string pokemonAbilityOne = "Super-Gostosa";
        public string pokemonAbilityTwo = "Tomar-No-U-Viu";
        public string pokemonAbilityThree = "Sedutore";
        public string pokemonAbilityFour = "Uper-Lifre";

        public string pokemonMale = "25%";
        public string pokemonFemale = "75%";
        public string pokemonCatchRate = "100%";

        public string pokemonHP = "0";
        public string pokemonATK = "1";
        public string pokemonDEF = "0";
        public string pokemonSpATK = "1";
        public string pokemonSpDEF = "0";
        public string pokemonSpeed = "0";

        public PokemonScreen() : base("", 800, 500)
        {
            Fixed fix = new Fixed();

            string title = $"PokéTrainer© // Pokémons tipo - Água // Pokémons - Pokemon [#0000]";
            SetIconFromFile("Images/poketrainer_icon.png");

            Title = title;
            Resizable = false;
            SetPosition(WindowPosition.Center);

            Image Background = new Image("Images/pokemon_water/pokemonWater_backgroung.png");
            fix.Put(Background, 0, 0);
            Image pokemonPic = new Image("Images/pokemon_water/Sem nome (175 × 200 px).png");
            fix.Put(pokemonPic, 75, 100);
            
            // Dex number, name and type
            Label lblPokemonDexNumber = new Label();
            lblPokemonDexNumber.Markup = $"<span font_desc='MS Gothic Regular 24'>{pokemonDex}</span>";
            fix.Put(lblPokemonDexNumber, 40, 45);
            Label lblPokemonName = new Label();
            lblPokemonName.Markup = $"<span font_desc='MS Gothic Regular 15'>{pokemonName}</span>";
            fix.Put(lblPokemonName, 40, 357);
            Label lblPokemonType = new Label();
            lblPokemonType.Markup = $"<span font_desc='MS Gothic Regular 15'>{pokemonType}</span>";
            fix.Put(lblPokemonType, 100, 437);

            // Ability
            Label lblPokemonAbilityOne = new Label();
            lblPokemonAbilityOne.Markup = $"<span font_desc='MS Gothic Regular 15'>[ {pokemonAbilityOne} ]</span>";
            fix.Put(lblPokemonAbilityOne, 375, 63);
            Label lblPokemonAbilityTwo = new Label();
            lblPokemonAbilityTwo.Markup = $"<span font_desc='MS Gothic Regular 15'>[ {pokemonAbilityTwo} ]</span>";
            fix.Put(lblPokemonAbilityTwo, 375, 90);
            Label lblPokemonAbilityThree = new Label();
            lblPokemonAbilityThree.Markup = $"<span font_desc='MS Gothic Regular 15'>[ {pokemonAbilityThree} ]</span>";
            fix.Put(lblPokemonAbilityThree, 585, 63);
            Label lblPokemonAbilityFour = new Label();
            lblPokemonAbilityFour.Markup = $"<span font_desc='MS Gothic Regular 15'>[ {pokemonAbilityFour} ]</span>";
            fix.Put(lblPokemonAbilityFour, 585, 90);

            // Gender ratio and Catch rate
            Label lblPokemonMale = new Label();
            lblPokemonMale.Markup = $"<span font_desc='MS Gothic Regular 15'>{pokemonMale}</span>";
            fix.Put(lblPokemonMale, 385, 225);
            Label lblPokemnFemale = new Label();
            lblPokemnFemale.Markup = $"<span font_desc='MS Gothic Regular 15'>{pokemonFemale}</span>";
            fix.Put(lblPokemnFemale, 500 , 225);
            Label lblPokemonCatchRate = new Label();
            lblPokemonCatchRate.Markup = $"<span font_desc='MS Gothic Regular 15'>{pokemonCatchRate}</span>";
            fix.Put(lblPokemonCatchRate, 665, 210);

            // Statistics
            Label lblPokemonHP = new Label();
            lblPokemonHP.Markup = $"<span font_desc='MS Gothic Regular 15'>{pokemonHP}</span>";
            fix.Put(lblPokemonHP, 384, 327);
            Label lblPokemonATK = new Label();
            lblPokemonATK.Markup = $"<span font_desc='MS Gothic Regular 15'>{pokemonATK}</span>";
            fix.Put(lblPokemonATK, 451, 327);
            Label lblPokemonDEF = new Label();
            lblPokemonDEF.Markup = $"<span font_desc='MS Gothic Regular 15'>{pokemonDEF}</span>";
            fix.Put(lblPokemonDEF, 520, 327);
            Label lblPokemonSpATK = new Label();
            lblPokemonSpATK.Markup = $"<span font_desc='MS Gothic Regular 15'>{pokemonSpATK}</span>";
            fix.Put(lblPokemonSpATK, 587, 327);
            Label lblPokemonSpDEF = new Label();
            lblPokemonSpDEF.Markup = $"<span font_desc='MS Gothic Regular 15'>{pokemonSpDEF}</span>";
            fix.Put(lblPokemonSpDEF, 655, 327);
            Label lblPokemonSpeed = new Label();
            lblPokemonSpeed.Markup = $"<span font_desc='MS Gothic Regular 15'>{pokemonSpeed}</span>";
            fix.Put(lblPokemonSpeed, 723, 327);



            Button btnMoves = new ButtonGenerator("Images/pokemon_water/Sem nome (75 × 50 px).png", 75, 50);
            fix.Put(btnMoves, 584, 410);

            Add(fix);
            ShowAll();
        }
    }
}
