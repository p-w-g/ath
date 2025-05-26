namespace ath.Commands;

public static class Help
{
    public static void ShowHelp()
    {
        string HelpText =
            @"
Available commands:
 
    * help          Show help information
    
    * fep           Run command for nested folders in CWD.
                    takes a list of optional folders to either skip or run command in, separated by '-'
                    `ath fep <<command>> [--skip-foo-bar-baz || --only-gris-gras-gres]`
                    
                    by default runs in current working folder or set working folder, 
                    which can be temporarily overrun with `--local` flag    

    * cfg (config)
    cfg path        prints out config file's path
    cfg file        prints out config file's content
    
    cfg here        sets current working directory as a default working directory for future
                    uses with fep, untill it gets unset or new directory is set
    cfg away        unsets default working directory and allows running fep in current working directory

    cfg ignore      adds folders to the permanently ignored list 
                    `ath cfg ignore .git .idea .vscode`
    cfg heed        removes folders from the permanently ignored list
                    `ath cfg heed .git .idea .vscode`
                    or
                    `ath cfg heed --all`
";
        Console.WriteLine(HelpText);
    }
}
