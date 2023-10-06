using NowComesGtk.Reusable_components;
using PokeApiNet;
using Gtk;
using NowComesGtk.Utils;
using PokeApi.BackEnd.Service;

namespace NowComesGtk.Screens
{
    public class PokedexScreen : BaseWindow
    {
#nullable disable
        private Entry txtSearchPokemon = new Entry();
        private Fixed fix = new Fixed();
        private Methods _methods = new();
        private ApiRequest _apiRequest = new();
        private string type = "water";
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

        public PokedexScreen() : base("PokéTrainer© // Pokémons tipo - Água // Pokémons", 500, 600)
        {
            EventBox eventBox = new EventBox();
            Image backgroundScreen = new Image("Images/pokemon_water/pokemon_water_homescreen.png");
            fix.Put(backgroundScreen, 0, 0);

            string defaultText = "Buscar Pokémon";
            txtSearchPokemon.SetSizeRequest(125, 20);
            fix.Put(txtSearchPokemon, 165, 25);
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

            VBox vboxPokemonButtonsFirstColumn = new(false, 0);
            fix.Put(vboxPokemonButtonsFirstColumn, 50, 145);
            VBox vboxPokemonButtonsSecondColumn = new(false, 0);
            fix.Put(vboxPokemonButtonsSecondColumn, 108, 145);
            VBox vboxPokemonButtonsThirdColumn = new(false, 0);
            fix.Put(vboxPokemonButtonsThirdColumn, 165, 145);
            VBox vboxPokemonButtonsFourthColumn = new(false, 0);
            fix.Put(vboxPokemonButtonsFourthColumn, 223, 145);
            VBox vboxPokemonButtonsFifthColumn = new(false, 0);
            fix.Put(vboxPokemonButtonsFifthColumn, 280, 145);
            VBox vboxPokemonButtonsSixthColumn = new(false, 0);
            fix.Put(vboxPokemonButtonsSixthColumn, 338, 145);
            VBox vboxPokemonButtonsSeventhColumn = new(false, 0);
            fix.Put(vboxPokemonButtonsSeventhColumn, 396, 145);

            #region First row of button

            // Pokeball 1
            pokeball1 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            vboxPokemonButtonsFirstColumn.PackStart(pokeball1, false, false, 4);
            pokeball1.Name = "pokemon1";
            pokeball1.Clicked += OpenPokemonScreenClicked;
            // Pokeball 2
            pokeball2 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            vboxPokemonButtonsSecondColumn.PackStart(pokeball2, false, false, 4);
            pokeball2.Name = "pokemon2";
            pokeball2.Clicked += OpenPokemonScreenClicked;
            // Pokeball 3
            pokeball3 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            vboxPokemonButtonsThirdColumn.PackStart(pokeball3, false, false, 4);
            pokeball3.Name = "pokemon3";
            pokeball3.Clicked += OpenPokemonScreenClicked;
            // Pokeball 4
            pokeball4 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            vboxPokemonButtonsFourthColumn.PackStart(pokeball4, false, false, 4);
            pokeball4.Name = "pokemon4";
            pokeball4.Clicked += OpenPokemonScreenClicked;
            // Pokeball 5
            pokeball5 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            vboxPokemonButtonsFifthColumn.PackStart(pokeball5, false, false, 4);
            pokeball5.Name = "pokemon5";
            pokeball5.Clicked += OpenPokemonScreenClicked;
            // Pokeball 6
            pokeball6 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            vboxPokemonButtonsSixthColumn.PackStart(pokeball6, false, false, 4);
            pokeball6.Name = "pokemon6";
            pokeball6.Clicked += OpenPokemonScreenClicked;
            // Pokeball 7
            pokeball7 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            vboxPokemonButtonsSeventhColumn.PackStart(pokeball7, false, false, 4);
            pokeball7.Name = "pokemon7";
            pokeball7.Clicked += OpenPokemonScreenClicked;

            #endregion First row of button

            #region Second button row

            // Pokeball 8
            pokeball8 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            vboxPokemonButtonsFirstColumn.PackStart(pokeball8, false, false, 4);
            pokeball8.Name = "pokemon8";
            pokeball8.Clicked += OpenPokemonScreenClicked;
            // Pokeball 9
            pokeball9 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            vboxPokemonButtonsSecondColumn.PackStart(pokeball9, false, false, 4);
            pokeball9.Name = "pokemon9";
            pokeball9.Clicked += OpenPokemonScreenClicked;
            // Pokeball 10
            pokeball10 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            vboxPokemonButtonsThirdColumn.PackStart(pokeball10, false, false, 4);
            pokeball10.Name = "pokemon10";
            pokeball10.Clicked += OpenPokemonScreenClicked;
            // Pokeball 11
            pokeball11 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            vboxPokemonButtonsFourthColumn.PackStart(pokeball11, false, false, 4);
            pokeball11.Name = "pokemon11";
            pokeball11.Clicked += OpenPokemonScreenClicked;
            // Pokeball 12
            pokeball12 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            vboxPokemonButtonsFifthColumn.PackStart(pokeball12, false, false, 4);
            pokeball12.Name = "pokemon12";
            pokeball12.Clicked += OpenPokemonScreenClicked;
            // Pokeball 13
            pokeball13 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            vboxPokemonButtonsSixthColumn.PackStart(pokeball13, false, false, 4);
            pokeball13.Name = "pokemon13";
            pokeball13.Clicked += OpenPokemonScreenClicked;
            // Pokeball 14
            pokeball14 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            vboxPokemonButtonsSeventhColumn.PackStart(pokeball14, false, false, 4);
            pokeball14.Name = "pokemon14";
            pokeball14.Clicked += OpenPokemonScreenClicked;

            #endregion Second button row

            #region Third button row

            // Pokeball 15
            pokeball15 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            vboxPokemonButtonsFirstColumn.PackStart(pokeball15, false, false, 4);
            pokeball15.Name = "pokemon15";
            pokeball15.Clicked += OpenPokemonScreenClicked;
            // Pokeball 16
            pokeball16 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            vboxPokemonButtonsSecondColumn.PackStart(pokeball16, false, false, 4);
            pokeball16.Name = "pokemon16";
            pokeball16.Clicked += OpenPokemonScreenClicked;
            // Pokeball 17
            pokeball17 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            vboxPokemonButtonsThirdColumn.PackStart(pokeball17, false, false, 4);
            pokeball17.Name = "pokemon17";
            pokeball17.Clicked += OpenPokemonScreenClicked;
            // Pokeball 18
            pokeball18 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            vboxPokemonButtonsFourthColumn.PackStart(pokeball18, false, false, 4);
            pokeball18.Name = "pokemon18";
            pokeball18.Clicked += OpenPokemonScreenClicked;
            // Pokeball 19
            pokeball19 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            vboxPokemonButtonsFifthColumn.PackStart(pokeball19, false, false, 4);
            pokeball19.Name = "pokemon19";
            pokeball19.Clicked += OpenPokemonScreenClicked;
            // Pokeball 20
            pokeball20 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            vboxPokemonButtonsSixthColumn.PackStart(pokeball20, false, false, 4);
            pokeball20.Name = "pokemon20";
            pokeball20.Clicked += OpenPokemonScreenClicked;
            // Pokeball 21
            pokeball21 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            vboxPokemonButtonsSeventhColumn.PackStart(pokeball21, false, false, 4);
            pokeball21.Name = "pokemon21";
            pokeball21.Clicked += OpenPokemonScreenClicked;

            #endregion Third button row

            #region Fourth button row

            // Pokeball 22
            pokeball22 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            vboxPokemonButtonsFirstColumn.PackStart(pokeball22, false, false, 4);
            pokeball22.Name = "pokemon22";
            pokeball22.Clicked += OpenPokemonScreenClicked;

            // Pokeball 23
            pokeball23 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            vboxPokemonButtonsSecondColumn.PackStart(pokeball23, false, false, 4);
            pokeball23.Name = "pokemon23";
            pokeball23.Clicked += OpenPokemonScreenClicked;
            // Pokeball 24
            pokeball24 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            vboxPokemonButtonsThirdColumn.PackStart(pokeball24, false, false, 4);
            pokeball24.Name = "pokemon24";
            pokeball24.Clicked += OpenPokemonScreenClicked;
            // Pokeball 25
            pokeball25 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            vboxPokemonButtonsFourthColumn.PackStart(pokeball25, false, false, 4);
            pokeball25.Name = "pokemon25";
            pokeball25.Clicked += OpenPokemonScreenClicked;
            // Pokeball 26
            pokeball26 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            vboxPokemonButtonsFifthColumn.PackStart(pokeball26, false, false, 4);
            pokeball26.Name = "pokemon26";
            pokeball26.Clicked += OpenPokemonScreenClicked;
            // Pokeball 27
            pokeball27 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            vboxPokemonButtonsSixthColumn.PackStart(pokeball27, false, false, 4);
            pokeball27.Name = "pokemon27";
            pokeball27.Clicked += OpenPokemonScreenClicked;
            // Pokeball 28
            pokeball28 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            vboxPokemonButtonsSeventhColumn.PackStart(pokeball28, false, false, 4);
            pokeball28.Name = "pokemon28";
            pokeball28.Clicked += OpenPokemonScreenClicked;

            #endregion Fourth button row

            #region Fifth button row

            // Pokeball 29
            pokeball29 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            vboxPokemonButtonsFirstColumn.PackStart(pokeball29, false, false, 4);
            pokeball29.Name = "pokemon29";
            pokeball29.Clicked += OpenPokemonScreenClicked;

            // Pokeball 30
            pokeball30 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            vboxPokemonButtonsSecondColumn.PackStart(pokeball30, false, false, 4);
            pokeball30.Name = "pokemon30";
            pokeball30.Clicked += OpenPokemonScreenClicked;
            // Pokeball 31
            pokeball31 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            vboxPokemonButtonsThirdColumn.PackStart(pokeball31, false, false, 4);
            pokeball31.Name = "pokemon31";
            pokeball31.Clicked += OpenPokemonScreenClicked;
            // Pokeball 32
            pokeball32 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            vboxPokemonButtonsFourthColumn.PackStart(pokeball32, false, false, 4);
            pokeball32.Name = "pokemon32";
            pokeball32.Clicked += OpenPokemonScreenClicked;
            // Pokeball 33
            pokeball33 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            vboxPokemonButtonsFifthColumn.PackStart(pokeball33, false, false, 4);
            pokeball33.Name = "pokemon33";
            pokeball33.Clicked += OpenPokemonScreenClicked;
            // Pokeball 34
            pokeball34 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            vboxPokemonButtonsSixthColumn.PackStart(pokeball34, false, false, 4);
            pokeball34.Name = "pokemon34";
            pokeball34.Clicked += OpenPokemonScreenClicked;
            // Pokeball 35
            pokeball35 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            vboxPokemonButtonsSeventhColumn.PackStart(pokeball35, false, false, 4);
            pokeball35.Name = "pokemon35";
            pokeball35.Clicked += OpenPokemonScreenClicked;

            #endregion Fifth button row

            #region Sixth button row

            // Pokeball 36
            pokeball36 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            vboxPokemonButtonsFirstColumn.PackStart(pokeball36, false, false, 4);
            pokeball36.Name = "pokemon36";
            pokeball36.Clicked += OpenPokemonScreenClicked;

            // Pokeball 37
            pokeball37 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            vboxPokemonButtonsSecondColumn.PackStart(pokeball37, false, false, 4);
            pokeball37.Name = "pokemon37";
            pokeball37.Clicked += OpenPokemonScreenClicked;
            // Pokeball 38
            pokeball38 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            vboxPokemonButtonsThirdColumn.PackStart(pokeball38, false, false, 4);
            pokeball38.Name = "pokemon38";
            pokeball38.Clicked += OpenPokemonScreenClicked;
            // Pokeball 39
            pokeball39 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            vboxPokemonButtonsFourthColumn.PackStart(pokeball39, false, false, 4);
            pokeball39.Name = "pokemon39";
            pokeball39.Clicked += OpenPokemonScreenClicked;
            // Pokeball 40
            pokeball40 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            vboxPokemonButtonsFifthColumn.PackStart(pokeball40, false, false, 4);
            pokeball40.Name = "pokemon40";
            pokeball40.Clicked += OpenPokemonScreenClicked;
            // Pokeball 41
            pokeball41 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            vboxPokemonButtonsSixthColumn.PackStart(pokeball41, false, false, 4);
            pokeball41.Name = "pokemon41";
            pokeball41.Clicked += OpenPokemonScreenClicked;
            // Pokeball 42
            pokeball42 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            vboxPokemonButtonsSeventhColumn.PackStart(pokeball42, false, false, 4);
            pokeball42.Name = "pokemon42";
            pokeball42.Clicked += OpenPokemonScreenClicked;

            #endregion Sixth button row

            #region Seventh button row

            // Pokeball 43
            pokeball43 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            vboxPokemonButtonsFirstColumn.PackStart(pokeball43, false, false, 4);
            pokeball43.Name = "pokemon43";
            pokeball43.Clicked += OpenPokemonScreenClicked;
            // Pokeball 44
            pokeball44 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            vboxPokemonButtonsSecondColumn.PackStart(pokeball44, false, false, 4);
            pokeball44.Name = "pokemon44";
            pokeball44.Clicked += OpenPokemonScreenClicked;
            // Pokeball 45
            pokeball45 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            vboxPokemonButtonsThirdColumn.PackStart(pokeball45, false, false, 4);
            pokeball45.Name = "pokemon45";
            pokeball45.Clicked += OpenPokemonScreenClicked;
            // Pokeball 46
            pokeball46 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            vboxPokemonButtonsFourthColumn.PackStart(pokeball46, false, false, 4);
            pokeball46.Name = "pokemon46";
            pokeball46.Clicked += OpenPokemonScreenClicked;
            // Pokeball 47
            pokeball47 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            vboxPokemonButtonsFifthColumn.PackStart(pokeball47, false, false, 4);
            pokeball47.Name = "pokemon47";
            pokeball47.Clicked += OpenPokemonScreenClicked;
            // Pokeball 48
            pokeball48 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            vboxPokemonButtonsSixthColumn.PackStart(pokeball48, false, false, 4);
            pokeball48.Name = "pokemon48";
            pokeball48.Clicked += OpenPokemonScreenClicked;
            // Pokeball 49
            pokeball49 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            vboxPokemonButtonsSeventhColumn.PackStart(pokeball49, false, false, 4);
            pokeball49.Name = "pokemon49";
            pokeball49.Clicked += OpenPokemonScreenClicked;

            #endregion Seventh button row

            #endregion Buttons

            ListStore typeList = new ListStore(typeof(string));
            typeList.AppendValues("Todos");
            typeList.AppendValues("Puro tipo Água");
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
                    }
                    else if (typeSelected == "Puro tipo Água")
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

            eventBox.Add(vboxPokemonButtonsFifthColumn);
            eventBox.Add(vboxPokemonButtonsSecondColumn);
            eventBox.Add(vboxPokemonButtonsThirdColumn);
            eventBox.Add(vboxPokemonButtonsFourthColumn);
            eventBox.Add(vboxPokemonButtonsFifthColumn);
            eventBox.Add(vboxPokemonButtonsSixthColumn);
            eventBox.Add(vboxPokemonButtonsSeventhColumn);

            fix.Add(eventBox);
            _methods.UpdateButtons(fix, currentPage, type, choice);
            Add(fix);
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