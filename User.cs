using System;
using System.Diagnostics;
using System.Linq;

using ProgrammingGame.Data.JSONModels;

namespace ProgrammingGame
{
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
        public int BestStreak { get; set; } = 0;
        public TimeSpan TimeSpent { get; private set; } = TimeSpan.Zero;
        public List<Achievement> Achievements { get; private set; } = new List<Achievement>();
        public List<Reward> Awards { get; private set; } = new List<Reward>();
        public RankSystem Rank { get; set; }
        private Stopwatch stopwatch = new Stopwatch();

        public User()
        {
            var defaultValues = TextManager.Texts?.DefaultValues;
            
            UserName = defaultValues?.DefaultUserName ?? "";
            Age = defaultValues?.DefaultAge ?? 18;
            Rank = new RankSystem();
        }

        public User(string userName, int age)
        {
            UserName = userName;
            Age = age;
            Rank = new RankSystem();
        }

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
                var achievementMessages = TextManager.Texts?.AchievementMessages;
                
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine(string.Format(achievementMessages?.Unlock ?? "", achievement.Name));
                Console.ResetColor();
            }
        }
        
        public void UnlockAward(Reward award)
        {
            if (!Awards.Any(a => a.Name == award.Name))
            {
                Awards.Add(award);
                var achievementMessages = TextManager.Texts?.AchievementMessages;
                
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(string.Format(achievementMessages?.Award ?? "", award.Name));
                Console.ResetColor();
            }
        }
    }
}