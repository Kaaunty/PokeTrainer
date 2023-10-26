using GLib;
using Gtk;
using NowComesGtk.Reusable_components;
using NowComesGtk.Utils;
using PokeApi.BackEnd.Service;
using PokeApiNet;
using System.Data.Common;

namespace NowComesGtk.Screens
{
    public class PokemonEvolutionScreen : BaseWindow
    {
#nullable disable
        private ApiRequest _apiRequest = new ApiRequest();
        private EvolutionChain evolutionChain = new();
        private Pokemon pokemon = new();
        private PokemonSpecies pokeSpecies = new();
        private ListStore formsList;

        private ListStore forms = new(typeof(string), (typeof(string)));

        private enum Column
        {
            Form,
            FormMethod
        }

        public PokemonEvolutionScreen(EvolutionChain evolutionChain, Pokemon pokemon, PokemonSpecies pokeSpecies) : base("PokéTrainer©", 400, 400)
        {
            this.evolutionChain = evolutionChain;
            this.pokemon = pokemon;
            this.pokeSpecies = pokeSpecies;
            VBox vbox = new();

            ScrolledWindow sw = new ScrolledWindow();
            sw.ShadowType = ShadowType.EtchedIn;
            sw.SetPolicy(PolicyType.Automatic, PolicyType.Automatic);
            sw.SetSizeRequest(170, 170);
            formsList = CreateModel();
            TreeView treeView = new(formsList);
            treeView.RulesHint = true;
            treeView.RowActivated += OnRowActivated;
            sw.Add(treeView);
            AddColumns(treeView);
            vbox.PackStart(sw, true, true, 0);
            vbox.BorderWidth = 10;
            Add(vbox);
            ShowAll();
        }

        private void AddColumns(TreeView treeView)
        {
            CellRendererText rendererText = new CellRendererText();
            TreeViewColumn column = new TreeViewColumn("Evoluções e Formas", rendererText,
                "text", (int)Column.Form);
            column.SortColumnId = (int)Column.Form;
            treeView.AppendColumn(column);
            column = new TreeViewColumn("Forma de Evolução", rendererText,
                               "text", (int)Column.FormMethod);
            treeView.AppendColumn(column);
            //column.FixedWidth = 200;
        }

        private ListStore CreateModel()
        {
            if (evolutionChain.Chain.Species.Name != pokemon.Name)
            {
                forms.AppendValues("Primeira Evolução:" + evolutionChain.Chain.Species.Name);
            }

            foreach (var evo in evolutionChain.Chain.EvolvesTo)
            {
                foreach (var i in evo.EvolutionDetails)
                {
                    if (i != null)
                    {
                        if (i.Trigger.Name == "level-up")
                        {
                            if (i.MinLevel != null)
                            {
                                forms.AppendValues(evo.Species.Name, i.Trigger.Name + i.MinLevel);
                            }
                        }
                        else if (i.Trigger.Name == "trade")
                        {
                            forms.AppendValues(evo.Species.Name, i.Trigger.Name);
                        }
                        else if (i.Trigger.Name == "use-item")
                        {
                            forms.AppendValues(evo.Species.Name, i.Trigger.Name + " - " + i.Item.Name);
                        }
                    }
                }
            }
            if (evolutionChain.Chain.EvolvesTo[0].EvolvesTo.Count > 0)
            {
                foreach (var evo in evolutionChain.Chain.EvolvesTo[0].EvolvesTo)
                {
                    foreach (var i in evo.EvolutionDetails)
                    {
                        if (i != null)
                        {
                            forms.AppendValues($"{evo.Species.Name} - {i.Trigger.Name} Level Minimo: {i.MinLevel}");
                        }
                    }
                }
            }
            if (pokeSpecies.Varieties.Count > 1)
            {
                foreach (var poke in pokeSpecies.Varieties)
                {
                    if (poke.IsDefault == false)
                    {
                        forms.AppendValues(poke.Pokemon.Name);
                    }
                }
            }
            return forms;
        }

        private async void OnRowActivated(object o, RowActivatedArgs args)
        {
            TreeIter iter;
            if (formsList.GetIter(out iter, args.Path))
            {
                string value = (string)formsList.GetValue(iter, 0);
                if (value.Contains("Primeira Evolução:"))
                {
                    pokemon = await _apiRequest.GetPokemonAsync(evolutionChain.Chain.Species.Name);
                    PokemonScreen pokemonScreen = new(pokemon);
                    await pokemonScreen.PopulateFields();
                }
                else if (value.Contains("-normal"))
                {
                    value.Replace("-normal", "");
                    pokemon = await _apiRequest.GetPokemonAsync(value);
                    PokemonScreen pokemonScreen = new(pokemon);
                    await pokemonScreen.PopulateFields();
                }
                else
                {
                    string pokemonName = value;
                    pokemon = await _apiRequest.GetPokemonAsync(pokemonName);
                    PokemonScreen pokemonScreen = new(pokemon);
                    pokemonScreen.Show();
                    Destroy();
                }
            }
        }
    }
}