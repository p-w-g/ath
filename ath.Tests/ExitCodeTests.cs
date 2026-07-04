namespace AthTests;

using CliUtils = ath.CliUtils.CliUtils;
using Config = ath.Config.Config;
using FEP = ath.Commands.FEP;

public class ExitCodeTests
{
    private static string CreateTempDir()
    {
        string dir = Path.Combine(Path.GetTempPath(), "ath-tests-" + Guid.NewGuid());
        Directory.CreateDirectory(dir);
        return dir;
    }

    [Fact]
    public async Task RunCommandReturnsTrueWhenTheCommandSucceeds()
    {
        string dir = CreateTempDir();
        try
        {
            Dictionary<string, string[]> instance = new() { ["PayLoad"] = ["echo", "hi"] };

            bool result = await CliUtils.RunCommand(instance, new Config(), dir);

            Assert.True(result);
        }
        finally
        {
            Directory.Delete(dir, recursive: true);
        }
    }

    [Fact]
    public async Task RunCommandReturnsFalseWhenTheCommandFails()
    {
        string dir = CreateTempDir();
        try
        {
            Dictionary<string, string[]> instance = new() { ["PayLoad"] = ["exit", "1"] };

            bool result = await CliUtils.RunCommand(instance, new Config(), dir);

            Assert.False(result);
        }
        finally
        {
            Directory.Delete(dir, recursive: true);
        }
    }

    [Fact]
    public async Task RunParallelAsyncReturnsFalseIfAnyFolderFails()
    {
        string parent = CreateTempDir();
        string okDir = Path.Combine(parent, "ok");
        string failDir = Path.Combine(parent, "fail");
        Directory.CreateDirectory(okDir);
        Directory.CreateDirectory(failDir);
        string originalDir = Directory.GetCurrentDirectory();
        try
        {
            Dictionary<string, string[]> instance = new()
            {
                ["PayLoad"] = ["exit", "1"],
                ["local"] = [],
            };
            Directory.SetCurrentDirectory(parent);

            bool result = await FEP.RunParallelAsync(instance);

            Assert.False(result);
        }
        finally
        {
            Directory.SetCurrentDirectory(originalDir);
            Directory.Delete(parent, recursive: true);
        }
    }
}
