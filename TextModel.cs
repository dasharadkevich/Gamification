using System.Text.Json.Serialization;

namespace ProgrammingGame
{
    public class TextResources
    {
        public AuthorInfo? AuthorInfo { get; set; }
        public GameDescriptions? GameDescriptions { get; set; }
        public MenuOptions? MenuOptions { get; set; }
        public EducationalGameOptions? EducationalGameOptions { get; set; }
        public UserInterface? UserInterface { get; set; }
        public ProgressMessages? ProgressMessages { get; set; }
        public ContinuePrompt? ContinuePrompt { get; set; }
        public QuizMessages? QuizMessages { get; set; }
        public PuzzleMessages? PuzzleMessages { get; set; }
        public ErrorMessages? ErrorMessages { get; set; }
        public SimulationMessages? SimulationMessages { get; set; }
        public AchievementMessages? AchievementMessages { get; set; }
        public LeaderboardMessages? LeaderboardMessages { get; set; }
        public DefaultValues? DefaultValues { get; set; }
        public RankNames? RankNames { get; set; }
        public GameNames? GameNames { get; set; }
    }

    public class AuthorInfo
    {
        public string FullName { get; set; } = string.Empty;
        public int Course { get; set; }
        public string Group { get; set; } = string.Empty;
        public string Variant { get; set; } = string.Empty;
        public string Version { get; set; } = string.Empty;
    }

    public class GameDescriptions
    {
        public string PuzzleGame { get; set; } = string.Empty;
        public string QuizMode { get; set; } = string.Empty;
        public string GameMode { get; set; } = string.Empty;
    }

    public class MenuOptions
    {
        public string ChooseGame { get; set; } = string.Empty;
        public string Option1 { get; set; } = string.Empty;
        public string Option2 { get; set; } = string.Empty;
        public string Option0 { get; set; } = string.Empty;
        public string YourChoice { get; set; } = string.Empty;
    }

    public class EducationalGameOptions
    {
        public string Title { get; set; } = string.Empty;
        public string Title2 { get; set; } = string.Empty;
        public string Prompt { get; set; } = string.Empty;
    }

    public class UserInterface
    {
        public string LoginTitle { get; set; } = string.Empty;
        public string EnterName { get; set; } = string.Empty;
        public string EnterAge { get; set; } = string.Empty;
        public string NewProfileCreated { get; set; } = string.Empty;
        public string ProfileLoaded { get; set; } = string.Empty;
        public string Welcome { get; set; } = string.Empty;
        public string ThankYou { get; set; } = string.Empty;
    }

    public class ProgressMessages
    {
        public string Title { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Age { get; set; } = string.Empty;
        public string Rank { get; set; } = string.Empty;
        public string Points { get; set; } = string.Empty;
        public string BestStreak { get; set; } = string.Empty;
        public string Achievements { get; set; } = string.Empty;
    }

    public class ContinuePrompt
    {
        public string Message { get; set; } = string.Empty;
        public string Instruction { get; set; } = string.Empty;
    }

    public class QuizMessages
    {
        public string AllTestsCompleted { get; set; } = string.Empty;
        public string LoadFailed { get; set; } = string.Empty;
        public string QuestionPrefix { get; set; } = string.Empty;
        public string AnswerPrompt { get; set; } = string.Empty;
        public string Correct { get; set; } = string.Empty;
        public string Incorrect { get; set; } = string.Empty;
        public string CorrectAnswer { get; set; } = string.Empty;
        public string Explanation { get; set; } = string.Empty;
        public string ScoreInfo { get; set; } = string.Empty;
        public string StreakMessage { get; set; } = string.Empty;
    }

    public class PuzzleMessages
    {
        public string Start { get; set; } = string.Empty;
        public string Topic { get; set; } = string.Empty;
        public string Prompt { get; set; } = string.Empty;
        public string Skipped { get; set; } = string.Empty;
        public string Correct { get; set; } = string.Empty;
        public string Incorrect { get; set; } = string.Empty;
        public string Explanation { get; set; } = string.Empty;
        public string Completed { get; set; } = string.Empty;
        public string Result { get; set; } = string.Empty;
    }

    public class ErrorMessages
    {
        public string FileNotFound { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public string MoveFile { get; set; } = string.Empty;
        public string JsonEmpty { get; set; } = string.Empty;
        public string JsonParseError { get; set; } = string.Empty;
        public string CriticalError { get; set; } = string.Empty;
        public string InvalidChoice { get; set; } = string.Empty;
        public string SaveError { get; set; } = string.Empty;
    }

    public class SimulationMessages
    {
        public string Start { get; set; } = string.Empty;
        public string User { get; set; } = string.Empty;
        public string Discipline { get; set; } = string.Empty;
        public string Platform { get; set; } = string.Empty;
        public string Finish { get; set; } = string.Empty;
        public string CorrectCount { get; set; } = string.Empty;
        public string TotalTime { get; set; } = string.Empty;
    }

    public class AchievementMessages
    {
        public string Title { get; set; } = string.Empty;
        public string Unlock { get; set; } = string.Empty;
        public string Award { get; set; } = string.Empty;
        public string NoRewards { get; set; } = string.Empty;
    }

    public class LeaderboardMessages
    {
        public string Title { get; set; } = string.Empty;
        public string Entry { get; set; } = string.Empty;
        public string Footer { get; set; } = string.Empty;
    }

    public class DefaultValues
    {
        public string DefaultUserName { get; set; } = string.Empty;
        public int DefaultAge { get; set; }
        public string DefaultRank { get; set; } = string.Empty;
        public int DefaultRankPoints { get; set; }
        public string DefaultAchievementName { get; set; } = string.Empty;
        public string DefaultAchievementDescription { get; set; } = string.Empty;
        public string DefaultRewardName { get; set; } = string.Empty;
        public string DefaultRewardType { get; set; } = string.Empty;
        public string DefaultQuestionText { get; set; } = string.Empty;
        public string DefaultExplanation { get; set; } = string.Empty;
    }

    public class RankNames
    {
        public string Novice { get; set; } = string.Empty;
        public string Junior { get; set; } = string.Empty;
        public string Middle { get; set; } = string.Empty;
        public string Senior { get; set; } = string.Empty;
        public string Architect { get; set; } = string.Empty;
    }

    public class GameNames
    {
        public string OopTheory { get; set; } = string.Empty;
        public string CodePuzzle { get; set; } = string.Empty;
        public string Quiz { get; set; } = string.Empty;
    }
}
    