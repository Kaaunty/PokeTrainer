using Gdk;
using Gtk;
using NAudio.CoreAudioApi;
using NowComesGtk.Utils;
using NowComesGtk.Utils.WidgetGenerators;
using PokeApi.BackEnd.Entities;
using PokeApi.BackEnd.Service;
using PokeTrainerBackEnd;
using PokeTrainerBackEndTest.Controller;
using PokeTrainerBackEndTest.Entities;
using System.Globalization;

namespace NowComesGtk.Screens
{
    public class PokemonScreen : BaseWindow
    {
#nullable disable

        private IPokemonSpriteLoaderAPI _pokemonSpriteLoaderAPI = new PokemonImageApiRequest();
        private ITranslationAPI _translationApiRequest = new GoogleTranslationApi();
        private TextInfo _textInfo = new CultureInfo("pt-BR", false).TextInfo;
        private IPokemonAPI _pokemonApiRequest = new PokeApiNetController();

        private Pokemon _pokemon = new();

        private Image _megaIcon = new("Images/pokemon_forms/MegaKeyDesactivated.png");
        private Image _imagePokemonTypeSecondary = new();
        private Image _pokemonAnimation = new();
        private Image _pokemonTypeOne = new();
        private Button _shinyButton = new();
        private Image _megaKey = new();
        private Fixed _fix = new();

        private ListStore _forms = new(typeof(string), (typeof(string)));
        private ListStore _formsList;

        private enum Column
        {
            Form,
            FormMethod
        }

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
        private string _pokemonAbilityOneUpper = "< >", _pokemonAbilityTwoUpper = "< >", _pokemonAbilityThreeUpper = "< >", _pokemonAbilityFourUpper = "< >";

        public PokemonScreen(Pokemon Pokemon, ITranslationAPI translationApi, IPokemonAPI pokemonAPI, IPokemonSpriteLoaderAPI pokemonSpriteLoaderAPI) : base("", 1000, 500)
        {
            try
            {
                _translationApiRequest = translationApi;
                _pokemon = Pokemon;
                _pokemonApiRequest = pokemonAPI;
                _pokemonSpriteLoaderAPI = pokemonSpriteLoaderAPI;
                PopulateFields();

                while (!_isLoaded)
                {
                    Task.Delay(100).Wait();
                }

                Image Background = new Image($"Images/pokemon_homescreen/{_pokemon.Types[0].Type.Name}.png");

                _fix.Put(Background, 0, 0);

                var fontDescription = Pango.FontDescription.FromString("Pixeloid Mono Regular 12");
                ModifyFont(fontDescription);

                _pokemonFirstTypeFormattedTitle = (_textInfo.ToTitleCase(_pokemon.Types[0].Type.Name));

                Title = $"PokéTrainer© // Pokémon tipo - {_pokemonFirstTypeFormattedTitle} // Pokémon - {_pokemonNameFormatted} [{_pokemonDexFormatted}]";

                _megaKey.Pixbuf = new Pixbuf("Images/pokemon_forms/MegaKeyDesactivated.png");

                _pokemonAnimation.PixbufAnimation = new PixbufAnimation("Images/PokemonAnimated.gif");

                GetPokemonGifSize();

                lblPokemonName.Text = _pokemonNameFormatted;

                TranslateString();

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
                lblPokemonAbilityTwo.Text = _pokemonAbilityTwoUpper;
                _fix.Put(lblPokemonAbilityTwo, 345, 85);
                lblPokemonAbilityThree.Text = _pokemonAbilityThreeUpper;
                _fix.Put(lblPokemonAbilityThree, 575, 50);
                lblPokemonAbilityFour.Text = _pokemonAbilityFourUpper;
                _fix.Put(lblPokemonAbilityFour, 575, 85);

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

                if (_pokemon.PokemonForms[0].IsMega)
                {
                    _megaIcon.Pixbuf = new Pixbuf("Images/pokemon_forms/MegaKeyActivated.png");
                }

                if (_pokemon.isLegendary)
                {
                    FormDesactivated.Pixbuf = new Pixbuf("Images/pokemon_forms/LegendaryIcon.png");
                }
                else if (_pokemon.isMythical)
                {
                    FormDesactivated.Pixbuf = new Pixbuf("Images/pokemon_forms/MythicalIcon.png");
                }

                FormDesactivated.TooltipMarkup = $"<span foreground='white' font_desc='Pixeloid Mono Regular 12'>Indica se o pokemon é Lendario ou Mitico</span>";
                _megaIcon.TooltipMarkup = $"<span foreground='white' font_desc='Pixeloid Mono Regular 12'>Indica se o pokemon é uma Mega Evolução</span>";
                gMaxIcon.TooltipMarkup = $"<span foreground='white' font_desc='Pixeloid Mono Regular 12'>Indica se o pokemon é uma Gigantamax</span>";
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
                treeView.EnableSearch = false;

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

                DeleteEvent += DeleteEventClick;
                ShowAll();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar os dados do Pokémon: {ex.Message}");
            }
        }

