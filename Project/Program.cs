var registry = new Registry();

registry.Add(new BallPark("Wrigley Field", "Cubs", 41649));
registry.Add(new BallPark("Fenway Park", "Red Sox", 37755));
registry.Add(new BallPark("Oracle Park", "Giants", 41265));

Console.WriteLine(Registry.Topic);
Console.WriteLine($"{registry.Count} parks on file.");
Console.WriteLine();

foreach (BallPark park in registry.All())
{
    Console.WriteLine($"{park.Name} - {park.Team} - holds {park.Capacity:n0}");
}

Console.WriteLine();

// One I know is on the registry.
BallPark? known = registry.Find("Oracle Park");
Console.WriteLine(known == null ? "Nothing on file by that name." : "Found it.");

// And one nobody has ever heard of.
BallPark? missing = registry.Find("Google Park");
Console.WriteLine(missing == null ? "Nothing on file by that name." : "...found something that shouldn't be there.");

Console.WriteLine();
Console.WriteLine(registry.Remove("Oracle Park")
    ? "Removed."
    : "Nothing by that name.");
Console.WriteLine($"{registry.Count} on file.");

Console.WriteLine();

foreach (BallPark park in registry.All())
{
    Console.WriteLine($"{park.Kind,-12}{park.Line()}");
}
