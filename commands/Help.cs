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
        swd     sets current working directory as a default working directory for future
                uses with fep, untill it gets unset or new directory is set
        uwd     removes default working directory and allows running fep in current working directory
            ";
            Console.WriteLine(HelpText);
        }
    }
}