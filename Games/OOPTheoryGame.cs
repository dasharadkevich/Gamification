using System;
using System.Collections.Generic;
using System.Threading;

namespace ProgrammingGame
{
    public class OOPTheoryGame : IGame
    {
        public string GameName => "ООП Теорія та Сценарії";
        public string Description => "Інтерактивне вивчення принципів ООП через реальні сценарії та задачі";

        public bool IsCompleted { get; private set; }
        public int Score { get; private set; }

        private readonly User _user;
        private int _currentScore = 0;

        public OOPTheoryGame(User user)
        {
            _user = user ?? new User();
        }

        public void Start(User user)
        {
            Console.WriteLine($"\n=== {GameName} ===");
            Console.WriteLine(Description);
            Console.WriteLine("Відповідайте на сценарії, обираючи найкраще рішення.\n");
        }

        public void Run()
        {
            Start(_user);
            PlayTheoryScenarios();
            Score = _currentScore;
            IsCompleted = true;
            End();
        }

        private void PlayTheoryScenarios()
        {
            var scenarios = new List<(string Question, string[] Options, int Correct, string Explanation)>
            {
                (
                    "Ви створюєте клас BankAccount. Як найкраще захистити поле balance?",
                    new[] { "Зробити public", "Зробити private + методи Deposit/Withdraw", 
                           "Зробити protected", "Залишити без модифікатора" },
                    1,
                    "Інкапсуляція: приховуємо дані та надаємо контрольований доступ."
                ),
                (
                    "Вам потрібно створити кілька типів працівників (Manager, Developer, Tester). Який підхід кращий?",
                    new[] { "Окремі класи без спільного батька", "Використовувати успадкування від Employee", 
                           "Використовувати тільки інтерфейси", "Створити один великий клас" },
                    1,
                    "Спадкування дозволяє перевикористовувати спільний код."
                ),
                (
                    "Коли краще використовувати абстрактний клас, а не інтерфейс?",
                    new[] { "Коли потрібна реалізація методів за замовчуванням", 
                           "Коли потрібна тільки сигнатура методів", 
                           "Коли потрібна множинна реалізація", "Ніколи" },
                    0,
                    "Абстрактний клас дозволяє часткову реалізацію."
                )
            };

            foreach (var (question, options, correct, explanation) in scenarios)
            {
                Console.WriteLine($"\nСценарій: {question}");
                for (int i = 0; i < options.Length; i++)
                {
                    Console.WriteLine($"{i + 1}. {options[i]}");
                }

                Console.Write("\nВаш вибір: ");
                int.TryParse(Console.ReadLine(), out int answer);
                answer--;

                if (answer == correct)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("✓ Правильно!");
                    _currentScore += 30;
                    _user.AddPoints(30);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"✗ Неправильно. Правильна відповідь: {correct + 1}");
                }
                Console.ResetColor();
                Console.WriteLine($"Пояснення: {explanation}\n");

                Thread.Sleep(1500);
            }
        }

        public void End()
        {
            Console.WriteLine($"\n=== {GameName} завершено ===");
            Console.WriteLine($"Набрано балів: {Score}");
            Console.WriteLine($"Ваш поточний ранг: {_user.Rank.CurrentRank}");
        }
    }
}