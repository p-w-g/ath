namespace ath.Commands;

public static class FEP
{
    public static async Task RunParallelAsync(Dictionary<string, string[]> Instance)
    {
        string[] availableDirs = CliUtils.GetAvailableDirectories(Instance);
        Task[] tasks = availableDirs
            .Select(dir => Task.Run(() => CliUtils.RunCommand(Instance, dir)))
            .ToArray();
        await Task.WhenAll(tasks);
    }
}
