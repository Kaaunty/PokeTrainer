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

        private static ApiRequest _apiRequest = new();
        private bool isLoaded = false;
        private int variationId = 0;
        private Pokemon pokemon;
        private TextInfo textInfo = new CultureInfo("pt-BR", false).TextInfo;
        private List<Ability> pokeAbility = new();
        private CssProvider cssProvider = new();
        private Image PokemonAnimation = new();
        private PokemonSpecies pokeSpecies;

        private Image megaKey = new Image();
        private Image PokemonTypeOne = new Image();
        private PokemonForm pokeForm;
        private PokeApiNet.Type pokemonTypePrimary;
        private PokeApiNet.Type pokemonTypeSecondary;
        private Fixed fix = new Fixed();
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
        private string pokemonHPFormatted, pokemonATKFormatted, pokemonDEFFormatted, pokemonSpATKFormatted, pokemonSpDEFFormatted, pokemonSpeedFormatted;
        private string pokemonNameFormatted, pokemonDexFormatted, pokemonMaleFormatted, pokemonFemaleFormatted, pokemonCatchRate;
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

                Image Background = new Image("Images/pokemon_homescreen/water.png");
                fix.Put(Background, 0, 0);

                megaKey.Pixbuf = new Pixbuf("Images/MegaKeyDesactivated.png");
                fix.Put(megaKey, 138, 42);

                PokemonAnimation.PixbufAnimation = new PixbufAnimation("Images/PokemonAnimated.gif");

                GetPokemonGifSize();
                lblPokemonDexNumber.Text = pokemonDexFormatted;
                //lblPokemonDexNumber.Markup = $"<span foreground='black' font_desc='MS Gothic Regular 18'>[{pokemonDexFormatted}]</span>";
                fix.Put(lblPokemonDexNumber, 40, 45);

                //lblPokemonDexNumber.Text = pokemonDexFormatted;
                //fix.Put(lblPokemonDexNumber, 40, 45);

                lblPokemonName.Text = pokemonNameFormatted;
                lblPokemonName.TooltipMarkup = $"<span foreground='white' font_desc='Pixeloid Mono 15'>[ {textInfo.ToTitleCase(_apiRequest.Translate(pokeSpecies.FlavorTextEntries[0].FlavorText.ToLower()))} ]</span>";

                fix.Put(lblPokemonName, 40, 357);

                PokemonTypeOne = new Image($"Images/pokemon_types/{pokemon.Types[0].Type.Name}.png");

                string damageRelations = GetTypeDamageRelations(pokemonTypePrimary);
                PokemonTypeOne.TooltipMarkup = $"<span foreground='white' font_desc='MS Gothic Regular 12'>[ {damageRelations} ]</span>";

                fix.Put(PokemonTypeOne, 93, 427);

                if (pokemon.Types.Count > 1)
                {
                    Image imagePokemonTypeSecondary = new Image($"Images/pokemon_types/{pokemon.Types[1].Type.Name}.png");
                    string damageRelationsSecondary = GetTypeDamageRelations(pokemonTypeSecondary);
                    imagePokemonTypeSecondary.TooltipMarkup = $"<span foreground='white' font_desc='MS Gothic Regular 12'>[ {damageRelationsSecondary} ]</span>";

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

                if (pokeForm.IsMega)
                {
                    megaKey.Pixbuf = new Pixbuf("Images/MegaKeyActivated.png");
                    fix.Put(megaKey, 138, 42);
                }

                Button PreviousEvolution = new ButtonGenerator("Images/BackForm.png", 40, 40);
                fix.Put(PreviousEvolution, 180, 38);
                PreviousEvolution.Clicked += GetPreviousVariation;

                Button NextEvolution = new ButtonGenerator("Images/NextForm.png", 40, 40);
                fix.Put(NextEvolution, 230, 38);
                NextEvolution.Clicked += GetNextVariation;

                Button btnMoves = new ButtonGenerator("Images/buttons_type/MovesIcon.png", 255, 80);
                btnMoves.TooltipMarkup = "<span foreground='white' font_desc='MS Gothic Regular 10'>[Clique para ver os movimentos do Pokémon]</span>";
                fix.Put(btnMoves, 428, 395);
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

        private string GetTypeDamageRelations(PokeApiNet.Type type)
        {
            string HalfDamageFrom = "";
            string HalfDamageTo = "";
            string DoubleDamageFrom = "";
            string DoubleDamageTo = "";
            string NoDamageFrom = "";
            string NoDamageTo = "";
            if (type.DamageRelations.HalfDamageFrom.Count > 0)
            {
                foreach (var halfDamageFrom in type.DamageRelations.HalfDamageFrom)
                {
                    string translatedName = textInfo.ToTitleCase(_apiRequest.Translate(halfDamageFrom.Name));
                    HalfDamageFrom += translatedName + ",";
                }
                string removeLastComma = HalfDamageFrom.Remove(HalfDamageFrom.Length - 1);
                HalfDamageFrom = removeLastComma;
            }
            if (type.DamageRelations.HalfDamageTo.Count > 0)
            {
                foreach (var halfDamageTo in type.DamageRelations.HalfDamageTo)
                {
                    string translatedName = textInfo.ToTitleCase(_apiRequest.Translate(halfDamageTo.Name));
                    HalfDamageTo += translatedName + ",";
                }
                string removeLastComma = HalfDamageTo.Remove(HalfDamageTo.Length - 1);
                HalfDamageTo = removeLastComma;
            }
            if (type.DamageRelations.DoubleDamageFrom.Count > 0)
            {
                foreach (var doubleDamageFrom in type.DamageRelations.DoubleDamageFrom)
                {
                    string translatedName = textInfo.ToTitleCase(_apiRequest.Translate(doubleDamageFrom.Name));
                    DoubleDamageFrom += translatedName + ",";
                }
                string removeLastComma = DoubleDamageFrom.Remove(DoubleDamageFrom.Length - 1);
                DoubleDamageFrom = removeLastComma;
            }
            if (type.DamageRelations.DoubleDamageTo.Count > 0)
            {
                foreach (var doubleDamageTo in type.DamageRelations.DoubleDamageTo)
                {
                    string translatedName = textInfo.ToTitleCase(_apiRequest.Translate(doubleDamageTo.Name));
                    DoubleDamageTo += translatedName + ",";
                }
                string removeLastComma = DoubleDamageTo.Remove(DoubleDamageTo.Length - 1);
                DoubleDamageTo = removeLastComma;
            }
            if (type.DamageRelations.NoDamageFrom.Count > 0)
            {
                foreach (var noDamageFrom in type.DamageRelations.NoDamageFrom)
                {
                    string translatedName = textInfo.ToTitleCase(_apiRequest.Translate(noDamageFrom.Name));
                    NoDamageFrom += translatedName + ",";
                }
                string removeLastComma = NoDamageFrom.Remove(NoDamageFrom.Length - 1);
                NoDamageFrom = removeLastComma;
            }
            if (string.IsNullOrEmpty(NoDamageFrom))
            {
                NoDamageFrom = "Nenhum";
            }
            if (type.DamageRelations.NoDamageTo.Count > 0)
            {
                foreach (var noDamageFrom in type.DamageRelations.NoDamageTo)
                {
                    string translatedName = textInfo.ToTitleCase(_apiRequest.Translate(noDamageFrom.Name));
                    NoDamageTo += translatedName + ",";
                }
                string removeLastComma = NoDamageTo.Remove(NoDamageTo.Length - 1);
                NoDamageTo = removeLastComma;
            }
            if (string.IsNullOrEmpty(NoDamageTo))
            {
                NoDamageTo = "Nenhum";
            }
            string damageRelations = $"Dano Sofrido Pouco Efetivo: {HalfDamageFrom}\n Pouco Efetivo Contra: {HalfDamageTo}\n Dano Sofrido Super Efetivo: {DoubleDamageFrom}\n Super Efetivo Contra: {DoubleDamageTo}\n Imune: {NoDamageFrom}\n Nenhum Dano a: {NoDamageTo}";
            return damageRelations;
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

                pokeForm = await _apiRequest.GetPokemonForm(pokemon.Name);

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
                    PokeApiNet.Type pokemonType = await _apiRequest.GetTypeAsync(PokemonFirstTypeFormatted);
                    pokemonSecondaryTypeFormatted = "";
                }
                else
                {
                    PokemonFirstTypeFormatted = pokemon.Types[0].Type.Name;
                    pokemonTypePrimary = await _apiRequest.GetTypeAsync(PokemonFirstTypeFormatted);
                    pokemonSecondaryTypeFormatted = pokemon.Types[1].Type.Name;
                    pokemonTypeSecondary = await _apiRequest.GetTypeAsync(pokemonSecondaryTypeFormatted);
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
                        0 => "0,00%",
                        1 => "12,5%",
                        2 => "25,0%",
                        3 => "50,0%",
                        4 => "75,0%",
                        5 => "87,5%",
                        6 => "100%",
                        _ => pokemonFemaleFormatted
                    };

                    pokemonMaleFormatted = pokeSpecies.GenderRate switch
                    {
                        0 => "100%",
                        1 => "87,5%",
                        2 => "75,0%",
                        3 => "50,0%",
                        4 => "25,0%",
                        5 => "12,5%",
                        6 => "0,00%",
                        _ => pokemonMaleFormatted
                    };

                    pokemonCatchRate = pokeSpecies.CaptureRate switch
                    {
                        3 => "01.6%",
                        45 => "11.9%",
                        190 => "35.2%",
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
            int y = (380 - PokemonAnimation.PixbufAnimation.Height) / 2;

            fix.Put(PokemonAnimation, x, y);
        }

        private void UpdateLabels()
        {
            lblPokemonDexNumber.Text = pokemonDexFormatted;
            lblPokemonName.Text = pokemonNameFormatted;
            lblPokemonAbilityOne.Text = pokemonAbilityOneUpper;
            PokemonFirstTypeFormattedTitle = _apiRequest.Translate(textInfo.ToTitleCase(PokemonFirstTypeFormatted));

            lblPokemonAbilityOne.TooltipMarkup = $"<span foreground='white' font_desc='RetroPix 15'>[{_apiRequest.Translate(pokeAbility[0].EffectEntries[1].Effect)}]</span>";

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

            if (pokeForm.IsMega)
            {
                fix.Remove(megaKey);
                megaKey.Pixbuf = new Pixbuf("Images/MegaKeyActivated.png");
                fix.Put(megaKey, 138, 42);
            }
            else
            {
                fix.Remove(megaKey);
                megaKey.Pixbuf = new Pixbuf("Images/MegaKeyDesactivated.png");
                fix.Put(megaKey, 138, 42);
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