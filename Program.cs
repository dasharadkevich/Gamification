using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ProgrammingGame
{
    class Program
    {
        private static readonly HashSet<int> CompletedTestIds = new HashSet<int>();
        private static readonly string UserFile = "current_user.json";

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            AuthorIntroduction();

            var user = UserLogin();

            bool continuePlaying = true;

            while (continuePlaying)
            {
                Console.WriteLine("\n=== Оберіть режим ===");
                Console.WriteLine("1. Пройти Quiz (тест з питань)");
                Console.WriteLine("2. Пограти в Educational Game");
                Console.WriteLine("0. Вийти з програми");

                Console.Write("\nВаш вибір: ");
                string choice = Console.ReadLine()?.Trim() ?? "";

                switch (choice)
                {
                    case "1":
                        PlayQuizMode(user);
                        break;

                    case "2":
                        PlayGameMode(user);
                        break;

                    case "0":
                        continuePlaying = false;
                        break;

                    default:
                        Console.WriteLine("Невірний вибір! Спробуйте ще раз.");
                        continue;
                }

                if (continuePlaying)
                {
                    ShowUserProgress(user);
                    ShowLeaderboard(user);
                    continuePlaying = AskToContinue();
                }
            }

            user.EndSession();
            SaveUserToFile(user);

            Console.WriteLine("\nДякуємо за гру! До зустрічі 👋");
            Console.ReadKey();
        }

        static void ShowUserProgress(User user)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\n=== ПРОГРЕС ГРАВЦЯ ===");
            Console.WriteLine($"Ім'я: {user.UserName}");
            Console.WriteLine($"Вік: {user.Age}");
            Console.WriteLine($"Ранг: {user.Rank.CurrentRank}");
            Console.WriteLine($"Очки: {user.Rank.Points}");
            Console.WriteLine($"Найкраща серія: {user.BestStreak}");
            Console.WriteLine($"Досягнення: {user.Achievements.Count}");
            Console.ResetColor();
        }
        static void PlayQuizMode(User user)
        {
            Console.WriteLine("\n--- Режим Quiz ---");

            int testId = GetNextUnplayedTestId();
            if (testId == -1)
            {
                Console.WriteLine("Ви вже пройшли всі доступні тести!");
                return;
            }

            Quiz quiz = LoadQuizFromJson("tests.json", testId);

            if (quiz == null || quiz.IsEmpty())
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Не вдалося завантажити тест з JSON\n");
                Console.ResetColor();
            }

            var simulation = new Simulation(user, quiz);
            simulation.Run();

            CompletedTestIds.Add(testId);
        }


        static void PlayGameMode(User user)
        {
            Console.WriteLine("\n--- Режим Educational Game ---");
            Console.WriteLine("1. OOP Theory Game (Сценарії)");
            Console.WriteLine("2. Code Puzzle Game");
            Console.Write("\nВаш вибір: ");

            string gameChoice = Console.ReadLine()?.Trim() ?? "";


            IGame selectedGame = gameChoice switch
            {
                "1" => new PuzzleGame(user, 1, "OOP Theory Game"),
                "2" => new PuzzleGame(user, 0, "Code Puzzle Game"),
                _ => new PuzzleGame(user, 0, "Code Puzzle Game")
            };

            selectedGame.Run();

            user.EndSession();
            SaveUserToFile(user);
        }

        static int GetNextUnplayedTestId()
        {
            int[] availableTests = { 1, 2, 3, 4, 5 };

            foreach (int id in availableTests)
            {
                if (!CompletedTestIds.Contains(id))
                    return id;
            }

            return -1;
        }


        static bool AskToContinue()
        {
            Console.WriteLine("\n" + new string('═', 50));
            Console.WriteLine("Бажаєте пройти ще один тест?");
            Console.Write("Напишіть 'no' або 'ні', щоб вийти. Для продовження — просто натисніть Enter: ");

            string input = Console.ReadLine()?.Trim().ToLower() ?? "";

            return input != "no" && input != "ні" && input != "n";
        }


        static void AuthorIntroduction()
        {
            Console.WriteLine("ПІБ студента: Радкевич Даша Ігорівна");
            Console.WriteLine("Курс: 1   Група: ІПЗ-11");
            Console.WriteLine("Варіант завдання: Серйозна гра для вивчення ООП");
            Console.WriteLine("Версія 1\n");
        }

        static User UserLogin()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Для початку увійдіть або зареєструйтесь ...");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Введіть ім'я: ");
            string name = Console.ReadLine()?.Trim() ?? "Гравець";

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Введіть вік: ");
            int age = int.TryParse(Console.ReadLine(), out int result) ? result : 18;

            User user = LoadUserFromFile(name);

            if (user == null)
            {
                user = new User(name, age);
                Console.WriteLine("Новий профіль створено.");
            }
            else
            {
                Console.WriteLine("Профіль завантажено з файлу.");
            }

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"\nВітаємо, {user.UserName}! Вхід успішний.");
            Console.ResetColor();

            user.StartSession();
            return user;
        }

        static Quiz LoadQuizFromJson(string filePath, int targetTestId)
        {
            try
            {
                string fullPath = Path.GetFullPath(filePath);

                if (!File.Exists(fullPath))
                {
                    Console.WriteLine($"Файл не знайдено: {fullPath}");
                    return null;
                }

                string jsonString = File.ReadAllText(fullPath);

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var root = JsonSerializer.Deserialize<QuizFileRoot>(jsonString, options);

                if (root?.Tests == null || root.Tests.Count == 0)
                {
                    Console.WriteLine("У JSON-файлі немає тестів.");
                    return null;
                }

                var selectedTest = root.Tests.FirstOrDefault(t => t.TestId == targetTestId);

                if (selectedTest == null)
                {
                    Console.WriteLine($"Тест з ID {targetTestId} не знайдено.");
                    return null;
                }


                var questionList = selectedTest.Questions
                    .Select(q => (q.Text, q.Options.ToArray(), q.CorrectIndex, q.Explanation))
                    .ToList();

                return new Quiz(questionList);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Помилка читання JSON: {ex.Message}");
                Console.ResetColor();
                return null;
            }
        }


        static void ShowLeaderboard(User currentUser)
        {
            // Pre-created users (fake data)
            var leaderboard = new List<User>
    {
        new User("Alex", 20) { Rank = new RankSystem { Points = 10, CurrentRank = "Advanced" }, BestStreak = 1 },
        new User("Maria", 22) { Rank = new RankSystem { Points = 50, CurrentRank = "Expert" }, BestStreak = 2 },
        new User("Ivan", 19) { Rank = new RankSystem { Points = 120, CurrentRank = "Beginner" }, BestStreak = 4 },
        new User("Olena", 21) { Rank = new RankSystem { Points = 300, CurrentRank = "Advanced" }, BestStreak = 6 },
    };

            // Add current user
            leaderboard.Add(currentUser);

            // Sort by points descending
            var sorted = leaderboard
                .OrderByDescending(u => u.Rank.Points)
                .ToList();

            Console.WriteLine("\n\n🏆 ===== LEADERBOARD ===== 🏆");

            for (int i = 0; i < sorted.Count; i++)
            {
                var u = sorted[i];

                bool isCurrent = u == currentUser;

                if (isCurrent)
                    Console.ForegroundColor = ConsoleColor.Yellow;
                else
                    Console.ForegroundColor = ConsoleColor.White;

                Console.WriteLine(
                    $"{i + 1}. {u.UserName} | {u.Rank.Points} pts | {u.Rank.CurrentRank} | Streak: {u.BestStreak}"
                );
            }

            Console.ResetColor();
            Console.WriteLine("============================\n");
        }

        static User LoadUserFromFile(string name)
        {
            if (!File.Exists(UserFile)) return null;

            try
            {
                string json = File.ReadAllText(UserFile);
                var loaded = JsonSerializer.Deserialize<User>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                if (loaded != null && loaded.UserName.Equals(name, StringComparison.OrdinalIgnoreCase))
                    return loaded;
            }
            catch { }
            return null;
        }

        static void SaveUserToFile(User user)
        {
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
                Console.WriteLine($"Помилка збереження: {ex.Message}");
            }
        }
    }
}

