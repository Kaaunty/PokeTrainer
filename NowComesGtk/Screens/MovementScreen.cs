using NowComesGtk.Utils;
using Gtk;
using PokeApiNet;

namespace NowComesGtk.Screens
{
    public class MovementScreen : BaseWindow
    {
#nullable disable

        private List<Move> Moves = new();
        private Entry txtSearchMove = new Entry();
        private ListStore moves = new ListStore(typeof(string), typeof(Gdk.Pixbuf));
        private ListStore moveList;

        private enum Column
        {
            Move,
            Type
        }

        private int i = 0;

        public MovementScreen(List<Move> move) : base("", 500, 500)
        {
            this.Moves = move;

            string title = "PokéTrainer© // Pokémons tipo - Água // Pokemon [#0000] - Movimentos";
            Title = title;
            BorderWidth = 25;

            Fixed fix = new();
            VBox vBox = new(false, 50);
            vBox.BorderWidth = 25;

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

            moveList = CreateModel();
            TreeView treeView = new TreeView(moveList);
            treeView.RulesHint = true;
            sw.Add(treeView);

            AddColumns(treeView);
            fix.Add(vBox);
            Add(fix);
            ShowAll();
        }

        private void TxtSearchMove_Changed(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSearchMove.Text))
            {
                moveList.Clear();
                foreach (Move move in Moves)
                {
                    if (move.Name.Contains(txtSearchMove.Text))
                    {
                        moveList.AppendValues(move.Name, move.Type.Name);
                    }
                }
            }
            else
            {
                moveList.Clear();
                foreach (Move move in Moves)
                {
                    moveList.AppendValues(move.Name, move.Type.Name);
                }
            }
        }

        private void AddColumns(TreeView treeView)
        {
            CellRendererText rendererText = new CellRendererText();
            TreeViewColumn column = new TreeViewColumn("Movimento", rendererText,
                "text", (int)Column.Move);
            column.SortColumnId = (int)Column.Move;
            treeView.AppendColumn(column);
            treeView.AppendColumn("Tipo", new Gtk.CellRendererPixbuf(), "pixbuf", 1);
            column.FixedWidth = 200;

            //column = new TreeViewColumn("Tipo", rendererText, "text", (int)Column.Type);
            //column.SortColumnId = (int)Column.Type;
            //treeView.AppendColumn(column);
        }

        private ListStore CreateModel()
        {
            foreach (Move move in Moves)
            {
                Gdk.Pixbuf pixbuf = new Gdk.Pixbuf($"Images/pokemon_types/{move.Type.Name}.png");
                moves.AppendValues(move.Name, pixbuf);
            }
            return moves;
        }
    }
}