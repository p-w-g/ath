namespace ath.commands
{
    public static class FEP
    {
        public static async Task RunParallelAsync(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: ath fep <command> [--skip-folder-folder || --only-folder-folder]");
                return;
            }
            string[] filteredArgs = [.. args.Where(arg => !arg.Contains("--skip-") && !arg.Contains("--only-"))];
            if (filteredArgs.Length == 0)
            {
                Console.WriteLine("Usage: ath fep <command> [--skip-folder-folder || --only-folder-folder]");
                return;
            }

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
                allFolders = [.. allFolders.Where(dir => !IgnoredFolders.Any(folder => dir.Contains(folder)))];
            }

            bool skip = args?.Any(arg => arg.StartsWith("--skip")) ?? false;
            string[] skippedFlags = skip ? cliTooling.FilterFlags("--skip", args) : [];
            if (skip && skippedFlags.Length < 1)
            {
                Console.WriteLine("Is 'skip' flag properly used?");
                return;
            }
            string[] skippedFolders = [.. allFolders.Where(dir => !skippedFlags.Any(flag => dir.Contains(flag)))];

            bool only = args?.Any(arg => arg.StartsWith("--only")) ?? false;
            string[] onlyFlags = only ? cliTooling.FilterFlags("--only", args) : [];
            if (only && onlyFlags.Length < 1)
            {
                Console.WriteLine("Is 'only' flag properly used?");
                return;
            }
            string[] onlyFolders = [.. allFolders.Where(dir => onlyFlags.Any(flag => dir.Contains(flag)))];

            string innerCommand = filteredArgs[0];
            string innerCommandArgs = filteredArgs.Length > 1 ? string.Join(" ", filteredArgs[1..]) : string.Empty;

            string[] remainindFolders = skip
                ? skippedFolders
                : only
                    ? onlyFolders
                    : allFolders;

            bool sustain = args?.Any(arg => arg.StartsWith("--sustain")) ?? false;

            Task[] tasks = [.. remainindFolders.Select(dir => Task.Run(() => cliTooling.RunCommand(innerCommand, innerCommandArgs, dir, sustain)))];
            await Task.WhenAll(tasks);
        }
    }
}