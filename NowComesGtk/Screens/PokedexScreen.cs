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
        private int currentPage = 0;
        private Fixed fix = new();
        private int choice = 0;
        private string type;

        #region Pokeball buttons

        public Button pokeball1;
        public Button pokeball2;
        public Button pokeball3;
        public Button pokeball4;
        public Button pokeball5;
        public Button pokeball6;
        public Button pokeball7;
        public Button pokeball8;
        public Button pokeball9;
        public Button pokeball10;
        public Button pokeball11;
        public Button pokeball12;
        public Button pokeball13;
        public Button pokeball14;
        public Button pokeball15;
        public Button pokeball16;
        public Button pokeball17;
        public Button pokeball18;
        public Button pokeball19;
        public Button pokeball20;
        public Button pokeball21;
        public Button pokeball22;
        public Button pokeball23;
        public Button pokeball24;
        public Button pokeball25;

        #endregion Pokeball buttons

        public PokedexScreen(string type) : base($"PokéTrainer© // Pokémons tipo - {type} // Pokémons", 500, 600)
        {
            this.type = type;
            string TypeFormatted = textInfo.ToTitleCase(_apiRequest.Translate(type));
            Title = $"PokéTrainer© // Pokémons tipo - {TypeFormatted} // Pokémons";
            Image backgroundScreen = new Image($"Images/pokedex_homescreen/{type}.png");
            fix.Put(backgroundScreen, 0, 0);

            string defaultText = "Buscar Pokémon";
            txtSearchPokemon.SetSizeRequest(125, 20);
            fix.Put(txtSearchPokemon, 165, 25);
            txtSearchPokemon.Text = defaultText;

            if (!string.IsNullOrEmpty(txtSearchPokemon.Text))
            {
                //txtSearchPokemon.Changed += SearchPokemon;
            }
            CssProvider cssProvider = new CssProvider();
            cssProvider.LoadFromData("entry { color: rgb(200, 200, 200); }");
            txtSearchPokemon.StyleContext.AddProvider(cssProvider, StyleProviderPriority.Application);

            txtSearchPokemon.FocusInEvent += (sender, e) =>
            {
                txtSearchPokemon.Text = string.Empty;
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

            ComboBox cbTypePokemon = new ComboBox();
            fix.Put(cbTypePokemon, 180, 60);

            Button btnBack = new Button("<<");
            btnBack.SetSizeRequest(10, 10);
            fix.Put(btnBack, 25, 74);
            btnBack.Clicked += btnBack_Clicked;
            Button btnNext = new Button(">>");
            btnNext.SetSizeRequest(10, 10);
            fix.Put(btnNext, 425, 74);
            btnNext.Clicked += btnNext_Clicked;

            #region Buttons

            #region First row of button

            // Pokeball 1
            pokeball1 = new ButtonGenerator("Images/pokeball.png", 50, 50);
            pokeball1.Name = "pokemon1";
            pokeball1.Clicked += OpenPokemonScreenClicked;
            fix.Put(pokeball1, 72, 176);
            // Pokeball 2
            pokeball2 = new ButtonGenerator("Images/pokeball.png", 50, 50);
            pokeball2.Name = "pokemon2";
            pokeball2.Clicked += OpenPokemonScreenClicked;
            fix.Put(pokeball2, 146, 176);
            // Pokeball 3
            pokeball3 = new ButtonGenerator("Images/pokeball.png", 50, 50);
            pokeball3.Name = "pokemon3";
            pokeball3.Clicked += OpenPokemonScreenClicked;
            fix.Put(pokeball3, 219, 176);
            // Pokeball 4
            pokeball4 = new ButtonGenerator("Images/pokeball.png", 50, 50);
            pokeball4.Name = "pokemon4";
            pokeball4.Clicked += OpenPokemonScreenClicked;
            fix.Put(pokeball4, 292, 176);
            // Pokeball 5
            pokeball5 = new ButtonGenerator("Images/pokeball.png", 50, 50);
            pokeball5.Name = "pokemon5";
            pokeball5.Clicked += OpenPokemonScreenClicked;
            fix.Put(pokeball5, 365, 176);

            #endregion First row of button

            #region Second button row

            // Pokeball 6
            pokeball6 = new ButtonGenerator("Images/pokeball.png", 50, 50);
            pokeball6.Name = "pokemon6";
            pokeball6.Clicked += OpenPokemonScreenClicked;
            fix.Put(pokeball6, 72, 249);
            // Pokeball 7
            pokeball7 = new ButtonGenerator("Images/pokeball.png", 50, 50);
            pokeball7.Name = "pokemon7";
            pokeball7.Clicked += OpenPokemonScreenClicked;
            fix.Put(pokeball7, 146, 249);
            // Pokeball 8
            pokeball8 = new ButtonGenerator("Images/pokeball.png", 50, 50);
            pokeball8.Name = "pokemon8";
            pokeball8.Clicked += OpenPokemonScreenClicked;
            fix.Put(pokeball8, 219, 249);
            // Pokeball 9
            pokeball9 = new ButtonGenerator("Images/pokeball.png", 50, 50);
            pokeball9.Name = "pokemon9";
            pokeball9.Clicked += OpenPokemonScreenClicked;
            fix.Put(pokeball9, 292, 249);
            // Pokeball 10
            pokeball10 = new ButtonGenerator("Images/pokeball.png", 50, 50);

            pokeball10.Name = "pokemon10";
            pokeball10.Clicked += OpenPokemonScreenClicked;
            fix.Put(pokeball10, 365, 249);

            #endregion Second button row

            #region Third button row

            // Pokeball 11
            pokeball11 = new ButtonGenerator("Images/pokeball.png", 50, 50);
            pokeball11.Name = "pokemon11";
            pokeball11.Clicked += OpenPokemonScreenClicked;
            fix.Put(pokeball11, 72, 322);
            // Pokeball 12
            pokeball12 = new ButtonGenerator("Images/pokeball.png", 50, 50);

            pokeball12.Name = "pokemon12";
            pokeball12.Clicked += OpenPokemonScreenClicked;
            fix.Put(pokeball12, 146, 322);
            // Pokeball 13
            pokeball13 = new ButtonGenerator("Images/pokeball.png", 50, 50);

            pokeball13.Name = "pokemon13";
            pokeball13.Clicked += OpenPokemonScreenClicked;
            fix.Put(pokeball13, 219, 322);
            // Pokeball 14
            pokeball14 = new ButtonGenerator("Images/pokeball.png", 50, 50);
            pokeball14.Name = "pokemon14";
            pokeball14.Clicked += OpenPokemonScreenClicked;
            fix.Put(pokeball14, 292, 322);
            // Pokeball 15
            pokeball15 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            pokeball15.Name = "pokemon15";
            pokeball15.Clicked += OpenPokemonScreenClicked;
            fix.Put(pokeball15, 365, 322);

            #endregion Third button row

            #region Fourth button row

            // Pokeball 16
            pokeball16 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            pokeball16.Name = "pokemon16";
            pokeball16.Clicked += OpenPokemonScreenClicked;
            fix.Put(pokeball16, 72, 394);
            // Pokeball 17
            pokeball17 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            pokeball17.Name = "pokemon17";
            pokeball17.Clicked += OpenPokemonScreenClicked;
            fix.Put(pokeball17, 146, 394);
            // Pokeball 18
            pokeball18 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            pokeball18.Name = "pokemon18";
            pokeball18.Clicked += OpenPokemonScreenClicked;
            fix.Put(pokeball18, 219, 394);
            // Pokeball 19
            pokeball19 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            pokeball19.Name = "pokemon19";
            pokeball19.Clicked += OpenPokemonScreenClicked;
            fix.Put(pokeball19, 292, 394);
            // Pokeball 20
            pokeball20 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            pokeball20.Name = "pokemon20";
            pokeball20.Clicked += OpenPokemonScreenClicked;
            fix.Put(pokeball20, 365, 394);

            #endregion Fourth button row

            #region Fifth button row

            // Pokeball 21
            pokeball21 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            pokeball21.Name = "pokemon21";
            pokeball21.Clicked += OpenPokemonScreenClicked;
            fix.Put(pokeball21, 72, 467);
            // Pokeball 22
            pokeball22 = new ButtonGenerator("Images/pokeball.png", 50, 50);
            pokeball22.Name = "pokemon22";
            pokeball22.Clicked += OpenPokemonScreenClicked;
            fix.Put(pokeball22, 146, 467);
            // Pokeball 23
            pokeball23 = new ButtonGenerator("Images/pokeball.png", 50, 50);
            pokeball23.Name = "pokemon23";
            pokeball23.Clicked += OpenPokemonScreenClicked;
            fix.Put(pokeball23, 219, 467);
            // Pokeball 24
            pokeball24 = new ButtonGenerator("Images/pokeball.png", 50, 50);
            pokeball24.Name = "pokemon24";
            pokeball24.Clicked += OpenPokemonScreenClicked;
            fix.Put(pokeball24, 292, 467);
            // Pokeball 25
            pokeball25 = new ButtonGenerator("Images/pokeball.png", 50, 50);
            pokeball25.Name = "pokemon25";
            pokeball25.Clicked += OpenPokemonScreenClicked;
            fix.Put(pokeball25, 365, 467);

            #endregion Fifth button row

            #endregion Buttons

            ListStore typeList = new ListStore(typeof(string));
            typeList.AppendValues("Todos");
            typeList.AppendValues($"Puro tipo {TypeFormatted}");
            typeList.AppendValues("Meio tipo");
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
                    }
                    else if (typeSelected == $"Puro tipo  {TypeFormatted}")
                    {
                        currentPage = 0;
                        choice = 1;
                        AllTypeClicked();
                    }
                    else if (typeSelected == "Meio tipo")
                    {
                        currentPage = 0;

                        choice = 2;
                        AllTypeClicked();
                    }
                    else
                    {
                        currentPage = 0;
                        choice = 3;
                        AllTypeClicked();
                    }
                }
            };

            _methods.UpdateButtons(this.fix, currentPage, type, choice);
            Add(this.fix);
            ShowAll();
        }

        private void SearchPokemon(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSearchPokemon.Text))
            {
                if (txtSearchPokemon.Text != string.Empty && txtSearchPokemon.Text != "Buscar Pokémon")
                {
                    string PokemonName = txtSearchPokemon.Text.ToLower();
                    _methods.SearchPokemonName(fix, currentPage, type, choice, PokemonName);
                }
                else if (txtSearchPokemon.Text == string.Empty)
                {
                    _methods.UpdateButtons(fix, currentPage, type, choice);
                }
            }
        }

        private void btnNext_Clicked(object sender, EventArgs e)
        {
            currentPage += 25;
            _methods.UpdateButtons(fix, currentPage, type, choice);
        }

        private void btnBack_Clicked(object sender, EventArgs e)
        {
            currentPage -= 25;
            _methods.UpdateButtons(fix, currentPage, type, choice);
        }

        private async void OpenPokemonScreenClicked(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            var pokemonName = (string)btn.Data["name"];
            if (pokemonName != string.Empty)
            {
                Pokemon pokemonClicked = await _apiRequest.GetPokemonAsync(pokemonName);
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
            _methods.LoadPokemonList(currentPage, type, choice);
            _methods.UpdateButtons(fix, currentPage, type, choice);
        }
    }
}