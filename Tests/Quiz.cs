using Microsoft.VisualBasic;

namespace ProgrammingGame
{
    public class Quiz
    {
        public List<Question> Questions { get; private set; } = new List<Question>();

        public Quiz(List<(string Text, string[] Options, int CorrectIndex, string Explanation)> questions)
        {
            InitializeQuestions(questions);
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
        public string Text { get; set; }
        public List<string> Options { get; set; } = new List<string>();
        public int CorrectAnswerIndex { get; set; }
        public string Explanation { get; set; }

        public Question(string text, List<string> options, int correctIndex, string explanation)
        {
            Text = text;
            Options = options;
            CorrectAnswerIndex = correctIndex;
            Explanation = explanation;
        }
    }
}