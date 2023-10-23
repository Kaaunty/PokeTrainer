using Gdk;
using Gtk;
using NowComesGtk.Reusable_components;
using NowComesGtk.Utils;
using Pango;
using PokeApi.BackEnd.Service;
using PokeApiNet;
using System.Globalization;
using Type = PokeApiNet.Type;

namespace NowComesGtk.Screens
{
    public class PokemonScreen : BaseWindow
    {
#nullable disable

        private Pokemon pokemon;
        private Button ShinyButton;

        private Image megaIcon = new("Images/pokemon_forms/MegaKeyDesactivated.png");
        private PokemonForm pokeForm = new();
        private PokemonSpecies pokeSpecies = new();
        private Type pokemonTypePrimary = new();
        private Type pokemonTypeSecondary = new();
        private static ApiRequest _apiRequest = new();
        private TextInfo textInfo = new CultureInfo("pt-BR", false).TextInfo;
        private Image imagePokemonTypeSecondary = new();
        private Image PokemonAnimation = new();
        private Image PokemonTypeOne = new();
        private Image megaKey = new();

        private List<Ability> pokeAbility = new();
        private CssProvider cssProvider = new();
        private Fixed fix = new();
        private EvolutionChain evolutionChain = new();

        #region Labels

        private Label lblPokemonName = new();
        private Label lblPokemonDexNumber = new();
        private Label lblPokemonAbilityOne = new();
        private string lblPokemonAbilityOneToolTip = "";
        private Label lblPokemonAbilityTwo = new();
        private string lblPokemonAbilityTwoToolTip = "";
        private Label lblPokemonAbilityThree = new();
        private string lblPokemonAbilityThreeToolTip = "";
        private Label lblPokemonAbilityFour = new();
        private string lblPokemonAbilityFourToolTip = "";
        private Label lblPokemonHP = new();
        private Label lblPokemonATK = new();
        private Label lblPokemonDEF = new();
        private Label lblPokemonSpATK = new();
        private Label lblPokemonSpDEF = new();
        private Label lblPokemonSpeed = new();
        private Image shinyButtonActivedImage = new Image("Images/buttons/shinyButtonActived.png");
        private Image shinyButtonDesactivedImage = new Image("Images/buttons/shinyButtonDesactived.png");
        private Image FormDesactivated = new Image("Images/pokemon_forms/FormDesactivated.png");
        private Image gMaxIcon = new Image("Images/pokemon_forms/GigaMaxDesactived.png");

        #endregion Labels

        private bool isLoaded = false;
        private bool isShiny = false;
        private int variationId = 0;
        private int pokemonFormId = 0;
        private string pokemonHPFormatted, pokemonATKFormatted, pokemonDEFFormatted, pokemonSpATKFormatted, pokemonSpDEFFormatted, pokemonSpeedFormatted;
        private string pokemonNameFormatted, pokemonDexFormatted, pokemonMaleFormatted, pokemonFemaleFormatted, pokemonCatchRate, pokemonEggGroup;
        private string pokemonAbilityOneUpper, pokemonAbilityTwoUpper, pokemonAbilityThreeUpper, pokemonAbilityFourUpper;
        private string PokemonFirstTypeFormattedTitle, PokemonFirstTypeFormatted, pokemonSecondaryTypeFormatted, damageRelations, damageRelationsSecondary;

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
                Image Background = new Image($"Images/pokemon_homescreen/{pokemon.Types[0].Type.Name}.png");

                fix.Put(Background, 0, 0);

                var fontDescription = Pango.FontDescription.FromString("Pixeloid Mono Regular 12");
                ModifyFont(fontDescription);

                PokemonFirstTypeFormattedTitle = (textInfo.ToTitleCase(PokemonFirstTypeFormatted));

                Title = $"PokéTrainer© // Pokémon tipo - {PokemonFirstTypeFormattedTitle} // Pokémon - {pokemonNameFormatted} [{pokemonDexFormatted}]";

                megaKey.Pixbuf = new Pixbuf("Images/pokemon_forms/MegaKeyDesactivated.png");

                PokemonAnimation.PixbufAnimation = new PixbufAnimation("Images/PokemonAnimated.gif");

                GetPokemonGifSize();

                lblPokemonName.Text = pokemonNameFormatted;
                lblPokemonAbilityOneToolTip = "";

                TranslateString();

                lblPokemonName.TooltipMarkup = lblPokemonAbilityOneToolTip;
                fix.Put(lblPokemonName, 40, 357);

                PokemonTypeOne = new Image($"Images/pokemon_types/{pokemon.Types[0].Type.Name}.png");
                damageRelations = _apiRequest.GetTypeDamageRelation(pokemon.Types[0].Type.Name);
                PokemonTypeOne.TooltipMarkup = $"<span foreground='white' font_desc='Pixeloid Mono Regular 12'> {damageRelations}</span>";
                fix.Put(PokemonTypeOne, 93, 429);

