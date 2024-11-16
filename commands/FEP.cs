namespace ath.commands
{
    public static class FEP
    {
        public static async Task RunParallelAsync(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine(
                    "Usage: ath fep <command> [--skip-folder-folder || --only-folder-folder]"
                );
                return;
            }
            string[] filteredArgs =
            [
                .. args.Where(arg => !arg.Contains("--skip-") && !arg.Contains("--only-")),
            ];
            if (filteredArgs.Length == 0)
            {
                Console.WriteLine(
                    "Usage: ath fep <command> [--skip-folder-folder || --only-folder-folder]"
                );
                return;
            }

            bool skip = args?.Any(arg => arg.StartsWith("--skip")) ?? false;
            string[] skippedFlags = skip ? cliUtils.FilterFlags("--skip", args) : [];
            if (skip && skippedFlags.Length < 1)
            {
                Console.WriteLine("Is 'skip' flag properly used?");
                return;
            }

            bool only = args?.Any(arg => arg.StartsWith("--only")) ?? false;
            string[] onlyFlags = only ? cliUtils.FilterFlags("--only", args) : [];
            if (only && onlyFlags.Length < 1)
            {
                Console.WriteLine("Is 'only' flag properly used?");
                return;
            }

            string innerCommand = filteredArgs[0];
            string innerCommandArgs =
                filteredArgs.Length > 1 ? string.Join(" ", filteredArgs[1..]) : string.Empty;

            bool sustain = args?.Any(arg => arg.StartsWith("--sustain")) ?? false;

            Task[] tasks =
            [
                .. cliUtils
                    .AvailableDirectories(args)
                    .Select(dir =>
                        Task.Run(
                            () => cliUtils.RunCommand(innerCommand, innerCommandArgs, dir, sustain)
                        )
                    ),
            ];
            await Task.WhenAll(tasks);
        }
    }
}
