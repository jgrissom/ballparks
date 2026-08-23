// Project/Registry.cs
public class Registry
{
    private readonly List<BallPark> _ballParks = new List<BallPark>();

    public static string Topic => "Collection of baseball parks";     // ← yours, in words

    public BallPark NewItem(string name) => new BallPark(name);

    public void Add(BallPark item)
    {
        _ballParks.Add(item);
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
}
