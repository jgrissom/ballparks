public class Game
{
    public int Id { get; set; }

    public int BallParkId { get; set; }

    public string OpponentName { get; private set; }

    public Game(string opponentName)
    {
        OpponentName = opponentName;
    }
}
