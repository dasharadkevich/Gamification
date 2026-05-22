namespace ProgrammingGame
{
    public interface IGame
    {
        string Description { get; }
        
        void Start(User user);
        void Run();
        void End();

        bool IsCompleted { get; }
        int Score { get; }
    }
}