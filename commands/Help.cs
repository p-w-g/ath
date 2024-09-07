namespace ath.commands
{
    public static class Help
    {
        public static void ShowHelp()
        {
            Console.WriteLine("Available commands:");
            Console.WriteLine("  help    - Show help information");
            Console.WriteLine("  fep     - Run command for nested folders in CWD.");
            Console.WriteLine("            takes a list of optional folders to either skip or run command in, separated by -");
            Console.WriteLine("            ath fep <<command>> [--skip-foo-bar-baz || --only-gris-gras-gres]");
        }
    }
}