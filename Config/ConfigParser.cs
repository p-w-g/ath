using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ath.Config;

partial class Config
{
    [JsonPropertyName("defaultFolder")]
    public string? DefaultFolder { get; set; }

    [JsonPropertyName("ignoredFolders")]
    public List<string>? IgnoredFolders { get; set; }

    [JsonPropertyName("timeOut")]
    public int? TimeOut { get; set; }

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

    public static Config GetConfig()
    {
        if (!File.Exists(defaultConfigPath))
        {
            return new Config();
        }
        string config = File.ReadAllText(defaultConfigPath);

        return ParseConfig(config);
    }

    internal static Config ParseConfig(string json)
    {
        try
        {
            return JsonSerializer.Deserialize<Config>(json) ?? new Config();
        }
        catch (JsonException)
        {
            Console.WriteLine(
                $"Warning: config file at {defaultConfigPath} is corrupted or invalid; using default settings."
            );
            return new Config();
        }
    }

    private static void SaveConfig(Config config)
    {
        string configJson = JsonSerializer.Serialize(
            config,
            new JsonSerializerOptions { WriteIndented = true }
        );
        File.WriteAllText(defaultConfigPath, configJson);
        Console.WriteLine("Config file updated successfully!");
    }
}
