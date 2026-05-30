using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

using ProgrammingGame.Data.JSONModels;

namespace ProgrammingGame
{
    class Program
    {
        private static readonly HashSet<int> CompletedTestIds = new HashSet<int>(); 
    
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            TextManager.DisplayAuthorIntroduction(); 

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
                        Console.WriteLine(TextManager.Texts?.ErrorMessages?.InvalidChoice ?? "Невірний вибір! Спробуйте ще раз.");
                        continue;
                }

                if (continuePlaying)
                {
                    ProgramHelpers.ShowUserProgress(user);
                    ProgramHelpers.ShowLeaderboard(user);
                    continuePlaying = ProgramHelpers.AskToContinue();
                }
            }

            user.EndSession();
            ProgramHelpers.SaveUserToFile(user);

            Console.WriteLine(TextManager.Texts?.UserInterface?.ThankYou ?? "\nДякуємо за гру! До зустрічі 👋");
            Console.ReadKey();
        }

        static void PlayQuizMode(User user)
        {
            Console.WriteLine(TextManager.Texts?.GameDescriptions?.QuizMode ?? "\n--- Режим Quiz ---");

            int testId = GetNextUnplayedTestId();
            if (testId == -1)
            {
                Console.WriteLine(TextManager.Texts?.QuizMessages?.AllTestsCompleted ?? "Ви вже пройшли всі доступні тести!");
                return;
            }

            Quiz quiz = ProgramHelpers.LoadQuizFromJson("Data/JSON/tests.json", testId);

            if (quiz == null || quiz.IsEmpty())
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(TextManager.Texts?.QuizMessages?.LoadFailed ?? "Не вдалося завантажити тест з JSON\n");
                Console.ResetColor();
                return;
            }

            var simulation = new Simulation(user, quiz);
            simulation.Run();

            CompletedTestIds.Add(testId);
        }

        static void PlayGameMode(User user)
        {
            var gameNames = TextManager.Texts?.GameNames;
            var educationalOptions = TextManager.Texts?.EducationalGameOptions;
            
            Console.WriteLine(TextManager.Texts?.GameDescriptions?.GameMode ?? "\n--- Режим Educational Game ---");
            Console.WriteLine(educationalOptions?.Title ?? "1. OOP Theory Game (Сценарії)");
            Console.WriteLine(educationalOptions?.Title2 ?? "2. Code Puzzle Game");
            Console.Write($"\n{educationalOptions?.Prompt ?? "Ваш вибір: "}");

            string gameChoice = Console.ReadLine()?.Trim() ?? "";

            IGame selectedGame = gameChoice switch
            {
                "1" => new PuzzleGame(user, 1, gameNames?.OopTheory ?? "OOP Theory Game"),
                "2" => new PuzzleGame(user, 0, gameNames?.CodePuzzle ?? "Code Puzzle Game"),
                _ => new PuzzleGame(user, 0, gameNames?.CodePuzzle ?? "Code Puzzle Game")
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
    }
}