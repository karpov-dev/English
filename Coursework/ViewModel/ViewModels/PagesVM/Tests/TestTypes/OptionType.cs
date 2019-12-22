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
        private Settings _settings;
        private ObservableCollection<OneCollection> _collections;
        private OneWordPair _rightWordPair;

        private Visibility _timerVisibility;
        private DispatcherTimer _timer;
        private int _timerToNextPage;
        private int _secondsToNextTest;

        private string _topLeft;
        private string _topRight;
        private string _bottomLeft;
        private string _bottomRight;
        private string _rightOutput;

        private string _word;
        private string _translate;

        private bool _waiting;

        private RelayCommand _backButton;
        private RelayCommand _checkResult;


        public OptionType (Random random, ObservableCollection<OneCollection> userCollections, 
            TestManager owner, OneSessionStatistics oneSessionStatistics, Settings testSettings)
        {
            _timerVisibility = Visibility.Hidden;
            _waiting = true;
            _settings = testSettings;
            _oneSessionStatistics = oneSessionStatistics;
            _collections = userCollections;
            _owner = owner;
            _secondsToNextTest = testSettings.NextTestTime;
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
                     if(result == _translate)
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
            OneCollection _currentCollection = Test.SetRandomCurrentCollection(_collections);
            List<OneWordPair> WordPairs = Test.GetWordPairs(AmointButtons, _currentCollection);
            _rightWordPair = Test.GetRandomAnswer(WordPairs);
            if (_settings.FullTest && Test.GetSwap())
            {
                _word = _rightWordPair.Word;
                _translate = _rightWordPair.Translation;
                SetButtons(GetTranslations(WordPairs));
            }
            else
            {
                _word = _rightWordPair.Translation;
                _translate = _rightWordPair.Word;
                SetButtons(GetWords(WordPairs));
            }
        }

        private void SetButtons(List<string> answers)
        {
            BottomLeft = GetRandomStringAndRemove(answers);
            BottomRight = GetRandomStringAndRemove(answers);
            TopLeft = GetRandomStringAndRemove(answers);
            TopRight = GetRandomStringAndRemove(answers);
        }
        private string GetRandomStringAndRemove(List<string> strings)
        {
            int index = Test.GetRandomNumber(0, ( strings.Count - 1 ));
            string randomString = strings[index];
            strings.RemoveAt(index);
            return randomString;
        }
        private List<string> GetTranslations(List<OneWordPair> pairs)
        {
            List<string> translations = new List<string>();
            for(int i = 0; i < pairs.Count; i++ )
            {
                translations.Add(pairs[i].Translation);
            }
            return translations;
        }
        private List<string> GetWords(List<OneWordPair> pairs)
        {
            List<string> words = new List<string>();
            for ( int i = 0; i < pairs.Count; i++ )
            {
                words.Add(pairs[i].Word);
            }
            return words;
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
