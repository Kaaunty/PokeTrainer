using Gtk;
using NowComesGtk.Screens;
using NowComesGtk.Utils;
using Pango;
using PokeApi.BackEnd.Service;
using System.Drawing;
using System.Drawing.Text;
using static PokeApi.BackEnd.Service.ApiRequest;
using Image = Gtk.Image;

public class PokemonLoad : BaseWindow
{
#nullable disable

    private ApiRequest _apiRequest = new ApiRequest();
    private ProgressBar progressBar;
    private Image runningPikachu;
    private Label loadingLabel;

    private bool isLoaded = false;
    private int loadingDots = 0;

    public PokemonLoad() : base("", 400, 100)
    {
        var vbox = new VBox();

        loadingLabel = new Label("Carregando");
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
            Task.Delay(200).Wait();

            loadingLabel.Text = "Carregando" + new string('.', loadingDots);
            loadingDots = (loadingDots + 1) % 4;
        }
    }

    private async void LoadPokemonList()
    {
        try
        {
          //  await _apiRequest.GetPokemonsListAll();

           
            PopulateTypeDamageRelationDictionary();

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
            MessageDialogGenerator.ShowMessageDialog("Erro ao carregar a lista de Pokémon:" + ex);
            Destroy();
        }

        void PopulateTypeDamageRelationDictionary()
        {
            PokeList.TypeDamageRelations.Add("normal", "Dano Sofrido Pouco Efetivo: Nenhum\nPouco Efetivo Contra: Rocha, Aço\nDano Sofrido Super Efetivo: Lutador\nSuper Efetivo Contra: Nenhum\nImune: Fantasma\nNenhum Dano a: Fantasma");
            PokeList.TypeDamageRelations.Add("dark", "Dano Sofrido Pouco Efetivo: Fantasma, Sombrio\nPouco Efetivo Contra: Lutador, Sombrio, Fada\nDano Sofrido Super Efetivo: Lutador, Inseto, Fada\nSuper Efetivo Contra: Fantasma, Psíquico\nImune: Psíquico\nNenhum Dano a: Nenhum");
            PokeList.TypeDamageRelations.Add("bug", "Dano Sofrido Pouco Efetivo: Lutador, Terra, Grama\nPouco Efetivo Contra: Lutador, Voador, Venenoso, Fantasma, Aço, Fogo, Fada\nDano Sofrido Super Efetivo: Voador, Pedra, Fogo\nSuper Efetivo Contra: Grama, Psíquico, Sombrio\nImune: Nenhum\nNenhum Dano a: Nenhum");
            PokeList.TypeDamageRelations.Add("dragon", "Dano Sofrido Pouco Efetivo: Fogo, Água, Grama, Elétrica\nPouco Efetivo Contra: Aço\nDano Sofrido Super Efetivo: Gelo, Dragão, Fada\nSuper Efetivo Contra: Dragão\nImune: Nenhum\nNenhum Dano a: Fada");
            PokeList.TypeDamageRelations.Add("electric", "Dano Sofrido Pouco Efetivo: Voador, Aço, elétrico\nPouco Efetivo Contra: Grama, Elétrica, Dragão\nDano Sofrido Super Efetivo: Terra\nSuper Efetivo Contra: Voador, Água\nImune: Nenhum\nNenhum Dano a: Terra");
            PokeList.TypeDamageRelations.Add("fighting", "Dano Sofrido Pouco Efetivo: Rocha, Inseto, Sombrio\nPouco Efetivo Contra: Voador, Venenososo, Inseto, Psíquico, Fada\nDano Sofrido Super Efetivo: Voador, Psíquico, Fada\nSuper Efetivo Contra: Normal, Pedra, Aço, Gelo, Sombrio\nImune: Nenhum\nNenhum Dano a: Fantasma");
            PokeList.TypeDamageRelations.Add("fire", "Dano Sofrido Pouco Efetivo: Inseto, Aço, Fogo, Grama, Gelo, Fada\nPouco Efetivo Contra: Pedra, Fogo, Água, Dragão\nDano Sofrido Super Efetivo: Terra, Pedra, Água\nSuper Efetivo Contra: Inseto, aço, grama, gelo\nImune: Nenhum\nNenhum Dano a: Nenhum");
            PokeList.TypeDamageRelations.Add("flying", "Dano Sofrido Pouco Efetivo: Lutador, Inseto, Grama\nPouco Efetivo Contra: Rocha, aço, elétrica\nDano Sofrido Super Efetivo: Rock, elétrico, gelo\nSuper Efetivo Contra: Lutador, Inseto, Grama\nImune: Terra\nNenhum Dano a: Nenhum");
            PokeList.TypeDamageRelations.Add("ghost", "Dano Sofrido Pouco Efetivo: Venenososo, Inseto\nPouco Efetivo Contra: Sombrio\nDano Sofrido Super Efetivo: Fantasma, Sombrio\nSuper Efetivo Contra: Fantasma, Psíquico\nImune: Normal, Lutador\nNenhum Dano a: Normal");
            PokeList.TypeDamageRelations.Add("grass", "Dano Sofrido Pouco Efetivo: Terra, Água, Grama, Elétrica\nPouco Efetivo Contra: Voador, Venenososo, Inseto, Aço, Fogo, Grama, Dragão\nDano Sofrido Super Efetivo: Voador, Venenososo, Inseto, Fogo, Gelo\nSuper Efetivo Contra: Terra, Pedra, Água\nImune: Nenhum\nNenhum Dano a: Nenhum");
            PokeList.TypeDamageRelations.Add("ground", "Dano Sofrido Pouco Efetivo: Venenososo, Pedra\nPouco Efetivo Contra: Inseto, grama\nDano Sofrido Super Efetivo: Água, grama, gelo\nSuper Efetivo Contra: Venenoso, Pedra, Aço, Fogo, Elétrico\nImune: Elétrico\nNenhum Dano a: Voador");
            PokeList.TypeDamageRelations.Add("ice", "Dano Sofrido Pouco Efetivo: Gelo\nPouco Efetivo Contra: Aço, Fogo, Água, Gelo\nDano Sofrido Super Efetivo: Lutador, Pedra, Aço, Fogo\nSuper Efetivo Contra: Voador, Terra, Grama, Dragão\nImune: Nenhum\nNenhum Dano a: Nenhum");
            PokeList.TypeDamageRelations.Add("poison", "Dano Sofrido Pouco Efetivo: Lutador, Psíquico\nPouco Efetivo Contra: Aço, Psíquico\nDano Sofrido Super Efetivo: Inseto, Fantasma, Sombrio\nSuper Efetivo Contra: Lutador, Venenoso\nImune: Nenhum\nNenhum Dano a: Sombrio");
            PokeList.TypeDamageRelations.Add("psychic", "Dano Sofrido Pouco Efetivo: Lutador, Psíquico\nPouco Efetivo Contra: Aço, Psíquico\nDano Sofrido Super Efetivo: Inseto, Fantasma, Sombrio\nSuper Efetivo Contra: Lutador, Venenoso\nImune: Nenhum\nNenhum Dano a: Sombrio");
            PokeList.TypeDamageRelations.Add("rock", "Dano Sofrido Pouco Efetivo: Normal, Voador, Venenoso, Fogo\nPouco Efetivo Contra: Lutador,Terra,Aço\nDano Sofrido Super Efetivo: Lutador, Terra, Aço, Água, Grama\nSuper Efetivo Contra: Voador, Inseto, Fogo, Gelo\nImune: Nenhum\nNenhum Dano a: Nenhum");
            PokeList.TypeDamageRelations.Add("steel", "Dano Sofrido Pouco Efetivo: Normal, Voador, Pedra, Inseto, Aço, Grama, Psíquico, Gelo, Dragão, Fada\nPouco Efetivo Contra: Aço, Fogo, Água, Elétrica\nDano Sofrido Super Efetivo: Lutador,Terra,Fogo\nSuper Efetivo Contra: Pedra, Gelo, Fada\nImune: Tóxico\nNenhum Dano a: Nenhum");
            PokeList.TypeDamageRelations.Add("water", "Dano Sofrido Pouco Efetivo: Aço, Fogo, Água, Gelo\nPouco Efetivo Contra: Água, Grama, Dragão\nDano Sofrido Super Efetivo: Grama, elétrica\nSuper Efetivo Contra: Terra, Pedra, Fogo\nImune: Nenhum\nNenhum Dano a: Nenhum");
            PokeList.TypeDamageRelations.Add("fairy", "Dano Sofrido Pouco Efetivo: Lutador, Inseto, Sombrio\nPouco Efetivo Contra: Venenoso, Aço, Fogo\nDano Sofrido Super Efetivo: Venenoso, Aço\nSuper Efetivo Contra: Lutador, Dragão, Sombrio\nImune: Dragão\nNenhum Dano a: Nenhum");
        }
    }
}