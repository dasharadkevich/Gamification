using System;
using System.Runtime.CompilerServices;

namespace ProgrammingGame
{
    public class Simulation
    {
        public User User { get; private set; }
        public Quiz Quiz { get; private set; }



    public bool HasUser() => User != null;
    public bool HasQuiz() => Quiz != null && Quiz.HasQuestions();
    public bool IsQuizCompleted(int correctAnswers) => correctAnswers >= Quiz.Questions.Count * 0.7;
    public bool UserDeservesMasterTitle() => User.BestStreak >= 8 && User.TimeSpent.TotalMinutes <= 12;

        public Simulation(User user, Quiz quiz)
        {
            User = user ?? new User();
            Quiz = quiz ?? new Quiz();
        }

        // Конструктор копій
        public Simulation(Simulation other)
        {
            if (other == null) throw new ArgumentNullException(nameof(other));
            User = new User(other.User);     
            Quiz = new Quiz(other.Quiz);      
        }

        public void Run()
        {
            User.StartSession();

            Menu(User);

            int correctCount = 0;

            RunTest(correctCount);

            User.EndSession();

            Console.WriteLine("=== ФІНІШ ІМІТАЦІЇ ===");
            Console.WriteLine($"Правильних відповідей: {correctCount}/{Quiz.Questions.Count}");
            Console.WriteLine($"Найкраща серія: {User.BestStreak}");
            Console.WriteLine($"Загальний час у грі: {User.TimeSpent:mm\\:ss}");
            Console.WriteLine($"Найкраща серія: {User.BestStreak}");

            AwardAchievements();
        }

        public void RunTest(int correctCount)
        {
            foreach (var question in Quiz.Questions)
            {
                Console.WriteLine($"\nПитання: {question.Text}");
                for (int i = 0; i < question.Options.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {question.Options[i]}");
                }

                Console.Write("\nВаша відповідь (номер): ");
                int.TryParse(Console.ReadLine(), out int answer);
                answer--;

                bool isCorrect = answer == question.CorrectAnswerIndex;

                if (isCorrect)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("✓ Правильно!");
                    correctCount++;
                    User.AddPoints(20);
                    User.IncreaseStreak();

                    if (User.CurrentStreak >= 3)
                        Console.WriteLine($"🔥 Серія: {User.CurrentStreak}!");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("✗ Неправильно.");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Правильна відповідь: {question.CorrectAnswerIndex + 1}. {question.Options[question.CorrectAnswerIndex]}");
                    User.ResetStreak();
                }

                Console.ResetColor();
                Console.WriteLine($"Пояснення: {question.Explanation}");
                Console.WriteLine($"Бали: {(isCorrect ? "+20" : "+0")} | Найкраща серія: {User.BestStreak} | Стрік: {User.CurrentStreak}\n");

                Thread.Sleep(1600);
            }

        }
        private void AwardAchievements()
        {
            Console.WriteLine("\n--- Отримані нагороди та досягнення ---");

            if (User.BestStreak >= 2)
                User.UnlockAchievement(new Achievement("Незламний", "Досягти серії 5 правильних відповідей"));

            if (User.BestStreak >= 6)
                User.UnlockAchievement(new Achievement("Серійний вбивця питань", "Досягти серії 8+"));

            if (User.BestStreak >= 8)
                User.UnlockAchievement(new Achievement("ООП Легенда", "Досягти серії 12 правильних відповідей"));

            // Комбіновані досягнення (Streak + Час)
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
                Console.WriteLine("На жаль, цього разу немає нових нагород. Спробуйте покращити серію!");
            }
        }

        static void Menu(User User)
        {
            Console.WriteLine("=== СТАРТ ІМІТАЦІЇ ООП ТЕСТУ ===\n");
            Console.WriteLine($"Користувач: {User}");
            Console.WriteLine($"Дисципліна: Основи Об'єктно-Орієнтованого Програмування");
            Console.WriteLine($"Платформа: EduQuest OOP Simulator\n");
        }
    }
}