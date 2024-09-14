using System.Runtime.InteropServices;
namespace ath.commands
{
    public class Config
    {
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
    }
}