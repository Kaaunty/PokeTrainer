using System.Diagnostics;

namespace NowComesGtk.Utils
{
    public class SeparateMethods
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
        public void DialogWithXamuca()
        {


            //
            //        Fazer a introdução ao aplicativo.
            //


        }
    }
}
