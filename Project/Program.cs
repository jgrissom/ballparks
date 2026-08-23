// Project/Program.cs — swap Thing for your record's name and Find/Visit for yours
var registry = new Registry();

registry.Add(new BallPark("Wrigley Field", "Cubs", 41649));
registry.Add(new BallPark("Fenway Park", "Red Sox", 37755));
registry.Add(new BallPark("Oracle Park", "Giants", 41265));

Console.WriteLine(Registry.Topic);
Console.WriteLine($"{registry.Count} on file.");
Console.WriteLine();

// One I know something about.
BallPark? known = registry.Find("Red Sox");
if (known == null)
{
    Console.WriteLine("Nothing on file by that name.");
}
else
{
    known.Visit(new DateOnly(2026, 8, 22));
    Console.WriteLine($"{known.Name} - visited {known.GamesSeen}x");
}

// And one nobody has ever heard of.
BallPark? missing = registry.Find("Google Park");
Console.WriteLine(missing == null
    ? "Nothing on file by that name."
    : "...found something that shouldn't be there.");

Console.WriteLine();
Console.Write("Take one off the books (Enter to skip): ");
string? name = Console.ReadLine();
if (!string.IsNullOrWhiteSpace(name))
{
    Console.WriteLine(registry.Remove(name) ? "Removed." : "Nothing by that name.");
}

Console.WriteLine();
foreach (BallPark item in registry.All())
{
    Console.WriteLine(item.Name);
}
Console.WriteLine($"{registry.Count} on file.");
