namespace AthTests;

using Config = ath.Config.Config;

public class ConfigParserTests
{
    [Fact]
    public void CorruptedJsonFallsBackToDefaultConfigInsteadOfThrowing()
    {
        Config config = Config.ParseConfig("not valid json {{{");

        Assert.Null(config.DefaultFolder);
        Assert.Null(config.IgnoredFolders);
        Assert.Null(config.TimeOut);
    }

    [Fact]
    public void ValidJsonStillDeserializesNormally()
    {
        Config config = Config.ParseConfig(
            """{"defaultFolder":"C:\\code","ignoredFolders":[".git"],"timeOut":30}"""
        );

        Assert.Equal("C:\\code", config.DefaultFolder);
        Assert.Equal([".git"], config.IgnoredFolders);
        Assert.Equal(30, config.TimeOut);
    }
}
