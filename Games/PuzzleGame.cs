using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.Json;


namespace ProgrammingGame
{
    public partial class PuzzleGame : IGame
    {
        public string Description => "Склади правильний код з запропонованих варіантів";

        public readonly string _fileName = "games.json";
        public string GameName; 

        public int gameCode;


        public bool IsCompleted { get; private set; }
        public int Score { get; private set; }

        private readonly User _user;

        public PuzzleGame(User user, int gameCode, string gameName)
        {
            _user = user ?? new User();
            this.gameCode = gameCode;
            GameName = gameName; 
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
            int totalScore = 0;


            if (!IsValidateFile(_fileName)) return 0;

            try
            {
                JsonRoot? root = LoadTests();

                if (root?.Tests == null || root.Tests.Count == 0)
                {
                    Console.WriteLine("[Помилка] JSON порожній або має невірну структуру.");
                    return 0;
                }

                var activeTest = root.Tests[gameCode];

                Console.WriteLine($"\nТема: {activeTest.Title}");

                int totalScoreCount = RunQuestions(activeTest);

                _user.AddPoints(totalScore);

                return totalScore;
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"[Помилка] Не вдалося зчитати JSON. Невірний формат синтаксису: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Критична помилка]: {ex.Message}");
            }

            return totalScore;
        }

        public void End()
        {
            Console.WriteLine($"\n=== {GameName} завершено ===");
            Console.WriteLine($"Результат: {Score} балів");
        }

    }
}