using Gtk;
using NowComesGtk.Reusable_components;
using NowComesGtk.Utils;
using PokeApi.BackEnd.Entities;
using PokeApi.BackEnd.Service;
using PokeApiNet;
using System.Globalization;

namespace NowComesGtk.Screens
{
    public class PokedexScreen : BaseWindow
    {
#nullable disable

        private TextInfo _textInfo = new CultureInfo("pt-BR", false).TextInfo;
        private ComboBox _cbTypePokemon = new ComboBox();
        private Entry _txtSearchPokemon = new();

        private Methods _methods = new();
        private Button _btnNext = new();
        private Fixed _fix = new();
        private IPokemonAPI _pokemonAPI = new PokemonApiRequest();
        private string _TypeFormatted = "";
        private int _currentPage = 0;
        private int _choice = 0;
        private string _type = "";

        #region Pokeball buttons

        public Button pokeball1 = new ButtonGenerator("Images/pokeball.png", 50, 50);
        public Button pokeball2 = new ButtonGenerator("Images/pokeball.png", 50, 50);
        public Button pokeball3 = new ButtonGenerator("Images/pokeball.png", 50, 50);
        public Button pokeball4 = new ButtonGenerator("Images/pokeball.png", 50, 50);
        public Button pokeball5 = new ButtonGenerator("Images/pokeball.png", 50, 50);
        public Button pokeball6 = new ButtonGenerator("Images/pokeball.png", 50, 50);
        public Button pokeball7 = new ButtonGenerator("Images/pokeball.png", 50, 50);
        public Button pokeball8 = new ButtonGenerator("Images/pokeball.png", 50, 50);
        public Button pokeball9 = new ButtonGenerator("Images/pokeball.png", 50, 50);
        public Button pokeball10 = new ButtonGenerator("Images/pokeball.png", 50, 50);
        public Button pokeball11 = new ButtonGenerator("Images/pokeball.png", 50, 50);
        public Button pokeball12 = new ButtonGenerator("Images/pokeball.png", 50, 50);
        public Button pokeball13 = new ButtonGenerator("Images/pokeball.png", 50, 50);
        public Button pokeball14 = new ButtonGenerator("Images/pokeball.png", 50, 50);
        public Button pokeball15 = new ButtonGenerator("Images/pokeball.png", 50, 50);
        public Button pokeball16 = new ButtonGenerator("Images/pokeball.png", 50, 50);
        public Button pokeball17 = new ButtonGenerator("Images/pokeball.png", 50, 50);
        public Button pokeball18 = new ButtonGenerator("Images/pokeball.png", 50, 50);
        public Button pokeball19 = new ButtonGenerator("Images/pokeball.png", 50, 50);
        public Button pokeball20 = new ButtonGenerator("Images/pokeball.png", 50, 50);
        public Button pokeball21 = new ButtonGenerator("Images/pokeball.png", 50, 50);
        public Button pokeball22 = new ButtonGenerator("Images/pokeball.png", 50, 50);
        public Button pokeball23 = new ButtonGenerator("Images/pokeball.png", 50, 50);
        public Button pokeball24 = new ButtonGenerator("Images/pokeball.png", 50, 50);
        public Button pokeball25 = new ButtonGenerator("Images/pokeball.png", 50, 50);

        #endregion Pokeball buttons

