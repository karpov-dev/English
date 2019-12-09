using System;

namespace Coursework.Models.Classes.User.Achievements
{
    [Serializable]
    class OneAchievement
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Experience { get; set; }
    }
}
