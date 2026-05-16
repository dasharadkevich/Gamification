using System.Runtime.CompilerServices;
using Microsoft.VisualBasic;

namespace ProgrammingGame
{
    public class Quiz
    {
        public List<Question> Questions { get; private set; } = new List<Question>();

        public bool HasQuestions() => Questions.Count > 0;
        public bool IsEmpty() => Questions.Count == 0;
        public bool HasEnoughQuestions(int minimum = 3) => Questions.Count >= minimum;
        public bool AllQuestionsHaveExplanation() => Questions.All(q => q.HasExplanation());
        public bool ContainsQuestion(string keyword) =>
            Questions.Any(q => q.Text.Contains(keyword, StringComparison.OrdinalIgnoreCase));

        public Quiz(List<(string Text, string[] Options, int CorrectIndex, string Explanation)> questions)
        {
            InitializeQuestions(questions);
        
        }

        public Quiz()
        {
            Questions = new List<Question>();
        }

        public Quiz(Quiz other)
        {
            if (other == null) throw new ArgumentNullException(nameof(other));
            Questions = other.Questions.Select(q => new Question(q)).ToList();
        }

        private void InitializeQuestions(List<(string Text, string[] Options, int CorrectIndex, string Explanation)> questions)
        {
            foreach (var q in questions)
            {
                Questions.Add(new Question(q.Text, q.Options.ToList(), q.CorrectIndex, q.Explanation));
            }
        }


    }
    public class Question
    {
        public string Text { get; private set; }
        public List<string> Options { get; private set; } = new List<string>();
        public int CorrectAnswerIndex { get; private set; }
        public string Explanation { get; set; }


        public bool IsValid() => !string.IsNullOrWhiteSpace(Text) && Options.Count >= 2;
        public bool IsCorrectAnswer(int index) => index == CorrectAnswerIndex;
        public bool HasExplanation() => !string.IsNullOrWhiteSpace(Explanation);

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