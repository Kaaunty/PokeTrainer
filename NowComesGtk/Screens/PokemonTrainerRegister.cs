using Gtk;
using NowComesGtk.Utils;
using NowComesGtk.Utils.WidgetGenerators;
using PokeTrainerBackEnd.Controller;
using PokeTrainerBackEnd.Entities;
using PokeTrainerBackEnd.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NowComesGtk.Screens
{
#nullable disable

    public class PokemonTrainerRegister : BaseWindow
    {
        private PokemonTrainerModel _pokemonTrainerModel = new();
        private Entry _entryName = new();
        private PokemonTrainer _pokemonTrainer = new();

        public PokemonTrainerRegister() : base("Cadastro Pokemon Trainer", 600, 300)
        {
            Image background = new("Images/PokeTrainerRegister.png");
            Fixed fix = new();
            fix.Put(background, 0, 0);
            _entryName.SetSizeRequest(280, 10);
            fix.Put(_entryName, 185, 95);

            Button pokemonOne = new ButtonGenerator("btn1", 50, 50);
            Button pokemonTwo = new ButtonGenerator("btn2", 50, 50);
            Button pokemonThree = new ButtonGenerator("btn3", 50, 50);
            Button pokemonFour = new ButtonGenerator("btn4", 50, 50);
            Button pokemonFive = new ButtonGenerator("btn5", 50, 50);
            Button pokemonSix = new ButtonGenerator("btn6", 50, 50);
            fix.Put(pokemonOne, 185, 140);
            fix.Put(pokemonTwo, 245, 140);

            fix.Put(pokemonThree, 305, 140);

            Button buttonRegister = new("Cadastrar");
            buttonRegister.SetSizeRequest(100, 30);
            buttonRegister.Clicked += RegisterPokeTrainer;
            fix.Put(buttonRegister, 250, 240);
            Add(fix);
            ShowAll();
        }

        private void RegisterPokeTrainer(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(_entryName.Text))
            {
                _pokemonTrainer.Name = _entryName.Text;
                _pokemonTrainerModel.Register(_pokemonTrainer);
            }
            else
            {
                MessageDialog messageDialog = new(this, DialogFlags.Modal, MessageType.Error, ButtonsType.Ok, "Preencha o campo nome");
                messageDialog.Run();
                messageDialog.Destroy();
            }
        }
    }
}