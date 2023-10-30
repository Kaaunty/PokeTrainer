using NowComesGtk.Reusable_components;
using PokeApi.BackEnd.Service;
using System.Globalization;
using NowComesGtk.Utils;
using PokeApiNet;
using Gtk;

namespace NowComesGtk.Screens
{
    public class PokedexScreen : BaseWindow
    {
#nullable disable

        private TextInfo textInfo = new CultureInfo("pt-BR", false).TextInfo;
        private ApiRequest _apiRequest = new();
        private Entry txtSearchPokemon = new();
        private Methods _methods = new();
        private Fixed fix = new();
        private Button btnNext = new();
        private ComboBox cbTypePokemon = new ComboBox();

        private string TypeFormatted = "";
        private int currentPage = 0;
        private int choice = 0;
        private string type = "";

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

        public PokedexScreen(string type) : base($"PokéTrainer© // Pokémons tipo - {type} // Pokémons", 500, 600)
        {
            this.type = type;
            TypeFormatted = _apiRequest.TranslateType(textInfo.ToTitleCase(type));
            if (type == "bug")
            {
                TypeFormatted = "Inseto";
            }

            Title = $"PokéTrainer© // Pokémons tipo - {TypeFormatted} // Pokémons";
            Image backgroundScreen = new Image($"Images/pokedex_homescreen/{type}.png");
            fix.Put(backgroundScreen, 0, 0);

            string defaultText = "Buscar Pokémon";
            txtSearchPokemon.SetSizeRequest(125, 20);
            fix.Put(txtSearchPokemon, 168, 15);
            txtSearchPokemon.Text = defaultText;

            if (!string.IsNullOrEmpty(txtSearchPokemon.Text))
            {
                txtSearchPokemon.Changed += SearchPokemon;
            }
            CssProvider cssProvider = new CssProvider();
            cssProvider.LoadFromData("entry { color: rgb(200, 200, 200); }");
            txtSearchPokemon.StyleContext.AddProvider(cssProvider, StyleProviderPriority.Application);

            txtSearchPokemon.FocusInEvent += (sender, e) =>
            {
                txtSearchPokemon.Text = "";
                CssProvider cssProvider = new CssProvider();
                cssProvider.LoadFromData("entry { color: rgb(0, 0, 0); }");
                txtSearchPokemon.StyleContext.AddProvider(cssProvider, StyleProviderPriority.Application);
            };
            txtSearchPokemon.FocusOutEvent += (sender, e) =>
            {
                txtSearchPokemon.Text = defaultText;
                CssProvider cssProvider = new CssProvider();
                cssProvider.LoadFromData("entry { color: rgb(200, 200, 200); }");
                txtSearchPokemon.StyleContext.AddProvider(cssProvider, StyleProviderPriority.Application);
            };

            Button btnBack = new Button("<<");
            btnBack.SetSizeRequest(10, 10);
            fix.Put(btnBack, 25, 74);
            btnBack.Clicked += btnBack_Clicked;
            btnNext = new Button(">>");
            btnNext.SetSizeRequest(10, 10);
            fix.Put(btnNext, 425, 74);
            btnNext.Clicked += btnNext_Clicked;

            #region Buttons

            #region First row of button

            // Pokeball 1
            pokeball1.Name = "pokemon1";
            pokeball1.Clicked += OpenPokemonScreenClicked;
            fix.Put(pokeball1, 72, 177);
            // Pokeball 2
            pokeball2.Name = "pokemon2";
            pokeball2.Clicked += OpenPokemonScreenClicked;
            fix.Put(pokeball2, 146, 177);
            // Pokeball 3
            pokeball3.Name = "pokemon3";
            pokeball3.Clicked += OpenPokemonScreenClicked;
            fix.Put(pokeball3, 219, 177);
            // Pokeball 4
            pokeball4.Name = "pokemon4";
            pokeball4.Clicked += OpenPokemonScreenClicked;
            fix.Put(pokeball4, 292, 177);
            // Pokeball 5
            pokeball5.Name = "pokemon5";
            pokeball5.Clicked += OpenPokemonScreenClicked;
            fix.Put(pokeball5, 365, 177);

            #endregion First row of button

            #region Second button row

            // Pokeball 6
            pokeball6.Name = "pokemon6";
            pokeball6.Clicked += OpenPokemonScreenClicked;
            fix.Put(pokeball6, 72, 249);
            // Pokeball 7
            pokeball7.Name = "pokemon7";
            pokeball7.Clicked += OpenPokemonScreenClicked;
            fix.Put(pokeball7, 146, 249);
            // Pokeball 8
            pokeball8.Name = "pokemon8";
            pokeball8.Clicked += OpenPokemonScreenClicked;
            fix.Put(pokeball8, 219, 249);
            // Pokeball 9
            pokeball9.Name = "pokemon9";
            pokeball9.Clicked += OpenPokemonScreenClicked;
            fix.Put(pokeball9, 292, 249);
            // Pokeball 10

            pokeball10.Name = "pokemon10";
            pokeball10.Clicked += OpenPokemonScreenClicked;
            fix.Put(pokeball10, 365, 249);

            #endregion Second button row

            #region Third button row

            // Pokeball 11
            pokeball11.Name = "pokemon11";
            pokeball11.Clicked += OpenPokemonScreenClicked;
            fix.Put(pokeball11, 72, 323);
            // Pokeball 12

            pokeball12.Name = "pokemon12";
            pokeball12.Clicked += OpenPokemonScreenClicked;
            fix.Put(pokeball12, 146, 323);
            // Pokeball 13

            pokeball13.Name = "pokemon13";
            pokeball13.Clicked += OpenPokemonScreenClicked;
            fix.Put(pokeball13, 219, 323);
            // Pokeball 14
            pokeball14.Name = "pokemon14";
            pokeball14.Clicked += OpenPokemonScreenClicked;
            fix.Put(pokeball14, 292, 323);
            // Pokeball 15
            pokeball15.Name = "pokemon15";
            pokeball15.Clicked += OpenPokemonScreenClicked;
            fix.Put(pokeball15, 365, 323);

            #endregion Third button row

            #region Fourth button row

            // Pokeball 16
            pokeball16.Name = "pokemon16";
            pokeball16.Clicked += OpenPokemonScreenClicked;
            fix.Put(pokeball16, 72, 395);
            // Pokeball 17
            pokeball17.Name = "pokemon17";
            pokeball17.Clicked += OpenPokemonScreenClicked;
            fix.Put(pokeball17, 146, 395);
            // Pokeball 18
            pokeball18.Name = "pokemon18";
            pokeball18.Clicked += OpenPokemonScreenClicked;
            fix.Put(pokeball18, 219, 395);
            // Pokeball 19
            pokeball19.Name = "pokemon19";
            pokeball19.Clicked += OpenPokemonScreenClicked;
            fix.Put(pokeball19, 292, 395);
            // Pokeball 20
            pokeball20.Name = "pokemon20";
            pokeball20.Clicked += OpenPokemonScreenClicked;
            fix.Put(pokeball20, 365, 395);

            #endregion Fourth button row

            #region Fifth button row

            // Pokeball 21
            pokeball21.Name = "pokemon21";
            pokeball21.Clicked += OpenPokemonScreenClicked;
            fix.Put(pokeball21, 72, 468);
            // Pokeball 22
            pokeball22.Name = "pokemon22";
            pokeball22.Clicked += OpenPokemonScreenClicked;
            fix.Put(pokeball22, 146, 468);
            // Pokeball 23
            pokeball23.Name = "pokemon23";
            pokeball23.Clicked += OpenPokemonScreenClicked;
            fix.Put(pokeball23, 219, 468);
            // Pokeball 24
            pokeball24.Name = "pokemon24";
            pokeball24.Clicked += OpenPokemonScreenClicked;
            fix.Put(pokeball24, 292, 468);
            // Pokeball 25
            pokeball25.Name = "pokemon25";
            pokeball25.Clicked += OpenPokemonScreenClicked;
            fix.Put(pokeball25, 365, 468);

            #endregion Fifth button row

            #endregion Buttons

            if (type != "all")
            {
                fix.Put(cbTypePokemon, 175, 60);
                ListStore typeList = new ListStore(typeof(string));
                typeList.AppendValues("Todos");
                typeList.AppendValues($"Puro tipo {TypeFormatted}");
                typeList.AppendValues($"Meio tipo primário");
                typeList.AppendValues($"Meio tipo secundário");
                cbTypePokemon.Model = typeList;
                cbTypePokemon.Active = 0;

                CellRendererText cell = new CellRendererText();
                cbTypePokemon.PackStart(cell, false);
                cbTypePokemon.AddAttribute(cell, "text", 0);

                cbTypePokemon.Changed += (sender, e) =>
                {
                    TreeIter searchByType;
                    if (cbTypePokemon.GetActiveIter(out searchByType))
                    {
                        var typeSelected = (string)typeList.GetValue(searchByType, 0);
                        if (typeSelected == "Todos")
                        {
                            currentPage = 0;
                            choice = 0;
                            AllTypeClicked();
                            _methods.DisableButtons(btnNext);
                        }
                        else if (typeSelected == $"Puro tipo {TypeFormatted}")
                        {
                            currentPage = 0;
                            choice = 1;
                            AllTypeClicked();
                            _methods.DisableButtons(btnNext);
                        }
                        else if (typeSelected == $"Meio tipo primário")
                        {
                            currentPage = 0;
                            choice = 2;
                            AllTypeClicked();
                            _methods.DisableButtons(btnNext);
                        }
                        else if (typeSelected == $"Meio tipo secundário")
                        {
                            currentPage = 0;
                            choice = 3;
                            AllTypeClicked();
                            _methods.DisableButtons(btnNext);
                        }
                    }
                };
            }

            _methods.UpdateButtons(fix, currentPage, type, choice);
            Add(fix);
            ShowAll();
        }

