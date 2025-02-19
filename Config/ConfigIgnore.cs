using System.Text.Json;

namespace ath.Config;

partial class Config
{
    public static void SetIgnoredDirectories(string[] folders)
    {
        if (folders.Length == 0)
        {
            Console.WriteLine("Usage: ath ignore <list of folders separated by space>");
            return;
        }
        Config config = GetConfig();

        if (config.IgnoredFolders == null)
        {
            config.IgnoredFolders = [.. folders];
        }
        else
        {
            foreach (var folder in folders)
            {
                if (config.IgnoredFolders.Contains(folder))
                {
                    Console.WriteLine(
                        $"{folder} is already in the ignored folders list, skipping."
                    );
                    continue;
                }
                config.IgnoredFolders.Add(folder);
                Console.WriteLine($"{folder} added to permanently ignored folders list");
            }
        }

        SaveConfig(config);
    }

    public static void UnsetIgnoredDirectories(string[] folders)
    {
        if (folders.Length == 0)
        {
            Console.WriteLine("Usage: ath unignore <list of folders separated by space>");
            return;
        }

        Config config = GetConfig();
        string deepCopy = JsonSerializer.Serialize(config);
        Config prevConfig = JsonSerializer.Deserialize<Config>(deepCopy)!;

        if (config.IgnoredFolders == null)
        {
            Console.WriteLine("That list is already empty");
            return;
        }

        foreach (var folder in folders)
        {
            if (config.IgnoredFolders.Contains(folder))
            {
                config.IgnoredFolders.Remove(folder);
                Console.WriteLine($"{folder} removed from permanently ignored folders list");
            }
            else
            {
                Console.WriteLine($"{folder} isn't present in ignored folders list, skipping.");
                continue;
            }
        }

        if (JsonSerializer.Serialize(config) != JsonSerializer.Serialize(prevConfig))
        {
            SaveConfig(config);
        }
        else
        {
            Console.WriteLine("No changes detected. Config file not saved.");
        }
    }
}
