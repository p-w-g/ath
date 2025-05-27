namespace ath.Config;

partial class Config
{
    public static void SetTimeout(string duration)
    {
        if (duration.Length == 0)
        {
            Console.WriteLine("Forgot to add duration in `ath cfg to`?");
            return;
        }

        int.TryParse(duration, out int timeOutDuration);
        Config config = GetConfig();

        config.TimeOut = timeOutDuration == 0 ? null : timeOutDuration;

        SaveConfig(config);
    }

    public static void UnsetTimeout()
    {
        Config config = GetConfig();
        config.TimeOut = null;
        SaveConfig(config);
    }
}
