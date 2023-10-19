using Atk;
using Gtk;
using NowComesGtk.Screens;
using NowComesGtk.Utils;
using PokeApi.BackEnd.Service;

public class PokemonLoad : BaseWindow
{
#nullable disable
    private ApiRequest _apiRequest = new ApiRequest();
    private ProgressBar progressBar;
    private bool isLoaded = false;
    private Label loadingLabel;
    private Image runningPikachu;

    public PokemonLoad() : base("", 400, 100)
    {
        var vbox = new VBox();

        loadingLabel = new Label("Carregando...");
        vbox.PackStart(loadingLabel, false, false, 10);

        runningPikachu = new Image("Images/pikachu-running.gif");
        vbox.PackStart(runningPikachu, false, false, 10);

        progressBar = new ProgressBar();
        vbox.PackStart(progressBar, false, false, 10);

        Add(vbox);
        ShowAll();
        LoadPokemonList();

        Task.Run(() => UpdateProgressBar());
    }

    private void UpdateProgressBar()
    {
        while (!isLoaded)
        {
            progressBar.Fraction = _apiRequest.GetProgress();
            Task.Delay(100).Wait();
        }
    }

    private async void LoadPokemonList()
    {
        try
        {
            //await _apiRequest.GetPokemonsListAll();
            progressBar.Fraction = 1;
            isLoaded = true;
            loadingLabel.Text = "Carregamento concluído!";

            if (isLoaded)
            {
                PoketrainerMainScreen poketrainerMainScreen = new();
                poketrainerMainScreen.Show();
                Destroy();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao carregar a lista de Pokémon: {ex.Message}");
            loadingLabel.Text = "Erro ao carregar a lista de Pokémon.";
        }
    }
}