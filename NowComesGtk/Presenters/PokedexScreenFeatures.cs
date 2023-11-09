using static PokeApi.BackEnd.Service.PokemonApiRequest;
using PokeApi.BackEnd.Entities;
using PokeApi.BackEnd.Service;
using PokeApiNet;
using Gtk;
using Gdk;

namespace NowComesGtk.Presenters
{
    public class PokedexScreenFeatures
    {
#nullable disable

        private List<Pokemon> _pokemonList = new();
        private List<Pokemon> _allPokemon = new();
        private List<Pokemon> _pokemonBySearch = new();

        private int _maxPokemonPerPage = 25, _currentPage;

        enum Choice
        {
            All,
            PureType,
            PrimaryType,
            SecondaryType
        }

        public void Populating(int currentPage, string choiceOfType, int subTypeChoice)
        {
            FilterigForDisplay(currentPage, choiceOfType, subTypeChoice, new PokemonApiRequest());
        }

        public void FilterigForDisplay(int currentPage, string choiceOfType, int subTypeChoice, IPokemonAPI pokemonAPI)
        {
            _currentPage = currentPage;

            if (choiceOfType == "all")
            {
                _allPokemon = pokemonAPI.GetPokemonListAll(currentPage);
                _pokemonBySearch = PokeList.pokemonList;
            }
            if (subTypeChoice == (int)Choice.All)
            {
                _allPokemon = pokemonAPI.GetPokemonListByTypeAll(currentPage, choiceOfType);
                _pokemonBySearch = PokeList.pokemonListAllType;
            }
            else if (subTypeChoice == (int)Choice.PureType)
            {
                _allPokemon = pokemonAPI.GetPokemonListByTypePure(currentPage, choiceOfType);
                _pokemonBySearch = PokeList.pokemonListPureType;
            }
            else if (subTypeChoice == (int)Choice.PrimaryType)
            {
                _allPokemon = pokemonAPI.GetPokemonListByTypeHalfType(currentPage, choiceOfType);
                _pokemonBySearch = PokeList.pokemonListHalfType;
            }
            else if (subTypeChoice == (int)Choice.SecondaryType)
            {
                _allPokemon = pokemonAPI.GetPokemonlistByHalfTypeSecondary(currentPage, choiceOfType);
                _pokemonBySearch = PokeList.pokemonListHalfSecundaryType;
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
            Image pokemonImage = new();
            Pixbuf pokemonSprite = await pokemonSpriteLoaderAPI.LoadPokemonSprite(pokemonId);

            if (pokemonSprite != null)
            {
                pokemonSprite = pokemonSprite.ScaleSimple(50, 50, InterpType.Bilinear);
                pokemonImage.Pixbuf = pokemonSprite;

                if (pokemonImage != null)
                {
                    button.Image = pokemonImage;
                }
            }
            else
            {
                pokemonSprite = new Pixbuf("Images/pokemonerror.png");
                pokemonSprite = pokemonSprite.ScaleSimple(50, 50, InterpType.Bilinear);
                pokemonImage.Pixbuf = pokemonSprite;
                button.Image = pokemonImage;
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
