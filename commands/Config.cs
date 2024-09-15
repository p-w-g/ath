using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ath.commands
{
    public class Config
    {
        [JsonPropertyName("defaultFolder")]
        public string? DefaultFolder { get; set; }
        [JsonPropertyName("ignoredFolders")]
        public List<string>? IgnoredFolders { get; set; }

        static string GetHomeDir()
        {
            bool IsWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
            if (IsWindows)
            {
                string UserProfile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                return UserProfile;
            }
            string NixHome = Environment.GetEnvironmentVariable("HOME") ?? "/";
            return NixHome;
        }
        static readonly string defaultConfigPath = Path.Combine(GetHomeDir(), ".athconfig");

        public static void PrintConfigPath()
        {
            if (!File.Exists(defaultConfigPath))
            {
                Console.WriteLine("config file doesn't exist");
                return;
            }
            Console.WriteLine(defaultConfigPath);
        }

        public static void PrintConfig()
        {
            if (!File.Exists(defaultConfigPath))
            {
                Console.WriteLine("config file doesn't exist");
                return;
            }
            string config = File.ReadAllText(defaultConfigPath);
            Console.WriteLine(config);
        }

        public static Config GetConfig()
        {
            if (!File.Exists(defaultConfigPath))
            {
                return new Config();
            }
            string config = File.ReadAllText(defaultConfigPath);

            return JsonSerializer.Deserialize<Config>(config) ?? new Config();
        }

        private static void SaveConfig(Config config)
        {
            string configJson = JsonSerializer.Serialize(config, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(defaultConfigPath, configJson);
            Console.WriteLine("Config file updated successfully!");
        }

        public static void SetWorkingDirectory()
        {
            string NewWorkingDirectory = Directory.GetCurrentDirectory();
            Config config = GetConfig();
            config.DefaultFolder = NewWorkingDirectory;
            SaveConfig(config);
        }

        public static void UnsetWorkingDirectory()
        {
            Config config = GetConfig();
            config.DefaultFolder = null;
            SaveConfig(config);
        }

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
                        Console.WriteLine($"{folder} is already in the ignored folders list, skipping.");
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
}
