namespace ProgrammingGame
{
    public class Reward
    {
        public string Name { get; set; }
        public string Type { get; set; }

        public Reward(string name, string type = "Award")
        {
            Name = name;
            Type = type;
        }
    }
}