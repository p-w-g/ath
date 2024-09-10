using System.Diagnostics;
namespace ath.commands
{
    public static class cliTooling
    {
        internal static void RunCommand(string command, string arguments, string workingDirectory = "")
        {
            // Determine the shell to use based on the operating system
            string shell = Environment.OSVersion.Platform == PlatformID.Win32NT ? "cmd.exe" : "/bin/bash";
            string shellArguments = Environment.OSVersion.Platform == PlatformID.Win32NT ? $"/c {command} {arguments}" : $"-c \"{command} {arguments}\"";


            var processInfo = new ProcessStartInfo(shell, shellArguments)
            {
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true,
                WorkingDirectory = workingDirectory
            };

            Console.WriteLine($"Running '{command} {arguments}' in {workingDirectory}");

            using var process = new Process { StartInfo = processInfo };
            process.Start();

            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();

            process.WaitForExit();

            if (process.ExitCode == 0)
            {
                Console.WriteLine($"Command executed successfully in {workingDirectory}.");
                Console.WriteLine(output);
            }
            else
            {
                Console.WriteLine($"Command failed in {workingDirectory} with error:");
                Console.WriteLine(error);
            }
        }

        internal static string[] FilterFlags(string flag, string[] args)
        {
            return args != null ? Array.Find(args, arg => arg.Contains(flag))!
            .Split(flag)[1]
            .Split("-")
            .Where(arg => !string.IsNullOrWhiteSpace(arg)).ToArray() : [];
        }
    }
}