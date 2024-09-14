namespace ath.commands
{
    public static class Help
    {
        public static void ShowHelp()
        {
            string HelpText = @"
    Available commands:
        help    - Show help information
        fep     - Run command for nested folders in CWD.
                takes a list of optional folders to either skip or run command in, separated by '-'
                ath fep <<command>> [--skip-foo-bar-baz || --only-gris-gras-gres]
        pcp     prints out config file's path
            ";
            Console.WriteLine(HelpText);
        }
    }
}