namespace ath.Config;

partial class Config
{
    public static void Evaluate(string command, Dictionary<string, string[]> Instance)
    {
        switch (command)
        {
            case "pcp"
            or "path":
                PrintConfigPath();
                break;

            case "pcf"
            or "file":
                PrintConfig();
                break;

            case "swd"
            or "here":
                SetWorkingDirectory();
                break;

            case "uwd"
            or "away":
                UnsetWorkingDirectory();
                break;

            case "ignore":
                SetIgnoredDirectories(Instance);
                break;

            case "unignore":
                UnsetIgnoredDirectories(Instance);
                break;

            default:
                Console.WriteLine($"Unknown config command: {command} - refer to help (ath help)");
                break;
        }
    }
}
