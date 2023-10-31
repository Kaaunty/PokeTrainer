using System.Globalization;
using static PokeApi.BackEnd.Service.ApiRequest;
using NowComesGtk.Reusable_components;
using PokeApi.BackEnd.Service;
using Type = PokeApiNet.Type;
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

        private Pokemon _pokemon = new();
        private Button _shinyButton = new();
        private Image _megaIcon = new("Images/pokemon_forms/MegaKeyDesactivated.png");
        private PokemonForm _pokeForm = new();
        private PokemonSpecies _pokeSpecies = new();
        private Type _pokemonTypePrimary = new();
        private Type _pokemonTypeSecondary = new();
        private TextInfo _textInfo = new CultureInfo("pt-BR", false).TextInfo;
        private Image _imagePokemonTypeSecondary = new();
        private Image _pokemonAnimation = new();
        private Image _pokemonTypeOne = new();
        private Image _megaKey = new();
        private ListStore _formsList;
        private List<Ability> _pokeAbility = new();
        private Fixed _fix = new();
        private ListStore _forms = new(typeof(string), (typeof(string)));

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

        private bool _isLoaded = false;
        private bool _isShiny = false;
        private int _pokemonFormId = 0;

        private string _pokemonHPFormatted = "", _pokemonATKFormatted = "", _pokemonDEFFormatted = "", _pokemonSpATKFormatted = "", _pokemonSpDEFFormatted = "", _pokemonSpeedFormatted = "";
        private string _pokemonFirstTypeFormattedTitle = "", _pokemonFirstTypeFormatted = "", _pokemonSecondaryTypeFormatted = "", _damageRelations = "", _damageRelationsSecondary = "";
        private string _pokemonNameFormatted = "", _pokemonDexFormatted = "", _pokemonMaleFormatted = "", _pokemonFemaleFormatted = "", _pokemonCatchRate = "", _pokemonEggGroup = "";
        private string _pokemonAbilityOneUpper = "", _pokemonAbilityTwoUpper = "", _pokemonAbilityThreeUpper = "", _pokemonAbilityFourUpper = "", _pokemonFlavorText = "";

        public PokemonScreen(Pokemon Pokemon) : base("", 1000, 500)
        {
            try
            {
                _pokemon = Pokemon;
                PopulateFields();

                while (!_isLoaded)
                {
                    Task.Delay(100).Wait();
                }

                Image Background = new Image($"Images/pokemon_homescreen/{_pokemon.Types[0].Type.Name}.png");

                _fix.Put(Background, 0, 0);

                var fontDescription = Pango.FontDescription.FromString("Pixeloid Mono Regular 12");
                ModifyFont(fontDescription);

                _pokemonFirstTypeFormattedTitle = (_textInfo.ToTitleCase(_pokemonFirstTypeFormatted));

                Title = $"PokéTrainer© // Pokémon tipo - {_pokemonFirstTypeFormattedTitle} // Pokémon - {_pokemonNameFormatted} [{_pokemonDexFormatted}]";

                _megaKey.Pixbuf = new Pixbuf("Images/pokemon_forms/MegaKeyDesactivated.png");

                _pokemonAnimation.PixbufAnimation = new PixbufAnimation("Images/PokemonAnimated.gif");

                GetPokemonGifSize();

                lblPokemonName.Text = _pokemonNameFormatted;

                TranslateString();

                lblPokemonName.TooltipMarkup = $"<span foreground='white' font_desc='Pixeloid Mono Regular 12'>{_pokemonFlavorText}</span>";
                _fix.Put(lblPokemonName, 40, 357);

                _pokemonTypeOne = new Image($"Images/pokemon_types/{_pokemon.Types[0].Type.Name}.png");
                _damageRelations = GetTypeDamageRelation(_pokemon.Types[0].Type.Name);
                _pokemonTypeOne.TooltipMarkup = $"<span foreground='white' font_desc='Pixeloid Mono Regular 12'>{_damageRelations}</span>";
                _fix.Put(_pokemonTypeOne, 100, 433);

                if (_pokemon.Types.Count > 1)
                {
                    _imagePokemonTypeSecondary = new Image($"Images/pokemon_types/{_pokemon.Types[1].Type.Name}.png");
                    _damageRelationsSecondary = GetTypeDamageRelation(_pokemon.Types[1].Type.Name);
                    _imagePokemonTypeSecondary.TooltipMarkup = $"<span foreground='white' font_desc='Pixeloid Mono Regular 12'>{_damageRelationsSecondary}</span>";
                }

                _fix.Put(_imagePokemonTypeSecondary, 188, 433);

                lblPokemonAbilityOne.Text = _pokemonAbilityOneUpper;
                _fix.Put(lblPokemonAbilityOne, 345, 50);

                if (_pokemon.Abilities.Count == 2 && !String.IsNullOrEmpty(_pokemon.Abilities[1].Ability.Name))
                {
                    lblPokemonAbilityTwo.Text = _pokemonAbilityTwoUpper;
                    _fix.Put(lblPokemonAbilityTwo, 345, 85);
                }
                else if (_pokemon.Abilities.Count == 3 && !String.IsNullOrEmpty(_pokemon.Abilities[2].Ability.Name))
                {
                    lblPokemonAbilityTwo.Text = _pokemonAbilityTwoUpper;
                    _fix.Put(lblPokemonAbilityTwo, 345, 85);
                    lblPokemonAbilityThree.Text = _pokemonAbilityThreeUpper;
                    _fix.Put(lblPokemonAbilityThree, 575, 50);
                }
                else if (_pokemon.Abilities.Count == 4 && !String.IsNullOrEmpty(_pokemon.Abilities[3].Ability.Name))
                {
                    lblPokemonAbilityTwo.Text = _pokemonAbilityTwoUpper;
                    _fix.Put(lblPokemonAbilityTwo, 345, 85);
                    lblPokemonAbilityThree.Text = _pokemonAbilityThreeUpper;
                    _fix.Put(lblPokemonAbilityThree, 575, 50);
                    lblPokemonAbilityFour.Text = _pokemonAbilityFourUpper;
                    _fix.Put(lblPokemonAbilityFour, 575, 85);
                }

                lblPokemonMale = new Label(_pokemonMaleFormatted);
                _fix.Put(lblPokemonMale, 793, 110);
                lblPokemnFemale = new Label(_pokemonFemaleFormatted);
                _fix.Put(lblPokemnFemale, 905, 110);
                lblPokemonCatchRate = new Label(_pokemonCatchRate);
                _fix.Put(lblPokemonCatchRate, 897, 192);
                lblPokemonEggGroup = new Label(_pokemonEggGroup);
                _fix.Put(lblPokemonEggGroup, 830, 295);
                lblPokemonEggGroup.SetAlignment(0.5f, 0.5f);

                lblPokemonHP.Text = _pokemonHPFormatted;
                _fix.Put(lblPokemonHP, 353, 195);
                lblPokemonATK.Text = _pokemonATKFormatted;
                _fix.Put(lblPokemonATK, 417, 195);
                lblPokemonDEF.Text = _pokemonDEFFormatted;
                _fix.Put(lblPokemonDEF, 485, 195);
                lblPokemonSpATK.Text = _pokemonSpATKFormatted;
                _fix.Put(lblPokemonSpATK, 553, 195);
                lblPokemonSpDEF.Text = _pokemonSpDEFFormatted;
                _fix.Put(lblPokemonSpDEF, 621, 195);
                lblPokemonSpeed.Text = _pokemonSpeedFormatted;
                _fix.Put(lblPokemonSpeed, 689, 195);

                if (_pokeForm.IsMega)
                {
                    _megaIcon.Pixbuf = new Pixbuf("Images/pokemon_forms/MegaKeyActivated.png");
                }

                if (_pokeSpecies.IsLegendary)
                {
                    FormDesactivated.Pixbuf = new Pixbuf("Images/pokemon_forms/LegendaryIcon.png");
                }
                else if (_pokeSpecies.IsMythical)
                {
                    FormDesactivated.Pixbuf = new Pixbuf("Images/pokemon_forms/MythicalIcon.png");
                }

                _fix.Put(FormDesactivated, 61, 35);
                _fix.Put(_megaIcon, 36, 35);
                _fix.Put(gMaxIcon, 85, 35);

                #region Buttons UI

                ScrolledWindow sw = new ScrolledWindow();
                sw.ShadowType = ShadowType.EtchedIn;
                sw.SetPolicy(PolicyType.Automatic, PolicyType.Automatic);
                sw.SetSizeRequest(390, 170);
                _formsList = CreateModel();
                TreeView treeView = new(_formsList);
                treeView.RulesHint = true;
                treeView.RowActivated += OnRowActivated;
                sw.Add(treeView);
                AddColumns(treeView);

                _fix.Put(sw, 340, 295);

                _shinyButton = new ButtonGenerator("Images/buttons/shinyButtonDesactived.png", 40, 40);
                _fix.Put(_shinyButton, 138, 270);
                _shinyButton.Clicked += ShinyButtonClicked;
                _shinyButton.TooltipMarkup = "<span foreground='white' font_desc='Pixeloid Mono Regular 12'>Clique para ver a versão shiny do Pokémon</span>";

                Button btnMoves = new ButtonGenerator("Images/buttons/btnMoves.png", 192, 85);
                btnMoves.TooltipMarkup = "<span foreground='white' font_desc='Pixeloid Mono Regular 12'>Clique para ver os movimentos do Pokémon</span>";
                _fix.Put(btnMoves, 767, 393);
                btnMoves.Clicked += PokemonMoves;

                #endregion Buttons UI

                Add(_fix);

                DeleteEvent += delegate { Destroy(); };
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
            if (_formsList.GetIter(out iter, args.Path))
            {
                string value = (string)_formsList.GetValue(iter, 0);
                if (_pokemon.Forms.Count > 1)
                {
                    var d = _pokemon.Forms.FindIndex(x => x.Name == value);
                    _pokemonFormId = d;
                    string pokemonName = _pokemon.Forms[d].Name;
                    _pokeForm = await _apiRequest.GetPokemonForm(pokemonName);
                    if (VerifyType(pokemonName))
                    {
                        _pokemonTypePrimary = await _apiRequest.GetTypeAsync(_pokeForm.Name);
                        await UpdateLabels();
                    }
                    await UpdatePokemonSprite(pokemonName);
                    GetPokemonGifSize();
                    _formsList.Clear();
                    CreateModel();
                }
                else if (value.Contains("Primeira Evolução:"))
                {
                    _pokeAbility.Clear();
                    _pokemon = await _apiRequest.GetPokemon(evolutionChain.Chain.Species.Name);
                    await PopulateFields();
                    GetPokemonGifSize();
                    await UpdateLabels();
                    _formsList.Clear();
                    CreateModel();
                }
                else
                {
                    _pokeAbility.Clear();
                    string pokemonName = value;
                    _pokemon = await _apiRequest.GetPokemon(pokemonName);
                    await PopulateFields();
                    GetPokemonGifSize();
                    await UpdateLabels();
                    _formsList.Clear();
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
            _forms.AppendValues(evolutionChain.Chain.Species.Name, "Primeira Evolução");

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
                                    _forms.AppendValues($"{evo.Species.Name}", $"Metodo de Evolução: {i.Trigger.Name} Level Minimo: {i.MinLevel}");
                                }
                            }
                            else if (i.Trigger.Name == "trade")
                            {
                                _forms.AppendValues($"{evo.Species.Name}", $"Metodo de Evolução: {i.Trigger.Name}");
                            }
                            else if (i.Trigger.Name == "use-item")
                            {
                                _forms.AppendValues($"{evo.Species.Name}", $"Metodo de Evolução: {i.Trigger.Name} Item Usado: {i.Item.Name}");
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
                                if (i.Trigger.Name == "level-up")
                                {
                                    _forms.AppendValues($"{evo.Species.Name}", $"Metodo de Evolução: {i.Trigger.Name} Level Minimo: {i.MinLevel}");
                                }
                                else if (i.Trigger.Name == "trade")
                                {
                                    _forms.AppendValues($"{evo.Species.Name}", $"Metodo de Evolução: {i.Trigger.Name}");
                                }
                                else if (i.Trigger.Name == "use-item")
                                {
                                    _forms.AppendValues($"{evo.Species.Name}", $"Metodo de Evolução: {i.Trigger.Name} Item Usado: {i.Item.Name}");
                                }
                            }
                        }
                    }
                }
            }
            if (_pokemon.Forms.Count > 1)
            {
                foreach (var form in _pokemon.Forms)
                {
                    _forms.AppendValues(form.Name);
                }
            }
            if (_pokeSpecies.Varieties.Count > 1)
            {
                foreach (var poke in _pokeSpecies.Varieties)
                {
                    if (poke.IsDefault == false)
                    {
                        if (poke.Pokemon.Name.Contains("mega"))
                        {
                            _forms.AppendValues(poke.Pokemon.Name, "Mega Stone");
                        }
                        else if (poke.Pokemon.Name.Contains("gmax"))
                        {
                            _forms.AppendValues(poke.Pokemon.Name, "Dynamax");
                        }
                        else if (poke.Pokemon.Name.Contains("alola"))
                        {
                            _forms.AppendValues(poke.Pokemon.Name, "Alola");
                        }
                        else if (poke.Pokemon.Name.Contains("galar"))
                        {
                            _forms.AppendValues(poke.Pokemon.Name, "Galar");
                        }
                        else
                        {
                            _forms.AppendValues(poke.Pokemon.Name);
                        }
                    }
                }
            }
            return _forms;
        }

        private async void TranslateString()
        {
            try
            {
                if (_pokemon.Abilities.Count == 1)
                {
                    lblPokemonAbilityOneToolTip = await _apiRequest.Translate(_pokeAbility[0].EffectEntries.Last().Effect);
                    lblPokemonAbilityOne.TooltipMarkup = $"<span foreground='white' font_desc='Pixeloid Mono Regular 12'>{lblPokemonAbilityOneToolTip}</span>";
                }
                else
                {
                    lblPokemonAbilityOne.TooltipMarkup = $"<span foreground='white' font_desc='Pixeloid Mono Regular 12'>[Sem descrição da habilidade]</span>";
                }
                if (_pokemon.Abilities.Count == 2)
                {
                    if (_pokeAbility[0].EffectEntries.Count > 0)
                    {
                        lblPokemonAbilityOneToolTip = await _apiRequest.Translate(_pokeAbility[0].EffectEntries.Last().Effect);
                        lblPokemonAbilityOne.TooltipMarkup = $"<span foreground='white' font_desc='Pixeloid Mono Regular 12'>{lblPokemonAbilityOneToolTip}</span>";
                    }
                    else
                    {
                        lblPokemonAbilityOne.TooltipMarkup = $"<span foreground='white' font_desc='Pixeloid Mono Regular 12'>[Sem descrição da habilidade]</span>";
                    }
                    if (_pokeAbility[1].EffectEntries.Count > 0)
                    {
                        lblPokemonAbilityTwoToolTip = await _apiRequest.Translate(_pokeAbility[1].EffectEntries.Last().Effect);
                        lblPokemonAbilityTwo.TooltipMarkup = $"<span foreground='white' font_desc='Pixeloid Mono Regular 12'>{lblPokemonAbilityTwoToolTip}</span>";
                    }
                    else
                    {
                        lblPokemonAbilityTwo.TooltipMarkup = $"<span foreground='white' font_desc='Pixeloid Mono Regular 12'>[Sem descrição da habilidade]</span>";
                    }
                    if (_pokemon.Abilities.Count == 3)
                    {
                        if (_pokeAbility[0].EffectEntries.Count > 0)
                        {
                            lblPokemonAbilityOneToolTip = await _apiRequest.Translate(_pokeAbility[0].EffectEntries.Last().Effect);
                            lblPokemonAbilityOne.TooltipMarkup = $"<span foreground='white' font_desc='Pixeloid Mono Regular 12'> {lblPokemonAbilityOneToolTip} </span>";
                        }
                        else
                        {
                            lblPokemonAbilityOne.TooltipMarkup = $"<span foreground='white' font_desc='Pixeloid Mono Regular 12'>[Sem descrição da habilidade]</span>";
                        }
                        if (_pokeAbility[1].EffectEntries.Count > 0)
                        {
                            lblPokemonAbilityTwoToolTip = await _apiRequest.Translate(_pokeAbility[1].EffectEntries.Last().Effect);
                            lblPokemonAbilityTwo.TooltipMarkup = $"<span foreground='white' font_desc='Pixeloid Mono Regular 12'> {lblPokemonAbilityTwoToolTip} </span>";
                        }
                        else
                        {
                            lblPokemonAbilityTwo.TooltipMarkup = $"<span foreground='white' font_desc='Pixeloid Mono Regular 12'>[Sem descrição da habilidade]</span>";
                        }
                        if (_pokeAbility[2].EffectEntries.Count > 0)
                        {
                            lblPokemonAbilityThreeToolTip = await _apiRequest.Translate(_pokeAbility[2].EffectEntries.Last().Effect);
                            lblPokemonAbilityThree.TooltipMarkup = $"<span foreground='white' font_desc='Pixeloid Mono Regular 12'> {lblPokemonAbilityThreeToolTip} </span>";
                        }
                        else
                        {
                            lblPokemonAbilityThree.TooltipMarkup = $"<span foreground='white' font_desc='Pixeloid Mono Regular 12'>[Sem descrição da habilidade]</span>";
                        }
                    }
                    if (_pokemon.Abilities.Count == 4)
                    {
                        if (_pokeAbility[0].EffectEntries.Count > 0)
                        {
                            lblPokemonAbilityOneToolTip = await _apiRequest.Translate(_pokeAbility[0].EffectEntries.Last().Effect);
                            lblPokemonAbilityOne.TooltipMarkup = $"<span foreground='white' font_desc='Pixeloid Mono Regular 12'> {lblPokemonAbilityOneToolTip} </span>";
                        }
                        else
                        {
                            lblPokemonAbilityOne.TooltipMarkup = $"<span foreground='white' font_desc='Pixeloid Mono Regular 12'>[Sem descrição da habilidade]</span>";
                        }
                        if (_pokeAbility[1].EffectEntries.Count > 0)
                        {
                            lblPokemonAbilityTwoToolTip = await _apiRequest.Translate(_pokeAbility[1].EffectEntries.Last().Effect);
                            lblPokemonAbilityTwo.TooltipMarkup = $"<span foreground='white' font_desc='Pixeloid Mono Regular 12'> {lblPokemonAbilityTwoToolTip} </span>";
                        }
                        else
                        {
                            lblPokemonAbilityTwo.TooltipMarkup = $"<span foreground='white' font_desc='Pixeloid Mono Regular 12'>[Sem descrição da habilidade]</span>";
                        }
                        if (_pokeAbility[2].EffectEntries.Count > 0)
                        {
                            lblPokemonAbilityThreeToolTip = await _apiRequest.Translate(_pokeAbility[2].EffectEntries.Last().Effect);
                            lblPokemonAbilityThree.TooltipMarkup = $"<span foreground='white' font_desc='Pixeloid Mono Regular 12'> {lblPokemonAbilityThreeToolTip} </span>";
                        }
                        else
                        {
                            lblPokemonAbilityThree.TooltipMarkup = $"<span foreground='white' font_desc='Pixeloid Mono Regular 12'>[Sem descrição da habilidade]</span>";
                        }

                        if (_pokeAbility[3].EffectEntries.Count > 0)
                        {
                            lblPokemonAbilityFourToolTip = await _apiRequest.Translate(_pokeAbility[3].EffectEntries.Last().Effect);
                            lblPokemonAbilityFour.TooltipMarkup = $"<span foreground='white' font_desc='Pixeloid Mono Regular 12'> {lblPokemonAbilityFourToolTip} </span>";
                        }
                        else
                        {
                            lblPokemonAbilityFour.TooltipMarkup = $"<span foreground='white' font_desc='Pixeloid Mono Regular 12'>[Sem descrição da habilidade]</span>";
                        }
                    }
                }
                string pokemonFlavorTextReplaced = _textInfo.ToTitleCase(_pokeSpecies.FlavorTextEntries[6].FlavorText.Replace("\n", "").ToLower());
                _pokemonFlavorText = await _apiRequest.Translate(pokemonFlavorTextReplaced);
                lblPokemonName.TooltipMarkup = $"<span foreground='white' font_desc='Pixeloid Mono Regular 12'>{_pokemonFlavorText}</span>";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao traduzir a descrição da habilidade {_pokeAbility[0].Name}: {ex.Message}");
            }
        }

        private async void ShinyButtonClicked(object sender, EventArgs e)
        {
            if (_isShiny)
            {
                _shinyButton.Image = shinyButtonDesactivedImage;

                _isShiny = false;
                if (_pokemon.Forms.Count > 1)
                {
                    try
                    {
                        await _apiRequest.GetPokemonAnimatedSprite(_pokemon.Forms[_pokemonFormId].Name, _isShiny);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Erro ao carregar a sprite animada do Pokémon: {ex.Message}");
                        _pokemonAnimation.PixbufAnimation = new PixbufAnimation("Images/pokemonSpriteError.png");
                    }
                }
                else
                {
                    await _apiRequest.GetPokemonAnimatedSprite(_pokemon.Name, _isShiny);
                }
                _pokemonAnimation.PixbufAnimation = new PixbufAnimation("Images/PokemonAnimated.gif");
            }
            else
            {
                _shinyButton.Image = shinyButtonActivedImage;
                _isShiny = true;
                if (_pokemon.Forms.Count > 1)
                {
                    await _apiRequest.GetPokemonAnimatedSprite(_pokemon.Forms[_pokemonFormId].Name, _isShiny);
                }
                else
                {
                    await _apiRequest.GetPokemonAnimatedSprite(_pokemon.Name, _isShiny);
                }
                _pokemonAnimation.PixbufAnimation = new PixbufAnimation("Images/PokemonAnimatedShiny.gif");
            }
        }

        private async void PokemonMoves(object sender, EventArgs e)
        {
            Pokemon pokemonSpecie = await _apiRequest.GetPokemon(evolutionChain.Chain.Species.Name);
            List<Move> pokemonSpecieMoves = await _apiRequest.GetMoveLearnedByPokemon(pokemonSpecie);
            List<Move> pokemonMoves = await _apiRequest.GetMoveLearnedByPokemon(_pokemon);

            var MoveList = pokemonSpecieMoves.Where(pokeSpecieMove => !pokemonMoves.Any(pokeMove => pokeMove.Name == pokeSpecieMove.Name)).Concat(pokemonMoves).ToList();

            string pokemonType = _pokemon.Types[0].Type.Name;

            MovementScreen movementScreen = new(MoveList, _pokemon, pokemonSpecie, pokemonType);
            movementScreen.ShowAll();
        }

        public async Task PopulateFields()
        {
            try
            {
                _pokemonNameFormatted = _textInfo.ToTitleCase(_pokemon.Name);
                _pokemonHPFormatted = _pokemon.Stats[0].BaseStat.ToString("D3");
                _pokemonATKFormatted = _pokemon.Stats[1].BaseStat.ToString("D3");
                _pokemonDEFFormatted = _pokemon.Stats[2].BaseStat.ToString("D3");
                _pokemonSpATKFormatted = _pokemon.Stats[3].BaseStat.ToString("D3");
                _pokemonSpDEFFormatted = _pokemon.Stats[4].BaseStat.ToString("D3");
                _pokemonSpeedFormatted = _pokemon.Stats[5].BaseStat.ToString("D3");

                await Task.Run(() => GetPokemonSpecies(_pokemon.Species.Name)).ConfigureAwait(false);
                if (_pokeSpecies != null)
                {
                    evolutionChain = await _apiRequest.GetEvolutionChain(_pokeSpecies.EvolutionChain.Url);
                }

                await UpdatePokemonSprite(_pokemon.Name);

                _pokeForm = await _apiRequest.GetPokemonForm(_pokemon.Forms[0].Name);

                _pokemonDexFormatted = "#" + _pokeSpecies.Id.ToString("D4");

                foreach (var abilities in _pokemon.Abilities)
                {
                    await Task.Run(() => GetPokemonAbilitiesList(abilities.Ability.Name)).ConfigureAwait(false);
                }

                if (_pokemon.Abilities.Count == 1)
                {
                    _pokemonAbilityOneUpper = $"<{_textInfo.ToTitleCase(_pokemon.Abilities[0].Ability.Name)}>";
                }
                else if (_pokemon.Abilities.Count == 2)
                {
                    _pokemonAbilityOneUpper = $"<{_textInfo.ToTitleCase(_pokemon.Abilities[0].Ability.Name)}>";
                    _pokemonAbilityTwoUpper = $"<{_textInfo.ToTitleCase(_pokemon.Abilities[1].Ability.Name)}>";
                }
                else if (_pokemon.Abilities.Count == 3)
                {
                    _pokemonAbilityOneUpper = $"<{_textInfo.ToTitleCase(_pokemon.Abilities[0].Ability.Name)}>";
                    _pokemonAbilityTwoUpper = $"<{_textInfo.ToTitleCase(_pokemon.Abilities[1].Ability.Name)}>";
                    _pokemonAbilityThreeUpper = $"<{_textInfo.ToTitleCase(_pokemon.Abilities[2].Ability.Name)}>";
                }
                else if (_pokemon.Abilities.Count == 4)
                {
                    _pokemonAbilityOneUpper = $"<{_textInfo.ToTitleCase(_pokemon.Abilities[0].Ability.Name)}>";
                    _pokemonAbilityTwoUpper = $"<{_textInfo.ToTitleCase(_pokemon.Abilities[1].Ability.Name)}>";
                    _pokemonAbilityThreeUpper = $"<{_textInfo.ToTitleCase(_pokemon.Abilities[2].Ability.Name)}>";
                    _pokemonAbilityFourUpper = $"<{_textInfo.ToTitleCase(_pokemon.Abilities[3].Ability.Name)}>";
                }

                if (_pokemon.Types.Count == 1)
                {
                    _pokemonFirstTypeFormatted = _pokemon.Types[0].Type.Name;
                    _pokemonTypePrimary = await _apiRequest.GetTypeAsync(_pokemonFirstTypeFormatted);
                    _pokemonSecondaryTypeFormatted = "";
                }
                else
                {
                    _pokemonFirstTypeFormatted = _pokemon.Types[0].Type.Name;
                    _pokemonTypePrimary = await _apiRequest.GetTypeAsync(_pokemonFirstTypeFormatted);
                    _pokemonSecondaryTypeFormatted = _pokemon.Types[1].Type.Name;
                    _pokemonTypeSecondary = await _apiRequest.GetTypeAsync(_pokemonSecondaryTypeFormatted);
                }

                foreach (var eggGroup in _pokeSpecies.EggGroups)
                {
                    _pokemonEggGroup += _textInfo.ToTitleCase(eggGroup.Name) + "\n";
                }

                _isLoaded = true;
            }
            catch (Exception ex)
            {
                MessageDialogGenerator.ShowMessageDialog("Erro ao carregar os dados do Pokémon." + ex);
            }
        }

        private async Task GetPokemonAbilitiesList(string abilityName)
        {
            Ability ability = await _apiRequest.GetPokemonAbility(abilityName);
            _pokeAbility?.Add(ability);
        }

        private async Task GetPokemonSpecies(string PokemonName)
        {
            try
            {
                _pokeSpecies = await _apiRequest.GetPokemonSpecies(PokemonName);
                if (_pokeSpecies != null)
                {
                    _pokemonFemaleFormatted = _pokeSpecies.GenderRate switch
                    {
                        -1 => "????",
                        0 => "0,00%",
                        1 => "12,5%",
                        2 => "25,0%",
                        4 => "50,0%",
                        6 => "75,0%",
                        7 => "87,5%",
                        8 => "100%",
                        _ => _pokemonFemaleFormatted
                    };

                    _pokemonMaleFormatted = _pokeSpecies.GenderRate switch
                    {
                        -1 => "????",
                        0 => "100%",
                        1 => "87,5%",
                        2 => "75,0%",
                        4 => "50,0%",
                        6 => "25,0%",
                        7 => "12,5%",
                        8 => "0,00%",
                        _ => _pokemonMaleFormatted
                    };

                    _pokemonCatchRate = _pokeSpecies.CaptureRate switch
                    {
                        3 => "01.6%",
                        10 => "03.9%",
                        15 => "05.2%",
                        20 => "06.5%",
                        25 => "07.7%",
                        30 => "08.8%",
                        45 => "11.9%",
                        55 => "13.9%",
                        60 => "14.8%",
                        70 => "16.6%",
                        75 => "17.5%",
                        80 => "18.4%",
                        90 => "20.8%",
                        100 => "21.7%",
                        120 => "24.9%",
                        127 => "26.0%",
                        130 => "26.5%",
                        140 => "28.0%",
                        145 => "28.7%",
                        150 => "30.2%",
                        155 => "29.9%",
                        160 => "30.9%",
                        170 => "32.4%",
                        180 => "33.8%",
                        190 => "35.2%",
                        200 => "36.6%",
                        205 => "37.2%",
                        220 => "39.3%",
                        225 => "39.9%",
                        235 => "41.3%",
                        255 => "43.9%",
                        _ => _pokemonCatchRate
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
            if (!_isShiny)
            {
                await _apiRequest.GetPokemonAnimatedSprite(pokemonForm, _isShiny);
                _pokemonAnimation.PixbufAnimation = new PixbufAnimation("Images/PokemonAnimated.gif");
            }
            else if (_isShiny)
            {
                await _apiRequest.GetPokemonAnimatedSprite(pokemonForm, _isShiny);
                _pokemonAnimation.PixbufAnimation = new PixbufAnimation("Images/PokemonAnimatedShiny.gif");
            }
        }

        private void GetPokemonGifSize()
        {
            _fix.Remove(_pokemonAnimation);
            if (!_isShiny)
                _pokemonAnimation.PixbufAnimation = new PixbufAnimation("Images/PokemonAnimated.gif");
            else
                _pokemonAnimation.PixbufAnimation = new PixbufAnimation("Images/PokemonAnimatedShiny.gif");

            int x = (340 - _pokemonAnimation.PixbufAnimation.Width) / 2;
            int y = (380 - _pokemonAnimation.PixbufAnimation.Height) / 2;

            _fix.Put(_pokemonAnimation, x, y);
        }

        private async Task UpdateLabels()
        {
            lblPokemonDexNumber.Text = _pokemonDexFormatted;
            lblPokemonName.Text = _pokemonNameFormatted;
            _pokemonFirstTypeFormattedTitle = (_textInfo.ToTitleCase(_pokemonFirstTypeFormatted));

            lblPokemonAbilityOne.Text = _pokemonAbilityOneUpper;
            if (_pokeAbility != null)
            {
                if (_pokeAbility[0].EffectEntries.Count > 0)
                {
                    lblPokemonAbilityOne.TooltipMarkup = $"<span foreground='white' font_desc='Pixeloid Mono Regular 12'>[{await _apiRequest.Translate(_pokeAbility[0].EffectEntries.Last().Effect)}]</span>";
                }
                Title = $"PokéTrainer© // Pokémon tipo - {_pokemonFirstTypeFormattedTitle} // Pokémon - {_pokemonNameFormatted} [{_pokemonDexFormatted}]";
                if (_pokemon.Abilities.Count == 2)
                {
                    lblPokemonAbilityTwo.Text = _pokemonAbilityTwoUpper;
                    if (_pokeAbility[1].EffectEntries.Count > 0)
                    {
                        lblPokemonAbilityTwo.TooltipMarkup = $"<span foreground='white' font_desc='Pixeloid Mono Regular 12'>[{await _apiRequest.Translate(_pokeAbility[1].EffectEntries.Last().Effect)}]</span>";
                    }
                }
                else if (_pokemon.Abilities.Count == 3)
                {
                    lblPokemonAbilityTwo.Text = _pokemonAbilityTwoUpper;
                    if (_pokeAbility[1].EffectEntries.Count > 0)
                    {
                        lblPokemonAbilityTwo.TooltipMarkup = $"<span foreground='white' font_desc='Pixeloid Mono Regular 12'>[{await _apiRequest.Translate(_pokeAbility[1].EffectEntries.Last().Effect)}]</span>";
                    }
                    lblPokemonAbilityThree.Text = _pokemonAbilityThreeUpper;
                    if (_pokeAbility[2].EffectEntries.Count > 0)
                    {
                        lblPokemonAbilityThree.TooltipMarkup = $"<span foreground='white' font_desc='Pixeloid Mono Regular 12'>[{await _apiRequest.Translate(_pokeAbility[2].EffectEntries.Last().Effect)}]</span>";
                    }
                }
                else if (_pokemon.Abilities.Count == 4)
                {
                    lblPokemonAbilityTwo.Text = _pokemonAbilityTwoUpper;
                    if (_pokeAbility[1].EffectEntries.Count > 0)
                    {
                        lblPokemonAbilityTwo.TooltipMarkup = $"<span foreground='white' font_desc='Pixeloid Mono Regular 12'>[{await _apiRequest.Translate(_pokeAbility[1].EffectEntries.Last().Effect)}]</span>";
                    }
                    lblPokemonAbilityThree.Text = _pokemonAbilityThreeUpper;
                    if (_pokeAbility[2].EffectEntries.Count > 0)
                    {
                        lblPokemonAbilityThree.TooltipMarkup = $"<span foreground='white' font_desc='Pixeloid Mono Regular 12'>[{await _apiRequest.Translate(_pokeAbility[2].EffectEntries.Last().Effect)}]</span>";
                    }
                    lblPokemonAbilityFour.Text = _pokemonAbilityFourUpper;
                    if (_pokeAbility[3].EffectEntries.Count > 0)
                    {
                        lblPokemonAbilityFour.TooltipMarkup = $"<span foreground='white' font_desc='Pixeloid Mono Regular 12'>[{await _apiRequest.Translate(_pokeAbility[3].EffectEntries.Last().Effect)}]</span>";
                    }
                }

                if (_pokemonFirstTypeFormatted != _pokemonTypePrimary.Name)
                {
                    _pokemonFirstTypeFormatted = _pokemonTypePrimary.Name;
                    _pokemonTypeOne.Pixbuf = new Pixbuf($"Images/pokemon_types/{_pokemonFirstTypeFormatted}.png");
                    _damageRelations = GetTypeDamageRelation(_pokemonTypePrimary.Name);
                    _pokemonTypeOne.TooltipMarkup = $"<span foreground='white' font_desc='Pixeloid Mono Regular 12'>[{_damageRelations}]</span>";
                }
                if (_pokemon.Types.Count == 1)
                {
                    _imagePokemonTypeSecondary.Pixbuf = new Pixbuf("Images/pokemon_types/none.png");
                    _imagePokemonTypeSecondary.TooltipMarkup = "";
                }

                if (_pokemon.Types.Count > 1)
                {
                    _pokemonSecondaryTypeFormatted = _pokemon.Types[1].Type.Name;
                    _imagePokemonTypeSecondary.Pixbuf = new Pixbuf($"Images/pokemon_types/{_pokemonSecondaryTypeFormatted}.png");
                    _damageRelationsSecondary = GetTypeDamageRelation(_pokemonTypeSecondary.Name);
                    _imagePokemonTypeSecondary.TooltipMarkup = $"<span foreground='white' font_desc='Pixeloid Mono Regular 12'>[{_damageRelationsSecondary}]</span>";
                }

                if (_pokeForm.IsMega)
                {
                    _megaIcon.Pixbuf = new Pixbuf("Images/pokemon_forms/MegaKeyActivated.png");
                }
                else
                {
                    _megaIcon.Pixbuf = new Pixbuf("Images/pokemon_forms/MegaKeyDesactivated.png");
                }
                if (_pokeForm.FormName == "gmax")
                {
                    gMaxIcon.Pixbuf = new Pixbuf("Images/pokemon_forms/GigaMaxActivated.png");
                }
                else
                {
                    gMaxIcon.Pixbuf = new Pixbuf("Images/pokemon_forms/GigaMaxDesactived.png");
                }
                lblPokemonCatchRate.Text = _pokemonCatchRate;
                lblPokemnFemale.Text = _pokemonFemaleFormatted;
                lblPokemonMale.Text = _pokemonMaleFormatted;
                lblPokemonHP.Text = _pokemonHPFormatted;
                lblPokemonATK.Text = _pokemonATKFormatted;
                lblPokemonDEF.Text = _pokemonDEFFormatted;
                lblPokemonSpATK.Text = _pokemonSpATKFormatted;
                lblPokemonSpDEF.Text = _pokemonSpDEFFormatted;
                lblPokemonSpeed.Text = _pokemonSpeedFormatted;
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