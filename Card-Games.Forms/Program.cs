namespace Card_Games.Forms;

internal static class Program
{
    [STAThread]
    static void Main(string[] args)
    {
        // Ako pokreneš sa --sim argumentom
        if (args.Length > 0 && args[0] == "--sim")
        {
            MainStart.RunSim(args);  // Pokreni simulaciju
            return;
        }

        // Inače pokreni UI
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new Home());
    }
}
