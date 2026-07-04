using System.Diagnostics;

namespace ath.CliUtils;

using ath.Config;

partial class CliUtils
{
    internal static string QuoteArgumentIfNeeded(string argument, bool isWindows)
    {
        if (!argument.Any(char.IsWhiteSpace))
        {
            return argument;
        }

        return isWindows
            ? "\"" + argument.Replace("\"", "\\\"") + "\""
            : "'" + argument.Replace("'", "'\\''") + "'";
    }

    internal static async Task<bool> RunCommand(
        Dictionary<string, string[]> Instance,
        Config config,
        string workingDirectory = ""
    )
    {
        bool isWindows = Environment.OSVersion.Platform == PlatformID.Win32NT;
        string command = string.Join(
            " ",
            Instance["PayLoad"].Select(arg => QuoteArgumentIfNeeded(arg, isWindows))
        );
        string shell = isWindows ? "cmd.exe" : "/bin/bash";
        string shellArguments = isWindows ? $"/c {command}" : $"-c \"{command}\"";

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

            bool TimeOutConfigged = config.TimeOut is not null;
            bool TimeOutFlagged = Instance.ContainsKey("timeout");
            bool sustain = Instance.ContainsKey("sustain");
            int TimeOutDuration;

            if (sustain || (!TimeOutConfigged && !TimeOutFlagged))
            {
                process.WaitForExit();
            }
            else if (TimeOutFlagged || TimeOutConfigged)
            {
                TimeOutDuration = TimeOutFlagged
                    ? int.Parse(Instance["timeout"][0])
                    : (int)config.TimeOut;
                TimeOutDuration = TimeOutDuration * 1000;

                bool exited = await Task.Run(() => process.WaitForExit(TimeOutDuration));
                if (!exited)
                {
                    Console.WriteLine($"Command timed out in {workingDirectory} after 5 minutes.");
                    process.Kill();
                    return false;
                }
            }

            process.WaitForExit();
            if (process.ExitCode == 0)
            {
                Console.WriteLine($"Command executed successfully in {workingDirectory}.");
                Console.WriteLine(await output);
                return true;
            }
            else
            {
                Console.WriteLine($"Command failed gracefully in {workingDirectory}.");
                Console.WriteLine(await error);
                return false;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Caught exception in {workingDirectory} with error:");
            Console.WriteLine(e);
            return false;
        }
        finally
        {
            process.Dispose();
        }
    }
}
