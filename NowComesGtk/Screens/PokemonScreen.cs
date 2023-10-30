using Gdk;
using Gtk;
using NowComesGtk.Reusable_components;
using NowComesGtk.Utils;
using PokeApi.BackEnd.Service;
using PokeApiNet;
using System.Globalization;
using static PokeApi.BackEnd.Service.ApiRequest;
using Type = PokeApiNet.Type;

namespace NowComesGtk.Screens
{
    public class PokemonScreen : BaseWindow
    {
#nullable disable

        private static ApiRequest _apiRequest = new();

        private Pokemon pokemon = new();
        private Button ShinyButton = new();

        private Image megaIcon = new("Images/pokemon_forms/MegaKeyDesactivated.png");
        private PokemonForm pokeForm = new();
        private PokemonSpecies pokeSpecies = new();
        private Type pokemonTypePrimary = new();
        private Type pokemonTypeSecondary = new();
        private TextInfo textInfo = new CultureInfo("pt-BR", false).TextInfo;
        private Image imagePokemonTypeSecondary = new();
        private Image PokemonAnimation = new();
        private Image PokemonTypeOne = new();
        private Image megaKey = new();
        private ListStore formsList;
        private List<Ability> pokeAbility = new();
        private Fixed fix = new();
        private ListStore forms = new(typeof(string), (typeof(string)));

        private enum Column
        {
            Form,
            FormMethod
        }

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
        private Label lblPokemonMale = new();
        private Label lblPokemnFemale = new();
        private Label lblPokemonCatchRate = new();
        private Label lblPokemonEggGroup = new();
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

        private string pokemonHPFormatted = "", pokemonATKFormatted = "", pokemonDEFFormatted = "", pokemonSpATKFormatted = "", pokemonSpDEFFormatted = "", pokemonSpeedFormatted = "";
        private string PokemonFirstTypeFormattedTitle = "", PokemonFirstTypeFormatted = "", pokemonSecondaryTypeFormatted = "", damageRelations = "", damageRelationsSecondary = "";
        private string pokemonNameFormatted = "", pokemonDexFormatted = "", pokemonMaleFormatted = "", pokemonFemaleFormatted = "", pokemonCatchRate = "", pokemonEggGroup = "";
        private string pokemonAbilityOneUpper = "", pokemonAbilityTwoUpper = "", pokemonAbilityThreeUpper = "", pokemonAbilityFourUpper = "", pokemonFlavorText = "";

        public class CombinedPokemon
        {
            public Pokemon pokemon { get; set; }
            public PokemonForm pokemonForm { get; set; }
            public PokemonSpecies pokemonSpecies { get; set; }
        }

        public PokemonScreen(Pokemon Pokemon) : base("", 1000, 500)
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

                TranslateString();

                lblPokemonName.TooltipMarkup = $"<span foreground='white' font_desc='Pixeloid Mono Regular 12'>{pokemonFlavorText}</span>";
                fix.Put(lblPokemonName, 40, 357);

                PokemonTypeOne = new Image($"Images/pokemon_types/{pokemon.Types[0].Type.Name}.png");
                damageRelations = GetTypeDamageRelation(pokemon.Types[0].Type.Name);
                PokemonTypeOne.TooltipMarkup = $"<span foreground='white' font_desc='Pixeloid Mono Regular 12'>{damageRelations}</span>";
                fix.Put(PokemonTypeOne, 100, 433);
                fix.Put(imagePokemonTypeSecondary, 188, 433);

                if (pokemon.Types.Count > 1)
                {
                    imagePokemonTypeSecondary = new Image($"Images/pokemon_types/{pokemon.Types[1].Type.Name}.png");
                    damageRelationsSecondary = GetTypeDamageRelation(pokemon.Types[1].Type.Name);
                    imagePokemonTypeSecondary.TooltipMarkup = $"<span foreground='white' font_desc='Pixeloid Mono Regular 12'>{damageRelationsSecondary}</span>";
                }

                lblPokemonAbilityOne.Text = pokemonAbilityOneUpper;
                fix.Put(lblPokemonAbilityOne, 345, 50);

