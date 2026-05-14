// using System;
// using System.Collections.Generic;

// namespace ProgrammingGame
// {
//     public class ProgrammingBasicsQuiz
//     {
//         public List<Question> Questions { get; private set; } = new List<Question>();

//         public ProgrammingBasicsQuiz()
//         {
//             InitializeQuestions();
//         }

//         private void InitializeQuestions()
//         {
//             var data = new List<(string Text, string[] Options, int Correct, string Explanation)>
//             {
//                 (
//                     "Що означає ключове слово 'void' у методі?",
//                     new[] { "Метод нічого не повертає", "Метод повертає ціле число", "Метод є асинхронним", "Метод є конструктором" },
//                     0,
//                     "void означає, що метод не повертає жодного значення."
//                 ),
//                 (
//                     "Як оголосити змінну типу string у C#?",
//                     new[] { "string x = 5;", "string x = \"Hello\";", "String x = Hello;", "var x = \"Hello\";" },
//                     1,
//                     "Правильний синтаксис: string x = \"Hello\";"
//                 ),
//                 (
//                     "Що робить оператор ++ ?",
//                     new[] { "Зменшує значення на 1", "Збільшує значення на 1", "Множить на 2", "Ділить на 2" },
//                     1,
//                     "++ — це оператор інкременту (збільшення на 1)."
//                 ),
//                 (
//                     "Який цикл використовується, коли кількість ітерацій відома заздалегідь?",
//                     new[] { "while", "do-while", "for", "foreach" },
//                     2,
//                     "for — найкращий вибір, коли відома кількість повторень."
//                 ),
//                 (
//                     "Що таке Git?",
//                     new[] { "Мова програмування", "Система контролю версій", "Фреймворк", "База даних" },
//                     1,
//                     "Git — це розподілена система контролю версій."
//                 )
//                 // Додайте ще питання за потребою
//             };

//             foreach (var q in data)
//             {
//                 Questions.Add(new Question(q.Text, q.Options.ToList(), q.Correct, q.Explanation));
//             }
//         }
//     }
// }