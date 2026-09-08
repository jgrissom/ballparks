// Project/Registry.cs
using System.Text.Json;

public class Registry : IListed
{
    private readonly List<BallPark> _ballParks = new List<BallPark>();

    public static string Topic => "Collection of baseball parks";     // ← yours, in words

    public BallPark NewItem(string name) => new BallPark(name);

    public void Add(BallPark item)
    {
        if (Find(item.Name) == null)
        {
            _ballParks.Add(item);
        }
    }

    public int Count => _ballParks.Count;

    public List<BallPark> All()
    {
        return new List<BallPark>(_ballParks);      // ← a COPY
    }

    public BallPark? Find(string name)
    {
        foreach (BallPark ballPark in _ballParks)
        {
            if (ballPark.Name == name)
            {
                return ballPark;
            }
        }

        return null;
    }

    public bool Remove(string name)
    {
        BallPark? found = Find(name);

        if (found == null)
        {
            return false;
        }

        _ballParks.Remove(found);
        return true;
    }

    public string Kind => "REGISTRY";

    public string Line() => $"{Topic} - {Count} on file";

    public List<IListed> Everything()
    {
        List<IListed> listing = new List<IListed>();

        listing.Add(this);

        foreach (BallPark ballPark in _ballParks)
        {
            listing.Add(ballPark);
        }

        return listing;
    }

    public void Save(string path)
    {
        string json = JsonSerializer.Serialize(_ballParks,
            new JsonSerializerOptions { WriteIndented = true });

        File.WriteAllText(path, json);
    }

    // inside Rotation — the whole of loading
    public void Load(string path)
    {
        if (!File.Exists(path))
        {
            return;
        }

        List<BallPark>? loaded = JsonSerializer.Deserialize<List<BallPark>>(File.ReadAllText(path));

        if (loaded == null)
        {
            return;
        }

        _ballParks.Clear();

        foreach (BallPark ballPark in loaded)
        {
            _ballParks.Add(ballPark);
        }
    }
}
