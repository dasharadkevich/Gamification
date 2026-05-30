namespace ProgrammingGame
{
    public class RankSystem
    {
        public string CurrentRank { get; set; } = "Новачок";
        public int Points { get; set; } = 0;

        private List<(int points, string rank)> Ranks { get; set; }

        public RankSystem()
        {
            Points = 0;
            Ranks = new List<(int points, string rank)>();
            InitializeRanksFromTextResources();
            UpdateRank();
        }

        public RankSystem(int initialPoints)
        {
            Points = Math.Max(0, initialPoints);
            Ranks = new List<(int points, string rank)>();
            InitializeRanksFromTextResources();
            UpdateRank();
        }


        private void InitializeRanksFromTextResources()
        {

            var rankNames = TextManager.Texts?.RankNames;

                Ranks = new List<(int points, string rank)>
                {
                    (0, rankNames.Novice),
                    (100, rankNames.Junior),
                    (250, rankNames.Middle),
                    (450, rankNames.Senior),
                    (700, rankNames.Architect)
                };
        }

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