using Gtk;
using NowComesGtk.Reusable_components;
using NowComesGtk.Utils;
using PokeApi.BackEnd.Service;
using PokeApiNet;
using System.Globalization;

namespace NowComesGtk.Screens
{
    public class PokedexScreen : BaseWindow
    {
#nullable disable
        private Entry txtSearchPokemon = new Entry();
        private Fixed fix = new Fixed();
        private Methods _methods = new();
        private ApiRequest _apiRequest = new();
        private TextInfo textInfo = new CultureInfo("pt-BR", false).TextInfo;
        private string type;
        private int choice = 0;
        private int currentPage = 0;

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
        public Button pokeball26;
        public Button pokeball27;
        public Button pokeball28;
        public Button pokeball29;
        public Button pokeball30;
        public Button pokeball31;
        public Button pokeball32;
        public Button pokeball33;
        public Button pokeball34;
        public Button pokeball35;
        public Button pokeball36;
        public Button pokeball37;
        public Button pokeball38;
        public Button pokeball39;
        public Button pokeball40;
        public Button pokeball41;
        public Button pokeball42;
        public Button pokeball43;
        public Button pokeball44;
        public Button pokeball45;
        public Button pokeball46;
        public Button pokeball47;
        public Button pokeball48;
        public Button pokeball49;

        #endregion Pokeball buttons

