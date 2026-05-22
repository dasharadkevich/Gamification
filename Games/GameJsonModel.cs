using System;
using System.Collections.Generic;
using System.IO; 
using System.Text.Json;

namespace ProgrammingGame
{
    public class QuestionItem
    {
        public string Text { get; private set; }
        public List<string> Options { get; private set; }
        public int CorrectIndex { get; private set; }
        public string Explanation { get; private set; }


        public QuestionItem(string text, List<string> options, int correctIndex, string explanation)
        {
            Text = text; 
            Options = options; 
            CorrectIndex = correctIndex; 
            Explanation = explanation; 
            
        }
    }

    public class TestItem
    {
        public int TestId { get; set; }
        public string Title { get; set; }
        public List<QuestionItem> Questions { get; set; }

        public TestItem(int testId, string title, List<QuestionItem> questions)
        {
            TestId = testId;
            Title = title; 
            Questions = questions;
        }


    }

    public class JsonRoot
    {
        public List<TestItem> Tests { get; set; }

        public JsonRoot(List<TestItem> tests)
        {
            Tests = tests; 
        }
    }
}