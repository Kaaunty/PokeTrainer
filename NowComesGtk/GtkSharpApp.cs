using Gtk;
using Application = Gtk.Application;
using Window = Gtk.Window;

public class GtkSharpApp
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

    #endregion Pokeball buttons

    public GtkSharpApp()
    {
        Window homeScreen = WindowGenerator(800, 500, "PokéTrainer©");
        homeScreen.Resizable = false;
        Fixed fix = new Fixed();
        Image steelScreenBackground = new Image("Images/background_homescreen.png");
        fix.Put(steelScreenBackground, 0, 0);
        homeScreen.DeleteEvent += delegate { Application.Quit(); };

        #region Menubar Settings

        VBox vbMb = new VBox(false, 0);
        MenuBar mb = new MenuBar();
        vbMb.PackStart(mb, false, false, 0);

        Gtk.Menu pokemonsMenu = new Gtk.Menu();
        Gtk.MenuItem pokemonsMI = new Gtk.MenuItem("Pokémons");

        Gtk.MenuItem waterMenuItem = MenuItemGenerator("Água", "Images/pokemon_water/WaterIcon.png", WaterPokemonHomepage);
        pokemonsMenu.Append(waterMenuItem);

        //// Water MenuItem:
        //MenuItem waterMI = new MenuItem();
        //EventBox waterEventBox = new EventBox();
        //HBox waterHbox = new HBox(false, 0);
        //Label lblWaterText = new Label();
        //Image waterImage = new Image();
        //lblWaterText.Markup = "<span font_desc='Sans 9'>Água</span>";
        //waterImage.Pixbuf = new Gdk.Pixbuf("Images/pokemon_water/WaterIcon.png");
        //waterHbox.PackStart(waterImage, false, false, 2);
        //waterHbox.PackStart(lblWaterText, false, false, 2);
        //waterEventBox.Add(waterHbox);
        //waterMI.Child = waterEventBox;
        //waterMI.Activated += WaterPokemonHomepage;

        #endregion Menubar Settings

        // Pokémons button:
        Button btnPokemonsWater = ButtonGenerator("Images/pokemon_water/WaterIcon.png", 150, 175);
        fix.Put(btnPokemonsWater, 250, 50);
        btnPokemonsWater.Clicked += WaterPokemonScreen;

        mb.Append(pokemonsMI);
        pokemonsMI.Submenu = pokemonsMenu;
        pokemonsMenu.Append(waterMenuItem);

        fix.Add(vbMb);
        homeScreen.Add(fix);
        homeScreen.ShowAll();
    }

    #region Water screens

    private void WaterPokemonHomepage(object? sender, EventArgs e)
    {
        Window win = new Window("PokéTrainer© // Pokémons tipo - Água");
        Fixed fix = new Fixed();

        win.SetDefaultSize(800, 500);
        win.SetPosition(WindowPosition.Center);
        win.Resizable = false;
        win.SetIconFromFile("Images/poketrainer_icon.png");

        Image pokemonWaterHomescreen = new Image("Images/pokemon_water/background_pokemonWater_homescreen.png");
        fix.Put(pokemonWaterHomescreen, 0, 0);

        // Pokémons button:
        Button btnPokemonsWater = new Button();
        Image ImagePokemonsWater = new Image("Images/pokemon_water/btn_pokemon_water.png");
        btnPokemonsWater.Image = ImagePokemonsWater;
        btnPokemonsWater.SetSizeRequest(150, 175);
        btnPokemonsWater.Relief = ReliefStyle.None;
        fix.Put(btnPokemonsWater, 250, 50);
        btnPokemonsWater.Clicked += WaterPokemonScreen;

        // Items button:
        Button btnItemsWater = new Button();
        Image ImageItemsWater = new Image("Images/pokemon_water/WaterIcon.png");
        btnItemsWater.Image = ImageItemsWater;
        btnItemsWater.SetSizeRequest(150, 175);
        btnItemsWater.Relief = ReliefStyle.None;
        fix.Put(btnItemsWater, 425, 50);
        btnItemsWater.Clicked += PageWaterItemsPokemon;

        // Trainers button:
        Button btnTrainersWater = new Button();
        Image ImageTrainersWater = new Image("Images/pokemon_water/WaterIcon.png");
        btnTrainersWater.Image = ImageTrainersWater;
        btnTrainersWater.SetSizeRequest(150, 175);
        btnTrainersWater.Relief = ReliefStyle.None;
        fix.Put(btnTrainersWater, 600, 50);
        btnTrainersWater.Clicked += PageWaterTrainersPokemon;

        win.Add(fix);
        win.ShowAll();
    }

    private void WaterPokemonScreen(object? sender, EventArgs e)
    {
        Window waterPokemonScreen = WindowGenerator(500, 600, "PokéTrainer© // Pokémons tipo - Água // Pokémons");
        Image backgroundScreen = new Image("Images/pokemon_water/pokemon_water_homescreen.png");
        Fixed fix = new Fixed();
        fix.Put(backgroundScreen, 0, 0);

        Entry txtSearchPokemon = new Entry();
        txtSearchPokemon.Text = "Buscar Pokémon";
        txtSearchPokemon.SetSizeRequest(125, 20);
        fix.Put(txtSearchPokemon, 165, 25);

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
        pokeball1 = ButtonGenerator("Images/pokeball.png", 40, 40);
        fix.Put(pokeball1, 50, 145);
        // Pokeball 2
        pokeball2 = ButtonGenerator("Images/pokeball.png", 40, 40);
        fix.Put(pokeball2, 108, 145);
        // Pokeball 3
        pokeball3 = ButtonGenerator("Images/pokeball.png", 40, 40);
        fix.Put(pokeball3, 165, 145);
        // Pokeball 4
        pokeball4 = ButtonGenerator("Images/pokeball.png", 40, 40);
        fix.Put(pokeball4, 223, 145);
        // Pokeball 5
        pokeball5 = ButtonGenerator("Images/pokeball.png", 40, 40);
        fix.Put(pokeball5, 280, 145);
        // Pokeball 6
        pokeball6 = ButtonGenerator("Images/pokeball.png", 40, 40);
        fix.Put(pokeball6, 338, 145);
        // Pokeball 7
        pokeball7 = ButtonGenerator("Images/pokeball.png", 40, 40);
        fix.Put(pokeball7, 396, 145);

        #endregion First row of button

        #region Second button row

        // Pokeball 8
        pokeball8 = ButtonGenerator("Images/pokeball.png", 40, 40);
        fix.Put(pokeball8, 50, 203);
        // Pokeball 9
        pokeball9 = ButtonGenerator("Images/pokeball.png", 40, 40);
        fix.Put(pokeball9, 108, 203);
        // Pokeball 10
        pokeball10 = ButtonGenerator("Images/pokeball.png", 40, 40);
        fix.Put(pokeball10, 165, 203);
        // Pokeball 11
        pokeball11 = ButtonGenerator("Images/pokeball.png", 40, 40);
        fix.Put(pokeball11, 223, 203);
        // Pokeball 12
        pokeball12 = ButtonGenerator("Images/pokeball.png", 40, 40);
        fix.Put(pokeball12, 280, 203);
        // Pokeball 13
        pokeball13 = ButtonGenerator("Images/pokeball.png", 40, 40);
        fix.Put(pokeball13, 338, 203);
        // Pokeball 14
        pokeball14 = ButtonGenerator("Images/pokeball.png", 40, 40);
        fix.Put(pokeball14, 396, 203);

        #endregion Second button row

        #region Third button row

        // Pokeball 15
        pokeball15 = ButtonGenerator("Images/pokeball.png", 40, 40);
        fix.Put(pokeball15, 50, 261);
        // Pokeball 16
        pokeball16 = ButtonGenerator("Images/pokeball.png", 40, 40);
        fix.Put(pokeball16, 108, 261);
        // Pokeball 17
        pokeball17 = ButtonGenerator("Images/pokeball.png", 40, 40);
        fix.Put(pokeball17, 165, 261);
        // Pokeball 18
        pokeball18 = ButtonGenerator("Images/pokeball.png", 40, 40);
        fix.Put(pokeball18, 223, 261);
        // Pokeball 19
        pokeball19 = ButtonGenerator("Images/pokeball.png", 40, 40);
        fix.Put(pokeball19, 280, 261);
        // Pokeball 20
        pokeball20 = ButtonGenerator("Images/pokeball.png", 40, 40);
        fix.Put(pokeball20, 338, 261);
        // Pokeball 21
        pokeball21 = ButtonGenerator("Images/pokeball.png", 40, 40);
        fix.Put(pokeball21, 396, 261);

        #endregion Third button row

        #region Fourth button row

        // Pokeball 22
        pokeball22 = ButtonGenerator("Images/pokeball.png", 40, 40);
        fix.Put(pokeball22, 50, 319);
        // Pokeball 23
        pokeball23 = ButtonGenerator("Images/pokeball.png", 40, 40);
        fix.Put(pokeball23, 108, 319);
        // Pokeball 24
        pokeball24 = ButtonGenerator("Images/pokeball.png", 40, 40);
        fix.Put(pokeball24, 165, 319);
        // Pokeball 25
        pokeball25 = ButtonGenerator("Images/pokeball.png", 40, 40);
        fix.Put(pokeball25, 223, 319);
        // Pokeball 26
        pokeball26 = ButtonGenerator("Images/pokeball.png", 40, 40);
        fix.Put(pokeball26, 280, 319);
        // Pokeball 27
        pokeball27 = ButtonGenerator("Images/pokeball.png", 40, 40);
        fix.Put(pokeball27, 338, 319);
        // Pokeball 28
        pokeball28 = ButtonGenerator("Images/pokeball.png", 40, 40);
        fix.Put(pokeball28, 396, 319);

        #endregion Fourth button row

        #region Fifth button row

        // Pokeball 29
        pokeball29 = ButtonGenerator("Images/pokeball.png", 40, 40);
        fix.Put(pokeball29, 50, 376);
        // Pokeball 30
        pokeball30 = ButtonGenerator("Images/pokeball.png", 40, 40);
        fix.Put(pokeball30, 108, 376);
        // Pokeball 31
        pokeball31 = ButtonGenerator("Images/pokeball.png", 40, 40);
        fix.Put(pokeball31, 165, 376);
        // Pokeball 32
        pokeball32 = ButtonGenerator("Images/pokeball.png", 40, 40);
        fix.Put(pokeball32, 223, 376);
        // Pokeball 33
        pokeball33 = ButtonGenerator("Images/pokeball.png", 40, 40);
        fix.Put(pokeball33, 280, 376);
        // Pokeball 34
        pokeball34 = ButtonGenerator("Images/pokeball.png", 40, 40);
        fix.Put(pokeball34, 338, 376);
        // Pokeball 35
        pokeball35 = ButtonGenerator("Images/pokeball.png", 40, 40);
        fix.Put(pokeball35, 396, 376);

        #endregion Fifth button row

        #region Sixth button row

        // Pokeball 36
        pokeball36 = ButtonGenerator("Images/pokeball.png", 40, 40);
        fix.Put(pokeball36, 50, 434);
        // Pokeball 37
        pokeball37 = ButtonGenerator("Images/pokeball.png", 40, 40);
        fix.Put(pokeball37, 108, 434);
        // Pokeball 38
        pokeball38 = ButtonGenerator("Images/pokeball.png", 40, 40);
        fix.Put(pokeball38, 165, 434);
        // Pokeball 39
        pokeball39 = ButtonGenerator("Images/pokeball.png", 40, 40);
        fix.Put(pokeball39, 223, 434);
        // Pokeball 40
        pokeball40 = ButtonGenerator("Images/pokeball.png", 40, 40);
        fix.Put(pokeball40, 280, 434);
        // Pokeball 41
        pokeball41 = ButtonGenerator("Images/pokeball.png", 40, 40);
        fix.Put(pokeball41, 338, 434);
        // Pokeball 42
        pokeball42 = ButtonGenerator("Images/pokeball.png", 40, 40);
        fix.Put(pokeball42, 396, 434);

        #endregion Sixth button row

        #region Seventh button row

        // Pokeball 43
        pokeball43 = ButtonGenerator("Images/pokeball.png", 40, 40);
        fix.Put(pokeball43, 50, 493);
        // Pokeball 44
        pokeball44 = ButtonGenerator("Images/pokeball.png", 40, 40);
        fix.Put(pokeball44, 108, 493);
        // Pokeball 45
        pokeball45 = ButtonGenerator("Images/pokeball.png", 40, 40);
        fix.Put(pokeball45, 165, 493);
        // Pokeball 46
        pokeball46 = ButtonGenerator("Images/pokeball.png", 40, 40);
        fix.Put(pokeball46, 223, 493);
        // Pokeball 47
        pokeball47 = ButtonGenerator("Images/pokeball.png", 40, 40);
        fix.Put(pokeball47, 280, 493);
        // Pokeball 48
        pokeball48 = ButtonGenerator("Images/pokeball.png", 40, 40);
        fix.Put(pokeball48, 338, 493);
        // Pokeball 49
        pokeball49 = ButtonGenerator("Images/pokeball.png", 40, 40);
        fix.Put(pokeball49, 396, 493);

        #endregion Seventh button row

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
                    pokeball1.Clicked -= TaurosScreen;
                    pokeball1.Clicked -= PoliwratScreen;
                    ButtonUpdate("Images/pokemon_water/pure_pokemon/0007_squirtle.png", pokeball1, SquirtleScreen);
                }
                else if (typeSelected == "Puro tipo Água")
                {
                    pokeball1.Clicked -= TaurosScreen;
                    pokeball1.Clicked -= PoliwratScreen;
                    ButtonUpdate("Images/pokemon_water/pure_pokemon/0007_squirtle.png", pokeball1, SquirtleScreen);
                }
                else if (typeSelected == "Meio - Primário")
                {
                    pokeball1.Clicked -= SquirtleScreen;
                    pokeball1.Clicked -= TaurosScreen;
                    ButtonUpdate("Images/pokemon_water/primary_pokemon/0062_poliwrat.png", pokeball1, PoliwratScreen);
                }
                else
                {
                    pokeball1.Clicked -= SquirtleScreen;
                    pokeball1.Clicked -= PoliwratScreen;
                    ButtonUpdate("Images/pokemon_water/secundary_pokemon/0128_tauros.png", pokeball1, TaurosScreen);
                }
            }
        };

        waterPokemonScreen.Add(fix);
        waterPokemonScreen.ShowAll();
    }

    private void PageWaterItemsPokemon(object? sender, EventArgs e)
    {
        Window win = new Window("PokéWiki // Pokémons tipo - Água");
        Fixed fix = new Fixed();

        win.SetDefaultSize(200, 600);
        win.SetPosition(WindowPosition.Center);
        win.Resizable = false;
        win.SetIconFromFile("Images/AppIcon - Pokémon.png");
    }

    private void PageWaterTrainersPokemon(object? sender, EventArgs e)
    {
        Window win = new Window("PokéWiki // Pokémons tipo - Água");
        Fixed fix = new Fixed();

        win.SetDefaultSize(200, 600);
        win.SetPosition(WindowPosition.Center);
        win.Resizable = false;
        win.SetIconFromFile("Images/AppIcon - Pokémon.png");
    }

    #region Pokémons

    private void SquirtleScreen(object? sender, EventArgs e)
    {
        Window squirtleScreen = WindowGenerator(600, 500, "PokéTrainer© // Pokémons tipo - Água // Squirtle - #0007");
        squirtleScreen.ShowAll();
    }

    private void PoliwratScreen(object? sender, EventArgs e)
    {
        Window poliwratScreen = WindowGenerator(600, 500, "PokéTrainer© // Pokémons tipo - Água // Poliwrat - #0062");
        poliwratScreen.ShowAll();
    }

    private void TaurosScreen(object? sender, EventArgs e)
    {
        Window taurosScreen = WindowGenerator(600, 500, "PokéTrainer© // Pokémons tipo - Água // Tauros - #0128");
        taurosScreen.ShowAll();
    }

    #endregion Pokémons

    #endregion Water screens

    public Window WindowGenerator(int width, int height, string pageName)
    {
        Window win = new Window(pageName);
        win.SetIconFromFile("Images/poketrainer_icon.png");
        win.SetDefaultSize(width, height);
        win.SetPosition(WindowPosition.Center);
        win.Resizable = false;
        win.ShowAll();
        return win;
    }

    public Button ButtonGenerator(string imagePath, int width, int height)
    {
        Button btnPokeball = new Button();
        Image imgPokeball = new Image(imagePath);
        btnPokeball.SetSizeRequest(width, height);
        btnPokeball.Relief = ReliefStyle.None;
        btnPokeball.Image = imgPokeball;
        return btnPokeball;
    }

    private Gtk.MenuItem MenuItemGenerator(string label, string imagePath, EventHandler activatedHandler)
    {
        Gtk.MenuItem menuItem = new Gtk.MenuItem();
        EventBox eventBox = new EventBox();
        HBox hbox = new HBox(false, 0);
        Label labelWidget = new Label();
        Image image = new Image();

        labelWidget.Markup = $"<span font_desc='Sans 9'>{label}</span>";
        image.Pixbuf = new Gdk.Pixbuf(imagePath);
        hbox.PackStart(image, false, false, 2);
        hbox.PackStart(labelWidget, false, false, 2);
        eventBox.Add(hbox);
        menuItem.Child = eventBox;

        if (activatedHandler != null)
        {
            menuItem.Activated += activatedHandler;
        }

        return menuItem;
    }

    public void ButtonUpdate(string imagePath, Button button, EventHandler activatedHandler)
    {
        Image buttonImage = new Image(imagePath);
        button.Image = buttonImage;

        button.Clicked += activatedHandler;
    }

    public static void Main()
    {
        Application.Init();
        new GtkSharpApp();
        Application.Run();
    }
}