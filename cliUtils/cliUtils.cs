namespace ath.commands
{
    public partial class cliUtils
    {
        internal static string[] FilterFlags(string flag, string[] args)
        {
            return args != null
                ? Array
                    .Find(args, arg => arg.Contains(flag))!
                    .Split(flag)[1]
                    .Split("-")
                    .Where(arg => !string.IsNullOrWhiteSpace(arg))
                    .ToArray()
                : [];
        }
    }
}
