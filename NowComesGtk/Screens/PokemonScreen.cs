using NowComesGtk.Reusable_components;
using NowComesGtk.Utils;
using PokeApiNet;
using Gtk;
using PokeApi.BackEnd.Service;
using Gdk;

namespace NowComesGtk.Screens
{
    public class PokemonScreen : BaseWindow
    {
#nullable disable
        private ApiRequest _apiRequest = new();
        private Pokemon pokemon;
        public string pokemonDex = "#0123";
        public string pokemonName;
        public string pokemonType;
        private Pixbuf pixbuf = new Pixbuf("Images/PokemonAnimated.gif");
        public string pokemonAbilityOne;
        public string pokemonAbilityTwo;
        public string pokemonAbilityThree;
        public string pokemonAbilityFour;
        private Image pokemonPic = new Image("Images/PokemonAnimated.gif");
        public string pokemonMale = "25%";
        public string pokemonFemale = "75%";
        public string pokemonCatchRate = "100%";

        public string pokemonHP;
        public string pokemonATK;
        public string pokemonDEF;
        public string pokemonSpATK;
        public string pokemonSpDEF;
        public string pokemonSpeed;

        public PokemonScreen(Pokemon pokemon) : base("", 800, 500)
        {
            this.pokemon = pokemon;

            PopulateFields();
            Fixed fix = new Fixed();

            string title = $"PokéTrainer© // Pokémons tipo - Água // Pokémons - Pokemon [#0000]";
            Title = title;

            Image Background = new Image("Images/pokemon_water/pokemonWater_backgroung.png");
            fix.Put(Background, 0, 0);

            pokemonPic.Pixbuf = pixbuf.ScaleSimple(120, 120, InterpType.Bilinear);
            pokemonPic.PixbufAnimation = new PixbufAnimation("Images/PokemonAnimated.gif");

            fix.Put(pokemonPic, 90, 120);

            // Dex number, name and type
            Label lblPokemonDexNumber = new Label();
            lblPokemonDexNumber.Markup = $"<span font_desc='MS Gothic Regular 24'>#{pokemonDex}</span>";
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
            fix.Put(lblPokemnFemale, 500, 225);
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

            // Moves button
            Button btnMoves = new ButtonGenerator("Images/pokemon_water/Sem nome (75 × 50 px).png", 75, 50);
            fix.Put(btnMoves, 584, 410);
            btnMoves.Clicked += PokemonMoves;

            Add(fix);
            ShowAll();
        }

        private void PokemonMoves(object sender, EventArgs e)
        {
            List<string> moves = new List<string>();

            MovementScreen movementScreen = new();
            movementScreen.ShowAll();
        }

        private async void PopulateFields()
        {
            pokemonDex = pokemon.Id.ToString();
            pokemonName = pokemon.Name;

            if (pokemon.Abilities.Count < 2)
            {
                pokemonAbilityOne = pokemon.Abilities[0].Ability.Name;
            }

            if (pokemon.Abilities.Count >= 2)
            {
                pokemonAbilityOne = pokemon.Abilities[0].Ability.Name;
                pokemonAbilityTwo = pokemon.Abilities[1].Ability.Name;
            }

            if (pokemon.Abilities.Count >= 3)
            {
                pokemonAbilityOne = pokemon.Abilities[0].Ability.Name;
                pokemonAbilityTwo = pokemon.Abilities[1].Ability.Name;
                pokemonAbilityThree = pokemon.Abilities[2].Ability.Name;
            }
            if (pokemon.Abilities.Count >= 4)
            {
                pokemonAbilityOne = pokemon.Abilities[0].Ability.Name;
                pokemonAbilityTwo = pokemon.Abilities[1].Ability.Name;
                pokemonAbilityThree = pokemon.Abilities[2].Ability.Name;
                pokemonAbilityFour = pokemon.Abilities[3].Ability.Name;
            }

            foreach (var item in pokemon.Types)
            {
                pokemonType = item.Type.Name;
            }

            pokemonHP = pokemon.Stats[0].BaseStat.ToString();
            pokemonATK = pokemon.Stats[1].BaseStat.ToString();
            pokemonDEF = pokemon.Stats[2].BaseStat.ToString();
            pokemonSpATK = pokemon.Stats[3].BaseStat.ToString();
            pokemonSpDEF = pokemon.Stats[4].BaseStat.ToString();
            pokemonSpeed = pokemon.Stats[5].BaseStat.ToString();

            await _apiRequest.GetPokemonAnimatedSprite(pokemonName);

            pokemonPic.Pixbuf = pixbuf.ScaleSimple(150, 150, InterpType.Bilinear);
            pokemonPic.PixbufAnimation = new PixbufAnimation("Images/PokemonAnimated.gif");
        }
    }
}