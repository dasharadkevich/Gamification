namespace ProgrammingGame
{
    public class Reward
    {
        public string Name { get; private set; }
        public string Type { get; private set; }

        public Reward(string name, string type = "Award")
        {
            Name = string.IsNullOrWhiteSpace(name) ? "Невідома нагорода" : name;
            Type = string.IsNullOrWhiteSpace(type) ? "Award" : type;
        }

        public Reward(Reward other)
        {
            if (other == null) throw new ArgumentNullException(nameof(other));
            Name = other.Name;
            Type = other.Type;
        }
    }
}