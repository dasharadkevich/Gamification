using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.Json;


namespace ProgrammingGame
{
    public partial class PuzzleGame : IGame
    {
        private bool IsValidateFile(string fileName)
        {
            if (File.Exists(fileName))
                return true;

            Console.WriteLine($"\n[Помилка] Файл {fileName} не знайдено!");
            Console.WriteLine($"Шлях: {Path.GetFullPath(fileName)}");
            Console.WriteLine("Перемістіть файл або налаштуйте Copy to Output Directory.");

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
            Console.WriteLine($"\n> {q.Text}");

            DisplayOptions(q);

            Console.Write("Ваша відповідь: ");

            string input = Console.ReadLine()?.Trim().ToUpper() ?? "";

            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Завдання пропущено.");
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
            if (isCorrect)
            {
                Console.WriteLine("✅ Правильно!");
            }
            else
            {
                char correctLetter = (char)('A' + q.CorrectIndex);

                Console.WriteLine($"❌ Невірно. Правильна відповідь: {correctLetter}");
            }

            if (!string.IsNullOrWhiteSpace(q.Explanation))
            {
                Console.WriteLine($"💡 Пояснення: {q.Explanation}");
            }
        }
    }

} 