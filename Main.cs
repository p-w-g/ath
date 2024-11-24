using ath.commands;

if (args.Length == 0)
{
    Console.WriteLine("Usage: ath <command> [arguments]");
    return;
}
string command = args[0].ToLower();
string[] commandArgs = args.Length > 1 ? args[1..] : Array.Empty<string>();

Dictionary<string, string[]> optionsObject = cliUtils.OptionsParser(commandArgs);

switch (command)
{
    case "help"
    or "h"
    or "-h"
    or "--h"
    or "-help"
    or "--help":
        Help.ShowHelp();
        break;

    case "fep":
        // TODO: implement as follows
        // await FEP.RunParallelAsync(optionsObject);
        await FEP.RunParallelAsync(commandArgs);
        break;

    // TODO: implement
    // case "config":
    //     Config.SetConfig(optionsObject);
    //     break;

    case "pcp":
        Config.PrintConfigPath();
        break;

    case "pcf":
        Config.PrintConfig();
        break;

    case "swd":
        Config.SetWorkingDirectory();
        break;

    case "uwd":
        Config.UnsetWorkingDirectory();
        break;

    case "ignore":
        Config.SetIgnoredDirectories(commandArgs);
        break;

    case "unignore":
        Config.UnsetIgnoredDirectories(commandArgs);
        break;

    default:
        Console.WriteLine($"Unknown command: {command}");
        break;
}