        private void DeleteEventClick(object o, DeleteEventArgs args)
        {
            Dispose();
            Destroy();
        }

        private void OnRowActivated(object o, RowActivatedArgs args)
        {
            TreeIter iter;
            string pokemonName;
            if (_formsList.GetIter(out iter, args.Path))
            {
                string form = (string)_formsList.GetValue(iter, (int)Column.Form);
                if (_pokemon.PokemonForms.Count > 1)
                {
                    _pokemonFormId = _pokemon.PokemonForms.FindIndex(pokeForm => pokeForm.Name == form);
                    if (_pokemonFormId >= 0)
                    {
                        pokemonName = _pokemon.PokemonForms[_pokemonFormId].Name;
                        string pokemonType = _pokemon.PokemonForms[_pokemonFormId].Types[0].Type.Name;

                        Pokemon pokemon = _pokemonApiRequest.GetPokemonByName(pokemonName);

                        if (pokemon != null)
                        {
                            _pokemon = pokemon;
                        }
                        else
                        {
                            if (_pokemon.Types[0].Type.Name != pokemonType)
                            {
                                _pokemonFirstTypeFormatted = pokemonType;
                                _pokemonTypeOne.Pixbuf = new Pixbuf($"Images/pokemon_types/{_pokemonFirstTypeFormatted}.png");
                                _damageRelations = GetTypeDamageRelation(_pokemon.Types[0].Type.Name);
                                _pokemonTypeOne.TooltipMarkup = $"<span foreground='white' font_desc='Pixeloid Mono Regular 12'>{_damageRelations}</span>";
                            }
                            if (_pokemon.Types.Count > 1)
                            {
                                if (_pokemon.Types[1].Type.Name != pokemon.Types[1].Type.Name)
                                {
                                    _pokemonSecondaryTypeFormatted = pokemon.Types[1].Type.Name;
                                    _imagePokemonTypeSecondary.Pixbuf = new Pixbuf($"Images/pokemon_types/{_pokemonSecondaryTypeFormatted}.png");
                                    _damageRelationsSecondary = GetTypeDamageRelation(_pokemon.Types[1].Type.Name);
                                    _imagePokemonTypeSecondary.TooltipMarkup = $"<span foreground='white' font_desc='Pixeloid Mono Regular 12'>{_damageRelationsSecondary}</span>";
                                }
                            }
                        }

                        PopulateFields();
                        UpdateLabels();
                        GetPokemonGifSize();
                        UpdatePokemonSprite(pokemonName);
                        CreateModel();
                    }
                    else
                    {
                        Pokemon pokemon = _pokemonApiRequest.GetPokemonByName(form);
                        _pokemon = pokemon;
                        PopulateFields();
                        UpdateLabels();
                        GetPokemonGifSize();
                        UpdatePokemonSprite(pokemon.Name);
                        CreateModel();
                    }
                }
                else
                {
                    Pokemon pokemon = _pokemonApiRequest.GetPokemonByName(form);
                    if (pokemon != null)
                    {
                        _pokemon = pokemon;
                    }
                    PopulateFields();
                    UpdateLabels();
                    GetPokemonGifSize();
                    UpdatePokemonSprite(pokemon.Name);
                    CreateModel();
                }
            }
        }

