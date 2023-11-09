using NowComesGtk.Utils.WidgetGenerators;
using PokeTrainerBackEndTest.Controller;
using PokeTrainerBackEndTest.Entities;
using PokeApi.BackEnd.Entities;
using PokeApi.BackEnd.Service;
using NowComesGtk.Presenters;
using System.Globalization;
using NowComesGtk.Utils;
using Gtk;

namespace NowComesGtk.Screens
{
    public class PokedexScreen : BaseWindow
    {
#nullable disable

        private readonly IPokemonAPI _pokemonAPI = new PokeApiNetController();
        private TextInfo _textInfo = new CultureInfo("pt-BR", false).TextInfo;
        private PokedexScreenFeatures _screenFeatures = new();

        private Entry _txtSearchPokemon = new TextBoxGenerator("Buscar Pokémon", 125, 20);
        private ComboBox _cbTypePokemon = new();
        private Button _btnNext = new(">>");
        private Button _btnBack = new("<<");
        private Fixed _fix = new();
        private enum Choice
        {
            All,
            PureType,
            PrimaryType,
            SecondaryType
        }

        private int _choice = 0, _currentPage = 0, _maxPokemonPerPage = 25;
        private string _pokemonType = "", _pokemonTypeFormatted = "";

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

        public PokedexScreen(string pokemonType, ITranslationAPI translationAPI, IPokemonAPI pokemonAPI) : base($"PokéTrainer© // Pokémons tipo - {pokemonType} // Pokémons", 500, 600)
        {
            _pokemonType = pokemonType;
            _pokemonAPI = pokemonAPI;
            _pokemonTypeFormatted = translationAPI.TranslateType(_textInfo.ToTitleCase(_pokemonType));

            Title = $"PokéTrainer© // Pokémons tipo - {_pokemonTypeFormatted} // Pokémons";
            Image background = new($"Images/pokedex_homescreen/{pokemonType}.png");
            _fix.Put(background, 0, 0);

            _fix.Put(_txtSearchPokemon, 165, 25);

            _txtSearchPokemon.Changed += delegate
            {
                SearchPokemon();
            };
            _fix.Put(_btnBack, 25, 74);
            _btnBack.Sensitive = false;
            _btnBack.Name = "BackButton";
            _btnBack.SetSizeRequest(10, 10);
            _btnBack.Clicked += btnBack_Clicked;

            _fix.Put(_btnNext, 425, 74);
            _btnNext.Name = "NextButton";
            _btnNext.SetSizeRequest(10, 10);
            _btnNext.Clicked += btnNext_Clicked;

            #region Pokeball buttons settings

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

            #endregion Pokeball buttons settings

            if (pokemonType != "all")
            {
                _fix.Put(_cbTypePokemon, 175, 60);
                ListStore typeList = new(typeof(string));
                typeList.AppendValues("Todos");
                typeList.AppendValues($"Puro tipo {_pokemonTypeFormatted}");
                typeList.AppendValues($"Meio tipo primário");
                typeList.AppendValues($"Meio tipo secundário");
                _cbTypePokemon.Model = typeList;
                _cbTypePokemon.Active = 0;

                CellRendererText cell = new();
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
                            _choice = (int)Choice.All;
                            Filtering();
                            _screenFeatures.DisableNextButton(_btnNext);
                        }
                        else if (typeSelected == $"Puro tipo {_pokemonTypeFormatted}")
                        {
                            _currentPage = 0;
                            _choice = (int)Choice.PureType;
                            Filtering();
                            _screenFeatures.DisableNextButton(_btnNext);
                        }
                        else if (typeSelected == $"Meio tipo primário")
                        {
                            _currentPage = 0;
                            _choice = (int)Choice.PrimaryType;
                            Filtering();
                            _screenFeatures.DisableNextButton(_btnNext);
                        }
                        else if (typeSelected == $"Meio tipo secundário")
                        {
                            _currentPage = 0;
                            _choice = (int)Choice.SecondaryType;
                            Filtering();
                            _screenFeatures.DisableNextButton(_btnNext);
                        }
                    }
                };
            }

            _screenFeatures.Filter(_fix, _currentPage, _pokemonType, _choice, _txtSearchPokemon.Text.ToLower());

            DeleteEvent += delegate { Destroy(); };
            Add(_fix);
            ShowAll();
        }

        private void SearchPokemon()
        {
            string PokemonName = _txtSearchPokemon.Text.ToLower();
            PokemonName = PokemonName.Replace(' ', '-');
            _screenFeatures.Filter(_fix, _currentPage, _pokemonType, _choice, PokemonName);
        }

        private void btnNext_Clicked(object sender, EventArgs e)
        {
            _currentPage += _maxPokemonPerPage;
            _screenFeatures.Filter(_fix, _currentPage, _pokemonType, _choice, _txtSearchPokemon.Text.ToLower());
            _screenFeatures.DisableNextButton(_btnNext);
            _btnBack.Sensitive = true;
        }

        private void btnBack_Clicked(object sender, EventArgs e)
        {
            _currentPage -= _maxPokemonPerPage;
            _screenFeatures.Filter(_fix, _currentPage, _pokemonType, _choice, _txtSearchPokemon.Text.ToLower());
            _screenFeatures.DisableBackButton(_btnBack);
            _btnNext.Sensitive = true;
        }

        private void OpenPokemonScreenClicked(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            var pokemonName = (string)btn.Data["name"];
            if (pokemonName != string.Empty)
            {
                Pokemon pokemonClicked = _pokemonAPI.GetPokemonByName(pokemonName);
                PokemonScreen pokemonScreen = new(pokemonClicked, new GoogleTranslationApi(), new PokeApiNetController(), new PokemonImageApiRequest());
                pokemonScreen.Show();
            }
            else
            {
                MessageDialog messageDialog = new(this, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, "Esse pokémon ainda não foi implementado!");
                messageDialog.Run();
                messageDialog.Destroy();
            }
        }

        private void Filtering()
        {
            _btnNext.Sensitive = true;
            _screenFeatures.FilterigForDisplay(_currentPage, _pokemonType, _choice);
            _screenFeatures.Filter(_fix, _currentPage, _pokemonType, _choice, _txtSearchPokemon.Text.ToLower());
        }
    }
}