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

        public Achievement(Achievement other)
        {
            if (other == null) throw new ArgumentNullException(nameof(other));

            Name = other.Name;
            Description = other.Description;
        }

        private Achievement()
        {
            Name = "Без назви";
            Description = "Немає опису";
        }

    }
}