        public PokedexScreen(string type) : base($"PokéTrainer© // Pokémons tipo - {type} // Pokémons", 500, 600)
        {
            this.type = type;
            string TypeFormatted = textInfo.ToTitleCase(_apiRequest.Translate(type));
            Title = $"PokéTrainer© // Pokémons tipo - {TypeFormatted} // Pokémons";
            //EventBox fix = new EventBox();
            Image backgroundScreen = new Image("Images/pokemon_water/pokemon_homescreen.png");
            this.fix.Put(backgroundScreen, 0, 0);

            string defaultText = "Buscar Pokémon";
            txtSearchPokemon.SetSizeRequest(125, 20);
            this.fix.Put(txtSearchPokemon, 165, 25);
            txtSearchPokemon.Text = defaultText;
            txtSearchPokemon.Changed += SearchPokemon;
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
            this.fix.Put(cbTypePokemon, 180, 60);

            Button btnBack = new Button("<<");
            btnBack.SetSizeRequest(10, 10);
            this.fix.Put(btnBack, 25, 74);
            btnBack.Clicked += btnBack_Clicked;
            Button btnNext = new Button(">>");
            btnNext.SetSizeRequest(10, 10);
            this.fix.Put(btnNext, 425, 74);
            btnNext.Clicked += btnNext_Clicked;

            #region Buttons

            #region First row of button

            // Pokeball 1
            pokeball1 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            pokeball1.Name = "pokemon1";
            pokeball1.Clicked += OpenPokemonScreenClicked;
            fix.Put(pokeball1, 51, 145);
            // Pokeball 2
            pokeball2 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            pokeball2.Name = "pokemon2";
            pokeball2.Clicked += OpenPokemonScreenClicked;
            fix.Put(pokeball2, 108, 145);
            // Pokeball 3
            pokeball3 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            pokeball3.Name = "pokemon3";
            pokeball3.Clicked += OpenPokemonScreenClicked;
            fix.Put(pokeball3, 166, 145);
            // Pokeball 4
            pokeball4 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            pokeball4.Name = "pokemon4";
            pokeball4.Clicked += OpenPokemonScreenClicked;
            fix.Put(pokeball4, 223, 145);
            // Pokeball 5
            pokeball5 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            pokeball5.Name = "pokemon5";
            pokeball5.Clicked += OpenPokemonScreenClicked;
            fix.Put(pokeball5, 281, 145);
            // Pokeball 6
            pokeball6 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            pokeball6.Name = "pokemon6";
            pokeball6.Clicked += OpenPokemonScreenClicked;
            fix.Put(pokeball6, 339, 145);
            // Pokeball 7
            pokeball7 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            pokeball7.Name = "pokemon7";
            pokeball7.Clicked += OpenPokemonScreenClicked;
            fix.Put(pokeball7, 397, 145);

            #endregion First row of button

            #region Second button row

            // Pokeball 8
            pokeball8 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            pokeball8.Name = "pokemon8";
            pokeball8.Clicked += OpenPokemonScreenClicked;
            fix.Put(pokeball8, 51, 203);
            // Pokeball 9
            pokeball9 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            pokeball9.Name = "pokemon9";
            pokeball9.Clicked += OpenPokemonScreenClicked;
            fix.Put(pokeball9, 108, 203);
            // Pokeball 10
            pokeball10 = new ButtonGenerator("Images/pokeball.png", 40, 40);

            pokeball10.Name = "pokemon10";
            pokeball10.Clicked += OpenPokemonScreenClicked;
            fix.Put(pokeball10, 166, 203);
            // Pokeball 11
            pokeball11 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            pokeball11.Name = "pokemon11";
            pokeball11.Clicked += OpenPokemonScreenClicked;
            fix.Put(pokeball11, 223, 203);
            // Pokeball 12
            pokeball12 = new ButtonGenerator("Images/pokeball.png", 40, 40);

            pokeball12.Name = "pokemon12";
            pokeball12.Clicked += OpenPokemonScreenClicked;
            fix.Put(pokeball12, 281, 203);
            // Pokeball 13
            pokeball13 = new ButtonGenerator("Images/pokeball.png", 40, 40);

            pokeball13.Name = "pokemon13";
            pokeball13.Clicked += OpenPokemonScreenClicked;
            fix.Put(pokeball13, 339, 203);
            // Pokeball 14
            pokeball14 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            pokeball14.Name = "pokemon14";
            pokeball14.Clicked += OpenPokemonScreenClicked;
            fix.Put(pokeball14, 397, 203);

            #endregion Second button row

            #region Third button row

            // Pokeball 15
            pokeball15 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            pokeball15.Name = "pokemon15";
            pokeball15.Clicked += OpenPokemonScreenClicked;
            fix.Put(pokeball15, 51, 261);

            // Pokeball 16
            pokeball16 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            pokeball16.Name = "pokemon16";
            pokeball16.Clicked += OpenPokemonScreenClicked;
            fix.Put(pokeball16, 108, 261);
            // Pokeball 17
            pokeball17 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            pokeball17.Name = "pokemon17";
            pokeball17.Clicked += OpenPokemonScreenClicked;
            fix.Put(pokeball17, 166, 261);
            // Pokeball 18
            pokeball18 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            pokeball18.Name = "pokemon18";
            pokeball18.Clicked += OpenPokemonScreenClicked;
            fix.Put(pokeball18, 223, 261);
            // Pokeball 19
            pokeball19 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            pokeball19.Name = "pokemon19";
            pokeball19.Clicked += OpenPokemonScreenClicked;
            fix.Put(pokeball19, 281, 261);
            // Pokeball 20

            pokeball20 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            pokeball20.Name = "pokemon20";
            pokeball20.Clicked += OpenPokemonScreenClicked;
            fix.Put(pokeball20, 339, 261);
            // Pokeball 21
            pokeball21 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            pokeball21.Name = "pokemon21";
            pokeball21.Clicked += OpenPokemonScreenClicked;
            fix.Put(pokeball21, 397, 261);

            #endregion Third button row

            #region Fourth button row

            // Pokeball 22
            pokeball22 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            pokeball22.Name = "pokemon22";
            pokeball22.Clicked += OpenPokemonScreenClicked;
            fix.Put(pokeball22, 51, 319);

            // Pokeball 23
            pokeball23 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            pokeball23.Name = "pokemon23";
            pokeball23.Clicked += OpenPokemonScreenClicked;
            fix.Put(pokeball23, 108, 319);
            // Pokeball 24
            pokeball24 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            pokeball24.Name = "pokemon24";
            pokeball24.Clicked += OpenPokemonScreenClicked;
            fix.Put(pokeball24, 166, 319);
            // Pokeball 25
            pokeball25 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            pokeball25.Name = "pokemon25";
            pokeball25.Clicked += OpenPokemonScreenClicked;
            fix.Put(pokeball25, 223, 319);
            // Pokeball 26
            pokeball26 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            pokeball26.Name = "pokemon26";
            pokeball26.Clicked += OpenPokemonScreenClicked;
            fix.Put(pokeball26, 281, 319);
            // Pokeball 27
            pokeball27 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            pokeball27.Name = "pokemon27";
            pokeball27.Clicked += OpenPokemonScreenClicked;
            fix.Put(pokeball27, 339, 319);
            // Pokeball 28
            pokeball28 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            pokeball28.Name = "pokemon28";
            pokeball28.Clicked += OpenPokemonScreenClicked;
            fix.Put(pokeball28, 397, 319);

            #endregion Fourth button row

            #region Fifth button row

            // Pokeball 29
            pokeball29 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            pokeball29.Name = "pokemon29";
            pokeball29.Clicked += OpenPokemonScreenClicked;
            fix.Put(pokeball29, 51, 377);

            // Pokeball 30
            pokeball30 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            pokeball30.Name = "pokemon30";
            pokeball30.Clicked += OpenPokemonScreenClicked;
            fix.Put(pokeball30, 108, 377);
            // Pokeball 31
            pokeball31 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            pokeball31.Name = "pokemon31";
            pokeball31.Clicked += OpenPokemonScreenClicked;
            fix.Put(pokeball31, 166, 377);
            // Pokeball 32
            pokeball32 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            pokeball32.Name = "pokemon32";
            pokeball32.Clicked += OpenPokemonScreenClicked;
            fix.Put(pokeball32, 223, 377);
            // Pokeball 33
            pokeball33 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            pokeball33.Name = "pokemon33";
            pokeball33.Clicked += OpenPokemonScreenClicked;
            fix.Put(pokeball33, 281, 377);
            // Pokeball 34
            pokeball34 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            pokeball34.Name = "pokemon34";
            pokeball34.Clicked += OpenPokemonScreenClicked;
            fix.Put(pokeball34, 339, 377);
            // Pokeball 35
            pokeball35 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            pokeball35.Name = "pokemon35";
            pokeball35.Clicked += OpenPokemonScreenClicked;
            fix.Put(pokeball35, 397, 377);

            #endregion Fifth button row

            #region Sixth button row

            // Pokeball 36
            pokeball36 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            pokeball36.Name = "pokemon36";
            pokeball36.Clicked += OpenPokemonScreenClicked;
            fix.Put(pokeball36, 51, 435);

            // Pokeball 37
            pokeball37 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            pokeball37.Name = "pokemon37";
            pokeball37.Clicked += OpenPokemonScreenClicked;
            fix.Put(pokeball37, 108, 435);
            // Pokeball 38
            pokeball38 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            pokeball38.Name = "pokemon38";
            pokeball38.Clicked += OpenPokemonScreenClicked;
            fix.Put(pokeball38, 166, 435);
            // Pokeball 39
            pokeball39 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            pokeball39.Name = "pokemon39";
            pokeball39.Clicked += OpenPokemonScreenClicked;
            fix.Put(pokeball39, 223, 435);
            // Pokeball 40
            pokeball40 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            pokeball40.Name = "pokemon40";
            pokeball40.Clicked += OpenPokemonScreenClicked;
            fix.Put(pokeball40, 281, 435);
            // Pokeball 41
            pokeball41 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            pokeball41.Name = "pokemon41";
            pokeball41.Clicked += OpenPokemonScreenClicked;
            fix.Put(pokeball41, 339, 435);
            // Pokeball 42
            pokeball42 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            pokeball42.Name = "pokemon42";
            pokeball42.Clicked += OpenPokemonScreenClicked;
            fix.Put(pokeball42, 397, 435);

            #endregion Sixth button row

            #region Seventh button row

            // Pokeball 43
            pokeball43 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            pokeball43.Name = "pokemon43";
            pokeball43.Clicked += OpenPokemonScreenClicked;
            fix.Put(pokeball43, 51, 493);
            // Pokeball 44
            pokeball44 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            pokeball44.Name = "pokemon44";
            pokeball44.Clicked += OpenPokemonScreenClicked;
            fix.Put(pokeball44, 108, 493);
            // Pokeball 45
            pokeball45 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            pokeball45.Name = "pokemon45";
            pokeball45.Clicked += OpenPokemonScreenClicked;
            fix.Put(pokeball45, 166, 493);
            // Pokeball 46
            pokeball46 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            pokeball46.Name = "pokemon46";
            pokeball46.Clicked += OpenPokemonScreenClicked;
            fix.Put(pokeball46, 223, 493);
            // Pokeball 47
            pokeball47 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            pokeball47.Name = "pokemon47";
            pokeball47.Clicked += OpenPokemonScreenClicked;
            fix.Put(pokeball47, 281, 493);
            // Pokeball 48
            pokeball48 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            pokeball48.Name = "pokemon48";
            pokeball48.Clicked += OpenPokemonScreenClicked;
            fix.Put(pokeball48, 339, 493);
            // Pokeball 49
            pokeball49 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            pokeball49.Name = "pokemon49";
            pokeball49.Clicked += OpenPokemonScreenClicked;
            fix.Put(pokeball49, 397, 493);

            #endregion Seventh button row

            #endregion Buttons

            ListStore typeList = new ListStore(typeof(string));
            typeList.AppendValues("Todos");
            typeList.AppendValues($"Puro tipo {TypeFormatted}");
            typeList.AppendValues("Meio - Primário");
            typeList.AppendValues("Meio - Secundário");
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
                    else if (typeSelected == "Meio - Primário")
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

        private void btnNext_Clicked(object sender, EventArgs e)
        {
            currentPage += 49;
            _methods.UpdateButtons(fix, currentPage, type, choice);
        }

        private void btnBack_Clicked(object sender, EventArgs e)
        {
            currentPage -= 49;
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