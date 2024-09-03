﻿using System.Linq;

string command = args[0].ToLower();
string[] commandArgs = args.Length > 1 ? args[1..] : Array.Empty<string>();


if (args.Length == 0)
{
  Console.WriteLine("Usage: ath <command> [arguments]");
  return;
}

switch (command)
{
  case "help" or "h" or "-h" or "--h" or "-help" or "--help":
    ath.commands.Help.ShowHelp();
    break;

  case "fep":
    await ath.commands.FEP.runParallelAsync(commandArgs);
    break;

  default:
    Console.WriteLine($"Unknown command: {command}");
    break;
}