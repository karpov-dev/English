using Coursework.Models.Classes.User.Statistics;
using Coursework.Models.Classes.User.Achievements;
using Coursework.Models.Classes.User.WordCollections;
using System.Collections.Generic;

namespace Coursework.Models
{
    class User
    {
        public string Name { get; set; }
        public UserStatistics Statistics;
        public Achievements Achievements;
        public WordCollection Collections;


        public User()
        {
            Statistics = new UserStatistics();
            Achievements = new Achievements();
            Collections = new WordCollection();
        }
    }
}
