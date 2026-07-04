namespace AthTests;

using CliUtils = ath.CliUtils.CliUtils;

public class OptionValueParsingTests
{
    [Fact]
    public void SingleHyphenatedFolderNameSurvivesAsOneValue()
    {
        Dictionary<string, string[]> result = CliUtils.InstanceParser(["--skip-my-project"]);

        Assert.Equal(["my-project"], result["skip"]);
    }

    [Fact]
    public void CommaSeparatesMultipleValues()
    {
        Dictionary<string, string[]> result = CliUtils.InstanceParser(["--skip-foo,bar,baz"]);

        Assert.Equal(["foo", "bar", "baz"], result["skip"]);
    }

    [Fact]
    public void CommaSeparatedListCanIncludeAHyphenatedName()
    {
        Dictionary<string, string[]> result = CliUtils.InstanceParser(["--skip-my-project,other"]);

        Assert.Equal(["my-project", "other"], result["skip"]);
    }

    [Fact]
    public void ValuelessFlagStillParsesToEmptyArray()
    {
        Dictionary<string, string[]> result = CliUtils.InstanceParser(["--local"]);

        Assert.Empty(result["local"]);
    }
}
