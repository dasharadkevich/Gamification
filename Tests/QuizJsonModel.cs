namespace ProgrammingGame
{
    public class QuizFileRoot
    {
        public List<TestData> Tests { get; set; } = new();
    }

    public class TestData
    {
        public int TestId { get; set; }
        public string Title { get; set; } = string.Empty;
        public List<QuestionData> Questions { get; set; } = new();
    }

    public class QuestionData
    {
        public string Text { get; set; } = string.Empty;
        public List<string> Options { get; set; } = new();
        public int CorrectIndex { get; set; }
        public string Explanation { get; set; } = string.Empty;
    }
}