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
            Console.WriteLine(defaultConfigPath);
        }

        private static Config GetConfig()
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
    }
}