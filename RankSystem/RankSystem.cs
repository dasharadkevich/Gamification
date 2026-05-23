namespace ProgrammingGame
{
    public class RankSystem
    {
        public string CurrentRank { get; set; } = "Новачок";
        public int Points { get; set; } = 0;

        public RankSystem()
        {
            Ranks = new List<(int, string)>
            {
                (0,   "Новачок"),
                (100, "Junior"),
                (250, "Middle"),
                (450, "Senior"),
                (700, "Архітектор")
            };

            CurrentRank = "Новачок";
            Points = 0;
        }


        public RankSystem(int initialPoints)
        {
            Points = Math.Max(0, initialPoints);
            UpdateRank();
        }


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