using Gtk;
using NowComesGtk.Utils;
using PokeTrainerBackEndTest.Entities;

namespace NowComesGtk.Screens
{
    public class MovementScreen : BaseWindow
    {
#nullable disable

        private ListStore _moves = new(typeof(string), typeof(Gdk.Pixbuf));
        private Dictionary<string, string> _moveLearnByEgg = new();
        private Dictionary<string, string> _moveLearnByTmHm = new();
        private Dictionary<string, string> _moveLearnByLevelUp = new();
        private Dictionary<string, string> _moveLearnByMoveTutor = new();
        private List<Move> _Moves = new();
        private ListStore _moveList;

        private int _choice;

        private enum Column
        {
            Move
        }

        public MovementScreen(List<Move> move, string pokemonType) : base("", 500, 500)
        {
            _Moves = move;

            string title = "PokéTrainer© // Pokémons tipo - Água // Pokemon [#0000] - Movimentos";
            Title = title;

            Fixed fix = new();

            Image backgroundScreen = new($"Images/moves_homescreen/{pokemonType}.png");
            fix.Put(backgroundScreen, 0, 0);

            VBox vBox = new(false, 50);
            fix.Put(vBox, 50, 85);

            #region ComboBox

            ComboBox cbWayOfLearning = new();
            fix.Put(cbWayOfLearning, 200, 45);

            ListStore waysToLearn = new ListStore(typeof(string));
            waysToLearn.AppendValues("Todos");
            waysToLearn.AppendValues("Ovo");
            waysToLearn.AppendValues("TM/HM");
            waysToLearn.AppendValues("Level Up");
            waysToLearn.AppendValues("Move Tutor");
            cbWayOfLearning.Model = waysToLearn;
            cbWayOfLearning.Active = 0;

            CellRendererText cell = new CellRendererText();
            cbWayOfLearning.PackStart(cell, false);
            cbWayOfLearning.AddAttribute(cell, "text", 0);

            cbWayOfLearning.Changed += (sender, e) =>
            {
                TreeIter searchByLearn;
                if (cbWayOfLearning.GetActiveIter(out searchByLearn))
                {
                    var way = (string)waysToLearn.GetValue(searchByLearn, 0);

                    if (way == "Ovo")
                    {
                        _choice = 1;
                        AllTypeClicked();
                    }
                    else if (way == "TM/HM")
                    {
                        _choice = 2;
                        AllTypeClicked();
                    }
                    else if (way == "Level Up")
                    {
                        _choice = 3;
                        AllTypeClicked();
                    }
                    else if (way == "Move Tutor")
                    {
                        _choice = 4;
                        AllTypeClicked();
                    }
                    else
                    {
                        _choice = 0;
                        AllTypeClicked();
                    }
                }
            };

            #endregion ComboBox

            ScrolledWindow sw = new();
            sw.ShadowType = ShadowType.EtchedIn;
            sw.SetPolicy(PolicyType.Automatic, PolicyType.Automatic);
            sw.SetSizeRequest(400, 400);
            vBox.PackStart(sw, true, true, 0);

            _moveList = CreateModel();
            TreeView treeView = new(_moveList);
            treeView.RulesHint = true;

            sw.Add(treeView);
            AddColumns(treeView);
            Add(fix);
            DeleteEvent += delegate { Dispose(); Destroy(); };
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
        }

        private ListStore CreateModel()
        {
            foreach (Move move in _Moves)
            {
                Gdk.Pixbuf pixbuf = new($"Images/pokemon_types/{move.Type}.png");
                _moves.AppendValues(move.Name, pixbuf);
            }
            return _moves;
        }

        private void AllTypeClicked()
        {
            try
            {
                if (_choice == 0)
                {
                    _moveList.Clear();
                    CreateModel();
                }
                else if (_choice == 1)
                {
                    _moveList.Clear();
                    foreach (var move in _moveLearnByEgg)
                    {
                        Gdk.Pixbuf pixbuf = new($"Images/pokemon_types/{move.Value}.png");
                        _moveList.AppendValues(move.Key, pixbuf);
                    }
                }
                else if (_choice == 2)
                {
                    _moveList.Clear();
                    foreach (var move in _moveLearnByTmHm)
                    {
                        Gdk.Pixbuf pixbuf = new($"Images/pokemon_types/{move.Value}.png");
                        _moveList.AppendValues(move.Key, pixbuf);
                    }
                }
                else if (_choice == 3)
                {
                    _moveList.Clear();
                    foreach (var move in _moveLearnByLevelUp)
                    {
                        Gdk.Pixbuf pixbuf = new($"Images/pokemon_types/{move.Value}.png");
                        _moveList.AppendValues(move.Key, pixbuf);
                    }
                }
                else if (_choice == 4)
                {
                    _moveList.Clear();
                    foreach (var move in _moveLearnByMoveTutor)
                    {
                        Gdk.Pixbuf pixbuf = new($"Images/pokemon_types/{move.Value}.png");
                        _moveList.AppendValues(move.Key, pixbuf);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}