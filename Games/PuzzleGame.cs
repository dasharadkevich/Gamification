using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.Json;

using ProgrammingGame.Data.JSONModels;

namespace ProgrammingGame
{
    public partial class PuzzleGame : IGame
    {
        public string Description => TextManager.Texts?.GameDescriptions?.PuzzleGame ?? "";

        public readonly string _fileName = "Data/JSON/games.json";
        
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
            var puzzleMessages = TextManager.Texts?.PuzzleMessages;
            Console.WriteLine(string.Format(puzzleMessages?.Start ?? "\n=== {0} ===", GameName));
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
            var errorMessages = TextManager.Texts?.ErrorMessages;

            if (!IsValidateFile(_fileName)) return 0;

            try
            {
                JsonRoot? root = LoadTests();

                if (root?.Tests == null || root.Tests.Count == 0)
                {
                    Console.WriteLine(errorMessages?.JsonEmpty ?? "");
                    return 0;
                }

                var activeTest = root.Tests[gameCode];
                
                var puzzleMessages = TextManager.Texts?.PuzzleMessages;
                Console.WriteLine(string.Format(puzzleMessages?.Topic ?? "\nТема: {0}", activeTest.Title));

                int totalScoreCount = RunQuestions(activeTest);

                _user.AddPoints(totalScore);

                return totalScore;
            }
            catch (JsonException ex)
            {
                Console.WriteLine(string.Format(errorMessages?.JsonParseError ?? "", ex.Message));
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format(errorMessages?.CriticalError ?? "", ex.Message));
            }

            return totalScore;
        }

        public void End()
        {
            var puzzleMessages = TextManager.Texts?.PuzzleMessages;
            Console.WriteLine(string.Format(puzzleMessages?.Completed ?? ""));
            Console.WriteLine(string.Format(puzzleMessages?.Result ?? "", Score));
        }
    }
}