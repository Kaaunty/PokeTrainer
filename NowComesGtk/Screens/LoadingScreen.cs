using Gtk;
using NAudio.Wave;
using NowComesGtk.Screens;
using NowComesGtk.Utils;
using PokeApi.BackEnd.Entities;
using PokeTrainerBackEnd;
using PokeTrainerBackEnd.Helper;
using PokeTrainerBackEndTest.Controller;
using static PokeApi.BackEnd.Service.PokemonApiRequest;
using Image = Gtk.Image;

public class PokemonLoad : BaseWindow
{
#nullable disable

    private IPokemonAPI _pokemonAPI = new PokeApiNetController();
    private DirectoryHelper directoryHelper = new DirectoryHelper();
    private Image _redAndPikachuRunning = new();
    private ProgressBar _progressBar = new();
    private Label _loadingLabel = new();

    private double _totalPokemonCount = 0;
    private bool _isLoaded = false;
    private int _loadingDots = 0;

    public PokemonLoad() : base("", 400, 100)
    {
        VBox vBox = new();
        _redAndPikachuRunning = new Image("Images/red-and-pikachu-running.gif");
        vBox.PackStart(_redAndPikachuRunning, false, false, 10);

        _progressBar = new ProgressBar();
        vBox.PackEnd(_progressBar, false, false, 10);

        _loadingLabel = new Label("Carregando");
        vBox.PackEnd(_loadingLabel, false, false, 10);
        Add(vBox);
        ShowAll();
        LoadPokemonList();
        Thread sound = new Thread(new ThreadStart(PlaySound));
        sound.Start();
        Task.Run(() => UpdateProgressBar());
    }

    private void UpdateProgressBar()
    {
        while (!_isLoaded)
        {
            _progressBar.Fraction = GetProgress(_totalPokemonCount);

            Task.Delay(200).Wait();

            _loadingLabel.Text = "Carregando" + new string('.', _loadingDots);
            _loadingDots = (_loadingDots + 1) % 4;
        }
    }

    private async void LoadPokemonList()
    {
        try
        {
            _totalPokemonCount = await _pokemonAPI.GetPokemonTotalCount();
            await directoryHelper.ValidateXmlArchive();
            PopulateTypeDamageRelationDictionary();
            _progressBar.Fraction = 1;
            _isLoaded = true;

            if (_isLoaded)
            {
                PoketrainerMainScreen poketrainerMainScreen = new();
                poketrainerMainScreen.Show();

                Destroy();
            }
        }
        catch (Exception ex)
        {
            MessageDialogGenerator.ShowMessageDialog("Erro ao carregar a lista de Pokémon:" + ex);
            Application.Quit();
        }
    }

    private static void PlaySound()
    {
        try
        {
            MediaFoundationReader readers = new MediaFoundationReader("Sounds/pokemon-opening2.mp3");
            WaveOutEvent _waveOut = new WaveOutEvent();

            _waveOut.Init(readers);
            _waveOut.Volume = 0.1f;
           // _waveOut.Play();
        }
        catch (Exception ex)
        {
            MessageDialogGenerator.ShowMessageDialog("Erro ao reproduzir o som:" + ex);
        }
    }

    public double GetProgress(double pokemonTotalCount)
    {
        double totalPokemonReceived = Repository.Pokemon.Count;
        if (pokemonTotalCount == 0 || totalPokemonReceived == 0)
            return 0;

        return totalPokemonReceived / pokemonTotalCount;
    }

