using System;
using Coursework.Models.Classes.Events;

namespace Coursework.Models.Classes.User.Statistics
{
    [Serializable]
    class StatisticsForTheDay : Event
    {
        public StatisticsForTheDay(DateTime dateOfCreate)
        {
            Date = dateOfCreate;
        }

        public int Exp { get; private set; }
        public int LearnedWords { get; private set; }
        public int AddedWords { get; private set; }
        public int RepitedWords { get; private set; }
        public int WrongWords { get; private set; }
        public  DateTime Date { get; }


        public void AddExp(int amount)
        {
            Exp += amount;
            OnPropertyChanged("Exp");
        }
        public void AddLearnedWords(int amount)
        {
            LearnedWords += amount;
            OnPropertyChanged("LearnedWords");
        }
        public void AddAddedWords(int amount)
        {
            AddedWords += amount;
            OnPropertyChanged("AddedWords");
        }
        public void AddRepitedWords(int amount)
        {
            RepitedWords += amount;
            OnPropertyChanged("RepitedWords");
        }
        public void AddWrongWords(int amount)
        {
            WrongWords += amount;
            OnPropertyChanged("WrongWords");
        }
    }
}
