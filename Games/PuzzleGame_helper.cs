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
        private bool IsValidateFile(string fileName)
        {
            var errorMessages = TextManager.Texts?.ErrorMessages;
            
            if (File.Exists(fileName))
                return true;

            Console.WriteLine(string.Format(errorMessages?.FileNotFound ?? "", fileName));
            Console.WriteLine(string.Format(errorMessages?.FilePath ?? "", Path.GetFullPath(fileName)));
            Console.WriteLine(errorMessages?.MoveFile ?? "");

            return false;
        }

        private JsonRoot? LoadTests()
        {
            string jsonString = File.ReadAllText(_fileName);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            return JsonSerializer.Deserialize<JsonRoot>(jsonString, options);
        }

        private int RunQuestions(TestItem activeTest)
        {
            int totalScore = 0;

            int pointsPerQuestion =
                activeTest.Questions.Count > 0
                ? 100 / activeTest.Questions.Count
                : 20;

            foreach (var question in activeTest.Questions)
            {
                bool isCorrect = AskQuestion(question);

                if (isCorrect)
                    totalScore += pointsPerQuestion;
            }

            return totalScore;
        }

        private bool AskQuestion(QuestionItem q)
        {
            var puzzleMessages = TextManager.Texts?.PuzzleMessages;
            
            Console.WriteLine($"\n> {q.Text}");

            DisplayOptions(q);

            Console.Write(puzzleMessages?.Prompt ?? "");

            string input = Console.ReadLine()?.Trim().ToUpper() ?? "";

            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine(puzzleMessages?.Skipped ?? "");
                return false;
            }

            int choiceIndex = input[0] - 'A';

            bool isCorrect = choiceIndex == q.CorrectIndex;

            ShowResult(isCorrect, q);

            return isCorrect;
        }

        private void DisplayOptions(QuestionItem q)
        {
            for (int i = 0; i < q.Options.Count; i++)
            {
                Console.WriteLine($"  {(char)('A' + i)}) {q.Options[i]}");
            }
        }

        private void ShowResult(bool isCorrect, QuestionItem q)
        {
            var puzzleMessages = TextManager.Texts?.PuzzleMessages;
            
            if (isCorrect)
            {
                Console.WriteLine(puzzleMessages?.Correct ?? "");
            }
            else
            {
                char correctLetter = (char)('A' + q.CorrectIndex);
                Console.WriteLine(string.Format(puzzleMessages?.Incorrect ?? "", correctLetter));
            }

            if (!string.IsNullOrWhiteSpace(q.Explanation))
            {
                Console.WriteLine(string.Format(puzzleMessages?.Explanation ?? "", q.Explanation));
            }
        }
    }
}