                if (pokemon.Abilities.Count == 2 && !String.IsNullOrEmpty(pokemon.Abilities[1].Ability.Name))
                {
                    lblPokemonAbilityTwo.Text = pokemonAbilityTwoUpper;
                    fix.Put(lblPokemonAbilityTwo, 345, 85);
                }
                else if (pokemon.Abilities.Count == 3 && !String.IsNullOrEmpty(pokemon.Abilities[2].Ability.Name))
                {
                    lblPokemonAbilityTwo.Text = pokemonAbilityTwoUpper;
                    fix.Put(lblPokemonAbilityTwo, 345, 85);
                    lblPokemonAbilityThree.Text = pokemonAbilityThreeUpper;
                    fix.Put(lblPokemonAbilityThree, 575, 50);
                }
                else if (pokemon.Abilities.Count == 4 && !String.IsNullOrEmpty(pokemon.Abilities[3].Ability.Name))
                {
                    lblPokemonAbilityTwo.Text = pokemonAbilityTwoUpper;
                    fix.Put(lblPokemonAbilityTwo, 345, 85);
                    lblPokemonAbilityThree.Text = pokemonAbilityThreeUpper;
                    fix.Put(lblPokemonAbilityThree, 575, 50);
                    lblPokemonAbilityFour.Text = pokemonAbilityFourUpper;
                    fix.Put(lblPokemonAbilityFour, 575, 85);
                }

                lblPokemonMale = new Label(pokemonMaleFormatted);
                fix.Put(lblPokemonMale, 793, 110);
                lblPokemnFemale = new Label(pokemonFemaleFormatted);
                fix.Put(lblPokemnFemale, 905, 110);
                lblPokemonCatchRate = new Label(pokemonCatchRate);
                fix.Put(lblPokemonCatchRate, 897, 192);
                lblPokemonEggGroup = new Label(pokemonEggGroup);
                fix.Put(lblPokemonEggGroup, 850, 305);
                lblPokemonEggGroup.SetAlignment(0.5f, 0.5f);

                lblPokemonHP.Text = pokemonHPFormatted;
                fix.Put(lblPokemonHP, 353, 195);
                lblPokemonATK.Text = pokemonATKFormatted;
                fix.Put(lblPokemonATK, 417, 195);
                lblPokemonDEF.Text = pokemonDEFFormatted;
                fix.Put(lblPokemonDEF, 485, 195);
                lblPokemonSpATK.Text = pokemonSpATKFormatted;
                fix.Put(lblPokemonSpATK, 553, 195);
                lblPokemonSpDEF.Text = pokemonSpDEFFormatted;
                fix.Put(lblPokemonSpDEF, 621, 195);
                lblPokemonSpeed.Text = pokemonSpeedFormatted;
                fix.Put(lblPokemonSpeed, 688, 195);

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

                ScrolledWindow sw = new ScrolledWindow();
                sw.ShadowType = ShadowType.EtchedIn;
                sw.SetPolicy(PolicyType.Automatic, PolicyType.Automatic);
                sw.SetSizeRequest(390, 170);
                formsList = CreateModel();
                TreeView treeView = new(formsList);
                treeView.RulesHint = true;
                treeView.RowActivated += OnRowActivated;
                sw.Add(treeView);
                AddColumns(treeView);

                fix.Put(sw, 340, 295);

                ShinyButton = new ButtonGenerator("Images/buttons/shinyButtonDesactived.png", 40, 40);
                fix.Put(ShinyButton, 138, 270);
                ShinyButton.Clicked += ShinyButtonClicked;
                ShinyButton.TooltipMarkup = "<span foreground='white' font_desc='Pixeloid Mono Regular 12'>Clique para ver a versão shiny do Pokémon</span>";

                Button btnMoves = new ButtonGenerator("Images/buttons/btnMoves.png", 192, 85);
                btnMoves.TooltipMarkup = "<span foreground='white' font_desc='Pixeloid Mono Regular 12'>Clique para ver os movimentos do Pokémon</span>";
                fix.Put(btnMoves, 767, 393);
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

        public string GetTypeDamageRelation(string type)
        {
            return PokeList.TypeDamageRelations[type];
        }

