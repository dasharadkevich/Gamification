namespace ProgrammingGame
{
    public class Achievement
    {
        public string Name { get; private set; }
        public string Description { get; private set; }


        public bool IsValid() => !string.IsNullOrWhiteSpace(Name);
        public bool IsSpecial() => Name.Contains("Легенда") || Name.Contains("Майстер");
        public bool HasLongDescription() => Description?.Length > 50;

        
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