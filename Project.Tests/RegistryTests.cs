using System.Net.Http.Headers;
using Microsoft.EntityFrameworkCore;

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
        DbContextOptions<ProjectContext> options = new DbContextOptionsBuilder<ProjectContext>()
           .UseInMemoryDatabase(Guid.NewGuid().ToString())
           .Options;

        var registry = new Registry();

        var amfam = new BallPark("American Family Field");

        registry.Add(amfam);

        amfam.Visit();

        registry.Save(new ProjectContext(options));

        var reopened = new Registry();
        reopened.Load(new ProjectContext(options));

        Assert.Equal(1, reopened.Count);
        Assert.Equal(1, reopened.All()[0].GamesSeen);
    }

    [Fact]
    public void Week10_ASecondSaveChangesTheRow()
    {
        DbContextOptions<ProjectContext> options = new DbContextOptionsBuilder<ProjectContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        Registry registry = new Registry();
        registry.Add(registry.NewItem("Fenway Park"));
        registry.Save(new ProjectContext(options));

        ProjectContext db = new ProjectContext(options);
        Registry nextRun = new Registry();
        nextRun.Load(db);
        BallPark item = nextRun.Find("Fenway Park")!;
        item.Visit();
        nextRun.Save(db);

        Registry reopened = new Registry();
        reopened.Load(new ProjectContext(options));

        Assert.Equal(1, reopened.Count);
        Assert.Equal(1, reopened.Find("Fenway Park")!.GamesSeen);
    }

    [Fact]
    public void Week9_FindComesBackEmptyHanded()
    {
        var registry = new Registry();

        var amfam = new BallPark("American Family Field");

        registry.Add(amfam);

        Assert.Same(amfam, registry.Find("American Family Field"));
        Assert.Null(registry.Find("Banana Park"));
    }

    [Fact]
    public void Week11_ARemovedRecordStaysGone()
    {
        DbContextOptions<ProjectContext> options = new DbContextOptionsBuilder<ProjectContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        Registry registry = new Registry();
        BallPark kept = registry.NewItem("a name of yours");
        BallPark removed = registry.NewItem("Fenway Park");
        registry.Add(kept);
        registry.Add(removed);
        registry.Save(new ProjectContext(options));

        ProjectContext db = new ProjectContext(options);
        Registry nextRun = new Registry();
        nextRun.Load(db);
        nextRun.Remove("Fenway Park");
        nextRun.Save(db);

        Registry reopened = new Registry();
        reopened.Load(new ProjectContext(options));

        Assert.Equal(1, reopened.Count);
        Assert.Null(reopened.Find("Fenway Park"));
    }
}