    private void PopulateTypeDamageRelationDictionary()
    {
        PokeList.typeDamageRelations.Add("normal", "Dano Sofrido Pouco Efetivo: Nenhum\nPouco Efetivo Contra: Rocha, Aço\nDano Sofrido Super Efetivo: Lutador\nSuper Efetivo Contra: Nenhum\nImune: Fantasma\nNenhum Dano a: Fantasma");
        PokeList.typeDamageRelations.Add("dark", "Dano Sofrido Pouco Efetivo: Fantasma, Sombrio\nPouco Efetivo Contra: Lutador, Sombrio, Fada\nDano Sofrido Super Efetivo: Lutador, Inseto, Fada\nSuper Efetivo Contra: Fantasma, Psíquico\nImune: Psíquico\nNenhum Dano a: Nenhum");
        PokeList.typeDamageRelations.Add("bug", "Dano Sofrido Pouco Efetivo: Lutador, Terra, Grama\nPouco Efetivo Contra: Lutador, Voador, Venenoso, Fantasma, Aço, Fogo, Fada\nDano Sofrido Super Efetivo: Voador, Pedra, Fogo\nSuper Efetivo Contra: Grama, Psíquico, Sombrio\nImune: Nenhum\nNenhum Dano a: Nenhum");
        PokeList.typeDamageRelations.Add("dragon", "Dano Sofrido Pouco Efetivo: Fogo, Água, Grama, Elétrica\nPouco Efetivo Contra: Aço\nDano Sofrido Super Efetivo: Gelo, Dragão, Fada\nSuper Efetivo Contra: Dragão\nImune: Nenhum\nNenhum Dano a: Fada");
        PokeList.typeDamageRelations.Add("electric", "Dano Sofrido Pouco Efetivo: Voador, Aço, elétrico\nPouco Efetivo Contra: Grama, Elétrica, Dragão\nDano Sofrido Super Efetivo: Terra\nSuper Efetivo Contra: Voador, Água\nImune: Nenhum\nNenhum Dano a: Terra");
        PokeList.typeDamageRelations.Add("fighting", "Dano Sofrido Pouco Efetivo: Rocha, Inseto, Sombrio\nPouco Efetivo Contra: Voador, Venenososo, Inseto, Psíquico, Fada\nDano Sofrido Super Efetivo: Voador, Psíquico, Fada\nSuper Efetivo Contra: Normal, Pedra, Aço, Gelo, Sombrio\nImune: Nenhum\nNenhum Dano a: Fantasma");
        PokeList.typeDamageRelations.Add("fire", "Dano Sofrido Pouco Efetivo: Inseto, Aço, Fogo, Grama, Gelo, Fada\nPouco Efetivo Contra: Pedra, Fogo, Água, Dragão\nDano Sofrido Super Efetivo: Terra, Pedra, Água\nSuper Efetivo Contra: Inseto, aço, grama, gelo\nImune: Nenhum\nNenhum Dano a: Nenhum");
        PokeList.typeDamageRelations.Add("flying", "Dano Sofrido Pouco Efetivo: Lutador, Inseto, Grama\nPouco Efetivo Contra: Rocha, aço, elétrica\nDano Sofrido Super Efetivo: Rock, elétrico, gelo\nSuper Efetivo Contra: Lutador, Inseto, Grama\nImune: Terra\nNenhum Dano a: Nenhum");
        PokeList.typeDamageRelations.Add("ghost", "Dano Sofrido Pouco Efetivo: Venenososo, Inseto\nPouco Efetivo Contra: Sombrio\nDano Sofrido Super Efetivo: Fantasma, Sombrio\nSuper Efetivo Contra: Fantasma, Psíquico\nImune: Normal, Lutador\nNenhum Dano a: Normal");
        PokeList.typeDamageRelations.Add("grass", "Dano Sofrido Pouco Efetivo: Terra, Água, Grama, Elétrica\nPouco Efetivo Contra: Voador, Venenososo, Inseto, Aço, Fogo, Grama, Dragão\nDano Sofrido Super Efetivo: Voador, Venenososo, Inseto, Fogo, Gelo\nSuper Efetivo Contra: Terra, Pedra, Água\nImune: Nenhum\nNenhum Dano a: Nenhum");
        PokeList.typeDamageRelations.Add("ground", "Dano Sofrido Pouco Efetivo: Venenososo, Pedra\nPouco Efetivo Contra: Inseto, grama\nDano Sofrido Super Efetivo: Água, grama, gelo\nSuper Efetivo Contra: Venenoso, Pedra, Aço, Fogo, Elétrico\nImune: Elétrico\nNenhum Dano a: Voador");
        PokeList.typeDamageRelations.Add("ice", "Dano Sofrido Pouco Efetivo: Gelo\nPouco Efetivo Contra: Aço, Fogo, Água, Gelo\nDano Sofrido Super Efetivo: Lutador, Pedra, Aço, Fogo\nSuper Efetivo Contra: Voador, Terra, Grama, Dragão\nImune: Nenhum\nNenhum Dano a: Nenhum");
        PokeList.typeDamageRelations.Add("poison", "Dano Sofrido Pouco Efetivo: Lutador, Psíquico\nPouco Efetivo Contra: Aço, Psíquico\nDano Sofrido Super Efetivo: Inseto, Fantasma, Sombrio\nSuper Efetivo Contra: Lutador, Venenoso\nImune: Nenhum\nNenhum Dano a: Sombrio");
        PokeList.typeDamageRelations.Add("psychic", "Dano Sofrido Pouco Efetivo: Lutador, Psíquico\nPouco Efetivo Contra: Aço, Psíquico\nDano Sofrido Super Efetivo: Inseto, Fantasma, Sombrio\nSuper Efetivo Contra: Lutador, Venenoso\nImune: Nenhum\nNenhum Dano a: Sombrio");
        PokeList.typeDamageRelations.Add("rock", "Dano Sofrido Pouco Efetivo: Normal, Voador, Venenoso, Fogo\nPouco Efetivo Contra: Lutador,Terra,Aço\nDano Sofrido Super Efetivo: Lutador, Terra, Aço, Água, Grama\nSuper Efetivo Contra: Voador, Inseto, Fogo, Gelo\nImune: Nenhum\nNenhum Dano a: Nenhum");
        PokeList.typeDamageRelations.Add("steel", "Dano Sofrido Pouco Efetivo: Normal, Voador, Pedra, Inseto, Aço, Grama, Psíquico, Gelo, Dragão, Fada\nPouco Efetivo Contra: Aço, Fogo, Água, Elétrica\nDano Sofrido Super Efetivo: Lutador,Terra,Fogo\nSuper Efetivo Contra: Pedra, Gelo, Fada\nImune: Tóxico\nNenhum Dano a: Nenhum");
        PokeList.typeDamageRelations.Add("water", "Dano Sofrido Pouco Efetivo: Aço, Fogo, Água, Gelo\nPouco Efetivo Contra: Água, Grama, Dragão\nDano Sofrido Super Efetivo: Grama, elétrica\nSuper Efetivo Contra: Terra, Pedra, Fogo\nImune: Nenhum\nNenhum Dano a: Nenhum");
        PokeList.typeDamageRelations.Add("fairy", "Dano Sofrido Pouco Efetivo: Lutador, Inseto, Sombrio\nPouco Efetivo Contra: Venenoso, Aço, Fogo\nDano Sofrido Super Efetivo: Venenoso, Aço\nSuper Efetivo Contra: Lutador, Dragão, Sombrio\nImune: Dragão\nNenhum Dano a: Nenhum");
    }
}