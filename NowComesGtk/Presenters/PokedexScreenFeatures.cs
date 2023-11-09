using Gtk;
using PokeApi.BackEnd.Entities;
using PokeApi.BackEnd.Service;
using PokeTrainerBackEnd;
using PokeTrainerBackEnd.Helper;
using PokeTrainerBackEndTest.Entities;

namespace NowComesGtk.Presenters
{
    public class PokedexScreenFeatures
    {
#nullable disable

        private List<Pokemon> _pokemonList = new();
        private PopulateLists populateLists = new();
        private List<Pokemon> _allPokemon = new();
        private List<Pokemon> _pokemonBySearch = new();

        private int _maxPokemonPerPage = 25, _currentPage;

        private enum Choice
        {
            All,
            PureType,
            PrimaryType,
            SecondaryType
        }

        public void Populating(int currentPage, string choiceOfType, int subTypeChoice)
        {
            FilterigForDisplay(currentPage, choiceOfType, subTypeChoice);
        }

        public void FilterigForDisplay(int currentPage, string choiceOfType, int subTypeChoice)
        {
            _currentPage = currentPage;

            if (choiceOfType == "all")
            {
                _allPokemon = populateLists.GetPokemonListByTypeAll(currentPage, choiceOfType);
                _pokemonBySearch = Repository.pokemonListAllType;
            }
            if (subTypeChoice == (int)Choice.All)
            {
                _allPokemon = populateLists.GetPokemonListByTypePure(currentPage, choiceOfType);
                _pokemonBySearch = Repository.pokemonListPureType;
            }
            else if (subTypeChoice == (int)Choice.PureType)
            {
                _allPokemon = populateLists.GetPokemonListByTypePure(currentPage, choiceOfType);
                _pokemonBySearch = Repository.pokemonListPureType;
            }
            else if (subTypeChoice == (int)Choice.PrimaryType)
            {
                _allPokemon = populateLists.GetPokemonListByTypeHalfType(currentPage, choiceOfType);
                _pokemonBySearch = Repository.pokemonListHalfType;
            }
            else if (subTypeChoice == (int)Choice.SecondaryType)
            {
                _allPokemon = populateLists.GetPokemonlistByHalfTypeSecondary(currentPage, choiceOfType);
                _pokemonBySearch = Repository.pokemonListHalfSecundaryType;
            }
        }

        public void Filter(Fixed fix, int currentPage, string type, int choice, string PokeName)
        {
            Populating(currentPage, type, choice);

            string pokemonName = PokeName;
            if (pokemonName != string.Empty && pokemonName != "buscar pokémon")
            {
                _pokemonBySearch = _pokemonBySearch.Where(pokemon => pokemon.Name.StartsWith(pokemonName)).ToList();
                _pokemonList = _pokemonBySearch;
            }
            else
            {
                _pokemonList = _allPokemon;
            }

            UpdateButtons(fix, _pokemonList);
        }

        private void UpdateButtons(Fixed fix, List<Pokemon> pokemonList)
        {
            int ButtonIndex = 0;
            foreach (var button in fix.AllChildren)
            {
                if (button is Button)
                {
                    Button btn = (Button)button;
                    if (ApprovingButtonsToModify(btn.Name))
                    {
                        if (ButtonIndex < pokemonList.Count)
                        {
                            btn.Data["id"] = pokemonList[ButtonIndex].Id;
                            btn.Data["name"] = pokemonList[ButtonIndex].Name;
                            btn.Sensitive = true;

                            if (ApprovingButtonsToModify(btn.Name))
                            {
                                var pokemon = pokemonList[ButtonIndex];
                                UpdateButtonImages(btn, pokemon.Id, new PokemonImageApiRequest());
                            }

                            ButtonIndex++;
                        }
                        else
                        {
                            btn.Sensitive = false;
                            btn.Data["id"] = 0;
                            btn.Data["name"] = "";
                            btn.Image = null;
                        }
                    }
                }
            }
        }

        private async void UpdateButtonImages(Button button, int pokemonId, IPokemonSpriteLoaderAPI pokemonSpriteLoaderAPI)
        {
            Image pokeimage = new Image();
            Byte[] pokemonImage = await pokemonSpriteLoaderAPI.LoadPokemonSprite(pokemonId);
            if (pokemonImage != null)
            {
                pokeimage.Pixbuf = new Gdk.Pixbuf(pokemonImage);
                pokeimage.Pixbuf = pokeimage.Pixbuf.ScaleSimple(50, 50, Gdk.InterpType.Bilinear);
                button.Image = pokeimage;
            }
        }

        private bool ApprovingButtonsToModify(string buttonName)
        {
            if (buttonName == "BackButton" || buttonName == "NextButton")
            {
                return false;
            }
            return true;
        }

        public void DisableNextButton(Button button)
        {
            if (_allPokemon != null && _allPokemon.Count < _maxPokemonPerPage)
            {
                button.Sensitive = false;
            }
        }

        public void DisableBackButton(Button button)
        {
            if (_allPokemon != null && _currentPage == 0)
            {
                button.Sensitive = false;
            }
        }
    }
}