        private void SearchPokemon(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSearchPokemon.Text))
            {
                if (txtSearchPokemon.Text != "" && txtSearchPokemon.Text != "Buscar Pokémon")
                {
                    string PokemonName = txtSearchPokemon.Text.ToLower();
                    PokemonName = PokemonName.Replace(' ', '-');
                    _methods.SearchPokemonName(fix, currentPage, type, choice, PokemonName);
                }
            }
            else
            {
                _methods.UpdateButtons(fix, currentPage, type, choice);
            }
        }

        private void btnNext_Clicked(object sender, EventArgs e)
        {
            currentPage += 25;
            _methods.UpdateButtons(fix, currentPage, type, choice);
            _methods.DisableButtons(btnNext);
        }

        private void btnBack_Clicked(object sender, EventArgs e)
        {
            currentPage -= 25;
            _methods.UpdateButtons(fix, currentPage, type, choice);
            btnNext.Sensitive = true;
        }

        private async void OpenPokemonScreenClicked(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            var pokemonName = (string)btn.Data["name"];
            if (pokemonName != string.Empty)
            {
                Pokemon pokemonClicked = await _apiRequest.GetPokemon(pokemonName);
                PokemonScreen pokemonScreen = new(pokemonClicked);
                pokemonScreen.Show();
            }
            else
            {
                MessageDialog messageDialog = new MessageDialog(this, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, "Esse pokémon ainda não foi implementado!");
                messageDialog.Run();
                messageDialog.Destroy();
            }
        }

        private void AllTypeClicked()
        {
            btnNext.Sensitive = true;
            _methods.LoadPokemonList(currentPage, type, choice);
            _methods.UpdateButtons(fix, currentPage, type, choice);
        }
    }
}