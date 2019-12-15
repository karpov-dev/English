using System;
using System.Windows;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Threading;
using System.ComponentModel;
using Coursework.Models;
using Coursework.Models.Classes.Events;
using Coursework.Models.Classes.Commands;
using Coursework.ViewModel.ViewModels.PagesVM.Tests.TestsManager;
using Coursework.Models.Classes.User.WordCollections.WordPair;
using Coursework.Models.Classes.User.WordCollections;
using Coursework.ViewModel.ViewModels.VM;
using Coursework.Models.Classes.User.Statistics;

namespace Coursework.ViewModel.ViewModels.PagesVM.Tests.TestTypes
{
    class SpellingType : TestBase
    {
        private const int SecondsToNextTest = 1;

        private UserStatistics _statistics;
        private TestManager _owner;
        private OneWordPair _wordPair;
        private DispatcherTimer _timer;
        private Visibility _timerVisibility;

        private string _word;
        private string _translation;
        private string _userInput;
        private string _textResult;
        private List<string> _textResults;
        private int _amountRightSymbols;
        private int _timeToNextPage;
        private bool _expectation;

        private RelayCommand _backButton;


        public SpellingType(UserStatistics userStatistics, ObservableCollection<OneCollection> userCollections, TestManager owner)
            : base(userCollections)
        {
            _timerVisibility = Visibility.Hidden;
            _expectation = true;
            _statistics = userStatistics;
            _owner = owner;
            _textResults = new List<string>();
            SetTextResults();
            UpdateTest();
        }


        public string Word
        {
            get => _word;
            set
            {
                _word = value;
                OnPropertyChanged("Word");
            }
        }
        public string UserInput
        {
            get => _userInput;
            set
            {
                _userInput = value;
                CheckInput();
                OnPropertyChanged("UserInput");
            }
        }
        public string TextResult
        {
            get => _textResult;
            set
            {
                _textResult = value;
                OnPropertyChanged("TextResult");
            }
        }
        public int AmountRightSymbols
        {
            get => _amountRightSymbols;
            set
            {
                _amountRightSymbols = value;
                OnPropertyChanged("AmountRightSymbols");
            }
        }
        public int Timer
        {
            get => _timeToNextPage;
            set
            {
                _timeToNextPage = value;
                OnPropertyChanged("Timer");
            }
        }
        public int TranslationLenght => _translation.Length;
        public Visibility TimerVisibility
        {
            get => _timerVisibility;
            set
            {
                _timerVisibility = value;
                OnPropertyChanged("TimerVisibility");
            }
        }
        public bool Expectation
        {
            get => _expectation;
            set
            {
                _expectation = value;
                OnPropertyChanged("Expectation");
            }
        }
        


        public RelayCommand BackButton
        {
            get
            {
                return _backButton ??
                    ( _backButton = new RelayCommand(obj =>
                     {
                         _owner.Manager.CurrentViewModel = _owner;
                     }) );
            }
        }


        private void UpdateTest()
        {
            _currentCollection = SetRandomCurrentCollection(_collections);
            _wordPair = GetWordPair(_currentCollection);
            Word = _wordPair.Word;
            _translation = _wordPair.Translation;
            AmountRightSymbols = 0;
            TextResult = "";
            UserInput = "";
            TimerVisibility = Visibility.Hidden;
            Expectation = true;
        }
        private void CheckInput()
        {
            if(_translation.Contains(UserInput))
            {
                AmountRightSymbols = UserInput.Length;
            }
            else
            {
                AmountRightSymbols = 0;
            }

            if(_translation == UserInput)
            {
                TextResult = _textResults[_random.Next(0, _textResults.Count)];
                TimerVisibility = Visibility.Visible;
                Expectation = false;
                GoToNextTest();
            }
        }
        private void GoToNextTest()
        {
            Timer = SecondsToNextTest;
            _timer = new DispatcherTimer();
            _timer.Tick += new EventHandler(TimerTick);
            _timer.Interval = new TimeSpan(0, 0, 1);
            _timer.Start();
        }
        private void TimerTick(object sender, EventArgs e)
        {
            Timer--;
            if(Timer < 0)
            {
                _timer.Stop();
                UpdateTest();
            }
        }
        private void SetTextResults()
        {
            _textResults.Add(Properties.Resources.Good);
            _textResults.Add(Properties.Resources.GoodJob);
            _textResults.Add(Properties.Resources.VeryGood);
            _textResults.Add(Properties.Resources.KeepItUp);
            _textResults.Add(Properties.Resources.Delightfully);
        }
    }
}
