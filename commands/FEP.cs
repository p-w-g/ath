namespace ath.commands
{
  public static class FEP
  {
    public static async Task runParallelAsync(string[] args)
    {
      if (args.Length == 0)
      {
        Console.WriteLine("Usage: ath fep <command>");
        return;
      }
      string innerCommand = args[0];
      string innerCommandArgs = args.Length > 1 ? string.Join(" ", args[1..]) : string.Empty;
      string[] directories = Directory.GetDirectories(Directory.GetCurrentDirectory());

      var tasks = directories.Select(dir => Task.Run(() => cliTooling.RunCommand(innerCommand, innerCommandArgs, dir))).ToArray();
      await Task.WhenAll(tasks);
    }
  }
}