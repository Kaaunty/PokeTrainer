using NowComesGtk.Reusable_components;
using PokeApi.BackEnd.Service;
using System.Globalization;
using NowComesGtk.Utils;
using PokeApiNet;
using Gdk;
using Gtk;

namespace NowComesGtk.Screens
{
    public class PokemonScreen : BaseWindow
    {
#nullable disable

        private TextInfo textInfo = new CultureInfo("pt-BR", false).TextInfo;
        private List<Ability> pokeAbility = new List<Ability>();
        public Label lblPokemonAbilityOne = new Label();
        public Label lblPokemonAbilityTwo = new Label();
        public Label lblPokemonAbilityThree = new Label();
        public Label lblPokemonAbilityFour = new Label();
        private PixbufAnimation pokemonAnimation;
        private ApiRequest _apiRequest = new();
        private EvolutionChain evolutionChain;
        private PokemonSpecies pokeSpecies;
        private Pokemon pokemon;

        public string pokemonName, pokemonType, pokemonTypeSecondary, pokemonMale, pokemonFemale, pokemonCatchRate;
        public int pokemonDex, pokemonHP, pokemonATK, pokemonDEF, pokemonSpATK, pokemonSpDEF, pokemonSpeed;
        public string pokemonAbilityOne, pokemonAbilityTwo, pokemonAbilityThree, pokemonAbilityFour;
        private bool isLoaded = false;

