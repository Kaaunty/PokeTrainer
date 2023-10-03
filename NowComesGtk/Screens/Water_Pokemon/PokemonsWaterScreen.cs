using NowComesGtk.Screens.Water_Pokemon.Pokemon_Screens_Water;
using NowComesGtk.Reusable_components;
using Gtk;

namespace NowComesGtk.Screens.Water_Pokemon
{
    public class PokemonsWaterScreen : BaseWindow
    {
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
        #endregion

        public PokemonsWaterScreen() : base("PokéTrainer© // Pokémons tipo - Água // Pokémons", 500, 600)
        {
            Fixed fix = new Fixed();
            EventBox eventBox = new EventBox();
            Image backgroundScreen = new Image("Images/pokemon_water/pokemon_water_homescreen.png");
            fix.Put(backgroundScreen, 0, 0);

            Entry txtSearchPokemon = new Entry();
            string defaultText = "Buscar Pokémon";
            txtSearchPokemon.SetSizeRequest(125, 20);
            fix.Put(txtSearchPokemon, 165, 25);
            txtSearchPokemon.Text = defaultText;
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
            Button btnNext = new Button(">>");
            btnNext.SetSizeRequest(10, 10);
            fix.Put(btnNext, 425, 74);

            #region Buttons

            VBox vboxPokemonButtonsFirstColumn = new VBox(false, 0);
            fix.Put(vboxPokemonButtonsFirstColumn, 50, 145);
            VBox vboxPokemonButtonsSecondColumn = new VBox(false, 0);
            fix.Put(vboxPokemonButtonsSecondColumn, 108, 145);
            VBox vboxPokemonButtonsThirdColumn = new VBox(false, 0);
            fix.Put(vboxPokemonButtonsThirdColumn, 165, 145);
            VBox vboxPokemonButtonsFourthColumn = new VBox(false, 0);
            fix.Put(vboxPokemonButtonsFourthColumn, 223, 145);
            VBox vboxPokemonButtonsFifthColumn = new VBox(false, 0);
            fix.Put(vboxPokemonButtonsFifthColumn, 280, 145);
            VBox vboxPokemonButtonsSixthColumn = new VBox(false, 0);
            fix.Put(vboxPokemonButtonsSixthColumn, 338, 145);
            VBox vboxPokemonButtonsSeventhColumn = new VBox(false, 0);
            fix.Put(vboxPokemonButtonsSeventhColumn, 396, 145);

            #region First row of button
            // Pokeball 1
            pokeball1 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            vboxPokemonButtonsFirstColumn.PackStart(pokeball1, false, false, 4);
            // Pokeball 2
            pokeball2 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            vboxPokemonButtonsSecondColumn.PackStart(pokeball2, false, false, 4);
            // Pokeball 3
            pokeball3 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            vboxPokemonButtonsThirdColumn.PackStart(pokeball3, false, false, 4);
            // Pokeball 4
            pokeball4 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            vboxPokemonButtonsFourthColumn.PackStart(pokeball4, false, false, 4);
            // Pokeball 5
            pokeball5 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            vboxPokemonButtonsFifthColumn.PackStart(pokeball5, false, false, 4);
            // Pokeball 6
            pokeball6 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            vboxPokemonButtonsSixthColumn.PackStart(pokeball6, false, false, 4);
            // Pokeball 7
            pokeball7 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            vboxPokemonButtonsSeventhColumn.PackStart(pokeball7, false, false, 4);

            #endregion

            #region Second button row
            // Pokeball 8
            pokeball8 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            vboxPokemonButtonsFirstColumn.PackStart(pokeball8, false, false, 4);
            // Pokeball 9
            pokeball9 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            vboxPokemonButtonsSecondColumn.PackStart(pokeball9, false, false, 4);
            // Pokeball 10
            pokeball10 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            vboxPokemonButtonsThirdColumn.PackStart(pokeball10, false, false, 4);
            // Pokeball 11
            pokeball11 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            vboxPokemonButtonsFourthColumn.PackStart(pokeball11, false, false, 4);
            // Pokeball 12
            pokeball12 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            vboxPokemonButtonsFifthColumn.PackStart(pokeball12, false, false, 4);
            // Pokeball 13
            pokeball13 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            vboxPokemonButtonsSixthColumn.PackStart(pokeball13, false, false, 4);
            // Pokeball 14
            pokeball14 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            vboxPokemonButtonsSeventhColumn.PackStart(pokeball14, false, false, 4);

            #endregion

            #region Third button row
            // Pokeball 15
            pokeball15 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            vboxPokemonButtonsFirstColumn.PackStart(pokeball15, false, false, 4);
            // Pokeball 16
            pokeball16 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            vboxPokemonButtonsSecondColumn.PackStart(pokeball16, false, false, 4);
            // Pokeball 17
            pokeball17 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            vboxPokemonButtonsThirdColumn.PackStart(pokeball17, false, false, 4);
            // Pokeball 18
            pokeball18 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            vboxPokemonButtonsFourthColumn.PackStart(pokeball18, false, false, 4);
            // Pokeball 19
            pokeball19 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            vboxPokemonButtonsFifthColumn.PackStart(pokeball19, false, false, 4);
            // Pokeball 20
            pokeball20 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            vboxPokemonButtonsSixthColumn.PackStart(pokeball20, false, false, 4);
            // Pokeball 21
            pokeball21 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            vboxPokemonButtonsSeventhColumn.PackStart(pokeball21, false, false, 4);

            #endregion

            #region Fourth button row
            // Pokeball 22
            pokeball22 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            vboxPokemonButtonsFirstColumn.PackStart(pokeball22, false, false, 4);
            // Pokeball 23
            pokeball23 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            vboxPokemonButtonsSecondColumn.PackStart(pokeball23, false, false, 4);
            // Pokeball 24
            pokeball24 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            vboxPokemonButtonsThirdColumn.PackStart(pokeball24, false, false, 4);
            // Pokeball 25
            pokeball25 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            vboxPokemonButtonsFourthColumn.PackStart(pokeball25, false, false, 4);
            // Pokeball 26
            pokeball26 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            vboxPokemonButtonsFifthColumn.PackStart(pokeball26, false, false, 4);
            // Pokeball 27
            pokeball27 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            vboxPokemonButtonsSixthColumn.PackStart(pokeball27, false, false, 4);
            // Pokeball 28
            pokeball28 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            vboxPokemonButtonsSeventhColumn.PackStart(pokeball28, false, false, 4);

            #endregion

            #region Fifth button row
            // Pokeball 29
            pokeball29 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            vboxPokemonButtonsFirstColumn.PackStart(pokeball29, false, false, 4);
            // Pokeball 30
            pokeball30 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            vboxPokemonButtonsSecondColumn.PackStart(pokeball30, false, false, 4);
            // Pokeball 31
            pokeball31 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            vboxPokemonButtonsThirdColumn.PackStart(pokeball31, false, false, 4);
            // Pokeball 32
            pokeball32 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            vboxPokemonButtonsFourthColumn.PackStart(pokeball32, false, false, 4);
            // Pokeball 33
            pokeball33 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            vboxPokemonButtonsFifthColumn.PackStart(pokeball33, false, false, 4);
            // Pokeball 34
            pokeball34 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            vboxPokemonButtonsSixthColumn.PackStart(pokeball34, false, false, 4);
            // Pokeball 35
            pokeball35 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            vboxPokemonButtonsSeventhColumn.PackStart(pokeball35, false, false, 4);

            #endregion

            #region Sixth button row
            // Pokeball 36
            pokeball36 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            vboxPokemonButtonsFirstColumn.PackStart(pokeball36, false, false, 4);
            // Pokeball 37
            pokeball37 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            vboxPokemonButtonsSecondColumn.PackStart(pokeball37, false, false, 4);
            // Pokeball 38
            pokeball38 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            vboxPokemonButtonsThirdColumn.PackStart(pokeball38, false, false, 4);
            // Pokeball 39
            pokeball39 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            vboxPokemonButtonsFourthColumn.PackStart(pokeball39, false, false, 4);
            // Pokeball 40
            pokeball40 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            vboxPokemonButtonsFifthColumn.PackStart(pokeball40, false, false, 4);
            // Pokeball 41
            pokeball41 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            vboxPokemonButtonsSixthColumn.PackStart(pokeball41, false, false, 4);
            // Pokeball 42
            pokeball42 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            vboxPokemonButtonsSeventhColumn.PackStart(pokeball42, false, false, 4);

            #endregion

            #region Seventh button row
            // Pokeball 43
            pokeball43 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            vboxPokemonButtonsFirstColumn.PackStart(pokeball43, false, false, 4);
            // Pokeball 44
            pokeball44 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            vboxPokemonButtonsSecondColumn.PackStart(pokeball44, false, false, 4);
            // Pokeball 45
            pokeball45 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            vboxPokemonButtonsThirdColumn.PackStart(pokeball45, false, false, 4);
            // Pokeball 46
            pokeball46 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            vboxPokemonButtonsFourthColumn.PackStart(pokeball46, false, false, 4);
            // Pokeball 47
            pokeball47 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            vboxPokemonButtonsFifthColumn.PackStart(pokeball47, false, false, 4);
            // Pokeball 48
            pokeball48 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            vboxPokemonButtonsSixthColumn.PackStart(pokeball48, false, false, 4);
            // Pokeball 49
            pokeball49 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            vboxPokemonButtonsSeventhColumn.PackStart(pokeball49, false, false, 4);

            #endregion

            #endregion Buttons

            ListStore typeList = new ListStore(typeof(string));
            typeList.AppendValues("Selecione");
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
                        ButtonUpdate("Images/pokemon_water/pure_pokemon/0007_squirtle.png", pokeball1, Squirtle_Click);
                    }
                    else if (typeSelected == "Puro tipo Água")
                    {
                        pokeball1.Clicked -= Tauros_Click;
                        pokeball1.Clicked -= Poliwrat_Click;
                        ButtonUpdate("Images/pokemon_water/pure_pokemon/0007_squirtle.png", pokeball1, Squirtle_Click);
                    }
                    else if (typeSelected == "Meio - Primário")
                    {
                        pokeball1.Clicked -= Squirtle_Click;
                        pokeball1.Clicked -= Poliwrat_Click;
                        ButtonUpdate("Images/pokemon_water/primary_pokemon/0062_poliwrat.png", pokeball1, Poliwrat_Click);
                    }
                    else
                    {
                        pokeball1.Clicked -= Squirtle_Click;
                        pokeball1.Clicked -= Tauros_Click;
                        ButtonUpdate("Images/pokemon_water/secundary_pokemon/0128_tauros.png", pokeball1, Tauros_Click);
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
            Add(fix);
            ShowAll();
        }
        public void ButtonUpdate(string imagePath, Button button, EventHandler activatedHandler)
        {
            Gtk.Image buttonImage = new Gtk.Image(imagePath);
            button.Image = buttonImage;
            button.Clicked += activatedHandler;
        }
        private void Squirtle_Click(object? sender, EventArgs e)
        {
            Close();
            SquirtleScreen squirtleCreen = new SquirtleScreen();
            squirtleCreen.Show();
        }
        private void Tauros_Click(object? sender, EventArgs e)
        {
            Close();
            TaurosScreen taurosScreen = new TaurosScreen();
            taurosScreen.Show();
        }
        private void Poliwrat_Click(object? sender, EventArgs e)
        {
            Close();
            PoliwratScreen poliwratScreen = new PoliwratScreen();
            poliwratScreen.Show();
        }
    }
}

