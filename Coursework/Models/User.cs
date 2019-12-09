using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Coursework.Models.Classes.User.Statistics;
using Coursework.Models.Classes.User.Achievements;
using Coursework.Models.Classes.User.WordCollections;
using Coursework.Models.Classes.User.Information;

namespace Coursework.Models
{
    [Serializable]
    class User
    {
        private const string _dataFileName = "data.bin";

        public UserInformation Information;
        public UserStatistics Statistics;
        public Achievements Achievements;
        public WordCollection Collections;


        public User()
        {
            Statistics = new UserStatistics();
            Achievements = new Achievements();
            Collections = new WordCollection();
            Information = new UserInformation();

            System.Windows.Application.Current.Exit += Serialization;
            Deserialization();
        }

        private void Serialization(object sender, System.Windows.ExitEventArgs e)
        {
            var formatter = new BinaryFormatter();
            using (FileStream stream = File.Create(_dataFileName))
            {
                formatter.Serialize(stream, Statistics);
                formatter.Serialize(stream, Achievements);
                formatter.Serialize(stream, Collections);
                formatter.Serialize(stream, Information);
            }
        }
        private void Deserialization()
        {
            var formatter = new BinaryFormatter();
            if(File.Exists(_dataFileName))
            {
                using (FileStream stream = File.OpenRead(_dataFileName))
                {
                    Statistics = (UserStatistics)formatter.Deserialize(stream);
                    Achievements = (Achievements)formatter.Deserialize(stream);
                    Collections = (WordCollection)formatter.Deserialize(stream);
                    Information = (UserInformation)formatter.Deserialize(stream);
                }
            }
        }
    }
}
