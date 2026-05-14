namespace ProgrammingGame{
    public class RankSystem
{
    public string CurrentRank { get; private set; } = "Новачок";
    public int Points { get; private set; } = 0;

    private readonly List<(int points, string rank)> Ranks = new()
    {
        (0, "Новачок"),
        (100, "Junior"),
        (250, "Middle"),
        (450, "Senior"),
        (700, "Архітектор")
    };

    public void AddPoints(int points)
    {
        Points += points;
        UpdateRank();
    }

    private void UpdateRank()
    {
        for (int i = Ranks.Count - 1; i >= 0; i--)
        {
            if (Points >= Ranks[i].points)
            {
                CurrentRank = Ranks[i].rank;
                break;
            }
        }
    }
}
}