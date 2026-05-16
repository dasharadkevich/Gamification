using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace ProgrammingGame
{
    class Program
    {
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
                Quiz quiz = LoadQuizFromJson("tests.json", targetTestId: GetNextTestId());

                if (quiz == null || quiz.IsEmpty())
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Не вдалося завантажити тест з JSON. Використовуємо вбудовані питання.\n");
                    Console.ResetColor();

                    quiz = CreateDefaultQuiz();
                }

                var simulation = new Simulation(user, quiz);
                simulation.Run();

                continuePlaying = AskToContinue();
            }

            Console.WriteLine("\nДякуємо за гру! До зустрічі 👋");
            Console.ReadKey();
        }

        private static int currentTestId = 1;

        static int GetNextTestId()
        {
            return currentTestId++;
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


                // Конвертуємо питання у формат для Quiz
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

