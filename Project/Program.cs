// Project/Program.cs — swap Thing for your record's name, and Visit for your verb
var registry = new Registry();

string registryFile = "registry.json";

registry.Add(new BallPark("Wrigley Field", "Cubs", 41649));
registry.Add(new BallPark("Fenway Park", "Red Sox", 37755));
registry.Add(new BallPark("Oracle Park", "Giants", 41265));

registry.Load(registryFile);

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

registry.Save(registryFile);
Console.WriteLine($"{registry.Count} on file, saved to {registryFile}.");

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
