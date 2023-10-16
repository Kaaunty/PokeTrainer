using Gdk;
using Gtk;
using NowComesGtk.Reusable_components;
using NowComesGtk.Utils;
using PokeApi.BackEnd.Service;
using PokeApiNet;
using System.Globalization;

namespace NowComesGtk.Screens
{
    public class PokemonScreen : BaseWindow
    {
#nullable disable

        private static ApiRequest _apiRequest = new();
        private Pokemon pokemon;
        private Fixed fix = new Fixed();
        private TextInfo textInfo = new CultureInfo("pt-BR", false).TextInfo;
        private PokemonSpecies pokeSpecies;
        private List<Ability> pokeAbility = new List<Ability>();
        private Image PokemonAnimation = new Image();
        private bool isLoaded = false;
        private CssProvider cssProvider = new CssProvider();
        private string pokemonNameFormatted;
        private string pokemonDexFormatted;
        private string pokemonHPFormatted;
        private string pokemonATKFormatted;
        private string pokemonDEFFormatted;
        private string pokemonSpATKFormatted;
        private string pokemonSpDEFFormatted;
        private string pokemonSpeedFormatted;
        private string pokemonMaleFormatted;
        private string pokemonFemaleFormatted;
        private string pokemonCatchRate;
        private string pokemonAbilityOneUpper;
        private string pokemonAbilityTwoUpper;
        private string pokemonAbilityThreeUpper;
        private string pokemonAbilityFourUpper;
        private string PokemonFirstTypeFormatted;
        private string pokemonSecondaryTypeFormatted;
        private string PokemonFirstTypeFormattedTitle;

        private int variationId = 0;
        private Label lblPokemonName = new Label();
        private Label lblPokemonDexNumber = new Label();
        private Label lblPokemonAbilityOne = new Label();
        private Label lblPokemonAbilityTwo = new Label();
        private Label lblPokemonAbilityThree = new Label();
        private Label lblPokemonAbilityFour = new Label();
        private Label lblPokemonHP = new Label();
        private Label lblPokemonATK = new Label();
        private Label lblPokemonDEF = new Label();
        private Label lblPokemonSpATK = new Label();
        private Label lblPokemonSpDEF = new Label();
        private Label lblPokemonSpeed = new Label();

        private Image PokemonTypeOne = new Image();

