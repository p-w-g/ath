using System.Diagnostics;
namespace ath.commands
{
    public static class cliTooling
    {
        internal static async Task RunCommand(string command, string arguments, string workingDirectory = "")
        {
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

            using Process process = new() { StartInfo = processInfo };

            try
            {
                process.Start();

                Task<string> output = process.StandardOutput.ReadToEndAsync();
                Task<string> error = process.StandardError.ReadToEndAsync();

                bool exited = await Task.Run(() => process.WaitForExit(300000));

                if (!exited)
                {
                    Console.WriteLine($"Command timed out in {workingDirectory} after 5 minutes.");
                    process.Kill();
                    return;
                }

                if (process.ExitCode == 0)
                {
                    Console.WriteLine($"Command executed successfully in {workingDirectory}.");
                    Console.WriteLine(await output);
                }
                else
                {
                    Console.WriteLine($"Command failed gracefully in {workingDirectory}.");
                    Console.WriteLine(await error);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine($"Caught exception in {workingDirectory} with error:");
                Console.WriteLine(e);
            }
            finally
            {
                process.Dispose();
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