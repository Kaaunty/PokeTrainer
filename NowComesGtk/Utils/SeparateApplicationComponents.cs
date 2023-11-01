using System.Diagnostics;
using Gtk;

namespace NowComesGtk.Utils
{
    public class SeparateApplicationComponents
    {
        public void GitHubOpen()
        {
            string repoGit = "https://github.com/Kaaunty/PokeTrainer";

            if (OperatingSystem.IsWindows())
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = repoGit,
                    UseShellExecute = true
                });
            }
            else if (OperatingSystem.IsLinux())
            {
                Process.Start("xdg-open", repoGit);
            }
            else if (OperatingSystem.IsMacOS())
            {
                Process.Start("open", repoGit);
            }
        }

        public void TalkToNPC()
        {
            ShowMessageDialogWithAnimation(
                  "Bem-vindo, Pokémon Trainer!\r\n\r\nVocê acaba de entrar no mundo emocionante dos Pokémon, e estamos felizes em tê-lo aqui.\r\n\r\n" +
                  "Aqui, você encontrará sua ferramenta essencial: a Pokédex.\r\n\r\n" +
                  "Ela é o seu guia para o vasto mundo dos Pokémon. Ao usá-la, você terá acesso a informações valiosas sobre cada Pokémon que encontrar.\r\n\r\n" +
                  "Simplesmente selecione um Pokémon da lista e desbloqueie um tesouro de conhecimento, incluindo seus tipos, habilidades, evoluções e muito mais.");
        }

        private async void ShowMessageDialogWithAnimation(string text)
        {
            var dialog = new MessageDialog(null, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, null);
            dialog.Text = "                                                                                                                             \n";
            dialog.WindowPosition = WindowPosition.Center;
            dialog.DeleteEvent += (sender, args) => { args.RetVal = true; };
            dialog.ShowAll();

            foreach (char letter in text)
            {
                dialog.Text += letter.ToString();
                dialog.WindowPosition = WindowPosition.Center;

                await Task.Delay(15);
            }

            dialog.Run();
            dialog.Destroy();
        }
    }
}
