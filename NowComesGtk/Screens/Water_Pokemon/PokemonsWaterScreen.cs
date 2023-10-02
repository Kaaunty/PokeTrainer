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
            btnBack.SetSizeRequest(10, 10);
            fix.Put(btnNext, 425, 74);

            #region Buttons

            #region First row of button
            // Pokeball 1
            pokeball1 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            fix.Put(pokeball1, 50, 145);
            // Pokeball 2
            pokeball2 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            fix.Put(pokeball2, 108, 145);
            // Pokeball 3
            pokeball3 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            fix.Put(pokeball3, 165, 145);
            // Pokeball 4
            pokeball4 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            fix.Put(pokeball4, 223, 145);
            // Pokeball 5
            pokeball5 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            fix.Put(pokeball5, 280, 145);
            // Pokeball 6
            pokeball6 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            fix.Put(pokeball6, 338, 145);
            // Pokeball 7
            pokeball7 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            fix.Put(pokeball7, 396, 145);
            #endregion

            #region Second button row
            // Pokeball 8
            pokeball8 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            fix.Put(pokeball8, 50, 203);
            // Pokeball 9
            pokeball9 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            fix.Put(pokeball9, 108, 203);
            // Pokeball 10
            pokeball10 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            fix.Put(pokeball10, 165, 203);
            // Pokeball 11
            pokeball11 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            fix.Put(pokeball11, 223, 203);
            // Pokeball 12
            pokeball12 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            fix.Put(pokeball12, 280, 203);
            // Pokeball 13
            pokeball13 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            fix.Put(pokeball13, 338, 203);
            // Pokeball 14
            pokeball14 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            fix.Put(pokeball14, 396, 203);
            #endregion

            #region Third button row
            // Pokeball 15
            pokeball15 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            fix.Put(pokeball15, 50, 261);
            // Pokeball 16
            pokeball16 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            fix.Put(pokeball16, 108, 261);
            // Pokeball 17
            pokeball17 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            fix.Put(pokeball17, 165, 261);
            // Pokeball 18
            pokeball18 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            fix.Put(pokeball18, 223, 261);
            // Pokeball 19
            pokeball19 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            fix.Put(pokeball19, 280, 261);
            // Pokeball 20
            pokeball20 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            fix.Put(pokeball20, 338, 261);
            // Pokeball 21
            pokeball21 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            fix.Put(pokeball21, 396, 261);
            #endregion

            #region Fourth button row
            // Pokeball 22
            pokeball22 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            fix.Put(pokeball22, 50, 319);
            // Pokeball 23
            pokeball23 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            fix.Put(pokeball23, 108, 319);
            // Pokeball 24
            pokeball24 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            fix.Put(pokeball24, 165, 319);
            // Pokeball 25
            pokeball25 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            fix.Put(pokeball25, 223, 319);
            // Pokeball 26
            pokeball26 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            fix.Put(pokeball26, 280, 319);
            // Pokeball 27
            pokeball27 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            fix.Put(pokeball27, 338, 319);
            // Pokeball 28
            pokeball28 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            fix.Put(pokeball28, 396, 319);
            #endregion

            #region Fifth button row
            // Pokeball 29
            pokeball29 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            fix.Put(pokeball29, 50, 376);
            // Pokeball 30
            pokeball30 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            fix.Put(pokeball30, 108, 376);
            // Pokeball 31
            pokeball31 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            fix.Put(pokeball31, 165, 376);
            // Pokeball 32
            pokeball32 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            fix.Put(pokeball32, 223, 376);
            // Pokeball 33
            pokeball33 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            fix.Put(pokeball33, 280, 376);
            // Pokeball 34
            pokeball34 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            fix.Put(pokeball34, 338, 376);
            // Pokeball 35
            pokeball35 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            fix.Put(pokeball35, 396, 376);
            #endregion

            #region Sixth button row
            // Pokeball 36
            pokeball36 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            fix.Put(pokeball36, 50, 434);
            // Pokeball 37
            pokeball37 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            fix.Put(pokeball37, 108, 434);
            // Pokeball 38
            pokeball38 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            fix.Put(pokeball38, 165, 434);
            // Pokeball 39
            pokeball39 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            fix.Put(pokeball39, 223, 434);
            // Pokeball 40
            pokeball40 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            fix.Put(pokeball40, 280, 434);
            // Pokeball 41
            pokeball41 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            fix.Put(pokeball41, 338, 434);
            // Pokeball 42
            pokeball42 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            fix.Put(pokeball42, 396, 434);
            #endregion

            #region Seventh button row
            // Pokeball 43
            pokeball43 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            fix.Put(pokeball43, 50, 493);
            // Pokeball 44
            pokeball44 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            fix.Put(pokeball44, 108, 493);
            // Pokeball 45
            pokeball45 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            fix.Put(pokeball45, 165, 493);
            // Pokeball 46
            pokeball46 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            fix.Put(pokeball46, 223, 493);
            // Pokeball 47
            pokeball47 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            fix.Put(pokeball47, 280, 493);
            // Pokeball 48
            pokeball48 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            fix.Put(pokeball48, 338, 493);
            // Pokeball 49
            pokeball49 = new ButtonGenerator("Images/pokeball.png", 40, 40);
            fix.Put(pokeball49, 396, 493);
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

            Add(fix);
            ShowAll();
        }
        public void ButtonUpdate(string imagePath, Button button, EventHandler activatedHandler)
        {
            Image buttonImage = new Image(imagePath);
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

