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

    [Fact]
    public void Check3_FindHandsBackTheRecordItHolds()
    {
        var registry = new Registry();
        var amfam = new BallPark("American Family Field");
        registry.Add(amfam);
        var found = registry.Find("American Family Field");
        // registry.Add(new BallPark("Wrigley Field"));
        Assert.Same(amfam, found);
    }
}
