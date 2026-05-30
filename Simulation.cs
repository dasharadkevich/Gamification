using System;
using System.Runtime.CompilerServices;
using System.Threading;

using ProgrammingGame.Data.JSONModels;

namespace ProgrammingGame
{
    public class Simulation
    {
        public User User { get; private set; }
        public Quiz Quiz { get; private set; }

        public Simulation(User user, Quiz quiz)
        {
            User = user ?? new User();
            Quiz = quiz ?? new Quiz();
        }

        public void Run()
        {
            User.StartSession();
            Menu(User);
            
            int correctCount = 0;
            RunTest(ref correctCount);
            
            User.EndSession();
            
            var simulationMessages = TextManager.Texts?.SimulationMessages;
            var achievementMessages = TextManager.Texts?.AchievementMessages;
            
            Console.WriteLine(simulationMessages?.Finish ?? "=== ФІНІШ ІМІТАЦІЇ ===");
            Console.WriteLine(string.Format(simulationMessages?.CorrectCount ?? "Правильних відповідей: {0}/{1}", correctCount, Quiz.Questions.Count));
            Console.WriteLine($"Найкраща серія: {User.BestStreak}");
            Console.WriteLine(string.Format(simulationMessages?.TotalTime ?? "Загальний час у грі: {0:mm\\:ss}", User.TimeSpent));
            
            AwardAchievements();
        }

        public void RunTest(ref int correctCount)
        {
            var quizMessages = TextManager.Texts?.QuizMessages;
            
            foreach (var question in Quiz.Questions)
            {
                Console.WriteLine(string.Format(quizMessages?.QuestionPrefix ?? "\nПитання: {0}", question.Text));
                
                for (int i = 0; i < question.Options.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {question.Options[i]}");
                }

                Console.Write(quizMessages?.AnswerPrompt ?? "\nВаша відповідь (номер): ");
                int.TryParse(Console.ReadLine(), out int answer);
                answer--;

                bool isCorrect = answer == question.CorrectAnswerIndex;

                if (isCorrect)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(quizMessages?.Correct ?? "✓ Правильно!");
                    correctCount++;
                    User.AddPoints(20);
                    User.IncreaseStreak();

                    if (User.CurrentStreak >= 3)
                        Console.WriteLine(string.Format(quizMessages?.StreakMessage ?? "🔥 Серія: {0}!", User.CurrentStreak));
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(quizMessages?.Incorrect ?? "✗ Неправильно.");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(string.Format(quizMessages?.CorrectAnswer ?? "Правильна відповідь: {0}. {1}", 
                        question.CorrectAnswerIndex + 1, question.Options[question.CorrectAnswerIndex]));
                    User.ResetStreak();
                }

                Console.ResetColor();
                Console.WriteLine(string.Format(quizMessages?.Explanation ?? "Пояснення: {0}", question.Explanation));
                Console.WriteLine(string.Format(quizMessages?.ScoreInfo ?? "Бали: {0} | Найкраща серія: {1} | Стрік: {2}\n", 
                    isCorrect ? "+20" : "+0", User.BestStreak, User.CurrentStreak));

                Thread.Sleep(1600);
            }
        }
        
        private void AwardAchievements()
        {
            var achievementMessages = TextManager.Texts?.AchievementMessages;
            Console.WriteLine($"\n{achievementMessages?.Title ?? "--- Отримані нагороди та досягнення ---"}");

            if (User.BestStreak >= 2)
                User.UnlockAchievement(new Achievement("Незламний", "Досягти серії 5 правильних відповідей"));

            if (User.BestStreak >= 4)
                User.UnlockAchievement(new Achievement("Серійний вбивця питань", "Досягти серії 8+"));

            if (User.BestStreak >= 5)
                User.UnlockAchievement(new Achievement("ООП Легенда", "Досягти серії 5 правильних відповідей"));

            if (User.BestStreak >= 6 && User.TimeSpent.TotalMinutes >= 5)
                User.UnlockAchievement(new Achievement("Швидкий та точний", "Серія 6+ за більше 5 хвилин"));

            if (User.BestStreak >= 10 && User.TimeSpent.TotalMinutes <= 8)
                User.UnlockAchievement(new Achievement("Блискавка ООП", "Серія 10+ менше ніж за 8 хвилин"));

            if (User.BestStreak >= 7 && User.TimeSpent.TotalMinutes >= 10)
                User.UnlockAchievement(new Achievement("Наполегливий практик", "Серія 7+ за більше 10 хвилин"));

            // Нагороди (Awards)
            if (User.BestStreak >= 5)
                User.UnlockAward(new Reward("Початківець ООП", "Badge"));

            if (User.BestStreak >= 10)
                User.UnlockAward(new Reward("Майстер Стріку", "Medal"));

            if (User.TimeSpent.TotalMinutes >= 15)
                User.UnlockAward(new Reward("Марафонець", "Title"));

            if (User.BestStreak >= 8 && User.TimeSpent.TotalMinutes <= 10)
                User.UnlockAward(new Reward("Ідеальний раунд", "Special"));

            if (User.Achievements.Count == 0 && User.Awards.Count == 0)
            {
                Console.WriteLine(achievementMessages?.NoRewards ?? "На жаль, цього разу немає нових нагород. Спробуйте покращити серію!");
            }
        }

        static void Menu(User User)
        {
            var simulationMessages = TextManager.Texts?.SimulationMessages;
            
            Console.WriteLine(simulationMessages?.Start ?? "=== СТАРТ ІМІТАЦІЇ ООП ТЕСТУ ===\n");
            Console.WriteLine(string.Format(simulationMessages?.User ?? "Користувач: {0}", User.UserName));
            Console.WriteLine(simulationMessages?.Discipline ?? "Дисципліна: Основи Об'єктно-Орієнтованого Програмування");
            Console.WriteLine(simulationMessages?.Platform ?? "Платформа: EduQuest OOP Simulator\n");
        }
    }
}