using System.Linq;

namespace ath.commands
{
    static partial class cliUtils
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
                .Where(option => ValidOptions.Contains(option.ToLower()));

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
