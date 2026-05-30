using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Linq;

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
            var achievements = TextManager.Texts?.Achievements;
            var rewards = TextManager.Texts?.Rewards;

            Console.WriteLine($"\n{achievementMessages?.Title ?? "--- Отримані нагороди та досягнення ---"}");

            // Unlock achievements from JSON
            if (achievements != null)
            {
                foreach (var achievementData in achievements)
                {
                    if (ShouldUnlockAchievement(achievementData))
                    {
                        var achievement = new Achievement(achievementData.Name, achievementData.Description);
                        User.UnlockAchievement(achievement);
                    }
                }
            }

            // Unlock rewards from JSON
            if (rewards != null)
            {
                foreach (var rewardData in rewards)
                {
                    if (ShouldUnlockReward(rewardData))
                    {
                        var reward = new Reward(rewardData.Name, rewardData.Type);
                        User.UnlockAward(reward);
                    }
                }
            }

            // Check if any achievements or rewards were unlocked
            if ((achievements == null || !achievements.Any(a => ShouldUnlockAchievement(a))) &&
                (rewards == null || !rewards.Any(r => ShouldUnlockReward(r))))
            {
                Console.WriteLine(achievementMessages?.NoRewards ?? "На жаль, цього разу немає нових нагород. Спробуйте покращити серію!");
            }
        }

        private bool ShouldUnlockAchievement(AchievementData achievement)
        {
            // Check streak requirement
            if (achievement.StreakRequired > 0 && User.BestStreak < achievement.StreakRequired)
                return false;

            // Check time requirement
            if (achievement.TimeRequiredMinutes > 0)
            {
                if (achievement.MaxTimeRequired)
                {
                    // Need to complete UNDER the time limit
                    if (User.TimeSpent.TotalMinutes > achievement.TimeRequiredMinutes)
                        return false;
                }
                else
                {
                    // Need to complete OVER the time limit
                    if (User.TimeSpent.TotalMinutes < achievement.TimeRequiredMinutes)
                        return false;
                }
            }

            return true;
        }

        private bool ShouldUnlockReward(RewardData reward)
        {
            // Check streak requirement
            if (reward.StreakRequired > 0 && User.BestStreak < reward.StreakRequired)
                return false;

            // Check time requirement
            if (reward.TimeRequiredMinutes > 0)
            {
                if (reward.MaxTimeRequired)
                {
                    // Need to complete UNDER the time limit
                    if (User.TimeSpent.TotalMinutes > reward.TimeRequiredMinutes)
                        return false;
                }
                else
                {
                    // Need to complete OVER the time limit
                    if (User.TimeSpent.TotalMinutes < reward.TimeRequiredMinutes)
                        return false;
                }
            }

            return true;
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