using Gtk;
using Window = Gtk.Window;

public class GtkSharpApp : Window
{
    public GtkSharpApp() : base("PokéTrainer©")
    {
        Fixed fix = new Fixed();

        SetDefaultSize(800, 500);
        SetPosition(WindowPosition.Center);
        Resizable = false;
        SetIconFromFile("Images/AppIcon - Pokémon.png");

        Image steelScreenBackground = new Image("Images/background_homescreen.png");
        fix.Put(steelScreenBackground, 0, 0);

        DeleteEvent += delegate
        {
            Application.Quit();
        };

        #region Menubar Settings :P // Design and functionality

        VBox vbMb = new VBox(false, 0);

        MenuBar mb = new MenuBar();
        vbMb.PackStart(mb, false, false, 0);

        Menu pokemonsMenu = new Menu();
        MenuItem pokemonsMI = new MenuItem("Pokémons");

        // Water MenuItem:
        MenuItem waterMI = new MenuItem();
        EventBox waterEventBox = new EventBox();
        HBox waterHbox = new HBox(false, 0);
        Label lblWaterText = new Label();
        Image waterImage = new Image();
        lblWaterText.Markup = "<span font_desc='Sans 9'>Água</span>";
        waterImage.Pixbuf = new Gdk.Pixbuf("Images/pokemon_water/WaterIcon.png");
        waterHbox.PackStart(waterImage, false, false, 2);
        waterHbox.PackStart(lblWaterText, false, false, 2);
        waterEventBox.Add(waterHbox);
        waterMI.Child = waterEventBox;
        waterMI.Activated += WaterPokemonHomepage;

        #endregion Menubar Settings :P // Design and functionality

        // Pokémons button:
        Button btnPokemonsWater = new Button();
        Image ImagePokemonsWater = new Image("Images/pokemon_water/WaterIcon.png");
        btnPokemonsWater.Image = ImagePokemonsWater;
        btnPokemonsWater.SetSizeRequest(150, 175);
        btnPokemonsWater.Relief = ReliefStyle.None;
        fix.Put(btnPokemonsWater, 250, 50);
        btnPokemonsWater.Clicked += PageWaterPokemon;

        mb.Append(pokemonsMI);
        pokemonsMI.Submenu = pokemonsMenu;
        pokemonsMenu.Append(waterMI);

        fix.Add(vbMb);
        Add(fix);
        ShowAll();
    }

    //Kaua você é gay

    #region Water screens

