using Gdk;
using Gtk;
using NowComesGtk;
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

        private ApiRequest _apiRequest = new();
        private Pokemon pokemon;
        public int pokemonDex;
        public string pokemonName;
        public string pokemonType;
        private string pokemonTypeSecondary;
        private bool isLoaded = false;
        private PokemonSpecies pokeSpecies;
        private PixbufAnimation pokemonAnimation;
        private List<Ability> pokeAbility = new List<Ability>();
        public string pokemonAbilityOne;
        public string pokemonAbilityTwo;
        public string pokemonAbilityThree;
        public string pokemonAbilityFour;
        public string pokemonMale;
        public string pokemonFemale;
        public string pokemonCatchRate;
        private TextInfo textInfo = new CultureInfo("pt-BR", false).TextInfo;
        public int pokemonHP;
        public int pokemonATK;
        public int pokemonDEF;
        public int pokemonSpATK;
        public int pokemonSpDEF;
        public int pokemonSpeed;

        public PokemonScreen(Pokemon pokemon) : base("", 800, 500)
        {
            this.pokemon = pokemon;
            PopulateFields();

            while (!isLoaded)
            {
                Task.Delay(100).Wait();
            }

            Fixed fix = new Fixed();

            string title = $"PokéTrainer© // Pokémons tipo - Água // Pokémons - Pokemon [#0000]";
            Title = title;

            Image Background = new Image("Images/pokemon_water/pokemonWater_backgroung.png");

            fix.Put(Background, 0, 0);

            var animationAlignment = new Alignment(0.5f, 0.5f, 0, 0);

            pokemonAnimation = new PixbufAnimation("Images/PokemonAnimated.gif");

            var animationImage = new Image();

            animationImage.PixbufAnimation = pokemonAnimation;

            animationAlignment.Add(animationImage);

            fix.Put(animationAlignment, 150, 200);

            // Dex number, name and type
            Label lblPokemonDexNumber = new Label();
            string pokemonDexFormatted = pokemonDex.ToString("D3");
            lblPokemonDexNumber.Markup = $"<span font_desc='MS Gothic Regular 24'>#{pokemonDexFormatted}</span>";

            fix.Put(lblPokemonDexNumber, 40, 45);

            Label lblPokemonName = new Label();

            string PokemonNameUpper = textInfo.ToTitleCase(pokemonName);
            lblPokemonName.Markup = $"<span font_desc='MS Gothic Regular 15'>{PokemonNameUpper}</span>";
            fix.Put(lblPokemonName, 40, 357);

            Image PokemonTypeOne = new Image($"Images/pokemon_types/{pokemonType}.png");
            fix.Put(PokemonTypeOne, 93, 427);

            if (pokemon.Types.Count > 1)
            {
                Image imagePokemonTypeSecondary = new Image($"Images/pokemon_types/{pokemonTypeSecondary}.png");
                fix.Put(imagePokemonTypeSecondary, 179, 427);
            }

            //Label lblPokemonType = new Label();

            //string pokemonTypeUpper = textInfo.ToTitleCase(pokemonType);
            //lblPokemonType.Markup = $"<span font_desc='MS Gothic Regular 15'>{pokemonTypeUpper}</span>";
            //fix.Put(lblPokemonType, 100, 437);

            //Label lblPokemonTypeSecondary = new Label();
            //if (!String.IsNullOrEmpty(pokemonTypeSecondary))
            //{
            //    string pokemonTypeSecondaryUpper = textInfo.ToTitleCase(pokemonTypeSecondary);
            //    lblPokemonType.Markup = $"<span font_desc='MS Gothic Regular 15'>{pokemonTypeUpper}/</span>";
            //    lblPokemonTypeSecondary.Markup = $"<span font_desc='MS Gothic Regular 15'>{pokemonTypeSecondaryUpper}</span>";
            //    fix.Put(lblPokemonTypeSecondary, 165, 437);
            //}

            // Ability
            Label lblPokemonAbilityOne = new Label();

            string pokemonAbilityOneUpper = textInfo.ToTitleCase(pokemonAbilityOne);
            lblPokemonAbilityOne.Markup = $"<span font_desc='MS Gothic Regular 15'>[ {pokemonAbilityOneUpper} ]</span>";
            fix.Put(lblPokemonAbilityOne, 375, 63);
            string PokemonAbilityOneToolTipTranslated = _apiRequest.Translate(pokeAbility[0].EffectEntries[1].Effect);

            lblPokemonAbilityOne.TooltipMarkup = $"<span font_desc='MS Gothic Regular 15'>{PokemonAbilityOneToolTipTranslated}</span>";
            Label lblPokemonAbilityTwo = new Label();
            if (!String.IsNullOrEmpty(pokemonAbilityTwo))
            {
                string pokemonAbilityTwoUpper = textInfo.ToTitleCase(pokemonAbilityTwo);
                lblPokemonAbilityTwo.Markup = $"<span font_desc='MS Gothic Regular 15'>[ {pokemonAbilityTwoUpper} ]</span>";
                string PokemonAbilityTwoToolTipTranslted = _apiRequest.Translate(pokeAbility[1].EffectEntries[1].Effect);
                lblPokemonAbilityTwo.TooltipMarkup = $"<span font_desc='MS Gothic Regular 15'>{PokemonAbilityTwoToolTipTranslted}</span>";
                fix.Put(lblPokemonAbilityTwo, 375, 90);
            }

            Label lblPokemonAbilityThree = new Label();
            if (!String.IsNullOrEmpty(pokemonAbilityThree))
            {
                string pokemonAbilityThreeUpper = textInfo.ToTitleCase(pokemonAbilityThree);
                lblPokemonAbilityThree.Markup = $"<span font_desc='MS Gothic Regular 15'>[ {pokemonAbilityThreeUpper} ]</span>";
                string PokemonAbilityThreeToolTipTranslated = _apiRequest.Translate(pokeAbility[2].EffectEntries[1].Effect);
                lblPokemonAbilityThree.TooltipMarkup = $"<span font_desc='MS Gothic Regular 15'>{PokemonAbilityThreeToolTipTranslated}</span>";
                fix.Put(lblPokemonAbilityThree, 585, 63);
            }

            Label lblPokemonAbilityFour = new Label();
            if (!String.IsNullOrEmpty(pokemonAbilityFour))
            {
                string pokemonAbilityFourUpper = textInfo.ToTitleCase(pokemonAbilityFour);
                lblPokemonAbilityFour.Markup = $"<span font_desc='MS Gothic Regular 15'>[ {pokemonAbilityFourUpper} ]</span>";
                string PokemonAbilityFourToolTipTranslated = _apiRequest.Translate(pokeAbility[3].EffectEntries[1].Effect);
                lblPokemonAbilityFour.TooltipMarkup = $"<span font_desc='MS Gothic Regular 15'>{PokemonAbilityFourToolTipTranslated}</span>";
                fix.Put(lblPokemonAbilityFour, 585, 90);
            }

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
            string pokemonHPFormatted = pokemonHP.ToString("D3");
            lblPokemonHP.Markup = $"<span font_desc='MS Gothic Regular 15'>{pokemonHPFormatted}</span>";
            fix.Put(lblPokemonHP, 375, 327);
            Label lblPokemonATK = new Label();
            string pokemonATKFormatted = pokemonATK.ToString("D3");
            lblPokemonATK.Markup = $"<span font_desc='MS Gothic Regular 15'>{pokemonATKFormatted}</span>";
            fix.Put(lblPokemonATK, 442, 327);
            Label lblPokemonDEF = new Label();
            string pokemonDEFFormatted = pokemonDEF.ToString("D3");
            lblPokemonDEF.Markup = $"<span font_desc='MS Gothic Regular 15'>{pokemonDEFFormatted}</span>";
            fix.Put(lblPokemonDEF, 511, 327);
            Label lblPokemonSpATK = new Label();
            string pokemonSpATKFormatted = pokemonSpATK.ToString("D3");
            lblPokemonSpATK.Markup = $"<span font_desc='MS Gothic Regular 15'>{pokemonSpATKFormatted}</span>";
            fix.Put(lblPokemonSpATK, 578, 327);
            Label lblPokemonSpDEF = new Label();
            string pokemonSpDEFFormatted = pokemonSpDEF.ToString("D3");
            lblPokemonSpDEF.Markup = $"<span font_desc='MS Gothic Regular 15'>{pokemonSpDEFFormatted}</span>";
            fix.Put(lblPokemonSpDEF, 646, 327);
            Label lblPokemonSpeed = new Label();
            string pokemonSpeedFormatted = pokemonSpeed.ToString("D3");
            lblPokemonSpeed.Markup = $"<span font_desc='MS Gothic Regular 15'>{pokemonSpeedFormatted}</span>";
            fix.Put(lblPokemonSpeed, 714, 327);

            // Moves button
            Button NextEvolution = new ButtonGenerator("Images/NextButton.png", 40, 40);
            fix.Put(NextEvolution, 180, 290);
            NextEvolution.Clicked += GetNextEvolution;

            Button PreviousEvolution = new ButtonGenerator("Images/BackButton.png", 40, 40);
            fix.Put(PreviousEvolution, 100, 290);
            PreviousEvolution.Clicked += GetPreviousEvolution;

            Button btnMoves = new ButtonGenerator("Images/pokemon_water/Sem nome (75 × 50 px).png", 75, 50);
            fix.Put(btnMoves, 584, 410);
            btnMoves.Clicked += PokemonMoves;

            Add(fix);
            ShowAll();
        }

        private async void GetNextEvolution(object sender, EventArgs e)
        {
            try
            {
                string nextEvolution = pokeSpecies.EvolutionChain.Url;
                EvolutionChain evolutionChain = await _apiRequest.GetEvolutionChain(nextEvolution);
                if (evolutionChain.Chain.EvolvesTo.Count >= 1 && evolutionChain.Chain.EvolvesTo[0].Species.Name != pokemonName)
                {
                    if (evolutionChain.Chain.EvolvesTo[0].EvolvesTo.Count == 1 && evolutionChain.Chain.Species.Name == pokemonName)
                    {
                        if (evolutionChain.Chain.EvolvesTo[0].Species.Name != pokemonName && evolutionChain.Chain.EvolvesTo[0].EvolvesTo[0].Species.Name != pokemonName)
                        {
                            if (evolutionChain != null)
                            {
                                Pokemon pokemonNextEvolution = await _apiRequest.GetPokemonAsync(evolutionChain.Chain.EvolvesTo[0].Species.Name);
                                PokemonScreen pokemonScreen = new(pokemonNextEvolution);
                                this.Destroy();
                                pokemonScreen.ShowAll();
                            }
                        }
                    }
                    else if (evolutionChain.Chain.EvolvesTo.Count == 1 && evolutionChain.Chain.Species.Name == pokemonName)
                    {
                        if (evolutionChain != null)
                        {
                            Pokemon pokemonNextEvolution = await _apiRequest.GetPokemonAsync(evolutionChain.Chain.EvolvesTo[0].Species.Name);
                            PokemonScreen pokemonScreen = new(pokemonNextEvolution);
                            this.Destroy();
                            pokemonScreen.ShowAll();
                        }
                    }
                    else if (evolutionChain.Chain.EvolvesTo[0].EvolvesTo[0].Species.Name != pokemonName && evolutionChain.Chain.Species.Name != pokemonName)
                    {
                        if (evolutionChain != null)
                        {
                            Pokemon pokemonNextEvolution = await _apiRequest.GetPokemonAsync(evolutionChain.Chain.EvolvesTo[0].EvolvesTo[0].Species.Name);
                            PokemonScreen pokemonScreen = new(pokemonNextEvolution);
                            this.Destroy();
                            pokemonScreen.ShowAll();
                        }
                    }
                    else
                    {
                        Console.WriteLine("This Pokémon doesn't have a next evolution.");
                        MessageDialogGenerator.ShowMessageDialog("Este Pokémon não possui uma próxima evolução.");
                    }
                }
                else
                {
                    Console.WriteLine("This Pokémon doesn't have a next evolution.");
                    MessageDialogGenerator.ShowMessageDialog("Este Pokémon não possui uma próxima evolução.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar os dados do Pokémon: {ex.Message}");
            }
        }

        private async void GetPreviousEvolution(object sender, EventArgs e)
        {
            try
            {
                string previousEvolution = pokeSpecies.EvolutionChain.Url;
                EvolutionChain evolutionChain = await _apiRequest.GetEvolutionChain(previousEvolution);
                if (evolutionChain.Chain.EvolvesTo.Count >= 1)
                {
                    if (evolutionChain.Chain.EvolvesTo[0].Species.Name != pokemonName && evolutionChain.Chain.Species.Name != pokemonName)
                    {
                        if (evolutionChain != null)
                        {
                            Pokemon pokemonPreviousEvolution = await _apiRequest.GetPokemonAsync(evolutionChain.Chain.EvolvesTo[0].Species.Name);
                            PokemonScreen pokemonScreen = new(pokemonPreviousEvolution);
                            this.Destroy();
                            pokemonScreen.ShowAll();
                        }
                    }
                    else if (evolutionChain.Chain.EvolvesTo[0].EvolvesTo.Count == 1 && evolutionChain.Chain.Species.Name != pokemonName)
                    {
                        if (evolutionChain.Chain.EvolvesTo[0].EvolvesTo[0].Species.Name != pokemonName)
                        {
                            if (evolutionChain != null)
                            {
                                Pokemon pokemonPreviousEvolution = await _apiRequest.GetPokemonAsync(evolutionChain.Chain.Species.Name);
                                PokemonScreen pokemonScreen = new(pokemonPreviousEvolution);
                                this.Destroy();
                                pokemonScreen.ShowAll();
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("This Pokémon doesn't have a previous evolution.");
                    MessageDialogGenerator.ShowMessageDialog("Este Pokémon não possui uma evolução anterior.");
                }
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

        private async void PopulateFields()
        {
            try
            {
                pokemonDex = pokemon.Id;
                pokemonName = pokemon.Name;

                await Task.Run(() => GetPokemonSpecies(pokemonName)).ConfigureAwait(false);
                await Task.Run(() => UpdatePokemonSprite()).ConfigureAwait(false);

                foreach (var abilities in pokemon.Abilities)
                {
                    await Task.Run(() => GetPokemonAbilitiesList(abilities.Ability.Name)).ConfigureAwait(false);
                }

                if (pokemon.Abilities.Count == 1)
                {
                    pokemonAbilityOne = pokemon.Abilities[0].Ability.Name;
                }
                else if (pokemon.Abilities.Count == 2)
                {
                    pokemonAbilityOne = pokemon.Abilities[0].Ability.Name;
                    pokemonAbilityTwo = pokemon.Abilities[1].Ability.Name;
                }
                else if (pokemon.Abilities.Count == 3)
                {
                    pokemonAbilityOne = pokemon.Abilities[0].Ability.Name;
                    pokemonAbilityTwo = pokemon.Abilities[1].Ability.Name;
                    pokemonAbilityThree = pokemon.Abilities[2].Ability.Name;
                }
                else if (pokemon.Abilities.Count == 4)
                {
                    pokemonAbilityOne = pokemon.Abilities[0].Ability.Name;
                    pokemonAbilityTwo = pokemon.Abilities[1].Ability.Name;
                    pokemonAbilityThree = pokemon.Abilities[2].Ability.Name;
                    pokemonAbilityFour = pokemon.Abilities[3].Ability.Name;
                }

                if (pokemon.Types.Count == 1)
                {
                    pokemonType = pokemon.Types[0].Type.Name;
                    pokemonTypeSecondary = "";
                }
                else
                {
                    pokemonType = pokemon.Types[0].Type.Name;
                    pokemonTypeSecondary = pokemon.Types[1].Type.Name;
                }

                pokemonHP = pokemon.Stats[0].BaseStat;
                pokemonATK = pokemon.Stats[1].BaseStat;
                pokemonDEF = pokemon.Stats[2].BaseStat;
                pokemonSpATK = pokemon.Stats[3].BaseStat;
                pokemonSpDEF = pokemon.Stats[4].BaseStat;
                pokemonSpeed = pokemon.Stats[5].BaseStat;

                isLoaded = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar os dados do Pokémon: {ex.Message}");
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

        private async Task GetPokemonSpecies(string PokemonName)
        {
            try
            {
                pokeSpecies = await _apiRequest.GetPokemonSpecies(PokemonName);
                if (pokeSpecies != null)
                {
                    pokemonMale = pokeSpecies.GenderRate switch
                    {
                        0 => "0%",
                        1 => "12.5%",
                        2 => "25%",
                        3 => "50%",
                        4 => "75%",
                        5 => "87.5%",
                        6 => "100%",
                        _ => pokemonMale
                    };

                    pokemonFemale = pokeSpecies.GenderRate switch
                    {
                        0 => "100%",
                        1 => "87.5%",
                        2 => "75%",
                        3 => "50%",
                        4 => "25%",
                        5 => "12.5%",
                        6 => "0%",
                        _ => pokemonFemale
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
                await _apiRequest.GetPokemonAnimatedSprite(pokemonName);
                pokemonAnimation = new PixbufAnimation("Images/PokemonAnimated.gif");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar os dados do Pokémon: {ex.Message}");
            }
        }
    }
}