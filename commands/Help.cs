namespace ath.commands
{
  public static class Help
  {
    public static void ShowHelp()
    {
      Console.WriteLine("Available commands:");
      Console.WriteLine("  help    - Show help information");
      Console.WriteLine("  fep     - Run command for nested folders in CWD.");
    }

  }
}