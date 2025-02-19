using ath.CliUtils;
using ath.Commands;
using ath.Config;

if (args.Length == 0)
{
    Console.WriteLine("Usage: ath <command> [arguments]");
    return;
}
string command = args[0].ToLower();
string[] commandArgs = args.Length > 1 ? args[1..] : Array.Empty<string>();
args = args[1..];
Dictionary<string, string[]> Instance = CliUtils.InstanceParser(args);

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
        await FEP.RunParallelAsync(Instance);
        break;

    // TODO: implement
    // case "config":
    //     Config(Instance);
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
        Console.WriteLine($"Unknown command: {command}, refer to help (ath help)");
        break;
}