        public PokemonScreen(Pokemon Pokemon) : base("", 800, 500)
        {
            try
            {
                pokemon = Pokemon;

                PopulateFields();

                while (!isLoaded)
                {
                    Task.Delay(100).Wait();
                }
                cssProvider.LoadFromPath("Styles/pokemonScreen.css");

                StyleContext.AddProviderForScreen(Gdk.Screen.Default, cssProvider, 800);

                PokemonFirstTypeFormattedTitle = _apiRequest.Translate(textInfo.ToTitleCase(PokemonFirstTypeFormatted));
                Title = $"PokéTrainer© // Pokémon tipo - {PokemonFirstTypeFormattedTitle} // Pokémon - {pokemonNameFormatted} [{pokemonDexFormatted}]";

                Image Background = new Image("Images/pokemon_water/pokemonWater_backgroung.png");
                fix.Put(Background, 0, 0);

                PokemonAnimation.PixbufAnimation = new PixbufAnimation("Images/PokemonAnimated.gif");

                GetPokemonGifSize();

                lblPokemonDexNumber.Text = pokemonDexFormatted;
                fix.Put(lblPokemonDexNumber, 40, 45);

                lblPokemonName.Text = pokemonNameFormatted;
                fix.Put(lblPokemonName, 40, 357);

                PokemonTypeOne = new Image($"Images/pokemon_types/{pokemon.Types[0].Type.Name}.png");
                fix.Put(PokemonTypeOne, 93, 427);

                if (pokemon.Types.Count > 1)
                {
                    Image imagePokemonTypeSecondary = new Image($"Images/pokemon_types/{pokemon.Types[1].Type.Name}.png");
                    fix.Put(imagePokemonTypeSecondary, 179, 427);
                }

                lblPokemonAbilityOne.Text = pokemonAbilityOneUpper;

                fix.Put(lblPokemonAbilityOne, 375, 63);
                string PokemonAbilityOneToolTipTranslated = _apiRequest.Translate(pokeAbility[0].EffectEntries[1].Effect);

                lblPokemonAbilityOne.TooltipMarkup = $"<span foreground='white' font_desc='MS Gothic Regular 15'>[{PokemonAbilityOneToolTipTranslated}]</span>";

                if (pokemon.Abilities.Count == 2 && !String.IsNullOrEmpty(pokemon.Abilities[1].Ability.Name))
                {
                    lblPokemonAbilityTwo.Text = pokemonAbilityTwoUpper;
                    string PokemonAbilityTwoToolTipTranslted = _apiRequest.Translate(pokeAbility[1].EffectEntries[1].Effect);
                    lblPokemonAbilityTwo.TooltipMarkup = $"<span foreground='white' font_desc='MS Gothic Regular 15'>[ {PokemonAbilityTwoToolTipTranslted}]</span>";
                    fix.Put(lblPokemonAbilityTwo, 375, 90);
                }

                if (pokemon.Abilities.Count == 3 && !String.IsNullOrEmpty(pokemon.Abilities[2].Ability.Name))
                {
                    lblPokemonAbilityThree.Text = pokemonAbilityThreeUpper;
                    string PokemonAbilityThreeToolTipTranslated = _apiRequest.Translate(pokeAbility[2].EffectEntries[1].Effect);
                    lblPokemonAbilityTwo.TooltipMarkup = $"<span foreground='white' font_desc='MS Gothic Regular 15'>[ {PokemonAbilityThreeToolTipTranslated}]</span>";
                    fix.Put(lblPokemonAbilityThree, 585, 63);
                }

                if (pokemon.Abilities.Count == 4 && !String.IsNullOrEmpty(pokemon.Abilities[3].Ability.Name))
                {
                    lblPokemonAbilityFour.Text = pokemonAbilityFourUpper;
                    string PokemonAbilityFourToolTipTranslated = _apiRequest.Translate(pokeAbility[3].EffectEntries[1].Effect);

                    lblPokemonAbilityTwo.TooltipMarkup = $"<span foreground='white' font_desc='MS Gothic Regular 15'>[ {PokemonAbilityFourToolTipTranslated}]</span>";

                    fix.Put(lblPokemonAbilityFour, 585, 90);
                }

                Label lblPokemonMale = new Label(pokemonMaleFormatted);
                fix.Put(lblPokemonMale, 377, 225);
                Label lblPokemnFemale = new Label(pokemonFemaleFormatted);
                fix.Put(lblPokemnFemale, 487, 225);
                Label lblPokemonCatchRate = new Label(pokemonCatchRate);
                fix.Put(lblPokemonCatchRate, 665, 210);

                lblPokemonHP.Text = pokemonHPFormatted;
                fix.Put(lblPokemonHP, 375, 327);
                lblPokemonATK.Text = pokemonATKFormatted;
                fix.Put(lblPokemonATK, 442, 327);
                lblPokemonDEF.Text = pokemonDEFFormatted;
                fix.Put(lblPokemonDEF, 511, 327);
                lblPokemonSpATK.Text = pokemonSpATKFormatted;
                fix.Put(lblPokemonSpATK, 578, 327);
                lblPokemonSpDEF.Text = pokemonSpDEFFormatted;
                fix.Put(lblPokemonSpDEF, 646, 327);
                lblPokemonSpeed.Text = pokemonSpeedFormatted;
                fix.Put(lblPokemonSpeed, 714, 327);

                Button PreviousEvolution = new ButtonGenerator("Images/BackButton.png", 40, 40);
                fix.Put(PreviousEvolution, 100, 290);
                PreviousEvolution.Clicked += GetPreviousVariation;

                Button NextEvolution = new ButtonGenerator("Images/NextButton.png", 40, 40);
                fix.Put(NextEvolution, 180, 290);
                NextEvolution.Clicked += GetNextVariation;

                Button btnMoves = new ButtonGenerator("Images/pokemon_water/Sem nome (75 × 50 px).png", 75, 50);
                fix.Put(btnMoves, 584, 410);
                btnMoves.Clicked += PokemonMoves;
                Add(fix);

                ShowAll();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar os dados do Pokémon: {ex.Message}");
            }
        }

