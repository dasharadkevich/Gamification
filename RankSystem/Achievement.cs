using ProgrammingGame.Data.JSONModels;

namespace ProgrammingGame
{
    public class Achievement
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        
        public Achievement(string name, string description)
        {
            var defaultValues = TextManager.Texts?.DefaultValues;
            
            Name = string.IsNullOrWhiteSpace(name) 
                ? (defaultValues?.DefaultAchievementName ?? "") 
                : name;
                
            Description = string.IsNullOrWhiteSpace(description) 
                ? (defaultValues?.DefaultAchievementDescription ?? "") 
                : description;
        }
        
        private Achievement()
        {
            var defaultValues = TextManager.Texts?.DefaultValues;
            Name = defaultValues?.DefaultAchievementName ?? "";
            Description = defaultValues?.DefaultAchievementDescription ?? "";
        }
    }
}