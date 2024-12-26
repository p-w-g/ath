namespace ath.commands
{
    partial class cliUtils
    {
        private static readonly HashSet<string> ValidOptions = new HashSet<string>
        {
            "sustain",
            "local",
            "skip",
            "only",
            "timeout",
        };

        private static Dictionary<string, string[]> InstanceObject =
            new Dictionary<string, string[]>();

        internal static Dictionary<string, string[]> InstanceParser(string[] args)
        {
            foreach (string arg in args)
            {
                ParseArgument(arg);
            }

            return InstanceObject;
        }

        internal static void ParseArgument(string arg)
        {
            // workingdirectory

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

        internal static void ParsePayLoad(string argument)
        {
            var _ = InstanceObject.ContainsKey("PayLoad")
                ? InstanceObject["PayLoad"] = InstanceObject["PayLoad"].Concat([argument]).ToArray()
                : InstanceObject["PayLoad"] = [argument];
        }
    }
}
