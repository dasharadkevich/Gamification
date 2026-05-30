using System;
using System.IO;
using System.Text.Json;

namespace ProgrammingGame
{
    public static class TextManager
    {
        private static TextResources? _resources;
        private static readonly string ResourcesFile = "text.json";

        static TextManager()
        {
            LoadResources();
        }

        private static void LoadResources()
        {
            try
            {
                if (!File.Exists(ResourcesFile))
                {
                    Console.WriteLine($"Warning: {ResourcesFile} not found. Using default values.");
                    CreateDefaultResources();
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
                    CreateDefaultResources();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading text resources: {ex.Message}");
                CreateDefaultResources();
            }
        }

        private static void CreateDefaultResources()
        {
            _resources = new TextResources
            {
                MenuOptions = new MenuOptions
                {
                    ChooseGame = "=== Оберіть режим ===",
                    Option1 = "1. Пройти Quiz (тест з питань)",
                    Option2 = "2. Пограти в Educational Game",
                    Option0 = "0. Вийти з програми",
                    YourChoice = "Ваш вибір: "
                },
                ErrorMessages = new ErrorMessages
                {
                    InvalidChoice = "Невірний вибір! Спробуйте ще раз.",
                    FileNotFound = "\n[Помилка] Файл {0} не знайдено!",
                    FilePath = "Шлях: {0}",
                    MoveFile = "Перемістіть файл або налаштуйте Copy to Output Directory.",
                    JsonEmpty = "[Помилка] JSON порожній або має невірну структуру.",
                    JsonParseError = "[Помилка] Не вдалося зчитати JSON. Невірний формат синтаксису: {0}",
                    CriticalError = "[Критична помилка]: {0}",
                    SaveError = "Помилка збереження: {0}"
                },
                UserInterface = new UserInterface
                {
                    LoginTitle = "Для початку увійдіть або зареєструйтесь ...",
                    EnterName = "Введіть ім'я: ",
                    EnterAge = "Введіть вік: ",
                    NewProfileCreated = "Новий профіль створено.",
                    ProfileLoaded = "Профіль завантажено з файлу.",
                    Welcome = "Вітаємо, {0}! Вхід успішний.",
                    ThankYou = "\nДякуємо за гру! До зустрічі 👋"
                },
                ProgressMessages = new ProgressMessages
                {
                    Title = "=== ПРОГРЕС ГРАВЦЯ ===",
                    Name = "Ім'я: {0}",
                    Age = "Вік: {0}",
                    Rank = "Ранг: {0}",
                    Points = "Очки: {0}",
                    BestStreak = "Найкраща серія: {0}",
                    Achievements = "Досягнення: {0}"
                },
                ContinuePrompt = new ContinuePrompt
                {
                    Message = "Бажаєте пройти ще один тест?",
                    Instruction = "Напишіть 'no' або 'ні', щоб вийти. Для продовження — просто натисніть Enter: "
                },
                GameDescriptions = new GameDescriptions
                {
                    PuzzleGame = "Склади правильний код з запропонованих варіантів",
                    QuizMode = "--- Режим Quiz ---",
                    GameMode = "--- Режим Educational Game ---"
                },
                EducationalGameOptions = new EducationalGameOptions
                {
                    Title = "1. OOP Theory Game (Сценарії)",
                    Title2 = "2. Code Puzzle Game",
                    Prompt = "Ваш вибір: "
                },
                QuizMessages = new QuizMessages
                {
                    AllTestsCompleted = "Ви вже пройшли всі доступні тести!",
                    LoadFailed = "Не вдалося завантажити тест з JSON\n",
                    QuestionPrefix = "\nПитання: {0}",
                    AnswerPrompt = "\nВаша відповідь (номер): ",
                    Correct = "✓ Правильно!",
                    Incorrect = "✗ Неправильно.",
                    CorrectAnswer = "Правильна відповідь: {0}. {1}",
                    Explanation = "Пояснення: {0}",
                    ScoreInfo = "Бали: {0} | Найкраща серія: {1} | Стрік: {2}\n",
                    StreakMessage = "🔥 Серія: {0}!"
                },
                PuzzleMessages = new PuzzleMessages
                {
                    Start = "\n=== {0} ===",
                    Topic = "\nТема: {0}",
                    Prompt = "Ваша відповідь: ",
                    Skipped = "Завдання пропущено.",
                    Correct = "✅ Правильно!",
                    Incorrect = "❌ Невірно. Правильна відповідь: {0}",
                    Explanation = "💡 Пояснення: {0}",
                    Completed = "\n=== {0} завершено ===",
                    Result = "Результат: {0} балів"
                },
                SimulationMessages = new SimulationMessages
                {
                    Start = "=== СТАРТ ІМІТАЦІЇ ООП ТЕСТУ ===\n",
                    User = "Користувач: {0}",
                    Discipline = "Дисципліна: Основи Об'єктно-Орієнтованого Програмування",
                    Platform = "Платформа: EduQuest OOP Simulator\n",
                    Finish = "=== ФІНІШ ІМІТАЦІЇ ===",
                    CorrectCount = "Правильних відповідей: {0}/{1}",
                    TotalTime = "Загальний час у грі: {0:mm\\:ss}"
                },
                AchievementMessages = new AchievementMessages
                {
                    Title = "--- Отримані нагороди та досягнення ---",
                    Unlock = "🏆 Нове досягнення: {0}!",
                    Award = "🎖 Нагорода: {0}!",
                    NoRewards = "На жаль, цього разу немає нових нагород. Спробуйте покращити серію!"
                },
                LeaderboardMessages = new LeaderboardMessages
                {
                    Title = "\n\n🏆 ===== LEADERBOARD ===== 🏆",
                    Entry = "{0}. {1} | {2} pts | {3} | Streak: {4}",
                    Footer = "============================\n"
                },
                DefaultValues = new DefaultValues
                {
                    DefaultUserName = "Гравець",
                    DefaultAge = 18,
                    DefaultRank = "Новачок",
                    DefaultRankPoints = 0,
                    DefaultAchievementName = "Без назви",
                    DefaultAchievementDescription = "Немає опису",
                    DefaultRewardName = "Невідома нагорода",
                    DefaultRewardType = "Award",
                    DefaultQuestionText = "Питання не вказано",
                    DefaultExplanation = "Пояснення відсутнє"
                },
                RankNames = new RankNames
                {
                    Novice = "Новачок",
                    Junior = "Junior",
                    Middle = "Middle",
                    Senior = "Senior",
                    Architect = "Архітектор"
                },
                GameNames = new GameNames
                {
                    OopTheory = "OOP Theory Game",
                    CodePuzzle = "Code Puzzle Game",
                    Quiz = "Quiz"
                },
                AuthorInfo = new AuthorInfo
                {
                    FullName = "Радкевич Даша Ігорівна",
                    Course = 1,
                    Group = "ІПЗ-11",
                    Variant = "Серйозна гра для вивчення ООП",
                    Version = "1"
                }
            };
        }

        public static TextResources Texts => _resources ?? CreateAndReturnDefault();

        private static TextResources CreateAndReturnDefault()
        {
            CreateDefaultResources();
            return _resources!;
        }
        
        // Helper methods with null-conditional operators
        // public static string FormatWelcome(string userName)
        // {
        //     return string.Format(Texts?.UserInterface?.Welcome ?? "Вітаємо, {0}! Вхід успішний.", userName);
        // }
        
        // public static string FormatProgressName(string name)
        // {
        //     return string.Format(Texts?.ProgressMessages?.Name ?? "Ім'я: {0}", name);
        // }
        
        // public static string FormatProgressAge(int age)
        // {
        //     return string.Format(Texts?.ProgressMessages?.Age ?? "Вік: {0}", age);
        // }
        
        // public static string FormatProgressRank(string rank)
        // {
        //     return string.Format(Texts?.ProgressMessages?.Rank ?? "Ранг: {0}", rank);
        // }
        
        // public static string FormatProgressPoints(int points)
        // {
        //     return string.Format(Texts?.ProgressMessages?.Points ?? "Очки: {0}", points);
        // }
        
        // public static string FormatProgressStreak(int streak)
        // {
        //     return string.Format(Texts?.ProgressMessages?.BestStreak ?? "Найкраща серія: {0}", streak);
        // }
        
        // public static string FormatProgressAchievements(int count)
        // {
        //     return string.Format(Texts?.ProgressMessages?.Achievements ?? "Досягнення: {0}", count);
        // }

        // public static string FormatFileNotFound(string fileName)
        // {
        //     return string.Format(Texts?.ErrorMessages?.FileNotFound ?? "\n[Помилка] Файл {0} не знайдено!", fileName);
        // }

        // public static string FormatFilePath(string path)
        // {
        //     return string.Format(Texts?.ErrorMessages?.FilePath ?? "Шлях: {0}", path);
        // }

        // public static string FormatJsonParseError(string error)
        // {
        //     return string.Format(Texts?.ErrorMessages?.JsonParseError ?? "[Помилка] Не вдалося зчитати JSON. Невірний формат синтаксису: {0}", error);
        // }

        // public static string FormatCriticalError(string error)
        // {
        //     return string.Format(Texts?.ErrorMessages?.CriticalError ?? "[Критична помилка]: {0}", error);
        // }
    }
}