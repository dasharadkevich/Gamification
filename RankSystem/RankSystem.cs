namespace ProgrammingGame
{
    public class RankSystem
    {
        public string CurrentRank { get; set; } = "Новачок";
        public int Points { get; set; } = 0;


        public RankSystem(RankSystem other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            Points = other.Points;
            CurrentRank = other.CurrentRank;

            Ranks = [.. other.Ranks];
        }

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

        // Бінарні арифметичні
        public static RankSystem operator +(RankSystem rs, int points)
        {
            if (rs == null) return new RankSystem(points);
            return new RankSystem(rs.Points + points);
        }

        public static RankSystem operator -(RankSystem rs, int points)
        {
            if (rs == null) return new RankSystem(0);
            return new RankSystem(Math.Max(0, rs.Points - points));
        }

        public static RankSystem operator *(RankSystem rs, int multiplier)
        {
            if (rs == null) return new RankSystem(0);
            return new RankSystem(rs.Points * Math.Max(0, multiplier));
        }

        // Порівняння
        public static bool operator ==(RankSystem a, RankSystem b)
            => (a?.Points ?? 0) == (b?.Points ?? 0);

        public static bool operator !=(RankSystem a, RankSystem b) => !(a == b);

        public static bool operator >(RankSystem a, RankSystem b)
            => (a?.Points ?? 0) > (b?.Points ?? 0);

        public static bool operator <(RankSystem a, RankSystem b)
            => (a?.Points ?? 0) < (b?.Points ?? 0);

        public static bool operator >=(RankSystem a, RankSystem b)
            => (a?.Points ?? 0) >= (b?.Points ?? 0);

        public static bool operator <=(RankSystem a, RankSystem b)
            => (a?.Points ?? 0) <= (b?.Points ?? 0);

        public override bool Equals(object? obj)
        {
            if (obj is RankSystem other)
                return this == other;
            return false;
        }
        public override int GetHashCode() => Points.GetHashCode();




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