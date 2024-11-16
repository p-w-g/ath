namespace ath.commands
{
    static partial class cliUtils
    {
        internal static string[] AvailableDirectories(string[] args)
        {
            Config config = Config.GetConfig();
            bool DefaultFolderExists = config.DefaultFolder != null;
            bool IgnoredFoldersExists = config.IgnoredFolders != null;
            string[] IgnoredFolders = IgnoredFoldersExists ? [.. config.IgnoredFolders!] : [];
            bool runLocal = args?.Any(arg => arg.StartsWith("--local")) ?? false;
            if (runLocal)
            {
                DefaultFolderExists = false;
            }

            string[] allFolders = DefaultFolderExists
                ? Directory.GetDirectories(config.DefaultFolder!)
                : Directory.GetDirectories(Directory.GetCurrentDirectory());

            if (IgnoredFoldersExists)
            {
                allFolders =
                [
                    .. allFolders.Where(dir => !IgnoredFolders.Any(folder => dir.Contains(folder))),
                ];
            }
            bool skip = args?.Any(arg => arg.StartsWith("--skip")) ?? false;
            string[] skippedFlags = skip ? FilterFlags("--skip", args) : [];
            string[] skippedFolders =
            [
                .. allFolders.Where(dir => !skippedFlags.Any(flag => dir.Contains(flag))),
            ];
            bool only = args?.Any(arg => arg.StartsWith("--only")) ?? false;
            string[] onlyFlags = only ? FilterFlags("--only", args) : [];
            string[] onlyFolders =
            [
                .. allFolders.Where(dir => onlyFlags.Any(flag => dir.Contains(flag))),
            ];

            string[] remainindFolders =
                skip ? skippedFolders
                : only ? onlyFolders
                : allFolders;

            return remainindFolders;
        }
    }
}
