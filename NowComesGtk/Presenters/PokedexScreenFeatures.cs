using PokeTrainerBackEndTest.Entities;
using PokeTrainerBackEnd.Helper;
using PokeApi.BackEnd.Entities;
using PokeApi.BackEnd.Service;
using PokeTrainerBackEnd;
using Gtk;

namespace NowComesGtk.Presenters
{
    public class PokedexScreenFeatures
    {
#nullable disable

        private PopulateLists _populateLists = new();

        private List<Pokemon> _pokemonBySearch = new();
        private List<Pokemon> _pokemonList = new();
        private List<Pokemon> _allPokemon = new();

        private int _maxPokemonPerPage = 25, _firstPage = 0;

        private enum Choice
        {
            All,
            PureType,
            PrimaryType,
            SecondaryType
        }

        public void FilterigForDisplay(int currentPage, string choiceOfType, int subTypeChoice)
        {
            if (choiceOfType == "all")
            {
                _allPokemon = _populateLists.GetPokemonListAll(currentPage);

                _pokemonBySearch = Repository.Pokemon;
            }
            else
            {
                if (subTypeChoice == (int)Choice.All)
                {
                    _allPokemon = _populateLists.GetPokemonListByTypeAll(currentPage, choiceOfType);
                    _pokemonBySearch = Repository.pokemonListAllType;
                }
                else if (subTypeChoice == (int)Choice.PureType)
                {
                    _allPokemon = _populateLists.GetPokemonListByTypePure(currentPage, choiceOfType);
                    _pokemonBySearch = Repository.pokemonListPureType;
                }
                else if (subTypeChoice == (int)Choice.PrimaryType)
                {
                    _allPokemon = _populateLists.GetPokemonListByTypeHalfType(currentPage, choiceOfType);
                    _pokemonBySearch = Repository.pokemonListHalfType;
                }
                else if (subTypeChoice == (int)Choice.SecondaryType)
                {
                    _allPokemon = _populateLists.GetPokemonlistByHalfTypeSecondary(currentPage, choiceOfType);
                    _pokemonBySearch = Repository.pokemonListHalfSecundaryType;
                }
            }
        }

        public void Filter(Fixed fix, int currentPage, string type, int choice, string PokeName)
        {
            FilterigForDisplay(currentPage, type, choice);

            if (!string.IsNullOrEmpty(PokeName) && PokeName != "buscar pokémon")
            {
                _pokemonList = _pokemonBySearch.FindAll(pokemon => pokemon.Name.StartsWith(PokeName.Replace(" ", "-")));
            }
            else
            {
                _pokemonList = _allPokemon;
            }
            UpdateButtons(fix);
        }

        private void UpdateButtons(Fixed fix)
        {
            int buttonIndex = 0;

            foreach (var button in fix.AllChildren)
            {
                if (button is Button)
                {
                    Button btn = (Button)button;
                    if (ApprovingButtonsToModify(btn.Name))
                    {
                        if (buttonIndex < _pokemonList.Count)
                        {
                            btn.Data["id"] = _pokemonList[buttonIndex].Id;
                            btn.Data["name"] = _pokemonList[buttonIndex].Name;
                            btn.Sensitive = true;

                            if (ApprovingButtonsToModify(btn.Name))
                            {
                                var pokemon = _pokemonList[buttonIndex];
                                UpdateButtonImages(btn, pokemon.Id, new PokemonImageApiRequest());
                            }

                            buttonIndex++;
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

        public void DisableBackButton(Button button, int currentPage)
        {
            if (_allPokemon != null && currentPage == _firstPage)
            {
                button.Sensitive = false;
            }
        }
    }
}