                if (pokemon.Types.Count > 1)
                {
                    imagePokemonTypeSecondary = new Image($"Images/pokemon_types/{pokemon.Types[1].Type.Name}.png");
                    damageRelationsSecondary = _apiRequest.GetTypeDamageRelation(pokemon.Types[1].Type.Name);
                    imagePokemonTypeSecondary.TooltipMarkup = $"<span foreground='white' font_desc='Pixeloid Mono Regular 12'>{damageRelationsSecondary}</span>";

                    fix.Put(imagePokemonTypeSecondary, 181, 429);
                }

                lblPokemonAbilityOne.Text = pokemonAbilityOneUpper;
                fix.Put(lblPokemonAbilityOne, 375, 63);

                if (pokemon.Abilities.Count == 2 && !String.IsNullOrEmpty(pokemon.Abilities[1].Ability.Name))
                {
                    lblPokemonAbilityTwo.Text = pokemonAbilityTwoUpper;
                    fix.Put(lblPokemonAbilityTwo, 375, 90);
                }

                if (pokemon.Abilities.Count == 3 && !String.IsNullOrEmpty(pokemon.Abilities[2].Ability.Name))
                {
                    lblPokemonAbilityThree.Text = pokemonAbilityThreeUpper;
                    fix.Put(lblPokemonAbilityThree, 585, 63);
                }

                if (pokemon.Abilities.Count == 4 && !String.IsNullOrEmpty(pokemon.Abilities[3].Ability.Name))
                {
                    lblPokemonAbilityFour.Text = pokemonAbilityFourUpper;
                    fix.Put(lblPokemonAbilityFour, 585, 90);
                }

                #region Labels de Status e Informações

                Label lblPokemonMale = new Label(pokemonMaleFormatted);
                fix.Put(lblPokemonMale, 377, 225);
                Label lblPokemnFemale = new Label(pokemonFemaleFormatted);
                fix.Put(lblPokemnFemale, 487, 225);
                Label lblPokemonCatchRate = new Label(pokemonCatchRate);
                fix.Put(lblPokemonCatchRate, 490, 431);
                Label lblPokemonEggGroup = new Label(pokemonEggGroup);
                fix.Put(lblPokemonEggGroup, 668, 195);
                lblPokemonEggGroup.SetAlignment(0.5f, 0.5f);

                lblPokemonHP.Text = pokemonHPFormatted;
                fix.Put(lblPokemonHP, 373, 327);
                lblPokemonATK.Text = pokemonATKFormatted;
                fix.Put(lblPokemonATK, 440, 327);
                lblPokemonDEF.Text = pokemonDEFFormatted;
                fix.Put(lblPokemonDEF, 509, 327);
                lblPokemonSpATK.Text = pokemonSpATKFormatted;
                fix.Put(lblPokemonSpATK, 576, 327);
                lblPokemonSpDEF.Text = pokemonSpDEFFormatted;
                fix.Put(lblPokemonSpDEF, 644, 327);
                lblPokemonSpeed.Text = pokemonSpeedFormatted;
                fix.Put(lblPokemonSpeed, 712, 327);

                #endregion Labels de Status e Informações

                if (pokeForm.IsMega)
                {
                    megaIcon.Pixbuf = new Pixbuf("Images/pokemon_forms/MegaKeyActivated.png");
                }

                if (pokeSpecies.IsLegendary)
                {
                    FormDesactivated.Pixbuf = new Pixbuf("Images/pokemon_forms/LegendaryIcon.png");
                }
                else if (pokeSpecies.IsMythical)
                {
                    FormDesactivated.Pixbuf = new Pixbuf("Images/pokemon_forms/MythicalIcon.png");
                }

                fix.Put(FormDesactivated, 61, 35);
                fix.Put(megaIcon, 36, 35);
                fix.Put(gMaxIcon, 85, 35);

                #region Buttons UI

                Button PreviousForm = new ButtonGenerator("Images/buttons/BackForm.png", 40, 40);
                fix.Put(PreviousForm, 185, 30);
                PreviousForm.Clicked += GetPreviousVariation;

                Button NextForm = new ButtonGenerator("Images/buttons/NextForm.png", 40, 40);
                fix.Put(NextForm, 235, 30);
                NextForm.Clicked += GetNextVariation;

                Button PreviousEvolution = new ButtonGenerator("Images/buttons/backEvolution.png", 40, 40);
                fix.Put(PreviousEvolution, 23, 270);
                PreviousEvolution.Clicked += GetPreviousEvolutionPokemon;

                Button NextEvolution = new ButtonGenerator("Images/buttons/NextEvolution.png", 40, 40);
                fix.Put(NextEvolution, 248, 270);
                NextEvolution.Clicked += GetNextEvolutionPokemon;