        public string GetTypeDamageRelation(string type)
        {
            return Repository.typeDamageRelations[type];
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
            _forms.Clear();

            if (_pokemonFormId > 0)
            {
                if (_pokemon.PokemonForms[_pokemonFormId].Name != _pokemon.EvolutionChain.Chain.Species.Name || _pokemon.Name != _pokemon.EvolutionChain.Chain.Species.Name)
                {
                    _forms.AppendValues($"{_pokemon.EvolutionChain.Chain.Species.Name}", "Forma Base");
                }
            }
            else if (_pokemon.Name != _pokemon.EvolutionChain.Chain.Species.Name)
            {
                _forms.AppendValues($"{_pokemon.Name}", "Forma Base");
            }

            if (_pokemon.EvolutionChain.Chain.EvolvesTo.Count > 0)
            {
                foreach (var evo in _pokemon.EvolutionChain.Chain.EvolvesTo)
                {
                    if (evo.Species.Name != _pokemon.Name && !ContainsItem(evo.Species.Name))
                    {
                        foreach (var i in evo.EvolutionDetails)
                        {
                            if (i != null && !ContainsItem(evo.Species.Name))
                            {
                                if (i.Trigger.Name == "level-up")
                                {
                                    if (i.MinLevel != null)
                                    {
                                        _forms.AppendValues($"{evo.Species.Name}", $"Metodo de Evolução: {i.Trigger.Name} Level Minimo: {i.MinLevel}");
                                    }
                                    else
                                    {
                                        _forms.AppendValues($"{evo.Species.Name}", $"Metodo de Evolução: {i.Trigger.Name} Sem Level Minimo");
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
                }
                if (_pokemon.EvolutionChain.Chain.EvolvesTo[0].EvolvesTo.Count > 0)
                {
                    foreach (var evo in _pokemon.EvolutionChain.Chain.EvolvesTo[0].EvolvesTo)
                    {
                        if (evo.Species.Name != _pokemon.Name && !ContainsItem(evo.Species.Name))
                        {
                            foreach (var i in evo.EvolutionDetails)
                            {
                                if (i != null && !ContainsItem(evo.Species.Name))
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
            }
            if (_pokemon.PokemonForms.Count > 1)
            {
                foreach (var form in _pokemon.PokemonForms)
                {
                    if (form.IsDefault == false)
                        _forms.AppendValues(form.Name);
                }
            }
            if (_pokemon.Varieties.Count > 1)
            {
                foreach (var poke in _pokemon.Varieties)
                {
                    if (poke.Pokemon.Name != _pokemon.Name)
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
            }

            return _forms;
        }

        private bool ContainsItem(string itemName)
        {
            TreeIter iter;
            if (_forms.GetIterFirst(out iter))
            {
                do
                {
                    string value = (string)_forms.GetValue(iter, 0);
                    if (value == itemName)
                        return true;
                } while (_forms.IterNext(ref iter));
            }
            return false;
        }

        private async void TranslateString()
        {
            try
            {
                if (_pokemon.Abilities.Count == 1)
                {
                    lblPokemonAbilityOneToolTip = await _translationApiRequest.Translate(_pokemon.Abilities[0].Effect);
                    lblPokemonAbilityOne.TooltipMarkup = $"<span foreground='white' font_desc='Pixeloid Mono Regular 12'>{lblPokemonAbilityOneToolTip}</span>";
                }
                else if (_pokemon.Abilities.Count == 2)
                {
                    lblPokemonAbilityOneToolTip = await _translationApiRequest.Translate(_pokemon.Abilities[0].Effect);
                    lblPokemonAbilityOne.TooltipMarkup = $"<span foreground='white' font_desc='Pixeloid Mono Regular 12'>{lblPokemonAbilityOneToolTip}</span>";
                    lblPokemonAbilityTwoToolTip = await _translationApiRequest.Translate(_pokemon.Abilities[1].Effect);
                    lblPokemonAbilityTwo.TooltipMarkup = $"<span foreground='white' font_desc='Pixeloid Mono Regular 12'>{lblPokemonAbilityTwoToolTip}</span>";
                }
                else if (_pokemon.Abilities.Count == 3)
                {
                    lblPokemonAbilityOneToolTip = await _translationApiRequest.Translate(_pokemon.Abilities[1].Effect);
                    lblPokemonAbilityOne.TooltipMarkup = $"<span foreground='white' font_desc='Pixeloid Mono Regular 12'> {lblPokemonAbilityOneToolTip} </span>";

                    lblPokemonAbilityTwoToolTip = await _translationApiRequest.Translate(_pokemon.Abilities[1].Effect);
                    lblPokemonAbilityTwo.TooltipMarkup = $"<span foreground='white' font_desc='Pixeloid Mono Regular 12'> {lblPokemonAbilityTwoToolTip} </span>";

                    lblPokemonAbilityThreeToolTip = await _translationApiRequest.Translate(_pokemon.Abilities[2].Effect);
                    lblPokemonAbilityThree.TooltipMarkup = $"<span foreground='white' font_desc='Pixeloid Mono Regular 12'> {lblPokemonAbilityThreeToolTip} </span>";
                }
                else if (_pokemon.Abilities.Count == 4)
                {
                    lblPokemonAbilityOneToolTip = await _translationApiRequest.Translate(_pokemon.Abilities[0].Effect);
                    lblPokemonAbilityOne.TooltipMarkup = $"<span foreground='white' font_desc='Pixeloid Mono Regular 12'> {lblPokemonAbilityOneToolTip} </span>";
                    lblPokemonAbilityTwoToolTip = await _translationApiRequest.Translate(_pokemon.Abilities[1].Effect);
                    lblPokemonAbilityTwo.TooltipMarkup = $"<span foreground='white' font_desc='Pixeloid Mono Regular 12'> {lblPokemonAbilityTwoToolTip} </span>";
                    lblPokemonAbilityThreeToolTip = await _translationApiRequest.Translate(_pokemon.Abilities[2].Effect);
                    lblPokemonAbilityThree.TooltipMarkup = $"<span foreground='white' font_desc='Pixeloid Mono Regular 12'> {lblPokemonAbilityThreeToolTip} </span>";
                    lblPokemonAbilityFourToolTip = await _translationApiRequest.Translate(_pokemon.Abilities[3].Effect);
                    lblPokemonAbilityFour.TooltipMarkup = $"<span foreground='white' font_desc='Pixeloid Mono Regular 12'> {lblPokemonAbilityFourToolTip} </span>";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao traduzir a descrição da habilidade {_pokemon.Abilities[0].Name}: {ex.Message}");
            }
        }

        private void ShinyButtonClicked(object sender, EventArgs e)
        {
            if (_isShiny)
            {
                _shinyButton.Image = shinyButtonDesactivedImage;

                _isShiny = false;
                if (_pokemon.PokemonForms.Count > 1)
                {
                    try
                    {
                        _pokemonSpriteLoaderAPI.GetPokemonAnimatedSprite(_pokemon.PokemonForms[_pokemonFormId].Name, _isShiny);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Erro ao carregar a sprite animada do Pokémon: {ex.Message}");
                        _pokemonAnimation.PixbufAnimation = new PixbufAnimation("Images/pokemonSpriteError.png");
                    }
                }
                else
                {
                    _pokemonSpriteLoaderAPI.GetPokemonAnimatedSprite(_pokemon.Name, _isShiny);
                }
                _pokemonAnimation.PixbufAnimation = new PixbufAnimation("Images/PokemonAnimated.gif");
            }
            else
            {
                _shinyButton.Image = shinyButtonActivedImage;
                _isShiny = true;
                if (_pokemon.PokemonForms.Count > 1)
                {
                    _pokemonSpriteLoaderAPI.GetPokemonAnimatedSprite(_pokemon.PokemonForms[_pokemonFormId].Name, _isShiny);
                }
                else
                {
                    _pokemonSpriteLoaderAPI.GetPokemonAnimatedSprite(_pokemon.Name, _isShiny);
                }
                _pokemonAnimation.PixbufAnimation = new PixbufAnimation("Images/PokemonAnimatedShiny.gif");
            }
        }

        private void PokemonMoves(object sender, EventArgs e)
        {
            Pokemon pokemonSpecie = _pokemonApiRequest.GetPokemonByName(_pokemon.EvolutionChain.Chain.Species.Name);

            List<Move> pokemonSpecieMoves = pokemonSpecie.Moves;
            List<Move> pokemonMoves = _pokemon.Moves;

            var MoveList = pokemonSpecieMoves.Where(pokeSpecieMove => !pokemonMoves.Any(pokeMove => pokeMove.Name == pokeSpecieMove.Name)).Concat(pokemonMoves).ToList();

            string pokemonType = _pokemon.Types[0].Type.Name;

            MovementScreen movementScreen = new(MoveList, pokemonType);
            movementScreen.ShowAll();
        }

        public void PopulateFields()
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

                UpdatePokemonSprite(_pokemon.Name);

                _pokemonDexFormatted = "#" + _pokemon.Id.ToString("D4");

                _pokemonFemaleFormatted = _pokemon.GenderRate switch
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

                _pokemonMaleFormatted = _pokemon.GenderRate switch
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

                _pokemonCatchRate = _pokemon.CaptureRate switch
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

                if (_pokemon.Abilities.Count == 1)
                {
                    _pokemonAbilityOneUpper = $"<{_textInfo.ToTitleCase(_pokemon.Abilities[0].Name)}>";
                }
                else if (_pokemon.Abilities.Count == 2)
                {
                    _pokemonAbilityOneUpper = $"<{_textInfo.ToTitleCase(_pokemon.Abilities[0].Name)}>";
                    _pokemonAbilityTwoUpper = $"<{_textInfo.ToTitleCase(_pokemon.Abilities[1].Name)}>";
                }
                else if (_pokemon.Abilities.Count == 3)
                {
                    _pokemonAbilityOneUpper = $"<{_textInfo.ToTitleCase(_pokemon.Abilities[0].Name)}>";
                    _pokemonAbilityTwoUpper = $"<{_textInfo.ToTitleCase(_pokemon.Abilities[1].Name)}>";
                    _pokemonAbilityThreeUpper = $"<{_textInfo.ToTitleCase(_pokemon.Abilities[2].Name)}>";
                }
                else if (_pokemon.Abilities.Count == 4)
                {
                    _pokemonAbilityOneUpper = $"<{_textInfo.ToTitleCase(_pokemon.Abilities[0].Name)}>";
                    _pokemonAbilityTwoUpper = $"<{_textInfo.ToTitleCase(_pokemon.Abilities[1].Name)}>";
                    _pokemonAbilityThreeUpper = $"<{_textInfo.ToTitleCase(_pokemon.Abilities[2].Name)}>";
                    _pokemonAbilityFourUpper = $"<{_textInfo.ToTitleCase(_pokemon.Abilities[3].Name)}>";
                }

                if (_pokemon.Types.Count == 1)
                {
                    _pokemonFirstTypeFormatted = _pokemon.Types[0].Type.Name;
                    _pokemonSecondaryTypeFormatted = "";
                }
                else
                {
                    _pokemonFirstTypeFormatted = _pokemon.Types[0].Type.Name;
                    _pokemonSecondaryTypeFormatted = _pokemon.Types[1].Type.Name;
                }

                foreach (var eggGroup in _pokemon.EggGroups)
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

        private void UpdatePokemonSprite(string pokemonForm)
        {
            if (!_isShiny)
            {
                _pokemonSpriteLoaderAPI.GetPokemonAnimatedSprite(pokemonForm, _isShiny);
                _pokemonAnimation.PixbufAnimation = new PixbufAnimation("Images/PokemonAnimated.gif");
            }
            else if (_isShiny)
            {
                _pokemonSpriteLoaderAPI.GetPokemonAnimatedSprite(pokemonForm, _isShiny);
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

        private void UpdateLabels()
        {
            lblPokemonDexNumber.Text = _pokemonDexFormatted;
            lblPokemonName.Text = _pokemonNameFormatted;
            _pokemonFirstTypeFormattedTitle = (_textInfo.ToTitleCase(_pokemonFirstTypeFormatted));

            TranslateString();

            if (_pokemon.Abilities.Count == 1)
            {
                lblPokemonAbilityOne.Text = _pokemonAbilityOneUpper;
            }
            else if (_pokemon.Abilities.Count == 2)
            {
                lblPokemonAbilityOne.Text = _pokemonAbilityOneUpper;
                lblPokemonAbilityTwo.Text = _pokemonAbilityTwoUpper;
            }
            else if (_pokemon.Abilities.Count == 3)
            {
                lblPokemonAbilityOne.Text = _pokemonAbilityOneUpper;
                lblPokemonAbilityTwo.Text = _pokemonAbilityTwoUpper;
                lblPokemonAbilityThree.Text = _pokemonAbilityThreeUpper;
            }
            else if (_pokemon.Abilities.Count == 4)
            {
                lblPokemonAbilityOne.Text = _pokemonAbilityOneUpper;
                lblPokemonAbilityTwo.Text = _pokemonAbilityTwoUpper;
                lblPokemonAbilityThree.Text = _pokemonAbilityThreeUpper;
                lblPokemonAbilityFour.Text = _pokemonAbilityFourUpper;
            }

            if (_pokemon.Types.Count == 1)
            {
                _imagePokemonTypeSecondary.Pixbuf = new Pixbuf("Images/pokemon_types/none.png");
                _imagePokemonTypeSecondary.TooltipMarkup = "";
            }
            else if (_pokemon.Types.Count > 1)
            {
                _pokemonSecondaryTypeFormatted = _pokemon.Types[1].Type.Name;
                _imagePokemonTypeSecondary.Pixbuf = new Pixbuf($"Images/pokemon_types/{_pokemonSecondaryTypeFormatted}.png");
                _damageRelationsSecondary = GetTypeDamageRelation(_pokemon.Types[1].Type.Name);
                _imagePokemonTypeSecondary.TooltipMarkup = $"<span foreground='white' font_desc='Pixeloid Mono Regular 12'>[{_damageRelationsSecondary}]</span>";
            }
            if (_pokemonFormId >= 0)
            {
                if (_pokemon.PokemonForms[_pokemonFormId].IsMega)
                {
                    _megaIcon.Pixbuf = new Pixbuf("Images/pokemon_forms/MegaKeyActivated.png");
                }
                else
                {
                    _megaIcon.Pixbuf = new Pixbuf("Images/pokemon_forms/MegaKeyDesactivated.png");
                }
                if (_pokemon.PokemonForms[_pokemonFormId].Name.Contains("gmax"))
                {
                    gMaxIcon.Pixbuf = new Pixbuf("Images/pokemon_forms/GigaMaxActivated.png");
                }
                else
                {
                    gMaxIcon.Pixbuf = new Pixbuf("Images/pokemon_forms/GigaMaxDesactived.png");
                }
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
}