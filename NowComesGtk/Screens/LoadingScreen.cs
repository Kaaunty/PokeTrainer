using NowComesGtk.Utils.WidgetGenerators;
using PokeTrainerBackEndTest.Controller;
using PokeTrainerBackEnd.Helper;
using PokeApi.BackEnd.Entities;
using NowComesGtk.Screens;
using PokeTrainerBackEnd;
using Image = Gtk.Image;
using NowComesGtk.Utils;
using NAudio.Wave;
using Gtk;

public class PokemonLoad : BaseWindow
{
#nullable disable

    private IPokemonAPI _pokemonAPI = new PokeApiNetController();
    private DirectoryHelper _directoryHelper = new();
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
        Thread sound = new(new ThreadStart(PlaySound));
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
            RepositoryPopulation.PopulateTypeDamageRelationDictionary();
            RepositoryPopulation.PokemonSpritesCorrection();
            await _directoryHelper.ValidateXmlArchive();
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
            MediaFoundationReader readers = new("Sounds/pokemon-opening2.mp3");
            WaveOutEvent _waveOut = new();

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
}