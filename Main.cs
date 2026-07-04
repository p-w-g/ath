using ath.CliUtils;
using ath.Commands;
using ath.Config;

if (args.Length == 0)
{
    Console.WriteLine("Usage: ath <command> [arguments]");
    return;
}
string command = args[0].ToLower();
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
        bool success = await FEP.RunParallelAsync(Instance);
        if (!success)
        {
            Environment.Exit(1);
        }
        break;

    case "cfg":
        Config.Evaluate(Instance);
        break;

    default:
        Console.WriteLine($"Unknown command: {command} - refer to help (ath help)");
        break;
}