        public PokedexScreen(string type, ITranslationAPI translationAPI, IPokemonAPI pokemonAPI) : base($"PokéTrainer© // Pokémons tipo - {type} // Pokémons", 500, 600)
        {
            this._type = type;

            //if (type == "bug")
            //{
            //    _TypeFormatted = "Inseto";
            //}
            _pokemonAPI = pokemonAPI;
            _TypeFormatted = translationAPI.TranslateType(_textInfo.ToTitleCase(_type));

            Title = $"PokéTrainer© // Pokémons tipo - {_TypeFormatted} // Pokémons";
            Image backgroundScreen = new($"Images/pokedex_homescreen/{type}.png");
            _fix.Put(backgroundScreen, 0, 0);

            string defaultText = "Buscar Pokémon";
            _txtSearchPokemon.SetSizeRequest(125, 20);
            _fix.Put(_txtSearchPokemon, 165, 25);
            _txtSearchPokemon.Text = defaultText;

            if (!string.IsNullOrEmpty(_txtSearchPokemon.Text))
            {
                _txtSearchPokemon.Changed += SearchPokemon;
            }
            CssProvider cssProvider = new();
            cssProvider.LoadFromData("entry { color: rgb(200, 200, 200); }");
            _txtSearchPokemon.StyleContext.AddProvider(cssProvider, StyleProviderPriority.Application);

            _txtSearchPokemon.FocusInEvent += (sender, e) =>
            {
                _txtSearchPokemon.Text = "";
                CssProvider cssProvider = new();
                cssProvider.LoadFromData("entry { color: rgb(0, 0, 0); }");
                _txtSearchPokemon.StyleContext.AddProvider(cssProvider, StyleProviderPriority.Application);
            };
            _txtSearchPokemon.FocusOutEvent += (sender, e) =>
            {
                _txtSearchPokemon.Text = defaultText;
                CssProvider cssProvider = new();
                cssProvider.LoadFromData("entry { color: rgb(200, 200, 200); }");
                _txtSearchPokemon.StyleContext.AddProvider(cssProvider, StyleProviderPriority.Application);
            };

            Button btnBack = new("<<");
            btnBack.SetSizeRequest(10, 10);
            _fix.Put(btnBack, 25, 74);
            btnBack.Clicked += btnBack_Clicked;
            _btnNext = new Button(">>");
            _btnNext.SetSizeRequest(10, 10);
            _fix.Put(_btnNext, 425, 74);
            _btnNext.Clicked += btnNext_Clicked;

            #region Buttons

            #region First row of button

            // Pokeball 1
            pokeball1.Name = "pokemon1";
            pokeball1.Clicked += OpenPokemonScreenClicked;
            _fix.Put(pokeball1, 72, 177);
            // Pokeball 2
            pokeball2.Name = "pokemon2";
            pokeball2.Clicked += OpenPokemonScreenClicked;
            _fix.Put(pokeball2, 146, 177);
            // Pokeball 3
            pokeball3.Name = "pokemon3";
            pokeball3.Clicked += OpenPokemonScreenClicked;
            _fix.Put(pokeball3, 219, 177);
            // Pokeball 4
            pokeball4.Name = "pokemon4";
            pokeball4.Clicked += OpenPokemonScreenClicked;
            _fix.Put(pokeball4, 292, 177);
            // Pokeball 5
            pokeball5.Name = "pokemon5";
            pokeball5.Clicked += OpenPokemonScreenClicked;
            _fix.Put(pokeball5, 365, 177);

            #endregion First row of button

            #region Second button row

            // Pokeball 6
            pokeball6.Name = "pokemon6";
            pokeball6.Clicked += OpenPokemonScreenClicked;
            _fix.Put(pokeball6, 72, 249);
            // Pokeball 7
            pokeball7.Name = "pokemon7";
            pokeball7.Clicked += OpenPokemonScreenClicked;
            _fix.Put(pokeball7, 146, 249);
            // Pokeball 8
            pokeball8.Name = "pokemon8";
            pokeball8.Clicked += OpenPokemonScreenClicked;
            _fix.Put(pokeball8, 219, 249);
            // Pokeball 9
            pokeball9.Name = "pokemon9";
            pokeball9.Clicked += OpenPokemonScreenClicked;
            _fix.Put(pokeball9, 292, 249);
            // Pokeball 10

            pokeball10.Name = "pokemon10";
            pokeball10.Clicked += OpenPokemonScreenClicked;
            _fix.Put(pokeball10, 365, 249);

            #endregion Second button row

            #region Third button row

            // Pokeball 11
            pokeball11.Name = "pokemon11";
            pokeball11.Clicked += OpenPokemonScreenClicked;
            _fix.Put(pokeball11, 72, 323);
            // Pokeball 12

            pokeball12.Name = "pokemon12";
            pokeball12.Clicked += OpenPokemonScreenClicked;
            _fix.Put(pokeball12, 146, 323);
            // Pokeball 13

            pokeball13.Name = "pokemon13";
            pokeball13.Clicked += OpenPokemonScreenClicked;
            _fix.Put(pokeball13, 219, 323);
            // Pokeball 14
            pokeball14.Name = "pokemon14";
            pokeball14.Clicked += OpenPokemonScreenClicked;
            _fix.Put(pokeball14, 292, 323);
            // Pokeball 15
            pokeball15.Name = "pokemon15";
            pokeball15.Clicked += OpenPokemonScreenClicked;
            _fix.Put(pokeball15, 365, 323);

            #endregion Third button row

            #region Fourth button row

            // Pokeball 16
            pokeball16.Name = "pokemon16";
            pokeball16.Clicked += OpenPokemonScreenClicked;
            _fix.Put(pokeball16, 72, 395);
            // Pokeball 17
            pokeball17.Name = "pokemon17";
            pokeball17.Clicked += OpenPokemonScreenClicked;
            _fix.Put(pokeball17, 146, 395);
            // Pokeball 18
            pokeball18.Name = "pokemon18";
            pokeball18.Clicked += OpenPokemonScreenClicked;
            _fix.Put(pokeball18, 219, 395);
            // Pokeball 19
            pokeball19.Name = "pokemon19";
            pokeball19.Clicked += OpenPokemonScreenClicked;
            _fix.Put(pokeball19, 292, 395);
            // Pokeball 20
            pokeball20.Name = "pokemon20";
            pokeball20.Clicked += OpenPokemonScreenClicked;
            _fix.Put(pokeball20, 365, 395);

            #endregion Fourth button row

            #region Fifth button row

            // Pokeball 21
            pokeball21.Name = "pokemon21";
            pokeball21.Clicked += OpenPokemonScreenClicked;
            _fix.Put(pokeball21, 72, 468);
            // Pokeball 22
            pokeball22.Name = "pokemon22";
            pokeball22.Clicked += OpenPokemonScreenClicked;
            _fix.Put(pokeball22, 146, 468);
            // Pokeball 23
            pokeball23.Name = "pokemon23";
            pokeball23.Clicked += OpenPokemonScreenClicked;
            _fix.Put(pokeball23, 219, 468);
            // Pokeball 24
            pokeball24.Name = "pokemon24";
            pokeball24.Clicked += OpenPokemonScreenClicked;
            _fix.Put(pokeball24, 292, 468);
            // Pokeball 25
            pokeball25.Name = "pokemon25";
            pokeball25.Clicked += OpenPokemonScreenClicked;
            _fix.Put(pokeball25, 365, 468);

            #endregion Fifth button row

            #endregion Buttons

            if (type != "all")
            {
                _fix.Put(_cbTypePokemon, 175, 60);
                ListStore typeList = new ListStore(typeof(string));
                typeList.AppendValues("Todos");
                typeList.AppendValues($"Puro tipo {_TypeFormatted}");
                typeList.AppendValues($"Meio tipo primário");
                typeList.AppendValues($"Meio tipo secundário");
                _cbTypePokemon.Model = typeList;
                _cbTypePokemon.Active = 0;

                CellRendererText cell = new CellRendererText();
                _cbTypePokemon.PackStart(cell, false);
                _cbTypePokemon.AddAttribute(cell, "text", 0);

                _cbTypePokemon.Changed += (sender, e) =>
                {
                    TreeIter searchByType;
                    if (_cbTypePokemon.GetActiveIter(out searchByType))
                    {
                        var typeSelected = (string)typeList.GetValue(searchByType, 0);
                        if (typeSelected == "Todos")
                        {
                            _currentPage = 0;
                            _choice = 0;
                            AllTypeClicked();
                            _methods.DisableButtons(_btnNext);
                        }
                        else if (typeSelected == $"Puro tipo {_TypeFormatted}")
                        {
                            _currentPage = 0;
                            _choice = 1;
                            AllTypeClicked();
                            _methods.DisableButtons(_btnNext);
                        }
                        else if (typeSelected == $"Meio tipo primário")
                        {
                            _currentPage = 0;
                            _choice = 2;
                            AllTypeClicked();
                            _methods.DisableButtons(_btnNext);
                        }
                        else if (typeSelected == $"Meio tipo secundário")
                        {
                            _currentPage = 0;
                            _choice = 3;
                            AllTypeClicked();
                            _methods.DisableButtons(_btnNext);
                        }
                    }
                };
            }

            _methods.UpdateButtons(_fix, _currentPage, type, _choice);
            Add(_fix);
            DeleteEvent += delegate { Destroy(); };
            ShowAll();
        }

