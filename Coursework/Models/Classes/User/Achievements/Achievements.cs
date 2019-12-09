using System.Collections.Generic;
using System;

namespace Coursework.Models.Classes.User.Achievements
{
    [Serializable]
    class Achievements
    {
        public List<OneAchievement> AllAchievements { get; set; }
        public List<OneAchievement> ProgressAchievements { get; set; }
    }
}
