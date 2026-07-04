namespace AthTests;

using CliUtils = ath.CliUtils.CliUtils;

public class ArgumentQuotingTests
{
    [Theory]
    [InlineData("git", false, "git")]
    [InlineData("fix critical bug", false, "'fix critical bug'")]
    [InlineData("fix critical bug", true, "\"fix critical bug\"")]
    [InlineData("&&", false, "&&")]
    public void QuotesOnlyArgumentsContainingWhitespace(
        string argument,
        bool isWindows,
        string expected
    )
    {
        Assert.Equal(expected, CliUtils.QuoteArgumentIfNeeded(argument, isWindows));
    }

    [Fact]
    public async Task GitCommitWithAQuotedMultiWordMessageSucceeds()
    {
        string repoDir = Path.Combine(Path.GetTempPath(), "ath-tests-" + Guid.NewGuid());
        Directory.CreateDirectory(repoDir);
        try
        {
            await RunGit(repoDir, "init -q");
            await RunGit(repoDir, "config user.email test@test.com");
            await RunGit(repoDir, "config user.name test");
            await File.WriteAllTextAsync(Path.Combine(repoDir, "file.txt"), "hello");
            await RunGit(repoDir, "add file.txt");

            Dictionary<string, string[]> instance = new()
            {
                ["PayLoad"] = ["git", "commit", "-m", "fix critical bug"],
            };

            await CliUtils.RunCommand(instance, new ath.Config.Config(), repoDir);

            string log = await RunGit(repoDir, "log -1 --pretty=%B");
            Assert.Contains("fix critical bug", log);
        }
        finally
        {
            // git leaves some object files read-only on Windows, which
            // Directory.Delete can't remove without clearing that first.
            foreach (
                string file in Directory.EnumerateFiles(
                    repoDir,
                    "*",
                    SearchOption.AllDirectories
                )
            )
            {
                File.SetAttributes(file, FileAttributes.Normal);
            }
            Directory.Delete(repoDir, recursive: true);
        }
    }

    private static async Task<string> RunGit(string workingDirectory, string arguments)
    {
        System.Diagnostics.ProcessStartInfo info = new("git", arguments)
        {
            WorkingDirectory = workingDirectory,
            RedirectStandardOutput = true,
            UseShellExecute = false,
        };
        using System.Diagnostics.Process process = System.Diagnostics.Process.Start(info)!;
        string output = await process.StandardOutput.ReadToEndAsync();
        await process.WaitForExitAsync();
        return output;
    }
}
