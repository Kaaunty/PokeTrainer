using NowComesGtk.Utils;
using PokeApiNet;
using Gtk;
using Gdk;

namespace NowComesGtk.Screens
{
    public class MovementScreen : BaseWindow
    {
#nullable disable

        private ListStore moves = new(typeof(string), typeof(Gdk.Pixbuf));
        private Entry txtSearchMove = new();
        private List<Move> Moves = new();
        private ListStore moveList;
        string defaultText = "Buscar movimento";

        private enum Column
        {
            Move,
            Type
        }

        public MovementScreen(List<Move> move) : base("", 500, 500)
        {
            Moves = move;
            Title = "PokéTrainer© // Pokémons tipo - Água // Pokemon [#0000] - Movimentos";
            BorderWidth = 25;
            Fixed fix = new();
            VBox vBox = new(false, 50);
            vBox.BorderWidth = 25;
            fix.Put(vBox, 0, 70);
            fix.Put(txtSearchMove, 150, 10);

            txtSearchMove.Text = defaultText;
            txtSearchMove.Changed += TxtSearchMovement;
            CssProvider cssProvider = new CssProvider();
            cssProvider.LoadFromData("entry { color: rgb(200, 200, 200); }");
            txtSearchMove.StyleContext.AddProvider(cssProvider, StyleProviderPriority.Application);

            txtSearchMove.FocusInEvent += (sender, e) =>
            {
                txtSearchMove.Text = string.Empty;
                CssProvider cssProvider = new CssProvider();
                cssProvider.LoadFromData("entry { color: rgb(0, 0, 0); }");
                txtSearchMove.StyleContext.AddProvider(cssProvider, StyleProviderPriority.Application);
            };
            txtSearchMove.FocusOutEvent += (sender, e) =>
            {
                txtSearchMove.Text = defaultText;
                CssProvider cssProvider = new CssProvider();
                cssProvider.LoadFromData("entry { color: rgb(200, 200, 200); }");
                txtSearchMove.StyleContext.AddProvider(cssProvider, StyleProviderPriority.Application);
            };

            ScrolledWindow sw = new ScrolledWindow();
            sw.ShadowType = ShadowType.EtchedIn;
            sw.SetPolicy(PolicyType.Automatic, PolicyType.Automatic);
            sw.SetSizeRequest(400, 400);
            vBox.PackStart(sw, true, true, 0);

            moveList = CreateModel();
            TreeView treeView = new(moveList);
            treeView.RulesHint = true;

            sw.Add(treeView);
            AddColumns(treeView);
            Add(fix);
            ShowAll();
        }
        private void TxtSearchMovement(object sender, EventArgs e)
        {
            if (txtSearchMove != null)
            {
                string searchText = txtSearchMove.Text.Replace(' ', '-').ToLower();
                moveList.Clear();
                foreach (Move move in Moves)
                {
                    if (move.Name.Contains(searchText))
                    {
                        moveList.AppendValues(move.Name, move.Type.Name);
                    }
                }
            }
        }
        private void AddColumns(TreeView treeView)
        {
            CellRendererText rendererText = new CellRendererText();
            TreeViewColumn column = new TreeViewColumn("Movimento", rendererText, "text", (int)Column.Move);
            column.SortColumnId = (int)Column.Move;
            treeView.AppendColumn(column);
            treeView.AppendColumn("Tipo", new CellRendererPixbuf(), "pixbuf", 1);
            column.FixedWidth = 200;
        }

        private ListStore CreateModel()
        {
            try
            {
                foreach (Move move in Moves)
                {
                    Pixbuf pixbuf = new($"Images/pokemon_types/{move.Type.Name}.png");
                    moves.AppendValues(move.Name, pixbuf);
                }
                return moves;
            }
            catch(Exception) 
            {
                throw;
            }
        }
    }
}