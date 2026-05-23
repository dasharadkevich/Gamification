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
    
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            ProgramHelpers.AuthorIntroduction();

            var user = ProgramHelpers.UserLogin();

            bool continuePlaying = true;

            while (continuePlaying)
            {
                ProgramHelpers.ChooseGameMenu(); 

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
                    ProgramHelpers.ShowUserProgress(user);
                    ProgramHelpers.ShowLeaderboard(user);
                    continuePlaying = AskToContinue();
                }
            }

            user.EndSession();
            ProgramHelpers.SaveUserToFile(user);

            Console.WriteLine("\nДякуємо за гру! До зустрічі 👋");
            Console.ReadKey();
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

            Quiz quiz = ProgramHelpers.LoadQuizFromJson("tests.json", testId);

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
            ProgramHelpers.SaveUserToFile(user);
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
    }
}