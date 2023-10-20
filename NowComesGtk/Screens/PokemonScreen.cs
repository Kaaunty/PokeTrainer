﻿using NowComesGtk.Reusable_components;
using PokeApi.BackEnd.Service;
using System.Globalization;
using NowComesGtk.Utils;
using PokeApiNet;
using Gdk;
using Gtk;
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
        private List<EggGroup> pokeEggGroup = new();
        private CssProvider cssProvider = new();
        private Fixed fix = new();
        private EvolutionChain evolutionChain = new();

        #region Labels

        private Label lblPokemonName = new();
        private Label lblPokemonDexNumber = new();
        private Label lblPokemonAbilityOne = new();
        private Label lblPokemonAbilityTwo = new();
        private Label lblPokemonAbilityThree = new();
        private Label lblPokemonAbilityFour = new();
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

        private string pokemonHPFormatted, pokemonATKFormatted, pokemonDEFFormatted, pokemonSpATKFormatted, pokemonSpDEFFormatted, pokemonSpeedFormatted;
        private string pokemonNameFormatted, pokemonDexFormatted, pokemonMaleFormatted, pokemonFemaleFormatted, pokemonCatchRate, pokemonEggGroup;
        private string pokemonAbilityOneUpper, pokemonAbilityTwoUpper, pokemonAbilityThreeUpper, pokemonAbilityFourUpper;
        private string PokemonFirstTypeFormattedTitle, PokemonFirstTypeFormatted, pokemonSecondaryTypeFormatted;

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
                Image Background = new Image($"Images/pokemon_homescreen/{pokemon.Types[0].Type.Name}.png");
                fix.Put(Background, 0, 0);

                //fix.Put(Background, 0, 0);
                megaKey.Pixbuf = new Pixbuf("Images/pokemon_forms/MegaKeyDesactivated.png");
                PokemonAnimation.PixbufAnimation = new PixbufAnimation("Images/PokemonAnimated.gif");

                GetPokemonGifSize();
                lblPokemonName.Text = pokemonNameFormatted;
                lblPokemonName.TooltipMarkup = $"<span foreground='white' font_desc='Pixeloid Mono 15'>[ {textInfo.ToTitleCase(_apiRequest.Translate(pokeSpecies.FlavorTextEntries[0].FlavorText.ToLower()))} ]</span>";
                fix.Put(lblPokemonName, 40, 357);

                PokemonTypeOne = new Image($"Images/pokemon_types/{pokemon.Types[0].Type.Name}.png");
                string damageRelations = GetTypeDamageRelations(pokemonTypePrimary);
                PokemonTypeOne.TooltipMarkup = $"<span foreground='white' font_desc='MS Gothic Regular 12'>[ {damageRelations} ]</span>";
                fix.Put(PokemonTypeOne, 93, 429);

                if (pokemon.Types.Count > 1)
                {
                    imagePokemonTypeSecondary = new Image($"Images/pokemon_types/{pokemon.Types[1].Type.Name}.png");
                    string damageRelationsSecondary = GetTypeDamageRelations(pokemonTypeSecondary);
                    imagePokemonTypeSecondary.TooltipMarkup = $"<span foreground='white' font_desc='MS Gothic Regular 12'>[ {damageRelationsSecondary} ]</span>";

                    fix.Put(imagePokemonTypeSecondary, 181, 429);
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

                #region Labels de Status e Informações

                Label lblPokemonMale = new Label(pokemonMaleFormatted);
                fix.Put(lblPokemonMale, 377, 225);
                Label lblPokemnFemale = new Label(pokemonFemaleFormatted);
                fix.Put(lblPokemnFemale, 487, 225);
                Label lblPokemonCatchRate = new Label(pokemonCatchRate);
                fix.Put(lblPokemonCatchRate, 490, 431);
                Label lblPokemonEggGroup = new Label(pokemonEggGroup);
                fix.Put(lblPokemonEggGroup, 668, 195);
                lblPokemonEggGroup.SetAlignment(0.5f, 0.0f);
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
                    MessageDialogGenerator.ShowMessageDialog("Não há mais evoluções para este Pokémon.");
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
                    MessageDialogGenerator.ShowMessageDialog("Não há mais evoluções para este Pokémon.");
                }
            }
        }

        private async void ShinyButtonClicked(object sender, EventArgs e)
        {
            if (isShiny)
            {
                ShinyButton.Image = shinyButtonDesactivedImage;

                isShiny = false;
                await _apiRequest.GetPokemonAnimatedSprite(pokemon.Name);
                PokemonAnimation.PixbufAnimation = new PixbufAnimation("Images/PokemonAnimated.gif");
            }
            else
            {
                ShinyButton.Image = shinyButtonActivedImage;
                isShiny = true;
                await _apiRequest.GetPokemonShinyAnimatedSprite(pokemon.Name);
                PokemonAnimation.PixbufAnimation = new PixbufAnimation("Images/PokemonAnimatedShiny.gif");
            }
        }

        private async void PokemonMoves(object sender, EventArgs e)
        {
            List<Move> pokemonMoves = await _apiRequest.GetMoveLearnedByPokemon(pokemon);

            MovementScreen movementScreen = new(pokemonMoves);
            movementScreen.ShowAll();
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

        private string GetTypeDamageRelations(Type type)
        {
            
            try
            {
                List<string> damageRelationsList = new();
                string HalfDamageFrom = "";
                string HalfDamageTo = "";
                string DoubleDamageFrom = "";
                string DoubleDamageTo = "";
                string NoDamageFrom = "";
                string NoDamageTo = "";
                if (type != null)
                {
                    if (type.DamageRelations.HalfDamageFrom.Count > 0)
                    {
                        foreach (var halfDamageFrom in type.DamageRelations.HalfDamageFrom)
                        {
                            HalfDamageFrom += textInfo.ToTitleCase(halfDamageFrom.Name) + ",";
                        }
                        string removeLastComma = HalfDamageFrom.Remove(HalfDamageFrom.Length - 1);
                        HalfDamageFrom = removeLastComma;
                        damageRelationsList.Add(HalfDamageFrom);
                    }
                    if (string.IsNullOrEmpty(HalfDamageFrom))
                    {
                        HalfDamageFrom = "Nenhum";
                        damageRelationsList.Add(HalfDamageFrom);
                    }
                    if (type.DamageRelations.HalfDamageTo.Count > 0)
                    {
                        foreach (var halfDamageTo in type.DamageRelations.HalfDamageTo)
                        {
                            HalfDamageTo += textInfo.ToTitleCase(halfDamageTo.Name) + ",";
                        }
                        string removeLastComma = HalfDamageTo.Remove(HalfDamageTo.Length - 1);
                        HalfDamageTo = removeLastComma;
                        damageRelationsList.Add(HalfDamageTo);
                    }
                    if (string.IsNullOrEmpty(HalfDamageTo))
                    {
                        HalfDamageTo = "Nenhum";
                        damageRelationsList.Add(HalfDamageTo);
                    }

                    if (type.DamageRelations.DoubleDamageFrom.Count > 0)
                    {
                        foreach (var doubleDamageFrom in type.DamageRelations.DoubleDamageFrom)
                        {
                            DoubleDamageFrom += textInfo.ToTitleCase(doubleDamageFrom.Name) + ",";
                        }
                        string removeLastComma = DoubleDamageFrom.Remove(DoubleDamageFrom.Length - 1);
                        DoubleDamageFrom = removeLastComma;
                        damageRelationsList.Add(DoubleDamageFrom);
                    }
                    if (string.IsNullOrEmpty(DoubleDamageFrom))
                    {
                        DoubleDamageFrom = "Nenhum";
                        damageRelationsList.Add(DoubleDamageFrom);
                    }

                    if (type.DamageRelations.DoubleDamageTo.Count > 0)
                    {
                        foreach (var doubleDamageTo in type.DamageRelations.DoubleDamageTo)
                        {
                            DoubleDamageTo += textInfo.ToTitleCase(doubleDamageTo.Name) + ",";
                        }
                        string removeLastComma = DoubleDamageTo.Remove(DoubleDamageTo.Length - 1);
                        DoubleDamageTo = removeLastComma;
                        damageRelationsList.Add(DoubleDamageTo);
                    }
                    if (string.IsNullOrEmpty(DoubleDamageTo))
                    {
                        DoubleDamageTo = "Nenhum";
                        damageRelationsList.Add(DoubleDamageTo);
                    }

                    if (type.DamageRelations.NoDamageFrom.Count > 0)
                    {
                        foreach (var noDamageFrom in type.DamageRelations.NoDamageFrom)
                        {
                            NoDamageFrom += textInfo.ToTitleCase(noDamageFrom.Name) + ",";
                        }
                        string removeLastComma = NoDamageFrom.Remove(NoDamageFrom.Length - 1);
                        NoDamageFrom = removeLastComma;
                        damageRelationsList.Add(NoDamageFrom);
                    }
                    if (string.IsNullOrEmpty(NoDamageFrom))
                    {
                        NoDamageFrom = "Nenhum";
                        damageRelationsList.Add(NoDamageFrom);
                    }

                    if (type.DamageRelations.NoDamageTo.Count > 0)
                    {
                        foreach (var noDamageFrom in type.DamageRelations.NoDamageTo)
                        {
                            NoDamageTo += textInfo.ToTitleCase(noDamageFrom.Name) + ",";
                        }
                        string removeLastComma = NoDamageTo.Remove(NoDamageTo.Length - 1);
                        NoDamageTo = removeLastComma;
                        damageRelationsList.Add(NoDamageTo);
                    }
                    if (string.IsNullOrEmpty(NoDamageTo))
                    {
                        NoDamageTo = "Nenhum";
                        damageRelationsList.Add(NoDamageTo);
                    }

                    List<string> DamageRelationsListTranslated = new List<string>();
                    foreach (var damageRelation in damageRelationsList)
                    {
                        if (damageRelation != "Nenhum")
                        {
                            DamageRelationsListTranslated.Add(textInfo.ToTitleCase(_apiRequest.Translate(damageRelation)));
                        }
                        else
                        {
                            DamageRelationsListTranslated.Add(damageRelation);
                        }
                    }

                    string damageRelations = $"Dano Sofrido Pouco Efetivo: {DamageRelationsListTranslated[0]}\n Pouco Efetivo Contra: {DamageRelationsListTranslated[1]}\n Dano Sofrido Super Efetivo: {DamageRelationsListTranslated[2]}\n Super Efetivo Contra: {DamageRelationsListTranslated[3]}\n Imune: {DamageRelationsListTranslated[4]}\n Nenhum Dano a: {DamageRelationsListTranslated[5]}";
                    return damageRelations;
                }
                else
                {
                    return "Nenhum";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar as informaçoes do tipo:{type.Name} Erro: {ex.Message}");
                throw;
            }
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
                int pokemonMaxForms = 4;
                await Task.Run(() => GetPokemonSpecies(pokemon.Species.Name)).ConfigureAwait(false);
                evolutionChain = await _apiRequest.GetEvolutionChain(pokeSpecies.EvolutionChain.Url);

                await Task.Run(() => UpdatePokemonSprite()).ConfigureAwait(false);
                if (pokemon.Forms.Count < pokemonMaxForms)
                {
                    pokeForm = await _apiRequest.GetPokemonForm(pokemon.Name);
                }

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
                MessageDialogGenerator.ShowMessageDialog("Não há mais variações para este Pokémon.");
                variationId -= 1;
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
                await PopulateFields();
                GetPokemonGifSize();
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
                        150 => "29.5%",
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

        private async Task UpdatePokemonSprite()
        {
            try
            {
                if (!isShiny)
                {
                    await _apiRequest.GetPokemonAnimatedSprite(pokemon.Name);
                    PokemonAnimation.PixbufAnimation = new PixbufAnimation("Images/PokemonAnimated.gif");
                }
                else
                {
                    await _apiRequest.GetPokemonShinyAnimatedSprite(pokemon.Name);
                    PokemonAnimation.PixbufAnimation = new PixbufAnimation("Images/PokemonAnimatedShiny.gif");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar os dados do Pokémon: {ex.Message}");
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

        private void UpdateLabels()
        {
            lblPokemonDexNumber.Text = pokemonDexFormatted;
            lblPokemonName.Text = pokemonNameFormatted;
            lblPokemonAbilityOne.Text = pokemonAbilityOneUpper;
            PokemonFirstTypeFormattedTitle = _apiRequest.Translate(textInfo.ToTitleCase(PokemonFirstTypeFormatted));

            // lblPokemonAbilityOne.TooltipMarkup = $"<span foreground='white' font_desc='RetroPix 15'>[{_apiRequest.Translate(pokeAbility[0].EffectEntries[1].Effect)}]</span>";

            Title = $"PokéTrainer© // Pokémon tipo - {PokemonFirstTypeFormattedTitle} // Pokémon - {pokemonNameFormatted} [{pokemonDexFormatted}]";
            if (pokemon.Abilities.Count == 2)
            {
                lblPokemonAbilityTwo.Text = pokemonAbilityTwoUpper;
                //lblPokemonAbilityTwo.TooltipMarkup = $"<span foreground='white' font_desc='MS Gothic Regular 15'>[{_apiRequest.Translate(pokeAbility[1].EffectEntries[1].Effect)}]</span>";
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

            if (PokemonFirstTypeFormatted != pokemon.Types[0].Type.Name)
            {
                PokemonFirstTypeFormatted = pokemon.Types[0].Type.Name;
                PokemonTypeOne.Pixbuf = new Pixbuf($"Images/pokemon_types/{PokemonFirstTypeFormatted}.png");
                string damageRelations = GetTypeDamageRelations(pokemonTypePrimary);
                PokemonTypeOne.TooltipMarkup = $"<span foreground='white' font_desc='MS Gothic Regular 12'>[ {damageRelations} ]</span>";
            }
            if (pokemon.Types.Count > 1)
            {
                pokemonSecondaryTypeFormatted = pokemon.Types[1].Type.Name;
                imagePokemonTypeSecondary.Pixbuf = new Pixbuf($"Images/pokemon_types/{pokemonSecondaryTypeFormatted}.png");
                string damageRelationsSecondary = GetTypeDamageRelations(pokemonTypeSecondary);
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
    }
}