using System.Collections.Generic;
using Coursework.Models.Classes.NotifyPropertyEvent;

namespace Coursework.Models.Classes.User
{
    class UserStatistics : NotifyPropertyChangedEvent
    {
        private int _experience;
        private int _totalNumberOfWords;
        private int _totalLearnedWords;
        private List<StatisticsForTheDay> _daysStatistics;

        public int Experience
        {
            get => _experience;
            set
            {
                _experience = value;
                OnPropertyChanged("Experience");
            }
        }
        public int TotalNumberOfWords
        {
            get => _totalNumberOfWords;
            set
            {
                _totalNumberOfWords = value;
                OnPropertyChanged("TotalNumberOfWords");
            }
        }
        public int TotalLearnedWords
        {
            get => _totalLearnedWords;
            set
            {
                _totalLearnedWords = value;
                OnPropertyChanged("TotalLearnedWords");
            }
        }

        public StatisticsForTheDay GetOneDayStatistics(int day) => _daysStatistics[day];
        public List<StatisticsForTheDay> GetAllStatistics() => _daysStatistics;
    }
}
