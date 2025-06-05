namespace ath.Config;

partial class Config
{
    public static void SetWorkingDirectory()
    {
        string NewWorkingDirectory = Directory.GetCurrentDirectory();
        Config config = GetConfig();
        config.DefaultFolder = NewWorkingDirectory;
        SaveConfig(config);
    }

    public static void UnsetWorkingDirectory()
    {
        Config config = GetConfig();
        config.DefaultFolder = null;
        SaveConfig(config);
    }
}
