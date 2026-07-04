namespace AthTests;

using CliUtils = ath.CliUtils.CliUtils;

public class DuplicateFlagTests
{
    [Fact]
    public void RepeatingAFlagMergesValuesInsteadOfThrowing()
    {
        Dictionary<string, string[]> result = CliUtils.InstanceParser(
            ["--skip-foo", "--skip-bar"]
        );

        Assert.Equal(["foo", "bar"], result["skip"]);
    }

    [Fact]
    public void RepeatingAValuelessFlagDoesNotThrow()
    {
        Dictionary<string, string[]> result = CliUtils.InstanceParser(["--local", "--local"]);

        Assert.Empty(result["local"]);
    }
}
