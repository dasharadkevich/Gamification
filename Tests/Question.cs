namespace ProgrammingGame
{
     public class Question
    {
        public string Text { get; private set; }
        public List<string> Options { get; private set; } = new List<string>();
        public int CorrectAnswerIndex { get; private set; }
        public string Explanation { get; set; }


        public Question(string text, List<string> options, int correctIndex, string explanation)
        {
            Text = string.IsNullOrWhiteSpace(text) ? "Питання не вказано" : text;
            Options = options ?? new List<string>();
            CorrectAnswerIndex = correctIndex;
            Explanation = explanation ?? "Пояснення відсутнє";

            if (CorrectAnswerIndex < 0 || CorrectAnswerIndex >= Options.Count)
                CorrectAnswerIndex = 0;
        }


        public Question(Question other)
        {
            if (other == null) throw new ArgumentNullException(nameof(other));

            Text = other.Text;
            Options = new List<string>(other.Options);
            CorrectAnswerIndex = other.CorrectAnswerIndex;
            Explanation = other.Explanation;
        }
    }
}