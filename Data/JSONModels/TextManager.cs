using System;
using System.IO;
using System.Text.Json;

namespace ProgrammingGame.Data.JSONModels
{
    public static class TextManager
    {
        private static TextResources? _resources;
        private static readonly string ResourcesFile = "Data/Config/text.json";


        static TextManager()
        {
            LoadResources();
        }

        public static void DisplayAuthorIntroduction()
        {
            var author = Texts?.AuthorInfo;
            var authorMessages = Texts?.AuthorMessages;

            if (author != null && authorMessages != null)
            {
                Console.WriteLine(string.Format(authorMessages.StudentNameLabel, author.FullName));
                Console.WriteLine(string.Format(authorMessages.CourseGroupLabel, author.Course, author.Group));
                Console.WriteLine(string.Format(authorMessages.VariantLabel, author.Variant));
                Console.WriteLine(string.Format(authorMessages.VersionLabel, author.Version));
            }
            else
            {
                // Fallback hardcoded values (should not happen if JSON loads correctly)
                Console.WriteLine("ПІБ студента: Радкевич Даша Ігорівна");
                Console.WriteLine("Курс: 1   Група: ІПЗ-11");
                Console.WriteLine("Варіант завдання: Серйозна гра для вивчення ООП");
                Console.WriteLine("Версія 1\n");
            }
        }
        private static void LoadResources()
        {
            try
            {
                if (!File.Exists(ResourcesFile))
                {
                    Console.WriteLine($"Warning: {ResourcesFile} not found. Using default values.");
                    return;
                }

                string jsonString = File.ReadAllText(ResourcesFile);
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                _resources = JsonSerializer.Deserialize<TextResources>(jsonString, options);

                if (_resources == null)
                {
                    Console.WriteLine("Warning: Failed to load resources. Using defaults.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading text resources: {ex.Message}");
    
            }
        }

        public static TextResources Texts => _resources;


        public static string FormatWelcome(string userName)
        {
            return string.Format(Texts?.UserInterface?.Welcome ?? "Вітаємо, {0}! Вхід успішний.", userName);
        }

        public static string FormatProgressName(string name)
        {
            return string.Format(Texts?.ProgressMessages?.Name ?? "Ім'я: {0}", name);
        }

        public static string FormatProgressAge(int age)
        {
            return string.Format(Texts?.ProgressMessages?.Age ?? "Вік: {0}", age);
        }

        public static string FormatProgressRank(string rank)
        {
            return string.Format(Texts?.ProgressMessages?.Rank ?? "Ранг: {0}", rank);
        }

        public static string FormatProgressPoints(int points)
        {
            return string.Format(Texts?.ProgressMessages?.Points ?? "Очки: {0}", points);
        }

        public static string FormatProgressStreak(int streak)
        {
            return string.Format(Texts?.ProgressMessages?.BestStreak ?? "Найкраща серія: {0}", streak);
        }

        public static string FormatProgressAchievements(int count)
        {
            return string.Format(Texts?.ProgressMessages?.Achievements ?? "Досягнення: {0}", count);
        }

        public static string FormatFileNotFound(string fileName)
        {
            return string.Format(Texts?.ErrorMessages?.FileNotFound ?? "\n[Помилка] Файл {0} не знайдено!", fileName);
        }

        public static string FormatJsonParseError(string error)
        {
            return string.Format(Texts?.ErrorMessages?.JsonParseError ?? "[Помилка] Не вдалося зчитати JSON. Невірний формат синтаксису: {0}", error);
        }

    }
}