        private async void PokemonMoves(object sender, EventArgs e)
        {
            List<Move> pokemonMoves = await _apiRequest.GetMoveLearnedByPokemon(pokemon);

            MovementScreen movementScreen = new(pokemonMoves);
            movementScreen.ShowAll();
        }

        private async Task PopulateFields()
        {
            try
            {
                pokemonNameFormatted = textInfo.ToTitleCase(pokemon.Name);
                pokemonHPFormatted = pokemon.Stats[0].BaseStat.ToString("D3");
                pokemonATKFormatted = pokemon.Stats[1].BaseStat.ToString("D3");
                pokemonDEFFormatted = pokemon.Stats[2].BaseStat.ToString("D3");
                pokemonSpATKFormatted = pokemon.Stats[3].BaseStat.ToString("D3");
                pokemonSpDEFFormatted = pokemon.Stats[4].BaseStat.ToString("D3");
                pokemonSpeedFormatted = pokemon.Stats[5].BaseStat.ToString("D3");

                string pokemonSpecie = pokemon.Species.Name;

                await Task.Run(() => GetPokemonSpecies(pokemonSpecie)).ConfigureAwait(false);

                await Task.Run(() => UpdatePokemonSprite()).ConfigureAwait(false);

                pokemonDexFormatted = "#" + pokeSpecies.Id.ToString("D4");

                foreach (var abilities in pokemon.Abilities)
                {
                    await Task.Run(() => GetPokemonAbilitiesList(abilities.Ability.Name)).ConfigureAwait(false);
                }

                if (pokemon.Abilities.Count == 1)
                {
                    pokemonAbilityOneUpper = textInfo.ToTitleCase(pokemon.Abilities[0].Ability.Name);
                }
                else if (pokemon.Abilities.Count == 2)
                {
                    pokemonAbilityOneUpper = textInfo.ToTitleCase(pokemon.Abilities[0].Ability.Name);
                    pokemonAbilityTwoUpper = textInfo.ToTitleCase(pokemon.Abilities[1].Ability.Name);
                }
                else if (pokemon.Abilities.Count == 3)
                {
                    pokemonAbilityOneUpper = textInfo.ToTitleCase(pokemon.Abilities[0].Ability.Name);
                    pokemonAbilityTwoUpper = textInfo.ToTitleCase(pokemon.Abilities[1].Ability.Name);
                    pokemonAbilityThreeUpper = textInfo.ToTitleCase(pokemon.Abilities[2].Ability.Name);
                }
                else if (pokemon.Abilities.Count == 4)
                {
                    pokemonAbilityOneUpper = textInfo.ToTitleCase(pokemon.Abilities[0].Ability.Name);
                    pokemonAbilityTwoUpper = textInfo.ToTitleCase(pokemon.Abilities[1].Ability.Name);
                    pokemonAbilityThreeUpper = textInfo.ToTitleCase(pokemon.Abilities[2].Ability.Name);
                    pokemonAbilityFourUpper = textInfo.ToTitleCase(pokemon.Abilities[3].Ability.Name);
                }

                if (pokemon.Types.Count == 1)
                {
                    PokemonFirstTypeFormatted = pokemon.Types[0].Type.Name;
                    pokemonSecondaryTypeFormatted = "";
                }
                else
                {
                    PokemonFirstTypeFormatted = pokemon.Types[0].Type.Name;
                    pokemonSecondaryTypeFormatted = pokemon.Types[1].Type.Name;
                }
                isLoaded = true;
            }
            catch (Exception ex)
            {
                MessageDialogGenerator.ShowMessageDialog("Erro ao carregar os dados do Pokémon." + ex);
            }
        }

