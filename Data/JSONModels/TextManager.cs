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
            return string.Format(Texts?.UserInterface?.Welcome ?? "", userName);
        }

        public static string FormatProgressName(string name)
        {
            return string.Format(Texts?.ProgressMessages?.Name ?? "", name);
        }

        public static string FormatProgressAge(int age)
        {
            return string.Format(Texts?.ProgressMessages?.Age ?? "", age);
        }

        public static string FormatProgressRank(string rank)
        {
            return string.Format(Texts?.ProgressMessages?.Rank ?? "", rank);
        }

        public static string FormatProgressPoints(int points)
        {
            return string.Format(Texts?.ProgressMessages?.Points ?? "", points);
        }

        public static string FormatProgressStreak(int streak)
        {
            return string.Format(Texts?.ProgressMessages?.BestStreak ?? "", streak);
        }

        public static string FormatProgressAchievements(int count)
        {
            return string.Format(Texts?.ProgressMessages?.Achievements ?? "", count);
        }

        public static string FormatFileNotFound(string fileName)
        {
            return string.Format(Texts?.ErrorMessages?.FileNotFound ?? "", fileName);
        }

        public static string FormatJsonParseError(string error)
        {
            return string.Format(Texts?.ErrorMessages?.JsonParseError ?? "", error);
        }

    }
}