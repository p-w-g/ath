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

        private static Dictionary<string, string[]> InstanceObject = [];

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
            // payload
            // ie git status along with its own --prune
            // command
            // workingdirectory

            //options
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
        }

        internal static void ParseOptions(string option)
        {
            string key = option.Split('-')[0];
            string[] values = option.Split('-')[1..] ?? [];

            InstanceObject.Add(key, values);
        }
    }
}
