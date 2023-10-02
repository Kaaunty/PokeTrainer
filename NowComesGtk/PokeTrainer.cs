using NowComesGtk.Screens;
using Gtk;

public class PokeTrainer
{
    public static void Main()
    {
        Application.Init();
        new PoketrainerMainScreen();
        Application.Run();
    }
}