                ShinyButton = new ButtonGenerator("Images/buttons/shinyButtonDesactived.png", 40, 40);
                fix.Put(ShinyButton, 138, 270);
                ShinyButton.Clicked += ShinyButtonClicked;
                Button btnMoves = new ButtonGenerator("Images/buttons/btnMoves.png", 192, 85);
                btnMoves.TooltipMarkup = "<span foreground='white' font_desc='MS Gothic Regular 10'>[Clique para ver os movimentos do Pokémon]</span>";
                fix.Put(btnMoves, 580, 393);
                btnMoves.Clicked += PokemonMoves;

                #endregion Buttons UI

                Add(fix);

                ShowAll();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar os dados do Pokémon: {ex.Message}");
            }
        }

        private async void TranslateString()
        {
            try
            {
                if (pokemon.Abilities.Count == 1)
                {
                    lblPokemonAbilityOneToolTip = await _apiRequest.Translate(pokeAbility[0].EffectEntries[1].Effect);
                    lblPokemonAbilityOne.TooltipMarkup = $"<span foreground='white' font_desc='MS Gothic Regular 15'>[{lblPokemonAbilityOneToolTip}]</span>";
                }
                else
                {
                    lblPokemonAbilityOne.TooltipMarkup = $"<span foreground='white' font_desc='MS Gothic Regular 15'>[Sem Descrição]</span>";
                }
                if (pokemon.Abilities.Count == 2)
                {
                    if (pokeAbility[0].EffectEntries.Count > 0)
                    {
                        lblPokemonAbilityOneToolTip = await _apiRequest.Translate(pokeAbility[0].EffectEntries[1].Effect);
                        lblPokemonAbilityOne.TooltipMarkup = $"<span foreground='white' font_desc='MS Gothic Regular 15'>[{lblPokemonAbilityOneToolTip}]</span>";
                    }
                    else
                    {
                        lblPokemonAbilityOne.TooltipMarkup = $"<span foreground='white' font_desc='MS Gothic Regular 15'>[Sem Descrição]</span>";
                    }
                    if (pokeAbility[1].EffectEntries.Count > 0)
                    {
                        lblPokemonAbilityTwoToolTip = await _apiRequest.Translate(pokeAbility[1].EffectEntries[1].Effect);
                        lblPokemonAbilityTwo.TooltipMarkup = $"<span foreground='white' font_desc='MS Gothic Regular 15'>[{lblPokemonAbilityTwoToolTip}]</span>";
                    }
                    else
                    {
                        lblPokemonAbilityTwo.TooltipMarkup = $"<span foreground='white' font_desc='MS Gothic Regular 15'>[Sem Descrição]</span>";
                    }
                    if (pokemon.Abilities.Count == 3)
                    {
                        if (pokeAbility[0].EffectEntries.Count > 0)
                        {
                            lblPokemonAbilityOneToolTip = await _apiRequest.Translate(pokeAbility[0].EffectEntries[1].Effect);
                            lblPokemonAbilityOne.TooltipMarkup = $"<span foreground='white' font_desc='MS Gothic Regular 15'>[{lblPokemonAbilityOneToolTip}]</span>";
                        }
                        else
                        {
                            lblPokemonAbilityOne.TooltipMarkup = $"<span foreground='white' font_desc='MS Gothic Regular 15'>[Sem Descrição]</span>";
                        }
                        if (pokeAbility[1].EffectEntries.Count > 0)
                        {
                            lblPokemonAbilityTwoToolTip = await _apiRequest.Translate(pokeAbility[1].EffectEntries[1].Effect);
                            lblPokemonAbilityTwo.TooltipMarkup = $"<span foreground='white' font_desc='MS Gothic Regular 15'>[{lblPokemonAbilityTwoToolTip}]</span>";
                        }
                        else
                        {
                            lblPokemonAbilityTwo.TooltipMarkup = $"<span foreground='white' font_desc='MS Gothic Regular 15'>[Sem Descrição]</span>";
                        }
                        if (pokeAbility[2].EffectEntries.Count > 0)
                        {
                            lblPokemonAbilityThreeToolTip = await _apiRequest.Translate(pokeAbility[2].EffectEntries[1].Effect);
                            lblPokemonAbilityThree.TooltipMarkup = $"<span foreground='white' font_desc='MS Gothic Regular 15'>[{lblPokemonAbilityThreeToolTip}]</span>";
                        }
                        else
                        {
                            lblPokemonAbilityThree.TooltipMarkup = $"<span foreground='white' font_desc='MS Gothic Regular 15'>[Sem Descrição]</span>";
                        }
                    }
                    if (pokemon.Abilities.Count == 4)
                    {
                        if (pokeAbility[0].EffectEntries.Count > 0)
                        {
                            lblPokemonAbilityOneToolTip = await _apiRequest.Translate(pokeAbility[0].EffectEntries[1].Effect);
                            lblPokemonAbilityOne.TooltipMarkup = $"<span foreground='white' font_desc='MS Gothic Regular 15'>[{lblPokemonAbilityOneToolTip}]</span>";
                        }
                        else
                        {
                            lblPokemonAbilityOne.TooltipMarkup = $"<span foreground='white' font_desc='MS Gothic Regular 15'>[Sem Descrição]</span>";
                        }
                        if (pokeAbility[1].EffectEntries.Count > 0)
                        {
                            lblPokemonAbilityTwoToolTip = await _apiRequest.Translate(pokeAbility[1].EffectEntries[1].Effect);
                            lblPokemonAbilityTwo.TooltipMarkup = $"<span foreground='white' font_desc='MS Gothic Regular 15'>[{lblPokemonAbilityTwoToolTip}]</span>";
                        }
                        else
                        {
                            lblPokemonAbilityTwo.TooltipMarkup = $"<span foreground='white' font_desc='MS Gothic Regular 15'>[Sem Descrição]</span>";
                        }
                        if (pokeAbility[2].EffectEntries.Count > 0)
                        {
                            lblPokemonAbilityThreeToolTip = await _apiRequest.Translate(pokeAbility[2].EffectEntries[1].Effect);
                            lblPokemonAbilityThree.TooltipMarkup = $"<span foreground='white' font_desc='MS Gothic Regular 15'>[{lblPokemonAbilityThreeToolTip}]</span>";
                        }
                        else
                        {
                            lblPokemonAbilityThree.TooltipMarkup = $"<span foreground='white' font_desc='MS Gothic Regular 15'>[Sem Descrição]</span>";
                        }

                        if (pokeAbility[3].EffectEntries.Count > 0)
                        {
                            lblPokemonAbilityFourToolTip = await _apiRequest.Translate(pokeAbility[3].EffectEntries[1].Effect);
                            lblPokemonAbilityFour.TooltipMarkup = $"<span foreground='white' font_desc='MS Gothic Regular 15'>[{lblPokemonAbilityFourToolTip}]</span>";
                        }
                        else
                        {
                            lblPokemonAbilityFour.TooltipMarkup = $"<span foreground='white' font_desc='MS Gothic Regular 15'>[Sem Descrição]</span>";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao traduzir a descrição da habilidade {pokeAbility[0].Name}: {ex.Message}");
            }
        }

        private async void GetNextEvolutionPokemon(object sender, EventArgs e)
        {
            if (evolutionChain != null)
            {
                var nextEvolution = GetNextEvolution(evolutionChain, pokemon.Name);
                if (nextEvolution != null && nextEvolution != "")
                {
                    pokemon = await _apiRequest.GetPokemonAsync(nextEvolution);
                    await PopulateFields();
                    GetPokemonGifSize();
                    UpdateLabels();
                }
                else
                {
                    MessageDialogGenerator.ShowMessageDialog("Não há proximas evoluções para este Pokémon.");
                }
            }
            else
            {
                Console.WriteLine($"Não foi possível encontrar informações de evolução para {pokemon.Name}.");
            }
        }

        private async void GetPreviousEvolutionPokemon(object sender, EventArgs e)
        {
            if (evolutionChain != null)
            {
                var previousEvolution = GetPreviousEvolution(evolutionChain, pokemon.Name);
                if (previousEvolution != null && previousEvolution != "")
                {
                    pokemon = await _apiRequest.GetPokemonAsync(previousEvolution);
                    await PopulateFields();
                    GetPokemonGifSize();
                    UpdateLabels();
                }
                else
                {
                    MessageDialogGenerator.ShowMessageDialog("Não há evoluções anteriores para este Pokémon.");
                }
            }
        }

        private async void ShinyButtonClicked(object sender, EventArgs e)
        {
            if (isShiny)
            {
                ShinyButton.Image = shinyButtonDesactivedImage;

                isShiny = false;
                if (pokemon.Forms.Count > 1)
                {
                    await _apiRequest.GetPokemonAnimatedSprite(pokemon.Forms[pokemonFormId].Name);
                }
                else
                {
                    await _apiRequest.GetPokemonAnimatedSprite(pokemon.Name);
                }
                PokemonAnimation.PixbufAnimation = new PixbufAnimation("Images/PokemonAnimated.gif");
            }
            else
            {
                ShinyButton.Image = shinyButtonActivedImage;
                isShiny = true;
                if (pokemon.Forms.Count > 1)
                {
                    await _apiRequest.GetPokemonShinyAnimatedSprite(pokemon.Forms[pokemonFormId].Name);
                }
                else
                {
                    await _apiRequest.GetPokemonShinyAnimatedSprite(pokemon.Name);
                }
                PokemonAnimation.PixbufAnimation = new PixbufAnimation("Images/PokemonAnimatedShiny.gif");
            }
        }

        private async void PokemonMoves(object sender, EventArgs e)
        {
            Dictionary<string, string> pokemonMoves2 = new();
            int id = 0;
            try
            {
                foreach (var pokemonMove in pokemon.Moves)
                {
                    pokemonMoves2.Add(pokemonMove.Move.Name, pokemonMove.VersionGroupDetails[0].MoveLearnMethod.Name);
                    id++;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar os movimentos do Pokémon: {ex.Message} erro no movimento de id: {id}");
            }

            List<Move> pokemonMoves = await _apiRequest.GetMoveLearnedByPokemon(pokemon);

            var t = pokemon.Moves.Select(x => x.VersionGroupDetails.FirstOrDefault(y => y.MoveLearnMethod.Name == "tutor"));

            MovementScreen movementScreen = new(pokemonMoves);
            movementScreen.ShowAll();

            //List<PokemonMoveVersion> test = new();

            //foreach (Move move in pokemonMoves)
            //{
            //    pokemon.Moves.ForEach(pMove =>
            //    {
            //        if (pMove.VersionGroupDetails.FirstOrDefault(x => x.MoveLearnMethod.Name == "tutor") != null)
            //        {
            //        }
            //    });
            //}
        }

        public static string GetNextEvolution(EvolutionChain evolutionChain, string currentPokemonName)
        {
            var primaryevolution = evolutionChain.Chain.Species.Name;

            if (primaryevolution == currentPokemonName)
            {
                var secondaryEvolution = evolutionChain.Chain.EvolvesTo.FirstOrDefault()?.Species.Name;
                return secondaryEvolution;
            }
            if (evolutionChain.Chain.EvolvesTo.FirstOrDefault()?.Species.Name == currentPokemonName)
            {
                var thirdEvolution = evolutionChain.Chain.EvolvesTo.FirstOrDefault()?.EvolvesTo.FirstOrDefault()?.Species.Name;
                return thirdEvolution;
            }
            return "";
        }

        private string GetPreviousEvolution(EvolutionChain evolutionChain, string name)
        {
            var primaryevolution = evolutionChain.Chain.Species.Name;
            if (primaryevolution == name)
            {
                return "";
            }
            if (evolutionChain.Chain.EvolvesTo.FirstOrDefault()?.Species.Name == name)
            {
                return primaryevolution;
            }
            if (evolutionChain.Chain.EvolvesTo.FirstOrDefault()?.EvolvesTo.FirstOrDefault()?.Species.Name == null)
            {
                return "";
            }
            if (evolutionChain.Chain.EvolvesTo.FirstOrDefault()?.EvolvesTo.FirstOrDefault()?.Species.Name == name)
            {
                var thirdevolution = evolutionChain.Chain.EvolvesTo.FirstOrDefault()?.Species.Name;
                return thirdevolution;
            }
            return "";
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

                await Task.Run(() => GetPokemonSpecies(pokemon.Species.Name)).ConfigureAwait(false);
                if (pokeSpecies != null)
                {
                    evolutionChain = await _apiRequest.GetEvolutionChain(pokeSpecies.EvolutionChain.Url);
                }

                await UpdatePokemonSprite(pokemon.Name);

                pokeForm = await _apiRequest.GetPokemonForm(pokemon.Forms[0].Name);

                pokemonDexFormatted = "#" + pokeSpecies.Id.ToString("D4");

                foreach (var abilities in pokemon.Abilities)
                {
                    await Task.Run(() => GetPokemonAbilitiesList(abilities.Ability.Name)).ConfigureAwait(false);
                }

                if (pokemon.Abilities.Count == 1)
                {
                    pokemonAbilityOneUpper = $"<{textInfo.ToTitleCase(pokemon.Abilities[0].Ability.Name)}>";
                }
                else if (pokemon.Abilities.Count == 2)
                {
                    pokemonAbilityOneUpper = $"<{textInfo.ToTitleCase(pokemon.Abilities[0].Ability.Name)}>";
                    pokemonAbilityTwoUpper = $"<{textInfo.ToTitleCase(pokemon.Abilities[1].Ability.Name)}>";
                }
                else if (pokemon.Abilities.Count == 3)
                {
                    pokemonAbilityOneUpper = $"<{textInfo.ToTitleCase(pokemon.Abilities[0].Ability.Name)}>";
                    pokemonAbilityTwoUpper = $"<{textInfo.ToTitleCase(pokemon.Abilities[1].Ability.Name)}>";
                    pokemonAbilityThreeUpper = $"<{textInfo.ToTitleCase(pokemon.Abilities[2].Ability.Name)}>";
                }
                else if (pokemon.Abilities.Count == 4)
                {
                    pokemonAbilityOneUpper = $"<{textInfo.ToTitleCase(pokemon.Abilities[0].Ability.Name)}>";
                    pokemonAbilityTwoUpper = $"<{textInfo.ToTitleCase(pokemon.Abilities[1].Ability.Name)}>";
                    pokemonAbilityThreeUpper = $"<{textInfo.ToTitleCase(pokemon.Abilities[2].Ability.Name)}>";
                    pokemonAbilityFourUpper = $"<{textInfo.ToTitleCase(pokemon.Abilities[3].Ability.Name)}>";
                }

                if (pokemon.Types.Count == 1)
                {
                    PokemonFirstTypeFormatted = pokemon.Types[0].Type.Name;
                    pokemonTypePrimary = await _apiRequest.GetTypeAsync(PokemonFirstTypeFormatted);
                    pokemonSecondaryTypeFormatted = "";
                }
                else
                {
                    PokemonFirstTypeFormatted = pokemon.Types[0].Type.Name;
                    pokemonTypePrimary = await _apiRequest.GetTypeAsync(PokemonFirstTypeFormatted);
                    pokemonSecondaryTypeFormatted = pokemon.Types[1].Type.Name;
                    pokemonTypeSecondary = await _apiRequest.GetTypeAsync(pokemonSecondaryTypeFormatted);
                }

                foreach (var eggGroup in pokeSpecies.EggGroups)
                {
                    pokemonEggGroup += eggGroup.Name + "\n";
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
            if (pokemon.Forms.Count > 1)
            {
                pokemonFormId += 1;
                if (pokemonFormId != 0 && pokemonFormId < pokemon.Forms.Count)
                {
                    string nextPokemon = pokemon.Forms[pokemonFormId].Name;
                    pokeForm = await _apiRequest.GetPokemonForm(nextPokemon);
                    if (VerifyType(nextPokemon))
                    {
                        pokemonTypePrimary = await _apiRequest.GetTypeAsync(pokeForm.Name);
                        UpdateLabels();
                    }
                    await UpdatePokemonSprite(pokeForm.Name);
                    GetPokemonGifSize();
                }
                else if (pokemonFormId >= pokemon.Forms.Count)
                {
                    MessageDialogGenerator.ShowMessageDialog("Não há proximas variações para este Pokémon.");
                    pokemonFormId--;
                }
            }
            else if (pokemon.Forms.Count <= 1)
            {
                variationId += 1;
                if (variationId != 0 && variationId < pokeSpecies.Varieties.Count && variationId > -1)
                {
                    pokeAbility.Clear();
                    string nextPokemon = pokeSpecies.Varieties[variationId].Pokemon.Name;
                    pokemon = await _apiRequest.GetPokemonAsync(nextPokemon);
                    await PopulateFields();
                    GetPokemonGifSize();
                    UpdateLabels();
                }
                else
                {
                    MessageDialogGenerator.ShowMessageDialog("Não há proximas variações para este Pokémon.");
                    variationId -= 1;
                }
            }
        }

        private async void GetPreviousVariation(object sender, EventArgs e)
        {
            if (pokemon.Forms.Count > 1)
            {
                pokemonFormId -= 1;
                if (pokemonFormId < pokemon.Forms.Count && pokemonFormId > -1)
                {
                    string previousPokemon = pokemon.Forms[pokemonFormId].Name;
                    pokeForm = await _apiRequest.GetPokemonForm(previousPokemon);
                    if (VerifyType(previousPokemon))
                    {
                        pokemonTypePrimary = await _apiRequest.GetTypeAsync(pokeForm.Name);
                        UpdateLabels();
                    }

                    await UpdatePokemonSprite(pokeForm.Name);
                    GetPokemonGifSize();
                }
                else
                {
                    MessageDialogGenerator.ShowMessageDialog("Não há proximas formas para este Pokémon.");
                }
                if (pokemonFormId < 0)
                {
                    pokemonFormId = 0;
                }
            }
            else
            {
                variationId -= 1;
                if (variationId < pokeSpecies.Varieties.Count && variationId > -1)
                {
                    pokeAbility.Clear();
                    string previousPokemon = pokeSpecies.Varieties[variationId].Pokemon.Name;
                    pokemon = await _apiRequest.GetPokemonAsync(previousPokemon);
                    await PopulateFields();
                    GetPokemonGifSize();
                    UpdateLabels();
                }
                else
                {
                    MessageDialogGenerator.ShowMessageDialog("Não há proximas variações para este Pokémon.");
                }
                if (variationId < 0)
                {
                    variationId = 0;
                }
            }
        }

        private async Task GetPokemonAbilitiesList(string abilityName)
        {
            Ability ability = await _apiRequest.GetPokemonAbility(abilityName);
            pokeAbility?.Add(ability);
        }

        private async Task GetPokemonSpecies(string PokemonName)
        {
            try
            {
                pokeSpecies = await _apiRequest.GetPokemonSpecies(PokemonName);
                if (pokeSpecies != null)
                {
                    pokemonFemaleFormatted = pokeSpecies.GenderRate switch
                    {
                        -1 => "????",
                        0 => "0,00%",
                        1 => "12,5%",
                        2 => "25,0%",
                        4 => "50,0%",
                        6 => "75,0%",
                        7 => "87,5%",
                        8 => "100%",
                        _ => pokemonFemaleFormatted
                    };

                    pokemonMaleFormatted = pokeSpecies.GenderRate switch
                    {
                        -1 => "????",
                        0 => "100%",
                        1 => "87,5%",
                        2 => "75,0%",
                        4 => "50,0%",
                        6 => "25,0%",
                        7 => "12,5%",
                        8 => "0,00%",
                        _ => pokemonMaleFormatted
                    };

                    pokemonCatchRate = pokeSpecies.CaptureRate switch
                    {
                        3 => "01.6%",
                        45 => "11.9%",
                        100 => "21.7%",
                        120 => "24.9%",
                        127 => "26,0%",
                        150 => "29.5%",
                        160 => "30.9%",
                        190 => "35.2%",
                        220 => "39.3%",
                        225 => "39.9%",
                        255 => "43.9%",
                        _ => pokemonCatchRate
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar os dados do Pokémon: {ex.Message}");
            }
        }

        private async Task UpdatePokemonSprite(string pokemonForm)
        {
            try
            {
                if (!isShiny)
                {
                    await _apiRequest.GetPokemonAnimatedSprite(pokemonForm);
                    PokemonAnimation.PixbufAnimation = new PixbufAnimation("Images/PokemonAnimated.gif");
                }
                else
                {
                    await _apiRequest.GetPokemonShinyAnimatedSprite(pokemonForm);
                    PokemonAnimation.PixbufAnimation = new PixbufAnimation("Images/PokemonAnimatedShiny.gif");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao atualizar a imagem do Pokémon: {ex.Message}");
            }
        }

        private void GetPokemonGifSize()
        {
            fix.Remove(PokemonAnimation);
            if (!isShiny)
                PokemonAnimation.PixbufAnimation = new PixbufAnimation("Images/PokemonAnimated.gif");
            else
                PokemonAnimation.PixbufAnimation = new PixbufAnimation("Images/PokemonAnimatedShiny.gif");

            int x = (340 - PokemonAnimation.PixbufAnimation.Width) / 2;
            int y = (380 - PokemonAnimation.PixbufAnimation.Height) / 2;

            fix.Put(PokemonAnimation, x, y);
        }

        private async void UpdateLabels()
        {
            lblPokemonDexNumber.Text = pokemonDexFormatted;
            lblPokemonName.Text = pokemonNameFormatted;
            PokemonFirstTypeFormattedTitle = (textInfo.ToTitleCase(PokemonFirstTypeFormatted));

            lblPokemonAbilityOne.Text = pokemonAbilityOneUpper;

            if (pokeAbility[0].EffectEntries.Count > 0)
            {
                lblPokemonAbilityOne.TooltipMarkup = $"<span foreground='white' font_desc='MS Gothic Regular 15'>[{await _apiRequest.Translate(pokeAbility[0].EffectEntries[1].Effect)}]</span>";
            }
            Title = $"PokéTrainer© // Pokémon tipo - {PokemonFirstTypeFormattedTitle} // Pokémon - {pokemonNameFormatted} [{pokemonDexFormatted}]";
            if (pokemon.Abilities.Count == 2)
            {
                lblPokemonAbilityTwo.Text = pokemonAbilityTwoUpper;
                if (pokeAbility[1].EffectEntries.Count > 0)
                {
                    lblPokemonAbilityTwo.TooltipMarkup = $"<span foreground='white' font_desc='MS Gothic Regular 15'>[{await _apiRequest.Translate(pokeAbility[1].EffectEntries[1].Effect)}]</span>";
                }
            }
            else if (pokemon.Abilities.Count == 3)
            {
                lblPokemonAbilityTwo.Text = pokemonAbilityTwoUpper;
                if (pokeAbility[1].EffectEntries.Count > 0)
                {
                    lblPokemonAbilityTwo.TooltipMarkup = $"<span foreground='white' font_desc='MS Gothic Regular 15'>[{await _apiRequest.Translate(pokeAbility[1].EffectEntries[1].Effect)}]</span>";
                }
                lblPokemonAbilityThree.Text = pokemonAbilityThreeUpper;
                if (pokeAbility[2].EffectEntries.Count > 0)
                {
                    lblPokemonAbilityThree.TooltipMarkup = $"<span foreground='white' font_desc='MS Gothic Regular 15'>[{await _apiRequest.Translate(pokeAbility[2].EffectEntries[1].Effect)}]</span>";
                }
            }
            else if (pokemon.Abilities.Count == 4)
            {
                lblPokemonAbilityTwo.Text = pokemonAbilityTwoUpper;
                if (pokeAbility[1].EffectEntries.Count > 0)
                {
                    lblPokemonAbilityTwo.TooltipMarkup = $"<span foreground='white' font_desc='MS Gothic Regular 15'>[{await _apiRequest.Translate(pokeAbility[1].EffectEntries[1].Effect)}]</span>";
                }
                lblPokemonAbilityThree.Text = pokemonAbilityThreeUpper;
                if (pokeAbility[2].EffectEntries.Count > 0)
                {
                    lblPokemonAbilityThree.TooltipMarkup = $"<span foreground='white' font_desc='MS Gothic Regular 15'>[{await _apiRequest.Translate(pokeAbility[2].EffectEntries[1].Effect)}]</span>";
                }
                lblPokemonAbilityFour.Text = pokemonAbilityFourUpper;
                if (pokeAbility[3].EffectEntries.Count > 0)
                {
                    lblPokemonAbilityFour.TooltipMarkup = $"<span foreground='white' font_desc='MS Gothic Regular 15'>[{await _apiRequest.Translate(pokeAbility[3].EffectEntries[1].Effect)}]</span>";
                }
            }

            if (PokemonFirstTypeFormatted != pokemonTypePrimary.Name)
            {
                PokemonFirstTypeFormatted = pokemonTypePrimary.Name;
                PokemonTypeOne.Pixbuf = new Pixbuf($"Images/pokemon_types/{PokemonFirstTypeFormatted}.png");
                damageRelations = _apiRequest.GetTypeDamageRelation(pokemonTypePrimary.Name);
                PokemonTypeOne.TooltipMarkup = $"<span foreground='white' font_desc='MS Gothic Regular 12'>{damageRelations}</span>";
            }

            if (pokemon.Types.Count > 1)
            {
                pokemonSecondaryTypeFormatted = pokemon.Types[1].Type.Name;
                imagePokemonTypeSecondary.Pixbuf = new Pixbuf($"Images/pokemon_types/{pokemonSecondaryTypeFormatted}.png");
                damageRelationsSecondary = _apiRequest.GetTypeDamageRelation(pokemonTypeSecondary.Name);
                imagePokemonTypeSecondary.TooltipMarkup = $"<span foreground='white' font_desc='MS Gothic Regular 12'>[ {damageRelationsSecondary} ]</span>";
            }

            if (pokeForm.IsMega)
            {
                megaIcon.Pixbuf = new Pixbuf("Images/pokemon_forms/MegaKeyActivated.png");
            }
            else
            {
                megaIcon.Pixbuf = new Pixbuf("Images/pokemon_forms/MegaKeyDesactivated.png");
            }
            if (pokeForm.FormName == "gmax")
            {
                gMaxIcon.Pixbuf = new Pixbuf("Images/pokemon_forms/GigaMaxActivated.png");
            }
            else
            {
                gMaxIcon.Pixbuf = new Pixbuf("Images/pokemon_forms/GigaMaxDesactived.png");
            }
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

        private bool VerifyType(string pokeForm)
        {
            if (pokeForm.Contains("bug"))
            {
                return true;
            }
            else if (pokeForm.Contains("dark"))
            {
                return true;
            }
            else if (pokeForm.Contains("dragon"))
            {
                return true;
            }
            else if (pokeForm.Contains("electric"))
            {
                return true;
            }
            else if (pokeForm.Contains("fairy"))
            {
                return true;
            }
            else if (pokeForm.Contains("fighting"))
            {
                return true;
            }
            else if (pokeForm.Contains("fire"))
            {
                return true;
            }
            else if (pokeForm.Contains("flying"))
            {
                return true;
            }
            else if (pokeForm.Contains("ghost"))
            {
                return true;
            }
            else if (pokeForm.Contains("grass"))
            {
                return true;
            }
            else if (pokeForm.Contains("ground"))
            {
                return true;
            }
            else if (pokeForm.Contains("ice"))
            {
                return true;
            }
            else if (pokeForm.Contains("normal"))
            {
                return true;
            }
            else if (pokeForm.Contains("poison"))
            {
                return true;
            }
            else if (pokeForm.Contains("psychic"))
            {
                return true;
            }
            else if (pokeForm.Contains("rock"))
            {
                return true;
            }
            else if (pokeForm.Contains("steel"))
            {
                return true;
            }
            else if (pokeForm.Contains("water"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}