        private async void OnRowActivated(object o, RowActivatedArgs args)
        {
            TreeIter iter;
            if (formsList.GetIter(out iter, args.Path))
            {
                string value = (string)formsList.GetValue(iter, 0);
                if (pokemon.Forms.Count > 1)
                {
                    var pokemonFormIndex = pokemon.Forms.FindIndex(x => x.Name == value);
                    pokemonFormId = pokemonFormIndex;
                    string pokemonName = pokemon.Forms[pokemonFormIndex].Name;
                    pokeForm = await _apiRequest.GetPokemonForm(pokemonName);
                    if (VerifyType(pokemonName))
                    {
                        pokemonTypePrimary = await _apiRequest.GetTypeAsync(pokeForm.Name);
                        await UpdateLabels();
                    }
                    await UpdatePokemonSprite(pokemonName);
                    GetPokemonGifSize();
                    formsList.Clear();
                    CreateModel();
                }
                else if (value.Contains("Primeira Evolução:"))
                {
                    pokeAbility.Clear();
                    pokemon = await _apiRequest.GetPokemon(evolutionChain.Chain.Species.Name);
                    await PopulateFields();
                    GetPokemonGifSize();
                    await UpdateLabels();
                    formsList.Clear();
                    CreateModel();
                }
                else
                {
                    pokeAbility.Clear();
                    string pokemonName = value;
                    pokemon = await _apiRequest.GetPokemon(pokemonName);
                    await PopulateFields();
                    GetPokemonGifSize();
                    await UpdateLabels();
                    formsList.Clear();
                    CreateModel();
                }
            }
        }

        private void AddColumns(TreeView treeView)
        {
            CellRendererText rendererText = new CellRendererText();
            TreeViewColumn column = new TreeViewColumn("Evoluções e Formas", rendererText,
                "text", (int)Column.Form);
            column.SortColumnId = (int)Column.Form;
            treeView.AppendColumn(column);
            column = new TreeViewColumn("Forma de Evolução", rendererText,
                               "text", (int)Column.FormMethod);
            treeView.AppendColumn(column);
            //column.FixedWidth = 200;
        }

        private ListStore CreateModel()
        {
            forms.AppendValues(evolutionChain.Chain.Species.Name, "Primeira Evolução");

            if (evolutionChain.Chain.EvolvesTo.Count > 0)
            {
                foreach (var evo in evolutionChain.Chain.EvolvesTo)
                {
                    foreach (var i in evo.EvolutionDetails)
                    {
                        if (i != null)
                        {
                            if (i.Trigger.Name == "level-up")
                            {
                                if (i.MinLevel != null)
                                {
                                    forms.AppendValues($"{evo.Species.Name}", $"Metodo de Evolução: {i.Trigger.Name} Level Minimo: {i.MinLevel}");
                                }
                            }
                            else if (i.Trigger.Name == "trade")
                            {
                                forms.AppendValues(evo.Species.Name, i.Trigger.Name);
                            }
                            else if (i.Trigger.Name == "use-item")
                            {
                                forms.AppendValues($"{evo.Species.Name}", $"Metodo de Evolução: {i.Trigger.Name} Item Usado: {i.Item.Name}");
                            }
                        }
                    }
                }
                if (evolutionChain.Chain.EvolvesTo[0].EvolvesTo.Count > 0)
                {
                    foreach (var evo in evolutionChain.Chain.EvolvesTo[0].EvolvesTo)
                    {
                        foreach (var i in evo.EvolutionDetails)
                        {
                            if (i != null)
                            {
                                forms.AppendValues($"{evo.Species.Name}", $"Metodo de Evolução: {i.Trigger.Name} Level Minimo: {i.MinLevel}");
                            }
                        }
                    }
                }
            }
            if (pokemon.Forms.Count > 1)
            {
                foreach (var form in pokemon.Forms)
                {
                    forms.AppendValues(form.Name);
                }
            }
            if (pokeSpecies.Varieties.Count > 1)
            {
                foreach (var poke in pokeSpecies.Varieties)
                {
                    if (poke.IsDefault == false)
                    {
                        if (poke.Pokemon.Name.Contains("mega"))
                        {
                            forms.AppendValues(poke.Pokemon.Name, "Mega Stone");
                        }
                        else if (poke.Pokemon.Name.Contains("gmax"))
                        {
                            forms.AppendValues(poke.Pokemon.Name, "Dynamax");
                        }
                        else if (poke.Pokemon.Name.Contains("alola"))
                        {
                            forms.AppendValues(poke.Pokemon.Name, "Alola");
                        }
                        else if (poke.Pokemon.Name.Contains("galar"))
                        {
                            forms.AppendValues(poke.Pokemon.Name, "Galar");
                        }
                        else
                        {
                            forms.AppendValues(poke.Pokemon.Name);
                        }
                    }
                }
            }
            return forms;
        }

