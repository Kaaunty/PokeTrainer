using Gtk;
using NowComesGtk.Utils;
using PokeApi.BackEnd.Service;
using PokeApiNet;
using System.Security.Authentication.ExtendedProtection;

namespace NowComesGtk.Screens
{
    public class MovementScreen : BaseWindow
    {
#nullable disable

        private ListStore moves = new(typeof(string), typeof(Gdk.Pixbuf));
        private Dictionary<string, string> moveLearnByEgg = new();
        private Dictionary<string, string> moveLearnByTmHm = new();
        private Dictionary<string, string> moveLearnByLevelUp = new();
        private Dictionary<string, string> moveLearnByMoveTutor = new();
        private ApiRequest _apiRequest = new();
        private Entry txtSearchMoves = new();
        private List<Move> Moves = new();
        private ListStore moveList;
        private Pokemon poke;
        private Pokemon pokeSpecie;

        private string defaultText = "Buscar Movimento";
        private int choice;
        private int i = 0;

        private enum Column
        {
            Move,
            Type
        }

        public MovementScreen(List<Move> move, Pokemon pokemon, Pokemon pokemonSpecie) : base("", 500, 500)
        {
            Moves = move;
            poke = pokemon;
            pokeSpecie = pokemonSpecie;

            string title = "PokéTrainer© // Pokémons tipo - Água // Pokemon [#0000] - Movimentos";
            Title = title;
            BorderWidth = 25;

            GetMoveLearnMethod();

            Fixed fix = new();
            VBox vBox = new(false, 50);
            vBox.BorderWidth = 25;
            fix.Put(vBox, 0, 75);

            #region FocusIn and FocusOut Event (txtSearchMove)

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

            #endregion FocusIn and FocusOut Event (txtSearchMove)

            #region ComboBox

            ComboBox cbWayOfLearning = new();
            fix.Put(cbWayOfLearning, 180, 60);

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
                        choice = 1;
                        AllTypeClicked();
                    }
                    else if (way == "TM/HM")
                    {
                        choice = 2;
                        AllTypeClicked();
                    }
                    else if (way == "Level Up")
                    {
                        choice = 3;
                        AllTypeClicked();
                    }
                    else if (way == "Move Tutor")
                    {
                        choice = 4;
                        AllTypeClicked();
                    }
                    else
                    {
                        choice = 0;
                        AllTypeClicked();
                    }
                }
            };

            #endregion ComboBox

            ScrolledWindow sw = new ScrolledWindow();
            sw.ShadowType = ShadowType.EtchedIn;
            sw.SetPolicy(PolicyType.Automatic, PolicyType.Automatic);
            sw.SetSizeRequest(400, 400);
            vBox.PackStart(sw, true, true, 0);

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
            if (!string.IsNullOrEmpty(txtSearchMoves.Text) && txtSearchMoves.Text != "Buscar Movimento")
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
        private void AllTypeClicked()
        {
            try
            {
                if (choice == 0)
                {
                    moveList.Clear();
                    CreateModel();
                }
                else if (choice == 1)
                {
                    moveList.Clear();
                    foreach (var move in moveLearnByEgg)
                    {
                        Gdk.Pixbuf pixbuf = new($"Images/pokemon_types/{move.Value}.png");
                        moveList.AppendValues(move.Key, pixbuf);
                    }
                }
                else if (choice == 2)
                {
                    moveList.Clear();
                    foreach (var move in moveLearnByTmHm)
                    {
                        Gdk.Pixbuf pixbuf = new($"Images/pokemon_types/{move.Value}.png");
                        moveList.AppendValues(move.Key, pixbuf);
                    }
                }
                else if (choice == 3)
                {
                    moveList.Clear();
                    foreach (var move in moveLearnByLevelUp)
                    {
                        Gdk.Pixbuf pixbuf = new($"Images/pokemon_types/{move.Value}.png");
                        moveList.AppendValues(move.Key, pixbuf);
                    }
                }
                else if (choice == 4)
                {
                    moveList.Clear();
                    foreach (var move in moveLearnByMoveTutor)
                    {
                        Gdk.Pixbuf pixbuf = new($"Images/pokemon_types/{move.Value}.png");
                        moveList.AppendValues(move.Key, pixbuf);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void GetMoveLearnMethod()
        {
            foreach (var move in Moves)
            {


                var MoveLearnByEgg = poke.Moves.Any(pokemonMove => pokemonMove.Move.Name == move.Name && pokemonMove.VersionGroupDetails.Last().MoveLearnMethod.Name == "egg"
                || pokeSpecie.Moves.Any(specieMove => specieMove.Move.Name == move.Name && specieMove.VersionGroupDetails.Last().MoveLearnMethod.Name == "egg"));

                bool MoveLearnByMachine = poke.Moves.Any(pokemonMove => pokemonMove.Move.Name == move.Name && pokemonMove.VersionGroupDetails.Last().MoveLearnMethod.Name == "machine"
                || pokeSpecie.Moves.Any(specieMove => specieMove.Move.Name == move.Name && specieMove.VersionGroupDetails.Last().MoveLearnMethod.Name == "machine"));

                bool MoveLearnByLevelUp = poke.Moves.Any(pokemonMoves => pokemonMoves.Move.Name == move.Name && pokemonMoves.VersionGroupDetails.Last().MoveLearnMethod.Name == "level-up"
                || pokeSpecie.Moves.Any(specieMove => specieMove.Move.Name == move.Name && specieMove.VersionGroupDetails.Last().MoveLearnMethod.Name == "level-up"));

                bool MoveLearnByTutor = poke.Moves.Any(pokemonMoves => pokemonMoves.Move.Name == move.Name && pokemonMoves.VersionGroupDetails.Last().MoveLearnMethod.Name == "tutor"
                || pokeSpecie.Moves.Any(specieMove => specieMove.Move.Name == move.Name && specieMove.VersionGroupDetails.Last().MoveLearnMethod.Name == "tutor"));

                if (MoveLearnByEgg)
                {
                    moveLearnByEgg.Add(move.Name, move.Type.Name);
                }
                if (MoveLearnByMachine)
                {
                    moveLearnByTmHm.Add(move.Name, move.Type.Name);
                }
                if (MoveLearnByLevelUp)
                {
                    moveLearnByLevelUp.Add(move.Name, move.Type.Name);
                }
                if (MoveLearnByTutor)
                {
                    moveLearnByMoveTutor.Add(move.Name, move.Type.Name); ;
                }
            }
        }
    }
}