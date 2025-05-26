namespace ath.Config;

partial class Config
{
    public static void Evaluate(Dictionary<string, string[]> Instance)
    {
        string[] configInstance = Instance["PayLoad"];
        string command = configInstance[0].ToLower();
        string[] Payload = configInstance[1..];

        switch (command)
        {
            case "path":
                PrintConfigPath();
                break;

            case "file":
                PrintConfig();
                break;

            case "here":
                SetWorkingDirectory();
                break;

            case "away":
                UnsetWorkingDirectory();
                break;

            case "ignore":
                SetIgnoredDirectories(Payload);
                break;

            case "heed":
                bool clearAll = Instance.ContainsKey("all");
                UnsetIgnoredDirectories(Payload, clearAll);
                break;

            default:
                Console.WriteLine($"Unknown config command: {command} - refer to help (ath help)");
                break;
        }
    }
}
