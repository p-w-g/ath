namespace ath.Commands;

using ath.CliUtils;
using ath.Config;

public static class FEP
{
    public static async Task RunParallelAsync(Dictionary<string, string[]> Instance)
    {
        Config config = Config.GetConfig();
        string[] availableDirs = CliUtils.GetAvailableDirectories(Instance, config);

        Task[] tasks = availableDirs
            .Select(dir => Task.Run(() => CliUtils.RunCommand(Instance, config, dir)))
            .ToArray();
        await Task.WhenAll(tasks);
    }
}
