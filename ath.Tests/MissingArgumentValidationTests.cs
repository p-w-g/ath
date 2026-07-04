namespace AthTests;

using Config = ath.Config.Config;
using FEP = ath.Commands.FEP;

public class MissingArgumentValidationTests
{
    [Fact]
    public void BareCfgDoesNotThrow()
    {
        Config.Evaluate(new Dictionary<string, string[]>());
    }

    [Fact]
    public void CfgToWithoutDurationDoesNotThrow()
    {
        Config.Evaluate(new Dictionary<string, string[]> { ["PayLoad"] = ["to"] });
    }

    [Fact]
    public async Task BareFepDoesNotThrow()
    {
        await FEP.RunParallelAsync(new Dictionary<string, string[]>());
    }
}
