using System;
using System.Windows;
using System.Windows.Threading;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Coursework.Models.Classes.Events;
using Coursework.Models.Classes.Commands;
using Coursework.ViewModel.ViewModels.PagesVM.Tests.TestsManager;
using Coursework.Models.Classes.User.WordCollections.WordPair;
using Coursework.Models.Classes.User.WordCollections;
using Coursework.Models.Classes.Test;
using Coursework.Models.Classes.User.Information;

namespace Coursework.ViewModel.ViewModels.PagesVM.Tests.TestTypes
{
    class OptionType : Event
    {
        private const int AmointButtons = 4;

        private TestManager _owner;
        private OneSessionStatistics _oneSessionStatistics;
        private OneCollection _currentCollection;
        private ObservableCollection<OneCollection> _collections;
        private OneWordPair _rightWordPair;

        private Random _random;
        private Visibility _timerVisibility;
        private DispatcherTimer _timer;
        private int _timerToNextPage;
        private int _secondsToNextTest;

        private string _topLeft;
        private string _topRight;
        private string _bottomLeft;
        private string _bottomRight;
        private string _rightOutput;
        private bool _waiting;

        private RelayCommand _backButton;
        private RelayCommand _checkResult;


        public OptionType (Random random, ObservableCollection<OneCollection> userCollections, 
            TestManager owner, OneSessionStatistics oneSessionStatistics, Settings testSettings)
        {
            _timerVisibility = Visibility.Hidden;
            _waiting = true;
            _oneSessionStatistics = oneSessionStatistics;
            _random = random;
            _collections = userCollections;
            _owner = owner;
            _secondsToNextTest = testSettings.NextTestTime;
            UpdateTest();
        }


        public string Word
        {
            get => _rightWordPair.Word;
            set
            {
                _rightWordPair.Word = value;
                OnPropertyChanged("Word");
            }
        }
        public string TopLeft
        {
            get => _topLeft;
            set
            {
                _topLeft = value;
                OnPropertyChanged("TopLeft");
            }
        }
        public string TopRight
        {
            get => _topRight;
            set
            {
                _topRight = value;
                OnPropertyChanged("TopRight");
            }
        }
        public string BottomLeft
        {
            get => _bottomLeft;
            set
            {
                _bottomLeft = value;
                OnPropertyChanged("BottomLeft");
            }
        }
        public string BottomRight
        {
            get => _bottomRight;
            set
            {
                _bottomRight = value;
                OnPropertyChanged("BottomRight");
            }
        }
        public string RightOutput
        {
            get => _rightOutput;
            set
            {
                _rightOutput = value;
                OnPropertyChanged("RightOutput");
            }
        }
        public int Timer
        {
            get => _timerToNextPage;
            set
            {
                _timerToNextPage = value;
                OnPropertyChanged("Timer");
            }
        }
        public Visibility TimerVisibility
        {
            get => _timerVisibility;
            set
            {
                _timerVisibility = value;
                OnPropertyChanged("TimerVisibility");
            }
        }
        public bool Waiting
        {
            get => _waiting;
            set
            {
                _waiting = value;
                OnPropertyChanged("Waiting");
            }
        }


        public RelayCommand CheckResult
        {
            get => _checkResult ??
                (_checkResult = new RelayCommand(obj =>
                 {
                     string result = obj as string;
                     if(result == _rightWordPair.Translation)
                     {
                         RightResult();
                     }
                     else
                     {
                         WrongResult();
                     }
                 }));
        }
        public RelayCommand BackButton
        {
            get => _backButton ??
                ( _backButton = new RelayCommand(obj =>
                 {
                     _owner.BackAndClean();
                 }));
        }


        private void WrongResult()
        {
            UpdateStatistics(false);
        }
        private void RightResult()
        {
            Waiting = false;
            RightOutput = _owner.RightOutput();
            TimerVisibility = Visibility.Visible;
            UpdateStatistics(true);
            GoToTheNextTest();
        }
        private void UpdateStatistics(bool result)
        {
            if(result)
            {
                _rightWordPair.AmountRepetiotion++;
                _oneSessionStatistics.UpdateOneWordStatistic(_rightWordPair);
                _oneSessionStatistics.AddExp(_rightWordPair);
            }
            else
            {
                _oneSessionStatistics.AddAmountErrors();
                _rightWordPair.AmountErrors++;
            }
            
        }
        private void UpdateTest()
        {
            List<OneWordPair> WordPairs;
            _currentCollection = Test.SetRandomCurrentCollection(_random, _collections);
            WordPairs = Test.GetWordPairs(_random, AmointButtons, _currentCollection);
            _rightWordPair = Test.GetRandomAnswer(_random, WordPairs);
            Word = _rightWordPair.Word;
            TopLeft = SetForButton(WordPairs);
            TopRight = SetForButton(WordPairs);
            BottomLeft = SetForButton(WordPairs);
            BottomRight = SetForButton(WordPairs);
        }
        private string SetForButton(List<OneWordPair> wordPairs)
        {
            string button = null;
            int index = _random.Next(0, wordPairs.Count);
            if ( wordPairs.Count > 0 )
            {
                OneWordPair wordPair = wordPairs[index];
                button = wordPair.Translation;
                wordPairs.Remove(wordPair);
            }
            return button;
        }
        private void GoToTheNextTest()
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
            if ( Timer < 0 )
            {
                _timer.Stop();
                _owner.NextTest();
            }
        }
    }
}
