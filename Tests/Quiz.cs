using System.Runtime.CompilerServices;
using Microsoft.VisualBasic;
using System.Text.Json;

using ProgrammingGame.Data.JSONModels;

namespace ProgrammingGame
{
    public class Quiz
    {
        public List<Question> Questions { get; private set; } = new List<Question>();

        public bool IsEmpty() => Questions.Count == 0;

        public string _fileName = "Data/JSON/games.json"; 
       
        public Quiz(List<(string Text, string[] Options, int CorrectIndex, string Explanation)> questions)
        {
            InitializeQuestions(questions);
        }

        public Quiz()
        {
            Questions = new List<Question>();
        }

        private void InitializeQuestions(List<(string Text, string[] Options, int CorrectIndex, string Explanation)> questions)
        {
            foreach (var q in questions)
            {
                Questions.Add(new Question(q.Text, q.Options.ToList(), q.CorrectIndex, q.Explanation));
            }
        }
    }
}