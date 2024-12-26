namespace ath.commands
{
    public static class FEP
    {
        public static async Task RunParallelAsync(Dictionary<string, string[]> Instance)
        {
            string[] availableDirs = cliUtils.GetAvailableDirectories(Instance);
            Task[] tasks = availableDirs
                .Select(dir => Task.Run(() => cliUtils.RunCommand(Instance, dir)))
                .ToArray();
            await Task.WhenAll(tasks);
        }
    }
}
