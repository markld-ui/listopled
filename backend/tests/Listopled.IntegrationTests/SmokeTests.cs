namespace Listopled.IntegrationTests;

using Xunit;

public sealed class SmokeTests
{
    [Fact]
    public void Test_infrastructure_runs()
    {
        const string purpose = Phase1Placeholder.Purpose;

        Assert.Contains("Phase 1", purpose);
    }
}
