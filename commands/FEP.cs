namespace ath.commands
{
  public static class FEP
  {
    public static void runParallel(string[] args)
    {
      if (args.Length == 0)
      {
        Console.WriteLine("Usage: ath fep <command>");
        return;
      }
      string innerCommand = args[0];
      string innerCommandArgs = args.Length > 1 ? string.Join(" ", args[1..]) : string.Empty;
      string[] directories = Directory.GetDirectories(Directory.GetCurrentDirectory());

      //TODO: make actual concurency 
      foreach (string dir in directories)
      {
        Console.WriteLine($"Running '{innerCommand} {innerCommandArgs}' in {dir}");

        cliTooling.RunCommand(innerCommand, innerCommandArgs, dir);
      }

    }
  }
}