        public PokemonScreen(Pokemon pokemon) : base("", 800, 500)
        {
            this.pokemon = pokemon;
            PopulateFields();

            while (!isLoaded)
            {
                Task.Delay(100).Wait();
            }

            Fixed fix = new();

            string PokemonNameUpper = textInfo.ToTitleCase(pokemonName);
            string pokemonDexFormatted = pokemonDex.ToString("D3");
            string PokemonFirstTypeFormatted = textInfo.ToTitleCase(_apiRequest.Translate(pokemon.Types[0].Type.Name));

            string title = $"PokéTrainer© // Pokémon tipo - {PokemonFirstTypeFormatted} // Pokémon - {PokemonNameUpper} [#{pokemonDexFormatted}]";
            Title = title;
            Image Background = new Image("Images/pokemon_water/pokemonWater_backgroung.png");
            fix.Put(Background, 0, 0);
            Image animationImage = new Image();
            animationImage.PixbufAnimation = new PixbufAnimation("Images/PokemonAnimated.gif");

            Label lblPokemonDexNumber = new Label();
            lblPokemonDexNumber.Markup = $"<span font_desc='MS Gothic Regular 24'>#{pokemonDexFormatted}</span>";
            fix.Put(lblPokemonDexNumber, 40, 45);

            Label lblPokemonName = new Label();
            lblPokemonName.Markup = $"<span font_desc='MS Gothic Regular 15'>{PokemonNameUpper}</span>";
            fix.Put(lblPokemonName, 40, 357);

            Image PokemonTypeOne = new Image($"Images/pokemon_types/{pokemonType}.png");
            fix.Put(PokemonTypeOne, 93, 427);
            if (pokemon.Types.Count > 1)
            {
                Image imagePokemonTypeSecondary = new Image($"Images/pokemon_types/{pokemonTypeSecondary}.png");
                fix.Put(imagePokemonTypeSecondary, 179, 427);
            }

            if (!string.IsNullOrEmpty(pokemonAbilityOne))
            {
                string pokemonAbilityOneUpper = textInfo.ToTitleCase(pokemonAbilityOne);
                lblPokemonAbilityOne.Markup = $"<span font_desc='MS Gothic Regular 15'>[ {pokemonAbilityOneUpper} ]</span>";
                fix.Put(lblPokemonAbilityOne, 375, 63);
            }
            if (!string.IsNullOrEmpty(pokemonAbilityTwo))
            {
                string pokemonAbilityTwoUpper = textInfo.ToTitleCase(pokemonAbilityTwo);
                lblPokemonAbilityTwo.Markup = $"<span font_desc='MS Gothic Regular 15'>[ {pokemonAbilityTwoUpper} ]</span>";
                fix.Put(lblPokemonAbilityTwo, 375, 90);
            }
            if (!string.IsNullOrEmpty(pokemonAbilityThree))
            {
                string pokemonAbilityThreeUpper = textInfo.ToTitleCase(pokemonAbilityThree);
                lblPokemonAbilityThree.Markup = $"<span font_desc='MS Gothic Regular 15'>[ {pokemonAbilityThreeUpper} ]</span>";
                fix.Put(lblPokemonAbilityThree, 585, 63);
            }
            if (!string.IsNullOrEmpty(pokemonAbilityFour))
            {
                string pokemonAbilityFourUpper = textInfo.ToTitleCase(pokemonAbilityFour);
                lblPokemonAbilityFour.Markup = $"<span font_desc='MS Gothic Regular 15'>[ {pokemonAbilityFourUpper} ]</span>";
                fix.Put(lblPokemonAbilityFour, 585, 90);
            }

            //Gender ratio and Catch rate
            Label lblPokemonMale = new Label();
            lblPokemonMale.Markup = $"<span font_desc='MS Gothic Regular 15'>{pokemonMale}</span>";
            fix.Put(lblPokemonMale, 385, 225);
            Label lblPokemnFemale = new Label();
            lblPokemnFemale.Markup = $"<span font_desc='MS Gothic Regular 15'>{pokemonFemale}</span>";
            fix.Put(lblPokemnFemale, 500, 225);
            Label lblPokemonCatchRate = new Label();
            lblPokemonCatchRate.Markup = $"<span font_desc='MS Gothic Regular 15'>{pokemonCatchRate}</span>";
            fix.Put(lblPokemonCatchRate, 665, 210);

            //Statistics
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

             //Moves button
            if (evolutionChain.Chain.EvolvesTo.Count != 0 && evolutionChain.Chain.EvolvesTo[0].EvolvesTo.Count != 0)
            {
                Button NextEvolution = new ButtonGenerator("Images/NextButton.png", 40, 40);
                fix.Put(NextEvolution, 180, 290);
                NextEvolution.Clicked += GetNextEvolution;

                Button PreviousEvolution = new ButtonGenerator("Images/BackButton.png", 40, 40);
                fix.Put(PreviousEvolution, 100, 290);
                PreviousEvolution.Clicked += GetPreviousEvolution;
            }

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
                                Destroy();
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
                            Destroy();
                            pokemonScreen.ShowAll();
                        }
                    }
                    else if (evolutionChain.Chain.EvolvesTo[0].EvolvesTo[0].Species.Name != pokemonName && evolutionChain.Chain.Species.Name != pokemonName)
                    {
                        if (evolutionChain != null)
                        {
                            Pokemon pokemonNextEvolution = await _apiRequest.GetPokemonAsync(evolutionChain.Chain.EvolvesTo[0].EvolvesTo[0].Species.Name);
                            PokemonScreen pokemonScreen = new(pokemonNextEvolution);
                            Destroy();
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
                            Destroy();
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
                                Destroy();
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
                await Task.Run(() => GetPokemonEvolutionChain(pokeSpecies.EvolutionChain.Url)).ConfigureAwait(false);

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

                foreach (var ability in pokemon.Abilities)
                {
                    Ability abillity = await _apiRequest.GetPokemonAbility(ability.Ability.Name);
                    pokeAbility.Add(abillity);
                }

                for (int i = 0; i < pokeAbility.Count; i++)
                {
                    string abilityName = pokeAbility[i].Name;

                    if (i == 0 && pokemonAbilityOne == abilityName && pokemonAbilityOne != null)
                    {
                        lblPokemonAbilityOne.TooltipText = _apiRequest.Translate(pokeAbility[i].EffectEntries[1].Effect); ;
                    }
                    else if (i == 1 && pokemonAbilityTwo == abilityName && pokemonAbilityTwo != null)
                    {
                        lblPokemonAbilityTwo.TooltipText = _apiRequest.Translate(pokeAbility[i].EffectEntries[1].Effect);
                    }
                    else if (i == 2 && pokemonAbilityThree == abilityName && pokemonAbilityThree != null)
                    {
                        lblPokemonAbilityThree.TooltipText = _apiRequest.Translate(pokeAbility[i].EffectEntries[1].Effect);
                    }
                    else if (i == 3 && pokemonAbilityFour == abilityName && pokemonAbilityFour != null)
                    {
                        lblPokemonAbilityFour.TooltipText = _apiRequest.Translate(pokeAbility[i].EffectEntries[1].Effect);
                    }
                    else
                    {
                        lblPokemonAbilityOne.TooltipText = null;
                        lblPokemonAbilityTwo.TooltipText = null;
                        lblPokemonAbilityThree.TooltipText = null;
                        lblPokemonAbilityFour.TooltipText = null;
                    }
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

        private async Task GetPokemonEvolutionChain(string url)
        {
            try
            {
                evolutionChain = await _apiRequest.GetEvolutionChain(url);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar os dados do Pokémon: {ex.Message}");
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