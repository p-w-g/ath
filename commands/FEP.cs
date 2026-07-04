namespace ath.Commands;

using ath.CliUtils;
using ath.Config;

public static class FEP
{
    public static async Task RunParallelAsync(Dictionary<string, string[]> Instance)
    {
        if (!Instance.ContainsKey("PayLoad"))
        {
            Console.WriteLine(
                "Usage: ath fep <<command>> [--skip-foo,bar,baz || --only-gris,gras,gres]"
            );
            return;
        }

        Config config = Config.GetConfig();
        string[] availableDirs = CliUtils.GetAvailableDirectories(Instance, config);

        Task[] tasks = availableDirs
            .Select(dir => Task.Run(() => CliUtils.RunCommand(Instance, config, dir)))
            .ToArray();
        await Task.WhenAll(tasks);
    }
}
