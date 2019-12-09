using System;
using Coursework.Models.Classes.Events;

namespace Coursework.Models.Classes.User.Statistics
{
    class StatisticsForTheDay : Event
    {
        private int _learnedWords;
        private int _addedWords;
        private int _dayExperience;

        public StatisticsForTheDay(DateTime currentDate)
        {
            Date = currentDate;
        }

        public DateTime Date { get;}
        public int LearnedWords
        {
            get => _learnedWords;
            set
            {
                _learnedWords = value;
                OnPropertyChanged("LearnedWords");
            }
        }
        public int AddedWords
        {
            get => _addedWords;
            set
            {
                _addedWords = value;
                OnPropertyChanged("AddedWords");
            }
        }
        public int DayExperience
        {
            get => _dayExperience;
            set
            {
                _dayExperience = value;
                OnPropertyChanged("DayExperience");
            }
        }
    }
}
