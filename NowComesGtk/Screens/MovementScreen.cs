using NowComesGtk.Utils;
using Gtk;
using PokeApiNet;

namespace NowComesGtk.Screens
{
    public class MovementScreen : BaseWindow
    {
#nullable disable


        private ListStore moves = new(typeof(string), typeof(Gdk.Pixbuf));
        private Entry txtSearchMoves = new();
        private List<Move> Moves = new();
        private ListStore moveList;
        private string defaultText = "Buscar Movimento";

        private enum Column
        {
            Move,
            Type
        }

        private int i = 0;

        public MovementScreen(List<Move> move) : base("", 500, 500)
        {
            Moves = move;

            string title = "PokéTrainer© // Pokémons tipo - Água // Pokemon [#0000] - Movimentos";
            Title = title;
            BorderWidth = 25;

            Fixed fix = new();
            VBox vBox = new(false, 50);
            vBox.BorderWidth = 25;
            fix.Put(vBox, 0, 75);

            #region FocusIn and FocusOut Event (txtSearchMove)

            //string defaultText = "Buscar Movimento";
            //txtSearchMove.SetSizeRequest(200, 20);
            //txtSearchMove.Text = defaultText;
            //CssProvider cssProvider = new CssProvider();
            //cssProvider.LoadFromData("entry { color: rgb(200, 200, 200); }");
            //txtSearchMove.StyleContext.AddProvider(cssProvider, StyleProviderPriority.Application);
            //vBox.Add(txtSearchMove);

            //txtSearchMove.FocusInEvent += (sender, e) =>
            //{
            //    txtSearchMove.Text = string.Empty;
            //    CssProvider cssProvider = new CssProvider();
            //    cssProvider.LoadFromData("entry { color: rgb(0, 0, 0); }");
            //    txtSearchMove.StyleContext.AddProvider(cssProvider, StyleProviderPriority.Application);
            //};
            //txtSearchMove.FocusOutEvent += (sender, e) =>
            //{
            //    txtSearchMove.Text = defaultText;
            //    CssProvider cssProvider = new CssProvider();
            //    cssProvider.LoadFromData("entry { color: rgb(200, 200, 200); }");
            //    txtSearchMove.StyleContext.AddProvider(cssProvider, StyleProviderPriority.Application);
            //};

            //txtSearchMove.Changed += TxtSearchMove_Changed;

            #endregion FocusIn and FocusOut Event (txtSearchMove)

            ScrolledWindow sw = new ScrolledWindow();
            sw.ShadowType = ShadowType.EtchedIn;
            sw.SetPolicy(PolicyType.Automatic, PolicyType.Automatic);
            sw.SetSizeRequest(400, 400);
            vBox.PackStart(sw, true, true, 0);


            CssProvider cssProvider = new();
            cssProvider.LoadFromData("entry { color: rgb(200, 200, 200); }");
            txtSearchMoves.StyleContext.AddProvider(cssProvider, StyleProviderPriority.Application);
            fix.Put(txtSearchMoves, 150, 25);

            txtSearchMoves.FocusInEvent += (sender, e) =>
            {
                txtSearchMoves.Text = string.Empty;
                CssProvider cssProvider = new CssProvider();
                cssProvider.LoadFromData("entry { color: rgb(0, 0, 0); }");
                txtSearchMoves.StyleContext.AddProvider(cssProvider, StyleProviderPriority.Application);
            };
            txtSearchMoves.FocusOutEvent += (sender, e) =>
            {
                txtSearchMoves.Text = defaultText;
                CssProvider cssProvider = new CssProvider();
                cssProvider.LoadFromData("entry { color: rgb(200, 200, 200); }");
                txtSearchMoves.StyleContext.AddProvider(cssProvider, StyleProviderPriority.Application);
            };

            moveList = CreateModel();
            TreeView treeView = new TreeView(moveList);
            treeView.RulesHint = true;
            sw.Add(treeView);

            txtSearchMoves.Changed += SearchMove;
            AddColumns(treeView);
            Add(fix);
            ShowAll();
        }

        private void AddColumns(TreeView treeView)
        {
            CellRendererText rendererText = new CellRendererText();
            TreeViewColumn column = new TreeViewColumn("Movimento", rendererText,
                "text", (int)Column.Move);
            column.SortColumnId = (int)Column.Move;
            treeView.AppendColumn(column);
            treeView.AppendColumn("Tipo", new CellRendererPixbuf(), "pixbuf", 1);
            column.FixedWidth = 200;

            //column = new TreeViewColumn("Tipo", rendererText, "text", (int)Column.Type);
            //column.SortColumnId = (int)Column.Type;
            //treeView.AppendColumn(column);
        }

        private ListStore CreateModel()
        {
            foreach (Move move in Moves)
            {
                Gdk.Pixbuf pixbuf = new($"Images/pokemon_types/{move.Type.Name}.png");
                moves.AppendValues(move.Name, pixbuf);
            }
            return moves;
        }
        private void SearchMove(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(txtSearchMoves.Text))
            {
                string pokeMove = txtSearchMoves.Text;
                pokeMove = pokeMove.Replace(' ', '-');
                moveList.Clear();
                foreach (Move move in Moves)
                {
                    if (move.Name.StartsWith(pokeMove))
                    {
                        Gdk.Pixbuf pixbuf = new($"Images/pokemon_types/{move.Type.Name}.png");
                        moveList.AppendValues(move.Name, pixbuf);
                    }
                }
            }
            else
            {
                moveList.Clear();
                CreateModel();
            }
        }
    }
}