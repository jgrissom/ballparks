// Project/Program.cs — swap Thing for your record's name, and Visit for your verb
var registry = new Registry();

// A relative path is worked out from where you were STANDING when you ran
// the program — the top of this repo — not from where the program is.
string registryFile = "registry.json";

registry.Load(registryFile);

if (registry.Count == 0)
{
    registry.Add(new BallPark("Wrigley Field", "Cubs", 41649));
    registry.Add(new BallPark("Fenway Park", "Red Sox", 37755));
    registry.Add(new BallPark("Oracle Park", "Giants", 41265));

    // Week 7's rule, visible: the same name registered twice, refused quietly —
    // and the count is how you can tell it happened at all.
    // registry.Add(registry.NewItem("Wrigley Field"));
    // Console.WriteLine($"Registered \"Wrigley Field\" twice - {registry.Count} on file.");
}

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

Console.WriteLine();
Console.WriteLine($"{registry.Count} on file, saved to {registryFile}.");
