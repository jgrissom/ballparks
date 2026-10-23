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
        return _ballParks.FirstOrDefault(b => b.Name == name);
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

    public void Save(ProjectContext db)
    {
        foreach (BallPark item in _ballParks)
        {
            if (item.Id == 0)
            {
                db.BallParks.Add(item);
            }
            else
            {
                db.BallParks.Update(item);
            }
        }

        db.SaveChanges();
    }

    public void Load(ProjectContext db)
    {
        List<BallPark> loaded = db.BallParks.OrderBy(item => item.Id).ToList();

        if (loaded.Count == 0)
        {
            return;
        }

        _ballParks.Clear();

        _ballParks.AddRange(loaded);
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

    public List<BallPark> Matching(string term)
    {
        return _ballParks.Where(b => b.Name.Contains(term)).ToList();
    }

    public List<string> Names()
    {
        return _ballParks.Select(b => b.Name).ToList();
    }

    public List<BallPark> Sorted()
    {
        return _ballParks.OrderBy(b => b.Name).ToList();
    }
}
