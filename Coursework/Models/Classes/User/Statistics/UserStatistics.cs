using System;
using System.Collections.ObjectModel;

namespace Coursework.Models.Classes.User.Statistics
{
    [Serializable]
    class UserStatistics
    {
        private const int _levelSeed = 500;

        private ObservableCollection<StatisticsForTheDay> _daysStatistics;
        private int _currentDayIndex;
        private int _currentLevel;


        public UserStatistics()
        {
            _daysStatistics = new ObservableCollection<StatisticsForTheDay>();
            _currentDayIndex = _daysStatistics.Count - 1;
            _currentLevel = 1;
            CheckDate(_currentDayIndex);
        }


        public StatisticsForTheDay DayStatistics() => _daysStatistics[_currentDayIndex];
        public int DayLearnedWords(int day) => _daysStatistics[day].LearnedWords;
        public int DayAddedWords(int day) => _daysStatistics[day].AddedWords;
        public int DayExperience(int day) => _daysStatistics[day].Exp;
        public int DayRepitedWords(int day) => _daysStatistics[day].RepitedWords;
        public int DayWrongWords(int day) => _daysStatistics[day].WrongWords;


        public int TotalLearnedWords
        {
            get
            {
                int learnedWords = 0;
                for(int i = 0; i < _daysStatistics.Count; i++)
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
                    experience += _daysStatistics[i].Exp;
                }
                return experience;
            }
            set
            {

            }
        }
        public int TotalRepited
        {
            get
            {
                int repitedWords = 0;
                for(int i = 0; i < _daysStatistics.Count; i++ )
                {
                    repitedWords += _daysStatistics[i].RepitedWords;
                }
                return repitedWords;
            }
        }
        public int TotalWrongs
        {
            get
            {
                int wrongs = 0;
                for(int i = 0; i < _daysStatistics.Count; i++ )
                {
                    wrongs += _daysStatistics[i].WrongWords;
                }
                return wrongs;
            }
        }
        public int CurrentLevel
        {
            get
            {
                _currentLevel = 1;
                while ( TotalExperience > _currentLevel * ( _levelSeed + _currentLevel / 2 ) )
                {
                    _currentLevel++;
                }   
                return _currentLevel;
            }
        }
        public int ExpForNextLevel
        {
            get
            {
                return _currentLevel * ( _levelSeed + _currentLevel * 2 );
            }
        }
        public int PreviusLevel
        {
            get => CurrentLevel - 1;
        }
        public int SecondLevel
        {
            get => CurrentLevel + 1;
        }
        public int CurrentLevelDifference
        {
            get => ExpForNextLevel - TotalExperience;
        }


        public void AddLearnedWords(int amount)
        {
            _daysStatistics[_currentDayIndex].AddLearnedWords(amount);
        }
        public void AddAddedWords(int amount)
        {
            _daysStatistics[_currentDayIndex].AddAddedWords(amount);
        }
        public void AddExperience(int amountExperience)
        {
            _daysStatistics[_currentDayIndex].AddExp(amountExperience);
        }
        public void AddRepitedWords(int amount)
        {
            _daysStatistics[_currentDayIndex].AddRepitedWords(amount);
        }
        public void AddWrongs(int amount)
        {
            _daysStatistics[_currentDayIndex].AddWrongWords(amount);
        }


        public void ClearStatistics()
        {
            _daysStatistics.Clear();
            _daysStatistics.Add(new StatisticsForTheDay(DateTime.Today));
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