        private async void GetNextVariation(object sender, EventArgs e)
        {
            variationId += 1;
            if (variationId != 0 && variationId < pokeSpecies.Varieties.Count && variationId > -1)
            {
                pokeAbility.Clear();
                string nextPokemon = pokeSpecies.Varieties[variationId].Pokemon.Name;
                pokemon = await _apiRequest.GetPokemonAsync(nextPokemon);
                await UpdatePokemonSprite2(nextPokemon);
                GetPokemonGifSize();
                await PopulateFields();
                UpdateLabels();
            }
            else
            {
                MessageDialogGenerator.ShowMessageDialog("Não há mais variações para este Pokémon.");
            }
        }

        private async void GetPreviousVariation(object sender, EventArgs e)
        {
            variationId -= 1;
            if (variationId < pokeSpecies.Varieties.Count && variationId > -1)
            {
                pokeAbility.Clear();
                string previousPokemon = pokeSpecies.Varieties[variationId].Pokemon.Name;
                pokemon = await _apiRequest.GetPokemonAsync(previousPokemon);
                await UpdatePokemonSprite2(previousPokemon);
                GetPokemonGifSize();
                await PopulateFields();
                UpdateLabels();
            }
            else
            {
                MessageDialogGenerator.ShowMessageDialog("Não há mais variações para este Pokémon.");
            }
            if (variationId < 0)
            {
                variationId = 0;
            }
        }

        private async Task GetPokemonAbilitiesList(string abilityName)
        {
            Ability ability = await _apiRequest.GetPokemonAbility(abilityName);
            if (pokeAbility != null)
            {
                pokeAbility.Add(ability);
            }
        }

        //private async Task GetPokemonEvolutionChain(string url)
        //{
        //    try
        //    {
        //        evolutionChain = await _apiRequest.GetEvolutionChain(url);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Erro ao carregar os dados do Pokémon: {ex.Message}");
        //    }
        //}

