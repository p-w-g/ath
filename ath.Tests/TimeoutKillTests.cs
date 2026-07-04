using System.Diagnostics;

namespace AthTests;

using CliUtils = ath.CliUtils.CliUtils;
using Config = ath.Config.Config;

public class TimeoutKillTests
{
    [Fact]
    public async Task TimeoutKillsTheEntireProcessTreeNotJustTheShell()
    {
        // cmd.exe never exec-replaces itself with its child the way some
        // shells can, so on Windows this reliably reproduces a real
        // parent/grandchild relationship: ath -> cmd.exe -> ping.exe.
        if (!OperatingSystem.IsWindows())
        {
            return;
        }

        string dir = Path.Combine(Path.GetTempPath(), "ath-tests-" + Guid.NewGuid());
        Directory.CreateDirectory(dir);
        try
        {
            Dictionary<string, string[]> instance = new()
            {
                ["PayLoad"] = ["ping", "-n", "30", "127.0.0.1"],
            };
            Config config = new() { TimeOut = 1 };

            bool result = await CliUtils.RunCommand(instance, config, dir);

            Assert.False(result);

            // Give the OS a moment to actually tear down the tree, then
            // confirm no grandchild "ping" process is still running.
            bool grandchildStillAlive = false;
            for (int i = 0; i < 20 && !grandchildStillAlive; i++)
            {
                await Task.Delay(100);
                grandchildStillAlive = Process
                    .GetProcessesByName("PING")
                    .Any(p => IsDescendantOfCurrentProcess(p));
            }

            Assert.False(
                grandchildStillAlive,
                "The grandchild 'ping' process outlived the timed-out parent shell."
            );
        }
        finally
        {
            // If the grandchild did survive, it may still be holding this
            // directory open as its working directory; don't let that
            // mask the assertion failure above.
            try
            {
                Directory.Delete(dir, recursive: true);
            }
            catch (IOException) { }
        }
    }

    private static bool IsDescendantOfCurrentProcess(Process candidate)
    {
        try
        {
            // Best-effort: a ping.exe still alive shortly after our own
            // timeout-triggered kill, started during this test, is
            // overwhelmingly likely to be the one we spawned.
            return (DateTime.Now - candidate.StartTime) < TimeSpan.FromSeconds(10);
        }
        catch
        {
            return false;
        }
    }
}