        private async void TranslateString()
        {
            try
            {
                if (pokemon.Abilities.Count == 1)
                {
                    lblPokemonAbilityOneToolTip = await _apiRequest.Translate(pokeAbility[0].EffectEntries.Last().Effect);
                    lblPokemonAbilityOne.TooltipMarkup = $"<span foreground='white' font_desc='Pixeloid Mono Regular 12'>{lblPokemonAbilityOneToolTip}</span>";
                }
                else
                {
                    lblPokemonAbilityOne.TooltipMarkup = $"<span foreground='white' font_desc='Pixeloid Mono Regular 12'>[Sem descrição da habilidade]</span>";
                }
                if (pokemon.Abilities.Count == 2)
                {
                    if (pokeAbility[0].EffectEntries.Count > 0)
                    {
                        lblPokemonAbilityOneToolTip = await _apiRequest.Translate(pokeAbility[0].EffectEntries.Last().Effect);
                        lblPokemonAbilityOne.TooltipMarkup = $"<span foreground='white' font_desc='Pixeloid Mono Regular 12'>{lblPokemonAbilityOneToolTip}</span>";
                    }
                    else
                    {
                        lblPokemonAbilityOne.TooltipMarkup = $"<span foreground='white' font_desc='Pixeloid Mono Regular 12'>[Sem descrição da habilidade]</span>";
                    }
                    if (pokeAbility[1].EffectEntries.Count > 0)
                    {
                        lblPokemonAbilityTwoToolTip = await _apiRequest.Translate(pokeAbility[1].EffectEntries.Last().Effect);
                        lblPokemonAbilityTwo.TooltipMarkup = $"<span foreground='white' font_desc='Pixeloid Mono Regular 12'>{lblPokemonAbilityTwoToolTip}</span>";
                    }
                    else
                    {
                        lblPokemonAbilityTwo.TooltipMarkup = $"<span foreground='white' font_desc='Pixeloid Mono Regular 12'>[Sem descrição da habilidade]</span>";
                    }
                    if (pokemon.Abilities.Count == 3)
                    {
                        if (pokeAbility[0].EffectEntries.Count > 0)
                        {
                            lblPokemonAbilityOneToolTip = await _apiRequest.Translate(pokeAbility[0].EffectEntries.Last().Effect);
                            lblPokemonAbilityOne.TooltipMarkup = $"<span foreground='white' font_desc='Pixeloid Mono Regular 12'> {lblPokemonAbilityOneToolTip} </span>";
                        }
                        else
                        {
                            lblPokemonAbilityOne.TooltipMarkup = $"<span foreground='white' font_desc='Pixeloid Mono Regular 12'>[Sem descrição da habilidade]</span>";
                        }
                        if (pokeAbility[1].EffectEntries.Count > 0)
                        {
                            lblPokemonAbilityTwoToolTip = await _apiRequest.Translate(pokeAbility[1].EffectEntries.Last().Effect);
                            lblPokemonAbilityTwo.TooltipMarkup = $"<span foreground='white' font_desc='Pixeloid Mono Regular 12'> {lblPokemonAbilityTwoToolTip} </span>";
                        }
                        else
                        {
                            lblPokemonAbilityTwo.TooltipMarkup = $"<span foreground='white' font_desc='Pixeloid Mono Regular 12'>[Sem descrição da habilidade]</span>";
                        }
                        if (pokeAbility[2].EffectEntries.Count > 0)
                        {
                            lblPokemonAbilityThreeToolTip = await _apiRequest.Translate(pokeAbility[2].EffectEntries.Last().Effect);
                            lblPokemonAbilityThree.TooltipMarkup = $"<span foreground='white' font_desc='Pixeloid Mono Regular 12'> {lblPokemonAbilityThreeToolTip} </span>";
                        }
                        else
                        {
                            lblPokemonAbilityThree.TooltipMarkup = $"<span foreground='white' font_desc='Pixeloid Mono Regular 12'>[Sem descrição da habilidade]</span>";
                        }
                    }
                    if (pokemon.Abilities.Count == 4)
                    {
                        if (pokeAbility[0].EffectEntries.Count > 0)
                        {
                            lblPokemonAbilityOneToolTip = await _apiRequest.Translate(pokeAbility[0].EffectEntries.Last().Effect);
                            lblPokemonAbilityOne.TooltipMarkup = $"<span foreground='white' font_desc='Pixeloid Mono Regular 12'> {lblPokemonAbilityOneToolTip} </span>";
                        }
                        else
                        {
                            lblPokemonAbilityOne.TooltipMarkup = $"<span foreground='white' font_desc='Pixeloid Mono Regular 12'>[Sem descrição da habilidade]</span>";
                        }
                        if (pokeAbility[1].EffectEntries.Count > 0)
                        {
                            lblPokemonAbilityTwoToolTip = await _apiRequest.Translate(pokeAbility[1].EffectEntries.Last().Effect);
                            lblPokemonAbilityTwo.TooltipMarkup = $"<span foreground='white' font_desc='Pixeloid Mono Regular 12'> {lblPokemonAbilityTwoToolTip} </span>";
                        }
                        else
                        {
                            lblPokemonAbilityTwo.TooltipMarkup = $"<span foreground='white' font_desc='Pixeloid Mono Regular 12'>[Sem descrição da habilidade]</span>";
                        }
                        if (pokeAbility[2].EffectEntries.Count > 0)
                        {
                            lblPokemonAbilityThreeToolTip = await _apiRequest.Translate(pokeAbility[2].EffectEntries.Last().Effect);
                            lblPokemonAbilityThree.TooltipMarkup = $"<span foreground='white' font_desc='Pixeloid Mono Regular 12'> {lblPokemonAbilityThreeToolTip} </span>";
                        }
                        else
                        {
                            lblPokemonAbilityThree.TooltipMarkup = $"<span foreground='white' font_desc='Pixeloid Mono Regular 12'>[Sem descrição da habilidade]</span>";
                        }

                        if (pokeAbility[3].EffectEntries.Count > 0)
                        {
                            lblPokemonAbilityFourToolTip = await _apiRequest.Translate(pokeAbility[3].EffectEntries.Last().Effect);
                            lblPokemonAbilityFour.TooltipMarkup = $"<span foreground='white' font_desc='Pixeloid Mono Regular 12'> {lblPokemonAbilityFourToolTip} </span>";
                        }
                        else
                        {
                            lblPokemonAbilityFour.TooltipMarkup = $"<span foreground='white' font_desc='Pixeloid Mono Regular 12'>[Sem descrição da habilidade]</span>";
                        }
                    }
                }
                string pokemonFlavorTextReplaced = textInfo.ToTitleCase(pokeSpecies.FlavorTextEntries[6].FlavorText.Replace("\n", "").ToLower());
                pokemonFlavorText = await _apiRequest.Translate(pokemonFlavorTextReplaced);
                lblPokemonName.TooltipMarkup = $"<span foreground='white' font_desc='Pixeloid Mono Regular 12'>{pokemonFlavorText}</span>";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao traduzir a descrição da habilidade {pokeAbility[0].Name}: {ex.Message}");
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
                    try
                    {
                        await _apiRequest.GetPokemonAnimatedSprite(pokemon.Forms[pokemonFormId].Name, isShiny);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Erro ao carregar a sprite animada do Pokémon: {ex.Message}");
                        PokemonAnimation.PixbufAnimation = new PixbufAnimation("Images/pokemonSpriteError.png");
                    }
                }
                else
                {
                    await _apiRequest.GetPokemonAnimatedSprite(pokemon.Name, isShiny);
                }
                PokemonAnimation.PixbufAnimation = new PixbufAnimation("Images/PokemonAnimated.gif");
            }
            else
            {
                ShinyButton.Image = shinyButtonActivedImage;
                isShiny = true;
                if (pokemon.Forms.Count > 1)
                {
                    await _apiRequest.GetPokemonAnimatedSprite(pokemon.Forms[pokemonFormId].Name, isShiny);
                }
                else
                {
                    await _apiRequest.GetPokemonAnimatedSprite(pokemon.Name, isShiny);
                }
                PokemonAnimation.PixbufAnimation = new PixbufAnimation("Images/PokemonAnimatedShiny.gif");
            }
        }

