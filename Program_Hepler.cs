using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

using ProgrammingGame.Data.JSONModels;

namespace ProgrammingGame
{
    internal static class ProgramHelpers
    {
        private static readonly string UserFile = "Data/Profiles/current_user.json";

        public static User UserLogin()
        {
            var userInterface = TextManager.Texts?.UserInterface;
            var defaultValues = TextManager.Texts?.DefaultValues;
            
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(userInterface?.LoginTitle ?? "");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(userInterface?.EnterName ?? "Введіть ім'я: ");
            string name = Console.ReadLine()?.Trim() ?? defaultValues?.DefaultUserName ?? "";

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(userInterface?.EnterAge ?? "");
            int age = int.TryParse(Console.ReadLine(), out int result) ? result : (defaultValues?.DefaultAge ?? 18);

            User user = LoadUserFromFile(name);

            if (user == null)
            {
                user = new User(name, age);
                Console.WriteLine(userInterface?.NewProfileCreated ?? "");
            }
            else
            {
                Console.WriteLine(userInterface?.ProfileLoaded ?? "");
            }

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(TextManager.FormatWelcome(user.UserName));
            Console.ResetColor();

            user.StartSession();
            return user;
        }

        public static void ShowUserProgress(User user)
        {
            var progressMessages = TextManager.Texts?.ProgressMessages;
            
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("==========" + progressMessages?.Title ?? "" + "==========");
            Console.WriteLine(TextManager.FormatProgressName(user.UserName));
            Console.WriteLine(TextManager.FormatProgressAge(user.Age));
            Console.WriteLine(TextManager.FormatProgressRank(user.Rank.CurrentRank));
            Console.WriteLine(TextManager.FormatProgressPoints(user.Rank.Points));
            Console.WriteLine(TextManager.FormatProgressStreak(user.BestStreak));
            Console.WriteLine(TextManager.FormatProgressAchievements(user.Achievements.Count));
            

            Console.ResetColor();
        }

        public static bool AskToContinue()
        {
            var continuePrompt = TextManager.Texts?.ContinuePrompt;
            
            Console.WriteLine("\n" + new string('═', 50));
            Console.WriteLine(continuePrompt?.Message ?? "");
            Console.Write(continuePrompt?.Instruction ?? "");

            string input = Console.ReadLine()?.Trim().ToLower() ?? "";
            return input != "no" && input != "ні" && input != "n";
        }

        public static Quiz LoadQuizFromJson(string filePath, int targetTestId)
        {
            var errorMessages = TextManager.Texts?.ErrorMessages;
            
            try
            {
                string fullPath = Path.GetFullPath(filePath);
                if (!File.Exists(fullPath))
                {
                    Console.WriteLine(TextManager.FormatFileNotFound(filePath));
                    return null;
                }

                string jsonString = File.ReadAllText(fullPath);
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var root = JsonSerializer.Deserialize<QuizFileRoot>(jsonString, options);

                if (root?.Tests == null || root.Tests.Count == 0)
                {
                    Console.WriteLine(errorMessages?.JsonEmpty ?? "");
                    return null;
                }

                var selectedTest = root.Tests.FirstOrDefault(t => t.TestId == targetTestId);

                var questionList = selectedTest.Questions
                    .Select(q => (q.Text, q.Options.ToArray(), q.CorrectIndex, q.Explanation))
                    .ToList();

                return new Quiz(questionList);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(TextManager.FormatJsonParseError(ex.Message));
                Console.ResetColor();
                return null;
            }
        }

        public static void ShowLeaderboard(User currentUser)
        {
            var leaderboardMessages = TextManager.Texts?.LeaderboardMessages;
            
            var leaderboard = new List<User>
            {
                new User("Alex", 20) { Rank = new RankSystem { Points = 10, CurrentRank = "Advanced" }, BestStreak = 1 },
                new User("Maria", 22) { Rank = new RankSystem { Points = 50, CurrentRank = "Expert" }, BestStreak = 2 },
                new User("Ivan", 19) { Rank = new RankSystem { Points = 120, CurrentRank = "Beginner" }, BestStreak = 4 },
                new User("Olena", 21) { Rank = new RankSystem { Points = 300, CurrentRank = "Advanced" }, BestStreak = 6 },
            };

            leaderboard.Add(currentUser);

            var sorted = leaderboard.OrderByDescending(u => u.Rank.Points).ToList();

            Console.WriteLine(leaderboardMessages?.Title ?? "\n\n🏆 ===== LEADERBOARD ===== 🏆");
            for (int i = 0; i < sorted.Count; i++)
            {
                var u = sorted[i];
                if (u == currentUser)
                    Console.ForegroundColor = ConsoleColor.Yellow;
                else
                    Console.ForegroundColor = ConsoleColor.White;

                Console.WriteLine(string.Format(leaderboardMessages?.Entry ?? "", 
                    i + 1, u.UserName, u.Rank.Points, u.Rank.CurrentRank, u.BestStreak));
            }
            Console.ResetColor();
            Console.WriteLine(leaderboardMessages?.Footer ?? "============================\n");
        }

        public static User LoadUserFromFile(string name)
        {
            if (!File.Exists(UserFile)) return null;
            try
            {
                string json = File.ReadAllText(UserFile);
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var loaded = JsonSerializer.Deserialize<User>(json, options);
                
                if (loaded != null && loaded.UserName.Equals(name, StringComparison.OrdinalIgnoreCase))
                    return loaded;
            }
            catch { }
            return null;
        }

        public static void SaveUserToFile(User user)
        {
            var errorMessages = TextManager.Texts?.ErrorMessages;
            
            try
            {
                string json = JsonSerializer.Serialize(user, new JsonSerializerOptions
                {
                    WriteIndented = true,
                    ReferenceHandler = ReferenceHandler.IgnoreCycles
                });
                File.WriteAllText(UserFile, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format(errorMessages?.SaveError ?? "", ex.Message));
            }
        }

        public static void ChooseGameMenu()
        {
            var menuOptions = TextManager.Texts?.MenuOptions;
            
            Console.WriteLine($"\n{menuOptions?.ChooseGame ?? ""}");
            Console.WriteLine(menuOptions?.Option1 ?? "");
            Console.WriteLine(menuOptions?.Option2 ?? "");
            Console.WriteLine(menuOptions?.Option0 ?? "");

            Console.Write($"\n{menuOptions?.YourChoice ?? ""}");
        }
    }
}