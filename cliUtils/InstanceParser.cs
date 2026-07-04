namespace ath.CliUtils;

partial class CliUtils
{
    private static readonly HashSet<string> ValidOptions = new HashSet<string>
    {
        "sustain",
        "local",
        "skip",
        "only",
        "timeout",
        "all",
    };

    private static Dictionary<string, string[]> InstanceObject = new Dictionary<string, string[]>();

    internal static Dictionary<string, string[]> InstanceParser(string[] args)
    {
        InstanceObject = new Dictionary<string, string[]>();

        foreach (string arg in args)
        {
            ParseArgument(arg);
        }

        return InstanceObject;
    }

    internal static void ParseArgument(string arg)
    {
        // options
        if (arg.StartsWith("--"))
        {
            arg = arg.TrimStart('-');
            string option = arg.Split('-')[0];
            bool isInternal = ValidOptions.Contains(option);
            if (isInternal)
            {
                ParseOptions(arg);
            }
        }
        // payload
        else
        {
            ParsePayLoad(arg);
        }
    }

    internal static void ParseOptions(string option)
    {
        string key = option.Split('-')[0];
        string[] values = option.Split('-')[1..] ?? [];

        InstanceObject.Add(key, values);
    }

    internal static string[] ParsePayLoad(string argument)
    {
        return InstanceObject.ContainsKey("PayLoad")
            ? InstanceObject["PayLoad"] = [.. InstanceObject["PayLoad"], argument]
            : InstanceObject["PayLoad"] = [argument];
    }
}