        private async Task GetPokemonSpecies(string PokemonName)
        {
            try
            {
                pokeSpecies = await _apiRequest.GetPokemonSpecies(PokemonName);
                if (pokeSpecies != null)
                {
                    pokemonMaleFormatted = pokeSpecies.GenderRate switch
                    {
                        0 => "0,00%",
                        1 => "12,5%",
                        2 => "25,0%",
                        3 => "50,0%",
                        4 => "75,0%",
                        5 => "87,5%",
                        6 => "100%",
                        _ => pokemonMaleFormatted
                    };

                    pokemonFemaleFormatted = pokeSpecies.GenderRate switch
                    {
                        0 => "100%",
                        1 => "87,5%",
                        2 => "75,0%",
                        3 => "50,0%",
                        4 => "25,0%",
                        5 => "12,5%",
                        6 => "0,00%",
                        _ => pokemonFemaleFormatted
                    };

                    pokemonCatchRate = pokeSpecies.CaptureRate.ToString() + "%";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar os dados do Pokémon: {ex.Message}");
            }
        }

        private async Task UpdatePokemonSprite()
        {
            try
            {
                await _apiRequest.GetPokemonAnimatedSprite(pokemon.Name);
                PokemonAnimation.PixbufAnimation = new PixbufAnimation("Images/PokemonAnimated.gif");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar os dados do Pokémon: {ex.Message}");
            }
        }

        private void GetPokemonGifSize()
        {
            fix.Remove(PokemonAnimation);

            PokemonAnimation.PixbufAnimation = new PixbufAnimation("Images/PokemonAnimated.gif");

            int x = (340 - PokemonAnimation.PixbufAnimation.Width) / 2;
            int y = (450 - PokemonAnimation.PixbufAnimation.Height) / 2;

            fix.Put(PokemonAnimation, x, y);
        }

        private void UpdateLabels()
        {
            lblPokemonDexNumber.Text = pokemonDexFormatted;
            lblPokemonName.Text = pokemonNameFormatted;
            lblPokemonAbilityOne.Text = pokemonAbilityOneUpper;
            PokemonFirstTypeFormattedTitle = _apiRequest.Translate(textInfo.ToTitleCase(PokemonFirstTypeFormatted));

            lblPokemonAbilityOne.TooltipMarkup = $"<span foreground='white' font_desc='MS Gothic Regular 15'>[{_apiRequest.Translate(pokeAbility[0].EffectEntries[1].Effect)}]</span>";

            Title = $"PokéTrainer© // Pokémon tipo - {PokemonFirstTypeFormattedTitle} // Pokémon - {pokemonNameFormatted} [{pokemonDexFormatted}]";
            if (pokemon.Abilities.Count == 2)
            {
                lblPokemonAbilityTwo.Text = pokemonAbilityTwoUpper;
                lblPokemonAbilityTwo.TooltipMarkup = $"<span foreground='white' font_desc='MS Gothic Regular 15'>[{_apiRequest.Translate(pokeAbility[1].EffectEntries[1].Effect)}]</span>";
            }
            else if (pokemon.Abilities.Count == 3)
            {
                lblPokemonAbilityTwo.Text = pokemonAbilityTwoUpper;
                lblPokemonAbilityTwo.TooltipMarkup = $"<span foreground='white' font_desc='MS Gothic Regular 15'>[{_apiRequest.Translate(pokeAbility[1].EffectEntries[1].Effect)}]</span>";

                lblPokemonAbilityThree.Text = pokemonAbilityThreeUpper;
                lblPokemonAbilityThree.TooltipMarkup = $"<span foreground='white' font_desc='MS Gothic Regular 15'>[{_apiRequest.Translate(pokeAbility[2].EffectEntries[1].Effect)}]</span>";
            }
            else if (pokemon.Abilities.Count == 4)
            {
                lblPokemonAbilityTwo.Text = pokemonAbilityTwoUpper;
                lblPokemonAbilityTwo.TooltipMarkup = $"<span foreground='white' font_desc='MS Gothic Regular 15'>[{_apiRequest.Translate(pokeAbility[1].EffectEntries[1].Effect)}]</span>";

                lblPokemonAbilityThree.Text = pokemonAbilityThreeUpper;
                lblPokemonAbilityThree.TooltipMarkup = $"<span foreground='white' font_desc='MS Gothic Regular 15'>[{_apiRequest.Translate(pokeAbility[2].EffectEntries[1].Effect)}]</span>";

                lblPokemonAbilityFour.Text = pokemonAbilityFourUpper;
                lblPokemonAbilityFour.TooltipMarkup = $"<span foreground='white' font_desc='MS Gothic Regular 15'>[{_apiRequest.Translate(pokeAbility[3].EffectEntries[1].Effect)}]</span>";
            }

            PokemonFirstTypeFormatted = pokemon.Types[0].Type.Name;

            if (pokemon.Types.Count > 1)
            {
                pokemonSecondaryTypeFormatted = pokemon.Types[1].Type.Name;
            }

            lblPokemonHP.Text = pokemonHPFormatted;
            lblPokemonATK.Text = pokemonATKFormatted;
            lblPokemonDEF.Text = pokemonDEFFormatted;
            lblPokemonSpATK.Text = pokemonSpATKFormatted;
            lblPokemonSpDEF.Text = pokemonSpDEFFormatted;
            lblPokemonSpeed.Text = pokemonSpeedFormatted;
        }

        private async Task UpdatePokemonSprite2(string poke)
        {
            try
            {
                await _apiRequest.GetPokemonAnimatedSprite(poke);
                PokemonAnimation.PixbufAnimation = new PixbufAnimation("Images/PokemonAnimated.gif");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar os dados do Pokémon: {ex.Message}");
            }
        }
    }
}