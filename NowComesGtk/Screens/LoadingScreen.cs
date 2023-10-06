using System;
using Gtk;
using System.Threading.Tasks;
using System.Linq;
using PokeApiNet;
using PokeApi.BackEnd.Service;

public class PokemonLoader
{
    private ApiRequest _apiRequest = new ApiRequest();
    private Window mainWindow;
    private Label loadingLabel;
    private ProgressBar progressBar;
    private bool isLoaded = false;

    public PokemonLoader()
    {
        Application.Init();

        // Crie a janela principal
        mainWindow = new Window("Carregamento de Pokémon");
        mainWindow.Resize(400, 100);
        mainWindow.DeleteEvent += (o, args) => { Application.Quit(); };
        mainWindow.Resizable = false;

        // Crie um VBox para organizar os widgets
        var vbox = new VBox();

        // Crie um rótulo de carregamento
        loadingLabel = new Label("Carregando...");
        vbox.PackStart(loadingLabel, false, false, 10);

        // Crie uma barra de progresso
        progressBar = new ProgressBar();
        vbox.PackStart(progressBar, false, false, 10);

        // Adicione o VBox à janela principal
        mainWindow.Add(vbox);

        // Inicialize e exiba a janela principal
        mainWindow.ShowAll();

        // Carregue a lista de Pokémon em segundo plano
        Task.Run(() => LoadPokemonList());

        // Atualize a barra de progresso em segundo plano
        Task.Run(() => UpdateProgressBar());
    }

    private void UpdateProgressBar()
    {
        while (!isLoaded)
        {
            // Atualize a barra de progresso
            progressBar.Fraction = _apiRequest.GetProgress();

            Task.Delay(100).Wait();

        }


        mainWindow.Destroy();
    }

    private async void LoadPokemonList()
    {
        try
        {
            await _apiRequest.GetPokemonsListAll();

            isLoaded = true;

            loadingLabel.Text = "Carregamento concluído!";
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao carregar a lista de Pokémon: {ex.Message}");
            loadingLabel.Text = "Erro ao carregar a lista de Pokémon.";
        }
    }

    public void Run()
    {
        Application.Run();
    }

    public static void Main(string[] args)
    {
        PokemonLoader loader = new PokemonLoader();
        loader.Run();
    }
}