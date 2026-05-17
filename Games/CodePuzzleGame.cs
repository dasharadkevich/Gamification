using System;

namespace ProgrammingGame
{
    public class CodePuzzleGame : IGame
    {
        public string GameName => "Code Puzzle";
        public string Description => "Склади правильний код з запропонованих варіантів";

        public bool IsCompleted { get; private set; }
        public int Score { get; private set; }

        private readonly User _user;

        public CodePuzzleGame(User user)
        {
            _user = user ?? new User();
        }

        public void Start(User user) 
        {
            Console.WriteLine($"\n=== {GameName} ===");
            Console.WriteLine(Description);
        }

        public void Run()
        {
            Start(_user);
            Score = RunPuzzles();
            IsCompleted = true;
            End();
        }

        private int RunPuzzles()
        {
            int score = 0;

            // Пазл 1
            Console.WriteLine("\n1. Як правильно створити екземпляр класу?");
            Console.WriteLine("A) User u = new User");
            Console.WriteLine("B) User u = new User();");
            Console.WriteLine("C) User u = User();");
            if (Console.ReadLine()?.Trim().ToUpper() == "B") score += 35;

            // Пазл 2
            Console.WriteLine("\n2. Як правильно оголосити властивість тільки для читання?");
            Console.WriteLine("A) public int Age { get; set; }");
            Console.WriteLine("B) public int Age { get; }");
            Console.WriteLine("C) public int Age { get; private set; }");
            if (Console.ReadLine()?.Trim().ToUpper() == "C") score += 40;

            _user.AddPoints(score);
            return score;
        }

        public void End()
        {
            Console.WriteLine($"\n=== {GameName} завершено ===");
            Console.WriteLine($"Результат: {Score} балів");
        }
    }
}