using ProgrammingGame.Data.JSONModels;

namespace ProgrammingGame
{
    public class Reward
    {
        public string Name { get; private set; }
        public string Type { get; private set; }

        public Reward(string name, string type = "Award")
        {
            Name = string.IsNullOrWhiteSpace(name) ? "" : name;
            Type = string.IsNullOrWhiteSpace(type) ? "" : type;
        }
    }
}