public class BallPark : IListed
{
    private string _name = "unknown";
    public string Name                               // rule → explicit field
    {
        get { return _name; }
        set { if (!string.IsNullOrWhiteSpace(value)) { _name = value.Trim(); } }
    }

    public string Team { get; set; } = "unknown";    // no rule → short form

    private int _capacity;
    public int Capacity                              // rule → refuses nonsense, 0 = unknown
    {
        get { return _capacity; }
        set { if (value > 0) { _capacity = value; } }
    }

    public int GamesSeen { get; private set; }         // read by all, moved by Visit only
    public DateOnly? LastVisit { get; private set; }

    public void Visit(DateOnly when)
    {
        GamesSeen++;
        LastVisit = when;
    }

    public BallPark(string name, string team = "unknown", int capacity = 0)
    {
        Name = name;
        Team = team;
        Capacity = capacity;
    }

    public string Kind => "BALLPARK";

    public string Line() => $"{Name} - {Team} - visits {GamesSeen}";
}
