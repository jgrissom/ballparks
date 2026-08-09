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

