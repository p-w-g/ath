namespace AthTests;

using CliUtils = ath.CliUtils.CliUtils;

public class InstanceParserTests
{
    [Fact]
    public void ParsesPlainArgumentsAsPayload()
    {
        Dictionary<string, string[]> result = CliUtils.InstanceParser(["git", "status"]);

        Assert.Equal(["git", "status"], result["PayLoad"]);
    }

    [Fact]
    public void ParsesValuelessFlag()
    {
        Dictionary<string, string[]> result = CliUtils.InstanceParser(["--local"]);

        Assert.True(result.ContainsKey("local"));
        Assert.Empty(result["local"]);
    }

    [Fact]
    public void IsIndependentBetweenCalls()
    {
        CliUtils.InstanceParser(["--local"]);
        Dictionary<string, string[]> second = CliUtils.InstanceParser(["git", "status"]);

        Assert.False(second.ContainsKey("local"));
    }
}
