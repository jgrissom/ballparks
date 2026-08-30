// Project/Program.cs — swap Thing for your record's name, and Visit for your verb
var registry = new Registry();

registry.Add(new BallPark("Wrigley Field", "Cubs", 41649));
registry.Add(new BallPark("Fenway Park", "Red Sox", 37755));
registry.Add(new BallPark("Oracle Park", "Giants", 41265));

// One I know something about. Find hands back the record the registry is
// holding, so the change lands on the real one.
BallPark? known = registry.Find("Fenway Park");
if (known != null)
{
    known.Visit();
}

Console.Write("Take one off the books (Enter to skip): ");
string? name = Console.ReadLine();
if (!string.IsNullOrWhiteSpace(name))
{
    Console.WriteLine(registry.Remove(name) ? "Removed." : "Nothing by that name.");
}

Console.WriteLine();

// One loop. It knows about exactly one thing, and that thing is not a class.
foreach (IListed thing in registry.Everything())
{
    Console.WriteLine($"{thing.Kind,-12}{thing.Line()}");
}