        private void SearchPokemon(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(_txtSearchPokemon.Text))
            {
                if (_txtSearchPokemon.Text != "" && _txtSearchPokemon.Text != "Buscar Pokémon")
                {
                    string PokemonName = _txtSearchPokemon.Text.ToLower();
                    PokemonName = PokemonName.Replace(' ', '-');
                    _methods.SearchPokemonName(_fix, _currentPage, _type, _choice, PokemonName);
                }
            }
            else
            {
                _methods.UpdateButtons(_fix, _currentPage, _type, _choice);
            }
        }

        private void btnNext_Clicked(object sender, EventArgs e)
        {
            _currentPage += 25;
            _methods.UpdateButtons(_fix, _currentPage, _type, _choice);
            _methods.DisableButtons(_btnNext);
        }

        private void btnBack_Clicked(object sender, EventArgs e)
        {
            _currentPage -= 25;
            _methods.UpdateButtons(_fix, _currentPage, _type, _choice);
            _btnNext.Sensitive = true;
        }

        private void OpenPokemonScreenClicked(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            var pokemonName = (string)btn.Data["name"];
            if (pokemonName != string.Empty)
            {
                Pokemon pokemonClicked = _pokemonAPI.GetPokemonByName(pokemonName);
                PokemonScreen pokemonScreen = new(pokemonClicked);
                pokemonScreen.Show();
            }
            else
            {
                MessageDialog messageDialog = new(this, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, "Esse pokémon ainda não foi implementado!");
                messageDialog.Run();
                messageDialog.Destroy();
            }
        }

        private void AllTypeClicked()
        {
            _btnNext.Sensitive = true;
            _methods.LoadPokemonList(_currentPage, _type, _choice, new PokemonApiRequest());
            _methods.UpdateButtons(_fix, _currentPage, _type, _choice);
        }
    }
}