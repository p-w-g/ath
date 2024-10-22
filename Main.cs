if (args.Length == 0)
{
    Console.WriteLine("Usage: ath <command> [arguments]");
    return;
}
string command = args[0].ToLower();
string[] commandArgs = args.Length > 1 ? args[1..] : Array.Empty<string>();

switch (command)
{
    case "help" or "h" or "-h" or "--h" or "-help" or "--help":
        ath.commands.Help.ShowHelp();
        break;

    case "fep":
        await ath.commands.FEP.RunParallelAsync(commandArgs);
        break;

    case "pcp":
        ath.commands.Config.PrintConfigPath();
        break;

    case "pcf":
        ath.commands.Config.PrintConfig();
        break;

    case "swd":
        ath.commands.Config.SetWorkingDirectory();
        break;

    case "uwd":
        ath.commands.Config.UnsetWorkingDirectory();
        break;

    case "ignore":
        ath.commands.Config.SetIgnoredDirectories(commandArgs);
        break;

    case "unignore":
        ath.commands.Config.UnsetIgnoredDirectories(commandArgs);
        break;

    default:
        Console.WriteLine($"Unknown command: {command}");
        break;
}