    private void WaterPokemonHomepage(object sender, EventArgs e)
    {
        Window win = new Window("PokéTrainer© // Pokémons tipo - Água");
        Fixed fix = new Fixed();

        win.SetDefaultSize(800, 500);
        win.SetPosition(WindowPosition.Center);
        win.Resizable = false;
        win.SetIconFromFile("Images/AppIcon - Pokémon.png");

        Image pokemonWaterHomescreen = new Image("Images/pokemon_water/background_pokemonWater_homescreen.png");
        fix.Put(pokemonWaterHomescreen, 0, 0);

        // Pokémons button:
        Button btnPokemonsWater = new Button();
        Image ImagePokemonsWater = new Image("Images/pokemon_water/btn_pokemon_water.png");
        btnPokemonsWater.Image = ImagePokemonsWater;
        btnPokemonsWater.SetSizeRequest(150, 175);
        btnPokemonsWater.Relief = ReliefStyle.None;
        fix.Put(btnPokemonsWater, 250, 50);
        btnPokemonsWater.Clicked += PageWaterPokemon;

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

    private void PageWaterPokemon(object sender, EventArgs e)
    {
        Window win = new Window("PokéTrainer© // Pokémons tipo - Água");
        Fixed fix = new Fixed();

        Image pokemonsHomescreen = new Image("Images/pokemon_water/pokemon_water_homescreen.png");
        fix.Put(pokemonsHomescreen, 0, 0);

        win.SetDefaultSize(500, 600);
        win.SetPosition(WindowPosition.Center);
        win.Resizable = false;
        win.SetIconFromFile("Images/AppIcon - Pokémon.png");

        Entry txtSearchPokemon = new Entry();
        txtSearchPokemon.Text = "Buscar Pokémon";
        txtSearchPokemon.SetSizeRequest(125, 20);
        fix.Put(txtSearchPokemon, 160, 25);

        ComboBox cbTypePokemon = new ComboBox();
        fix.Put(cbTypePokemon, 175, 40);

        #region Buttons

        // btn 1
        Button btnPokemon1 = new Button();
        btnPokemon1.SetSizeRequest(40, 40);
        btnPokemon1.Relief = ReliefStyle.None;
        Image img1 = new Image("Images/pokebola.png");
        btnPokemon1.Image = img1;
        fix.Put(btnPokemon1, 50, 145);
        btnPokemon1.Clicked += WaterPokemonHomepage;

        //// btn 2
        //Button btnPokemon2 = new Button();
        //btnPokemon2.SetSizeRequest(40, 40);
        //Image img2 = new Image("Images/pokebola.png");
        //btnPokemon2.Image = img2;
        //fix.Put(btnPokemon2, 50, 145);
        //btnPokemon2.Clicked += WaterPokemonHomepage;

        //// btn 3
        //Button btnPokemon3 = new Button();
        //btnPokemon3.SetSizeRequest(40, 40);
        //Image img3 = new Image("Images/pokebola.png");
        //btnPokemon3.Image = img3;
        //fix.Put(btnPokemon3, 50, 145);
        //btnPokemon3.Clicked += WaterPokemonHomepage;

        //// btn 4
        //Button btnPokemon4 = new Button();
        //btnPokemon4.SetSizeRequest(40, 40);
        //Image img4 = new Image("Images/pokebola.png");
        //btnPokemon4.Image = img4;
        //fix.Put(btnPokemon4, 50, 145);
        //btnPokemon4.Clicked += WaterPokemonHomepage;

        //// btn 5
        //Button btnPokemon5 = new Button();
        //btnPokemon5.SetSizeRequest(40, 40);
        //Image img5 = new Image("Images/pokebola.png");
        //btnPokemon5.Image = img5;
        //fix.Put(btnPokemon5, 50, 145);
        //btnPokemon5.Clicked += WaterPokemonHomepage;

        //// btn 6
        //Button btnPokemon6 = new Button();
        //btnPokemon6.SetSizeRequest(40, 40);
        //Image img6 = new Image("Images/pokebola.png");
        //btnPokemon6.Image = img6;
        //fix.Put(btnPokemon6, 50, 145);
        //btnPokemon6.Clicked += WaterPokemonHomepage;

        //// btn 7
        //Button btnPokemon7 = new Button();
        //btnPokemon7.SetSizeRequest(40, 40);
        //Image img7 = new Image("Images/pokebola.png");
        //btnPokemon7.Image = img7;
        //fix.Put(btnPokemon7, 50, 145);
        //btnPokemon7.Clicked += WaterPokemonHomepage;

        #endregion

        ListStore typeList = new ListStore(typeof(string));
        typeList.AppendValues("Filtrar por tipo");
        typeList.AppendValues("Puro tipo Água");
        typeList.AppendValues("Meio - Primário");
        typeList.AppendValues("Maio - Secundário");
        typeList.AppendValues("Alterado");
        cbTypePokemon.Model = typeList;

        CellRendererText cell = new CellRendererText();
        cbTypePokemon.PackStart(cell, false);
        cbTypePokemon.AddAttribute(cell, "text", 0);

        cbTypePokemon.Changed += (sender, e) =>
        {
            TreeIter searchByType;
            if (cbTypePokemon.GetActiveIter(out searchByType))
            {
                var typeSelected = (string)typeList.GetValue(searchByType, 0);
                if (typeSelected == "Puro tipo Água")
                {
                    Image ImageAlomola = new Image("Images/pokemon_water/pure_pokemon/0007_squirtle.png");
                    btnPokemon1.Image = ImageAlomola;
                    btnPokemon1.Clicked -= WaterPokemonHomepage;
                    btnPokemon1.Clicked += SquirtleScreen;
                }
                else
                {
                    Image img = new Image("Images/pokemon_water/pure_pokemon/IndoAli.png");
                    btnPokemon1.Image = img;
                    fix.Put(btnPokemon1, 50, 150);
                    btnPokemon1.Clicked -= AlomolaScreen;
                    btnPokemon1.Clicked += WaterPokemonHomepage;
                }
            }
            
        };

        win.Add(fix);
        win.ShowAll();
    }

    private void PageWaterItemsPokemon(object sender, EventArgs e)
    {
        Window win = new Window("PokéWiki // Pokémons tipo - Água");
        Fixed fix = new Fixed();

        win.SetDefaultSize(200, 600);
        win.SetPosition(WindowPosition.Center);
        win.Resizable = false;
        win.SetIconFromFile("Images/AppIcon - Pokémon.png");
    }

    private void PageWaterTrainersPokemon(object sender, EventArgs e)
    {
        Window win = new Window("PokéWiki // Pokémons tipo - Água");
        Fixed fix = new Fixed();

        win.SetDefaultSize(200, 600);
        win.SetPosition(WindowPosition.Center);
        win.Resizable = false;
        win.SetIconFromFile("Images/AppIcon - Pokémon.png");
    }
    
    #region Pokémons

    private void SquirtleScreen (object sender, EventArgs e)
    {
        Window win = new Window("\"PokéTrainer© // Pokémons tipo - Água // Squirtle - #0007");
        Fixed fix = new Fixed();

        win.SetDefaultSize(300, 300);
        win.SetPosition(WindowPosition.Center);
        win.Resizable = false;

        win.Add(fix) ;
        win.ShowAll ();

    }

    #endregion

    #endregion
    public static void Main()
    {
        Application.Init();
        new GtkSharpApp();
        Application.Run();
    }
}