namespace ath.Config;

partial class Config
{
    static readonly string defaultConfigPath = Path.Combine(GetHomeDir(), ".athconfig");

    public static void PrintConfigPath()
    {
        if (!File.Exists(defaultConfigPath))
        {
            Console.WriteLine("config file doesn't exist");
            return;
        }
        Console.WriteLine(defaultConfigPath);
    }

    public static void PrintConfig()
    {
        if (!File.Exists(defaultConfigPath))
        {
            Console.WriteLine("config file doesn't exist");
            return;
        }
        string config = File.ReadAllText(defaultConfigPath);
        Console.WriteLine(config);
    }
}
