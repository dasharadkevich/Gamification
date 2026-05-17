using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace ProgrammingGame
{
    class Program
    {
        private static readonly HashSet<int> CompletedTestIds = new HashSet<int>();
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            AuthorIntroduction();

            Console.WriteLine("Демонстрація роботи унарних операторів");
            DemonstrateOperators();

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
                    continuePlaying = AskToContinue();
                }
            }

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
                Console.WriteLine("Не вдалося завантажити тест з JSON. Використовуємо вбудовані питання.\n");
                Console.ResetColor();
                quiz = CreateDefaultQuiz();
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
                "1" => new OOPTheoryGame(user),
                "2" => new CodePuzzleGame(user),
                _ => new OOPTheoryGame(user)
            };

            selectedGame.Run();
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
            Console.WriteLine("Для початку зареєструйтесь будь ласка ...");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Введіть ім'я: ");

            Console.ForegroundColor = ConsoleColor.White;
            string name = Console.ReadLine() ?? "";

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Введіть вік: ");

            Console.ForegroundColor = ConsoleColor.White;
            int age = int.TryParse(Console.ReadLine(), out int result) ? result : 0;

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"\nВітаємо, {name}! Реєстрація успішна.");

            Console.ResetColor();

            var user = new User(name, age);

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

        static void DemonstrateOperators()
        {
            Console.WriteLine("\n=== ДЕМОНСТРАЦІЯ ПЕРЕВАНТАЖЕНИХ ОПЕРАТОРІВ ===\n");

            var user1 = new User("Даша", 19);
            var user2 = new User("Юлія", 20);

            user1 += 100;

            Console.WriteLine($"User1: {user1}");
            Console.WriteLine($"User2: {user2}");

            Console.WriteLine($"user1 > user2 ? {(user1 > user2)}");
            Console.WriteLine($"user1 == user2 ? {(user1 == user2)}");

            // RankSystem
            var rank = new RankSystem(150);
            rank += 200;
            Console.WriteLine($"Rank після операцій: {rank.Points} очок");
        }


        static Quiz CreateDefaultQuiz()
        {
            var defaultQuestions = new List<(string Text, string[] Options, int CorrectIndex, string Explanation)>
    {
        (
            "Що таке інкапсуляція в ООП?",
            new[]
            {
                "Приховування деталей реалізації та захист даних",
                "Можливість об'єкта набувати різних форм",
                "Створення ієрархії класів",
                "Виділення тільки суттєвих характеристик"
            },
            0,
            "Інкапсуляція — це приховування внутрішньої реалізації класу та захист даних."
        ),
        (
            "Що таке поліморфізм?",
            new[]
            {
                "Приховування даних від зовнішнього доступу",
                "Можливість одного об'єкта мати кілька форм",
                "Успадкування властивостей від батьківського класу",
                "Створення нового класу на основі існуючого"
            },
            1,
            "Поліморфізм дозволяє об'єктам різних класів використовувати один інтерфейс."
        ),
        (
            "Який принцип ООП відповідає за створення ієрархії класів?",
            new[]
            {
                "Інкапсуляція",
                "Абстракція",
                "Спадкування",
                "Поліморфізм"
            },
            2,
            "Спадкування (Inheritance) дозволяє дочірньому класу успадковувати властивості та методи батьківського."
        ),
        (
            "Що таке абстракція в ООП?",
            new[]
            {
                "Приховування деталей реалізації",
                "Виділення тільки суттєвих характеристик об'єкта",
                "Можливість методу мати кілька реалізацій",
                "Створення копії об'єкта"
            },
            1,
            "Абстракція — це приховування складності та показ лише необхідної інформації."
        ),
        (
            "Який модифікатор доступу використовується за замовчуванням для членів класу в C#?",
            new[]
            {
                "public",
                "private",
                "protected",
                "internal"
            },
            1,
            "У C# члени класу (поля, методи) за замовчуванням мають доступ private."
        ),
        (
            "Для чого використовується ключове слово 'virtual'?",
            new[]
            {
                "Для створення абстрактного методу",
                "Для дозволу перевизначення методу в похідному класі",
                "Для статичного зв'язування",
                "Для приховування методу"
            },
            1,
            "virtual дозволяє перевизначати метод у дочірніх класах за допомогою override."
        )
    };

            Console.WriteLine($"Створено тест з {defaultQuestions.Count} питань (дефолтний).");
            return new Quiz(defaultQuestions);
        }
    }
}

