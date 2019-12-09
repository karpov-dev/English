using System;
using System.Collections.ObjectModel;

namespace Coursework.Models.Classes.User.Statistics
{
    [Serializable]
    class UserStatistics
    {
        private ObservableCollection<StatisticsForTheDay> _daysStatistics;
        private int _currentDayIndex;


        public UserStatistics()
        {
            _daysStatistics = new ObservableCollection<StatisticsForTheDay>();
            _currentDayIndex = _daysStatistics.Count - 1;
            CheckDate(_currentDayIndex);
        }


        public int DayLearnedWords(int day) => _daysStatistics[day].LearnedWords;
        public int DayAddedWords(int day) => _daysStatistics[day].AddedWords;
        public int DayExperience(int day) => _daysStatistics[day].DayExperience;


        public int TotalLearnedWords
        {
            get
            {
                int learnedWords = 0;
                for(int i = 0; 0 < _daysStatistics.Count; i++)
                {
                    learnedWords += _daysStatistics[i].LearnedWords;
                }
                return learnedWords;
            }
        }
        public int TotalAddedWords
        {
            get
            {
                int addedWords = 0;
                for (int i = 0; i < _daysStatistics.Count; i++)
                {
                    addedWords += _daysStatistics[i].AddedWords;
                }
                return addedWords;
            }
        }
        public int TotalExperience
        {
            get
            {
                int experience = 0;
                for(int i = 0; i < _daysStatistics.Count; i++)
                {
                    experience += _daysStatistics[i].DayExperience;
                }
                return experience;
            }
        }


        public void LearnedWords()
        {
            _daysStatistics[_currentDayIndex].LearnedWords++;
        }
        public void AddedWords()
        {
            _daysStatistics[_currentDayIndex].AddedWords++;
        }
        public void Experience(int amountExperience)
        {
            _daysStatistics[_currentDayIndex].DayExperience += amountExperience;
        }


        private void CheckDate(int currentDay)
        {
            if (_daysStatistics != null && _daysStatistics.Count > 0)
            {
                DateTime today = DateTime.Today;
                DateTime LastStatisticChange = _daysStatistics[currentDay].Date;
                if(today != LastStatisticChange)
                {
                    _daysStatistics.Add(new StatisticsForTheDay(DateTime.Today));
                }
            }
            else
            {
                _daysStatistics = new ObservableCollection<StatisticsForTheDay>();
                _daysStatistics.Add(new StatisticsForTheDay(DateTime.Today));
                _currentDayIndex++;
            }
        }
    }
}
