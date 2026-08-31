using System.Net.Http.Headers;

namespace Project.Tests;

public class RegistryTests
{
    [Fact]
    public void Check2_AddingGrowsTheCount()
    {
        // set the scene: a fresh Registry
        var registry = new Registry();
        // do the thing:   Add two records, with two DIFFERENT names
        registry.Add(new BallPark("American Family Field"));
        registry.Add(new BallPark("Wrigley Field"));
        // check:          Assert.Equal — what should Count be?
        Assert.Equal(2, registry.Count);
    }
}
