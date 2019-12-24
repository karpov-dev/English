using Coursework.Models.Classes.User.Achievements;
using Coursework.Models.Classes.User.Information;
using Coursework.Models.Classes.User.Statistics;
using Coursework.Models.Classes.User.WordCollections;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Coursework.Models
{
    [Serializable]
    class User
    {
        private const string _dataFileName = "data.bin";
        private const string _settingsFileName = "settings.bin";

        public Settings Information;
        public UserStatistics Statistics;
        public Achievements Achievements;
        public WordCollection Collections;
        public bool Administrator;


        public User()
        {
            Statistics = new UserStatistics();
            Achievements = new Achievements();
            Collections = new WordCollection();
            Information = new Settings();

            System.Windows.Application.Current.Exit += Serialization;
            Deserialization();
        }
        
        public void Clear()
        {
            if( File.Exists(_dataFileName))
            {
                File.Delete(_dataFileName);
            }
            Statistics.ClearStatistics();
            Collections.DeleteAllCollections();
            Information.Reset();
            Information.Name = "";
        }

        private void Serialization(object sender, System.Windows.ExitEventArgs e)
        {
            var formatter = new BinaryFormatter();
            using ( FileStream stream = File.Create(_dataFileName) )
            {
                formatter.Serialize(stream, Statistics);
                formatter.Serialize(stream, Achievements);
                formatter.Serialize(stream, Collections);
            }
            using ( FileStream stream = File.Create(_settingsFileName) )
            {
                formatter.Serialize(stream, Information);
            }
        }
        private void Deserialization()
        {
            var formatter = new BinaryFormatter();
            if ( File.Exists(_dataFileName) )
            {
                using ( FileStream stream = File.OpenRead(_dataFileName) )
                {
                    Statistics = (UserStatistics) formatter.Deserialize(stream);
                    Achievements = (Achievements) formatter.Deserialize(stream);
                    Collections = (WordCollection) formatter.Deserialize(stream);
                }
            }
            if( File.Exists(_settingsFileName))
            {
                using ( FileStream stream = File.OpenRead(_settingsFileName) )
                {
                    Information = (Settings)formatter.Deserialize(stream);
                }
            }
        }

    }
}
