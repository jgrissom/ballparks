// Project/Program.cs — swap Thing for your record's name, and Visit for your verb
using Microsoft.EntityFrameworkCore;

var registry = new Registry();

ProjectContext db = new ProjectContext();
if (!db.Database.CanConnect())
{
    Console.WriteLine("Failed to connect to your database");
    return;
}

registry.Add(new BallPark("Wrigley Field", "Cubs", 41649));
registry.Add(new BallPark("Fenway Park", "Red Sox", 37755));
registry.Add(new BallPark("Oracle Park", "Giants", 41265));

registry.Load(db);

// One I know something about. Find hands back the record the registry is
// holding, so the change lands on the real one.
BallPark? known = registry.Find("Fenway Park");
if (known != null)
{
    known.Visit();
}

RegistryDesk.Remove(registry);

// One loop. It knows about exactly one thing, and that thing is not a class.
foreach (IListed thing in registry.Everything())
{
    Console.WriteLine($"{thing.Kind,-12}{thing.Line()}");
}

BallPark? withDetails = registry.Find("Wrigley Field");

if (withDetails != null)
{
    withDetails.Games.Add(registry.NewDetail("Brewers"));
    Console.WriteLine($"{withDetails.Games.Count} details on file for that record.");
}
RegistryDesk.Import(registry);
try
{
    registry.Save(db);
}
catch (DbUpdateException)
{
    Console.WriteLine("A record this run was holding has gone from its table, so nothing was saved.");
    Console.WriteLine("Run the program again to pick up from what's there.");
    return;
}
Console.WriteLine($"With details: {string.Join(", ", registry.WithDetails(db))}");
Console.WriteLine($"{registry.Count} on file, saved to the database.");

Console.WriteLine();
Console.Write("Search (a word, or Enter to skip): ");
string? term = Console.ReadLine();

if (!string.IsNullOrWhiteSpace(term))
{
    var found = registry.Matching(term.Trim());

    Console.WriteLine(found.Count == 0
        ? $"  Nothing on file with \"{term.Trim()}\" in it."
        : $"  {found.Count} on file with \"{term.Trim()}\" in it.");
}

Console.WriteLine();
Console.WriteLine($"On the books, as they arrived: {string.Join(", ", registry.Names())}");

Console.WriteLine();
Console.WriteLine("In order:");

foreach (var record in registry.Sorted())
{
    Console.WriteLine($"  {record.Name}");
}
