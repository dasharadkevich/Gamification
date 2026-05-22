using System;
using System.Diagnostics;
namespace ProgrammingGame
{
    using System.Diagnostics;
    public class User
    {
        public string UserName { get; set; }
        private int age;
        public int Age
        {
            get => age;
            set
            {
                if (value < 6)
                    age = 6;
                else if (value > 99)
                    age = 99;
                else
                    age = value;
            }
        }


        public int CurrentStreak { get; private set; } = 0;
        public int BestStreak { get; private set; } = 0;
        public TimeSpan TimeSpent { get; private set; } = TimeSpan.Zero;
        public List<Achievement> Achievements { get; private set; } = new List<Achievement>();
        public List<Reward> Awards { get; private set; } = new List<Reward>();
        public RankSystem Rank { get; private set; }
        private Stopwatch stopwatch = new Stopwatch();



        public User()
        {
            UserName = "Гравець";
            Age = 18;

            stopwatch = new Stopwatch();
            Achievements = new List<Achievement>();
            Awards = new List<Reward>();
            Rank = new RankSystem();

            TimeSpent = TimeSpan.Zero;
        }

        public User(string userName, int age)
        {
            UserName = userName;
            Age = age;
            Rank = new RankSystem();
        }

        public static User operator +(User user, int points)
        {
            user.AddPoints(points);
            return user;
        }

        public static User operator +(User a, User b)
        {
            var result = new User(a.UserName + " & " + b.UserName, (a.Age + b.Age) / 2);
            result.Rank = new RankSystem(a.Rank.Points + b.Rank.Points);
            return result;
        }

        // Порівняння
        public static bool operator ==(User a, User b)
        {
            if (ReferenceEquals(a, b)) return true;
            if (a is null || b is null) return false;
            return a.UserName == b.UserName && a.Rank.Points == b.Rank.Points;
        }

        public static bool operator !=(User a, User b) => !(a == b);

        public static bool operator >(User a, User b) => a.Rank.Points > b.Rank.Points;
        public static bool operator <(User a, User b) => a.Rank.Points < b.Rank.Points;

        public override bool Equals(object obj) => obj is User other && this == other;
        public override int GetHashCode() => UserName.GetHashCode() ^ Rank.Points;




        public void StartSession() => stopwatch.Restart();
        public void EndSession()
        {
            stopwatch.Stop();
            TimeSpent += stopwatch.Elapsed;
        }
        public void AddPoints(int points)
        {
            Rank.AddPoints(points);
        }
        public void IncreaseStreak()
        {
            CurrentStreak++;
            if (CurrentStreak > BestStreak)
                BestStreak = CurrentStreak;
        }
        public void ResetStreak() => CurrentStreak = 0;
        public void UnlockAchievement(Achievement achievement)
        {
            if (!Achievements.Any(a => a.Name == achievement.Name))
            {
                Achievements.Add(achievement);
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($"🏆 Нове досягнення: {achievement.Name}!");
                Console.ResetColor();
            }
        }
        public void UnlockAward(Reward award)
        {
            if (!Awards.Any(a => a.Name == award.Name))
            {
                Awards.Add(award);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"🎖 Нагорода: {award.Name}!");
                Console.ResetColor();
            }
        }



        public override string ToString()
        {
            return $"{UserName}, Вік {Age} | Ранг: {Rank.CurrentRank}";
        }
    }
}