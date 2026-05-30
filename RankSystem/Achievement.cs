namespace ProgrammingGame
{
    public class Achievement
    {
        public string Name { get; private set; }
        public string Description { get; private set; }

        
        public Achievement(string name, string description)
        {
            Name = name;
            Description = description;
        }
        
        private Achievement()
        {
            Name = "Без назви";
            Description = "Немає опису";
        }
    }
}