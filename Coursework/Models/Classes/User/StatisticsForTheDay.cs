using Coursework.Models.Classes.NotifyPropertyEvent;

namespace Coursework.Models.Classes.User
{
    class StatisticsForTheDay : NotifyPropertyChangedEvent
    {
        private int _learnedWords;
        private int _addedWords;
        private int _dayExperience;

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
