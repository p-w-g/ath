namespace ath.Config;

partial class Config
{
    public static void Evaluate(Dictionary<string, string[]> Instance)
    {
        if (!Instance.TryGetValue("PayLoad", out string[]? configInstance) || configInstance.Length == 0)
        {
            Console.WriteLine("Usage: ath cfg <command> [arguments] - refer to help (ath help)");
            return;
        }

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

            case "to":
                if (Payload.Length == 0)
                {
                    Console.WriteLine("Usage: ath cfg to <duration in seconds>");
                    break;
                }
                SetTimeout(Payload[0]);
                break;

            case "nto":
                UnsetTimeout();
                break;

            default:
                Console.WriteLine($"Unknown config command: {command} - refer to help (ath help)");
                break;
        }
    }
}
