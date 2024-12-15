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

        internal static Dictionary<string, string[]> OptionsParser(string[] args)
        {
            Dictionary<string, string[]> additionalParams = [];

            var parsedOptions = args.Where(arg => arg.StartsWith("--"))
                .Select(arg => arg.TrimStart('-'))
                .Select(option => optionParams(option, additionalParams))
                .Where(option => ValidOptions.Contains(option.ToLower()))
                .ToList();

            // options object needs "internal command" field
            // ie git status along with its own --prune

            // and it needs to ignore already parsed options so that I dont have doubles

            // and add workingdirectory to options object

            return additionalParams;
        }

        internal static string optionParams(
            string option,
            Dictionary<string, string[]> additionalParams
        )
        {
            if (option.Contains('-'))
            {
                string name = option.Split('-')[0];
                string[] adds = option.Split('-')[1..];
                additionalParams.Add(name, adds);
                return name;
            }
            else
            {
                additionalParams.Add(option, []);
                return option;
            }
        }
    }
}
