namespace ath.commands
{
    public static class Help
    {
        public static void ShowHelp()
        {
            string HelpText = @"
    Available commands:
 
        help        - Show help information
        
        fep         - Run command for nested folders in CWD.
                    takes a list of optional folders to either skip or run command in, separated by '-'
                    `ath fep <<command>> [--skip-foo-bar-baz || --only-gris-gras-gres]`
        
        pcp         prints out config file's path
        pcf         prints out config file's content
        
        swd         sets current working directory as a default working directory for future
                    uses with fep, untill it gets unset or new directory is set
        uwd         unsets default working directory and allows running fep in current working directory

        ignore      adds folders to the permanently ignored list 
                    `ath ignore .git .idea .vscode`
        unignore    removes folders from the permanently ignored list
                    `ath ignore .git .idea .vscode`
";
            Console.WriteLine(HelpText);
        }
    }
}