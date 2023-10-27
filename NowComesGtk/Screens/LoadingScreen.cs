
﻿using static PokeApi.BackEnd.Service.ApiRequest;


using PokeApi.BackEnd.Service;
using NowComesGtk.Screens;
using Image = Gtk.Image;
using NowComesGtk.Utils;
using Gtk;
using NAudio.Wave;

public class PokemonLoad : BaseWindow
{
#nullable disable

    private ProgressBar _progressBar = new();
    private ApiRequest _apiRequest = new();
    private Image _redAndPikachuRunning = new();
    private Label _loadingLabel = new();

    private bool _isLoaded = false;
    private int _loadingDots = 0;

    public PokemonLoad() : base("", 400, 100)
    {
        VBox vBox = new();

        _loadingLabel = new Label("Carregando");
        vBox.PackStart(_loadingLabel, false, false, 10);

        _redAndPikachuRunning = new Image("Images/red-and-pikachu-running.gif");
        vBox.PackStart(_redAndPikachuRunning, false, false, 10);

        _progressBar = new ProgressBar();
        vBox.PackStart(_progressBar, false, false, 10);

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
            _progressBar.Fraction = GetProgress();

            Task.Delay(200).Wait();

            _loadingLabel.Text = "Carregando" + new string('.', _loadingDots);
            _loadingDots = (_loadingDots + 1) % 4;
        }
    }

    private async void LoadPokemonList()
    {
        try
        {
            await _apiRequest.GetPokemonsListAll();
            PopulateTypeDamageRelationDictionary();

            _progressBar.Fraction = 1;
            _isLoaded = true;
            _loadingLabel.Text = "Carregamento concluído!";

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

    private void PlaySound()
    {
        try
        {

            MediaFoundationReader readers = new MediaFoundationReader("Sounds/pokemon-opening2.mp3");

            var waveOut = new WaveOutEvent();
            waveOut.Init(readers);
            waveOut.Volume = 0.1f;
            waveOut.Play();
        }
        catch (Exception ex)
        {
            MessageDialogGenerator.ShowMessageDialog("Erro ao reproduzir o som:" + ex);
        }
    }

    public double GetProgress()
    {
        double progress = 0.0;
        int totalpokemoncount = PokeList.pokemonList.Count;

        if (totalpokemoncount == 0)
        {
            return progress;
        }
        if (totalpokemoncount > 0)
        {
            if (totalpokemoncount == 200)
            {
                progress = 0.2;
                return progress;
            }
            else if (totalpokemoncount == 400)
            {
                progress = 0.4;
                return progress;
            }
            else if (totalpokemoncount == 600)
            {
                progress = 0.6;
                return progress;
            }
            else if (totalpokemoncount == 800)
            {
                progress = 0.8;
                return progress;
            }
            else if (totalpokemoncount == 1000)
            {
                progress = 0.85;
                return progress;
            }
            else if (totalpokemoncount == 1200)
            {
                progress = 0.9;
                return progress;
            }
            else if (totalpokemoncount == 1292)
            {
                progress = 1.0;
                return progress;
            }
        }
        else
        {
            progress = 1.0;
            return progress;
        }
        return progress;
    }

    private void PopulateTypeDamageRelationDictionary()
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