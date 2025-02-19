using System.Diagnostics;

namespace ath.CliUtils;

partial class CliUtils
{
    internal static async Task RunCommand(
        Dictionary<string, string[]> Instance,
        string workingDirectory = ""
    )
    {
        string command = string.Join(" ", Instance["PayLoad"]);
        string shell =
            Environment.OSVersion.Platform == PlatformID.Win32NT ? "cmd.exe" : "/bin/bash";
        string shellArguments =
            Environment.OSVersion.Platform == PlatformID.Win32NT
                ? $"/c {command}"
                : $"-c \"{command}\"";

        var processInfo = new ProcessStartInfo(shell, shellArguments)
        {
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true,
            WorkingDirectory = workingDirectory,
        };

        Console.WriteLine($"Running '{command}' in {workingDirectory}");

        using Process process = new() { StartInfo = processInfo };

        try
        {
            process.Start();

            Task<string> output = process.StandardOutput.ReadToEndAsync();
            Task<string> error = process.StandardError.ReadToEndAsync();

            // Timeouts, Sustain and Config
            // if (sustain)
            // {
            //     process.WaitForExit();
            // }
            // else
            // {
            // bool exited = await Task.Run(() => process.WaitForExit(300000));
            // if (!exited)
            // {
            //     Console.WriteLine($"Command timed out in {workingDirectory} after 5 minutes.");
            //     process.Kill();
            //     return;
            // }
            // }

            process.WaitForExit();
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
}
