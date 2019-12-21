using System;
using Coursework.Models.Classes.Events;

namespace Coursework.Models.Classes.User.Information
{
    [Serializable]
    class Settings : Event
    {
        private const int DeafultAmountTests = 10;
        private const int DefaultTestTime = 1;
        private const bool DefaultFullTest = true;

        private string _name;
        private int _amountTests;
        private int _testTime;
        private bool _fullTest;

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }
        public int AmountTests
        {
            get => _amountTests;
            set
            {
                _amountTests = value;
                OnPropertyChanged("AmountTests");
            }
        }
        public int NextTestTime
        {
            get => _testTime;
            set
            {
                _testTime = value;
                OnPropertyChanged("NextTestTime");
            }
        }
        public bool FullTest
        {
            get => _fullTest;
            set
            {
                _fullTest = value;
                OnPropertyChanged("FullTest");
            }
        }

        public void Reset()
        {
            AmountTests = DeafultAmountTests;
            NextTestTime = DefaultTestTime;
            FullTest = DefaultFullTest;
        }
    }
}