        private async void PokemonMoves(object sender, EventArgs e)
        {
            Pokemon pokemonSpecie = await _apiRequest.GetPokemon(evolutionChain.Chain.Species.Name);
            List<Move> pokemonSpecieMoves = await _apiRequest.GetMoveLearnedByPokemon(pokemonSpecie);
            List<Move> pokemonMoves = await _apiRequest.GetMoveLearnedByPokemon(pokemon);

            var MoveList = pokemonSpecieMoves.Where(pokeSpecieMove => !pokemonMoves.Any(pokeMove => pokeMove.Name == pokeSpecieMove.Name)).Concat(pokemonMoves).ToList();

            string pokemonType = pokemon.Types[0].Type.Name;

            MovementScreen movementScreen = new(MoveList, pokemon, pokemonSpecie, pokemonType);
            movementScreen.ShowAll();
        }

        public async Task PopulateFields()
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
                    pokemonEggGroup += textInfo.ToTitleCase(eggGroup.Name) + "\n";
                }
                CombinedPokemon combinedPokemon = new CombinedPokemon();
                combinedPokemon.pokemon = pokemon;
                combinedPokemon.pokemonForm = pokeForm;
                combinedPokemon.pokemonSpecies = pokeSpecies;
                combinedPokemon.pokemon.Name = pokemon.Name;
                combinedPokemon.pokemonForm.Name = pokeForm.Name;
                combinedPokemon.pokemonSpecies.Name = pokeSpecies.Name;

                isLoaded = true;
            }
            catch (Exception ex)
            {
                MessageDialogGenerator.ShowMessageDialog("Erro ao carregar os dados do Pokémon." + ex);
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
                        10 => "03.9%",
                        25 => "07.7%",
                        45 => "11.9%",
                        60 => "14.8%",
                        75 => "17.5%",
                        90 => "20.8%",
                        100 => "21.7%",
                        120 => "24.9%",
                        127 => "26.0%",
                        150 => "29.5%",
                        160 => "30.9%",
                        180 => "33.8%",
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
            if (!isShiny)
            {
                await _apiRequest.GetPokemonAnimatedSprite(pokemonForm, isShiny);
                PokemonAnimation.PixbufAnimation = new PixbufAnimation("Images/PokemonAnimated.gif");
            }
            else if (isShiny)
            {
                await _apiRequest.GetPokemonAnimatedSprite(pokemonForm, isShiny);
                PokemonAnimation.PixbufAnimation = new PixbufAnimation("Images/PokemonAnimatedShiny.gif");
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

        private async Task UpdateLabels()
        {
            lblPokemonDexNumber.Text = pokemonDexFormatted;
            lblPokemonName.Text = pokemonNameFormatted;
            PokemonFirstTypeFormattedTitle = (textInfo.ToTitleCase(PokemonFirstTypeFormatted));

            lblPokemonAbilityOne.Text = pokemonAbilityOneUpper;
            if (pokeAbility != null)
            {
                if (pokeAbility[0].EffectEntries.Count > 0)
                {
                    lblPokemonAbilityOne.TooltipMarkup = $"<span foreground='white' font_desc='Pixeloid Mono Regular 12'>[{await _apiRequest.Translate(pokeAbility[0].EffectEntries.Last().Effect)}]</span>";
                }
                Title = $"PokéTrainer© // Pokémon tipo - {PokemonFirstTypeFormattedTitle} // Pokémon - {pokemonNameFormatted} [{pokemonDexFormatted}]";
                if (pokemon.Abilities.Count == 2)
                {
                    lblPokemonAbilityTwo.Text = pokemonAbilityTwoUpper;
                    if (pokeAbility[1].EffectEntries.Count > 0)
                    {
                        lblPokemonAbilityTwo.TooltipMarkup = $"<span foreground='white' font_desc='Pixeloid Mono Regular 12'>[{await _apiRequest.Translate(pokeAbility[1].EffectEntries.Last().Effect)}]</span>";
                    }
                }
                else if (pokemon.Abilities.Count == 3)
                {
                    lblPokemonAbilityTwo.Text = pokemonAbilityTwoUpper;
                    if (pokeAbility[1].EffectEntries.Count > 0)
                    {
                        lblPokemonAbilityTwo.TooltipMarkup = $"<span foreground='white' font_desc='Pixeloid Mono Regular 12'>[{await _apiRequest.Translate(pokeAbility[1].EffectEntries.Last().Effect)}]</span>";
                    }
                    lblPokemonAbilityThree.Text = pokemonAbilityThreeUpper;
                    if (pokeAbility[2].EffectEntries.Count > 0)
                    {
                        lblPokemonAbilityThree.TooltipMarkup = $"<span foreground='white' font_desc='Pixeloid Mono Regular 12'>[{await _apiRequest.Translate(pokeAbility[2].EffectEntries.Last().Effect)}]</span>";
                    }
                }
                else if (pokemon.Abilities.Count == 4)
                {
                    lblPokemonAbilityTwo.Text = pokemonAbilityTwoUpper;
                    if (pokeAbility[1].EffectEntries.Count > 0)
                    {
                        lblPokemonAbilityTwo.TooltipMarkup = $"<span foreground='white' font_desc='Pixeloid Mono Regular 12'>[{await _apiRequest.Translate(pokeAbility[1].EffectEntries.Last().Effect)}]</span>";
                    }
                    lblPokemonAbilityThree.Text = pokemonAbilityThreeUpper;
                    if (pokeAbility[2].EffectEntries.Count > 0)
                    {
                        lblPokemonAbilityThree.TooltipMarkup = $"<span foreground='white' font_desc='Pixeloid Mono Regular 12'>[{await _apiRequest.Translate(pokeAbility[2].EffectEntries.Last().Effect)}]</span>";
                    }
                    lblPokemonAbilityFour.Text = pokemonAbilityFourUpper;
                    if (pokeAbility[3].EffectEntries.Count > 0)
                    {
                        lblPokemonAbilityFour.TooltipMarkup = $"<span foreground='white' font_desc='Pixeloid Mono Regular 12'>[{await _apiRequest.Translate(pokeAbility[3].EffectEntries.Last().Effect)}]</span>";
                    }
                }

                if (PokemonFirstTypeFormatted != pokemonTypePrimary.Name)
                {
                    PokemonFirstTypeFormatted = pokemonTypePrimary.Name;
                    PokemonTypeOne.Pixbuf = new Pixbuf($"Images/pokemon_types/{PokemonFirstTypeFormatted}.png");
                    damageRelations = GetTypeDamageRelation(pokemonTypePrimary.Name);
                    PokemonTypeOne.TooltipMarkup = $"<span foreground='white' font_desc='MS Gothic Regular 12'>[{damageRelations}]</span>";
                }
                if (pokemon.Types.Count == 1)
                {
                    imagePokemonTypeSecondary.Pixbuf = new Pixbuf("Images/pokemon_types/none.png");
                    imagePokemonTypeSecondary.TooltipMarkup = "";
                }

                if (pokemon.Types.Count > 1)
                {
                    pokemonSecondaryTypeFormatted = pokemon.Types[1].Type.Name;
                    imagePokemonTypeSecondary.Pixbuf = new Pixbuf($"Images/pokemon_types/{pokemonSecondaryTypeFormatted}.png");
                    damageRelationsSecondary = GetTypeDamageRelation(pokemonTypeSecondary.Name);
                    imagePokemonTypeSecondary.TooltipMarkup = $"<span foreground='white' font_desc='Pixeloid Mono Regular 12'>{damageRelationsSecondary}</span>";
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
                lblPokemonCatchRate.Text = pokemonCatchRate;
                lblPokemnFemale.Text = pokemonFemaleFormatted;
                lblPokemonMale.Text = pokemonMaleFormatted;
                lblPokemonHP.Text = pokemonHPFormatted;
                lblPokemonATK.Text = pokemonATKFormatted;
                lblPokemonDEF.Text = pokemonDEFFormatted;
                lblPokemonSpATK.Text = pokemonSpATKFormatted;
                lblPokemonSpDEF.Text = pokemonSpDEFFormatted;
                lblPokemonSpeed.Text = pokemonSpeedFormatted;
            }
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