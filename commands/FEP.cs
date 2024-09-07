namespace ath.commands
{
    public static class FEP
    {
        public static async Task runParallelAsync(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: ath fep <command> [--skip-folder-folder || --only-folder-folder]");
                return;
            }
            var filteredArgs = args.Where(arg => !arg.Contains("--skip-") && !arg.Contains("--only-")).ToArray();
            if (filteredArgs.Length == 0)
            {
                Console.WriteLine("Usage: ath fep <command> [--skip-folder-folder || --only-folder-folder]");
                return;
            }

            bool skip = args.Any(arg => arg.StartsWith("--skip"));
            bool only = args.Any(arg => arg.StartsWith("--only"));

            string[] skippedFlag = skip && args != null ? Array.Find(args, arg => arg.Contains("--skip-"))!.Split("--skip-")[1].Split("-") : [];
            string[] onlyFlag = only && args != null ? Array.Find(args, arg => arg.Contains("--only-"))!.Split("--only-")[1].Split("-") : [];

            string innerCommand = filteredArgs[0];
            string innerCommandArgs = filteredArgs.Length > 1 ? string.Join(" ", filteredArgs[1..]) : string.Empty;

            string[] directories = Directory.GetDirectories(Directory.GetCurrentDirectory());

            string[] filteredDirectories = skip
            ? directories.Where(dir => skippedFlag.Any(flag => !dir.Contains(flag))).ToArray()
            : only
            ? directories.Where(dir => onlyFlag.Any(flag => dir.Contains(flag))).ToArray()
            : directories;

            var tasks = filteredDirectories.Select(dir => Task.Run(() => cliTooling.RunCommand(innerCommand, innerCommandArgs, dir))).ToArray();
            await Task.WhenAll(tasks);
        }
    }
}