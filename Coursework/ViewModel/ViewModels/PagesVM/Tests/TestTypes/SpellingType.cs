using System;
using System.Windows;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Threading;
using Coursework.Models.Classes.Events;
using Coursework.Models.Classes.Commands;
using Coursework.ViewModel.ViewModels.PagesVM.Tests.TestsManager;
using Coursework.Models.Classes.User.WordCollections.WordPair;
using Coursework.Models.Classes.User.WordCollections;
using Coursework.Models.Classes.Test;
using Coursework.Models.Classes.User.Information;


namespace Coursework.ViewModel.ViewModels.PagesVM.Tests.TestTypes
{
    class SpellingType : Event
    {
        private OneSessionStatistics _oneSessionStatistics;
        private TestManager _owner;
        private OneWordPair _wordPair;
        private Settings _settings;
        private OneCollection _currentCollection;
        private ObservableCollection<OneCollection> _userCollections;
        private DispatcherTimer _timer;
        private Visibility _timerVisibility;
        private Random _random;

        private string _userInput;
        private string _textResult;
        private List<string> _textResults;
        private int _amountRightSymbols;
        private int _timeToNextPage;
        private bool _expectation;
        private int _secondsToNextTest;

        private RelayCommand _backButton;
        private RelayCommand _speakWord;


        public SpellingType(Random random, ObservableCollection<OneCollection> userCollections, 
            TestManager owner, OneSessionStatistics oneSessionStatistics, Settings testSettings)
        {
            _random = random;
            _settings = testSettings;
            _oneSessionStatistics = oneSessionStatistics;
            _timerVisibility = Visibility.Hidden;
            _expectation = true;
            _userCollections = userCollections;
            _owner = owner;
            _textResults = new List<string>();
            _secondsToNextTest = testSettings.NextTestTime;
            UpdateTest();
        }


        public string Word
        {
            get => _wordPair.Word;
            set
            {
                _wordPair.Word = value;
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
        public int TranslationLenght => _wordPair.Translation.Length;
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
                         _owner.BackAndClean();
                     }) );
            }
        }
        public RelayCommand Speak
        {
            get
            {
                return _speakWord ??
                    ( _speakWord = new RelayCommand(obj =>
                     {

                     }) );
            }
        }

        private void UpdateTest()
        {
            _currentCollection = Test.SetRandomCurrentCollection(_random, _userCollections);
            _wordPair = Test.GetWordPair(_random, _currentCollection);
            if(_settings.FullTest)
            {
                _wordPair = Test.RandomSwap(_random, _wordPair);
            }
            Word = _wordPair.Word;
            Expectation = true;
        }
        private void CheckInput()
        {
            if(_wordPair.Translation.Contains(UserInput))
            {
                AmountRightSymbols = UserInput.Length;
            }
            else
            {
                AmountRightSymbols = 0;
                UpdateStatistics(false);
            }

            if(_wordPair.Translation == UserInput)
            {
                TextResult = _owner.RightOutput();
                TimerVisibility = Visibility.Visible;
                Expectation = false;
                UpdateStatistics(true);
                GoToNextTest();
            }
        }
        private void UpdateStatistics(bool result)
        {
            if(result)
            {
                _wordPair.AmountRepetiotion++;
                _oneSessionStatistics.AddExp(_wordPair);
                _oneSessionStatistics.UpdateOneWordStatistic(_wordPair);
            }
            else
            {
                _oneSessionStatistics.AddAmountErrors();
            }
        }
        private void GoToNextTest()
        {
            Timer = _secondsToNextTest;
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
                _owner.NextTest();
            }
        }
    }
}
