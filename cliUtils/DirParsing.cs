namespace ath.CliUtils;

using ath.Config;

partial class CliUtils
{
    internal static string[] GetAvailableDirectories(Dictionary<string, string[]> InstanceObject)
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
