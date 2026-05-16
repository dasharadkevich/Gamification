namespace ProgrammingGame
{
    public class Reward
    {
        public string Name { get; private set; }
        public string Type { get; private set; }

        public bool IsValid() => !string.IsNullOrWhiteSpace(Name);
        public bool IsBadge() => Type.Equals("Badge", StringComparison.OrdinalIgnoreCase);
        public bool IsMedal() => Type.Equals("Medal", StringComparison.OrdinalIgnoreCase);
        public bool IsSpecial() => Type.Equals("Special", StringComparison.OrdinalIgnoreCase);

        public Reward(string name, string type = "Award")
        {
            Name = string.IsNullOrWhiteSpace(name) ? "Невідома нагорода" : name;
            Type = string.IsNullOrWhiteSpace(type) ? "Award" : type;
        }

        private Reward()
        {
            Name = "Невідома нагорода";
            Type = "Award";
        }

        public Reward(Reward other)
        {
            if (other == null) throw new ArgumentNullException(nameof(other));
            Name = other.Name;
            Type = other.Type;
        }
    }
}