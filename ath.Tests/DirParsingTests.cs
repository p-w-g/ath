namespace AthTests;

using CliUtils = ath.CliUtils.CliUtils;

public class DirParsingTests
{
    private static string[] FakeDirs(string parent, params string[] names) =>
        [.. names.Select(name => Path.Combine(parent, name))];

    [Fact]
    public void RemoveTargetDirectoriesDoesNotMatchUnrelatedFoldersSharingASubstring()
    {
        string[] dirs = FakeDirs("C:\\repos", "my-project", "myapp", "other");

        string[] result = CliUtils.RemoveTargetDirectories(dirs, ["my-project"]);

        Assert.Equal(FakeDirs("C:\\repos", "myapp", "other"), result);
    }

    [Fact]
    public void RemoveTargetDirectoriesDoesNotMatchOnParentPathSubstring()
    {
        // Regression: matching used to be done against the full path, so a
        // parent directory named e.g. "git-projects" would cause every
        // child to be excluded by "--skip-git".
        string[] dirs = FakeDirs("C:\\git-projects", "alpha", "beta");

        string[] result = CliUtils.RemoveTargetDirectories(dirs, ["git"]);

        Assert.Equal(dirs, result);
    }

    [Fact]
    public void SelectTargetDirectoriesMatchesExactFolderNameOnly()
    {
        string[] dirs = FakeDirs("C:\\repos", "my-project", "myapp", "other");

        string[] result = CliUtils.SelectTargetDirectories(dirs, ["my-project"]);

        Assert.Equal(FakeDirs("C:\\repos", "my-project"), result);
    }
}
