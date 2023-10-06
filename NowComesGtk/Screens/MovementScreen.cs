using NowComesGtk.Utils;
using Gtk;

namespace NowComesGtk.Screens
{
    public class Moves
    {
        public string Move;

        public Moves(string move)
        {
            Move = move;
        }
    }

    public class MovementScreen : BaseWindow
    {
#nullable disable
        private ListStore moveList;

        private enum Column
        {
            Move
        }

        private List<string> moves;

        private int i = 0;

        //while (i<moves.Count)
        //    {
        //       // += moves[i].ToString() + "\n";
        //        i++;
        //    }
        public MovementScreen() : base("", 250, 250)
        {
            string title = "PokéTrainer© // Pokémons tipo - Água // Pokemon [#0000] - Movimentos";
            Title = title;
            BorderWidth = 25;

            Fixed fix = new();
            VBox vBox = new(false, 50);

            Entry txtSearchMove = new Entry();
            string defaultText = "Buscar Movimento";
            txtSearchMove.SetSizeRequest(200, 20);
            txtSearchMove.Text = defaultText;
            CssProvider cssProvider = new CssProvider();
            cssProvider.LoadFromData("entry { color: rgb(200, 200, 200); }");
            txtSearchMove.StyleContext.AddProvider(cssProvider, StyleProviderPriority.Application);
            vBox.Add(txtSearchMove);

            #region FocusIn and FocusOut Event (txtSearchMove)

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

            #endregion FocusIn and FocusOut Event (txtSearchMove)

            ScrolledWindow sw = new ScrolledWindow();
            sw.ShadowType = ShadowType.EtchedIn;
            sw.SetPolicy(PolicyType.Automatic, PolicyType.Automatic);
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

        private void AddColumns(TreeView treeView)
        {
            CellRendererText rendererText = new CellRendererText();
            TreeViewColumn column = new TreeViewColumn("Movimento", rendererText,
                "text", (int)Column.Move);
            column.SortColumnId = (int)Column.Move;
            treeView.AppendColumn(column);
        }

        private ListStore CreateModel()
        {
            ListStore moves = new ListStore(typeof(string),
                typeof(string), typeof(int));

            foreach (Moves mv in moves)
            {
                moves.AppendValues(mv.Move);
            }

            return moves;
        }
    }
}