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

    [Fact]
    public void Check4_RemovingAStrangerSaysNo()
    {
        var registry = new Registry();
        var amfam = new BallPark("American Family Field");
        registry.Add(amfam);
        var result = registry.Remove("Wrigley Field");
        Assert.False(result);
        Assert.Equal(1, registry.Count);
    }

    [Fact]
    public void Check5_TheSameNameCannotRegisterTwice()
    {
        var registry = new Registry();
        registry.Add(new BallPark("American Family Field"));
        registry.Add(new BallPark("American Family Field"));
        Assert.Equal(1, registry.Count);
    }

    [Fact]
    public void Week8_TheRegistrySurvivesARestart()
    {
        string path = Path.Combine(Path.GetTempPath(), "ballparks-test.json");
        File.Delete(path);

        Registry registry = new Registry();
        BallPark ballPark = registry.NewItem("Google Park");
        ballPark.Capacity = 42380;
        registry.Add(ballPark);
        ballPark.Visit();

        registry.Save(path);

        // A second registry, holding nothing, reading the same file.
        Registry reopened = new Registry();
        reopened.Load(path);

        Assert.Equal(1, reopened.Count);

        BallPark? back = reopened.Find("Google Park");

        Assert.NotNull(back);
        Assert.Equal(42380, back!.Capacity);
        Assert.Equal(1, back.GamesSeen);
    }
}
