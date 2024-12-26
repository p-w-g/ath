namespace ath.commands
{
    partial class cliUtils
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

            string[] remainingFolders =
                skip ? skippedFolders
                : only ? onlyFolders
                : allFolders;

            return remainingFolders;
        }

        internal static string[] GetAvailableDirectories(
            Dictionary<string, string[]> InstanceObject
        )
        {
            Config config = Config.GetConfig();

            string workingDirectory = AssumeWorkingDirectory(InstanceObject);
            string[] AllDirectories = Directory.GetDirectories(workingDirectory);

            bool IgnoredFoldersExist = config.IgnoredFolders is not null;
            bool SkippedFoldersExist = InstanceObject.ContainsKey("skip");
            bool OnlyFoldersExist = InstanceObject.ContainsKey("only");

            if (IgnoredFoldersExist)
            {
                string[] ignoredPaths = [.. config.IgnoredFolders];
                AllDirectories = RemoveTargetDirectories(AllDirectories, ignoredPaths);
            }

            if (SkippedFoldersExist && !OnlyFoldersExist)
            {
                string[] skippedPaths = InstanceObject["skip"];
                AllDirectories = RemoveTargetDirectories(AllDirectories, skippedPaths);
            }

            if (OnlyFoldersExist)
            {
                string[] onlyPaths = InstanceObject["only"];
                AllDirectories = SelectTargetDirectories(AllDirectories, onlyPaths);
            }

            return AllDirectories;
        }

        internal static string[] RemoveTargetDirectories(string[] AllDirectories, string[] Paths)
        {
            return [.. AllDirectories.Where(dir => !Paths.Any(Path => dir.Contains(Path)))];
        }

        internal static string[] SelectTargetDirectories(string[] AllDirectories, string[] Paths)
        {
            return [.. AllDirectories.Where(dir => Paths.Any(Path => dir.Contains(Path)))];
        }

        internal static string AssumeWorkingDirectory(Dictionary<string, string[]> InstanceObject)
        {
            Config config = Config.GetConfig();
            string LocalDirectory = Directory.GetCurrentDirectory();

            if (InstanceObject.ContainsKey("local"))
            {
                return LocalDirectory;
            }

            if (config.DefaultFolder is not null)
            {
                return config.DefaultFolder;
            }

            return LocalDirectory;
        }
    }
}
