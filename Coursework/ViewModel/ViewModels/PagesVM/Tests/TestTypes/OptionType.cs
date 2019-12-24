using System;
using System.Windows;
using System.Windows.Threading;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Media;
using Coursework.Models.Classes.Events;
using Coursework.Models.Classes.Commands;
using Coursework.ViewModel.ViewModels.PagesVM.Tests.TestsManager;
using Coursework.Models.Classes.User.WordCollections.WordPair;
using Coursework.Models.Classes.User.WordCollections;
using Coursework.Models.Classes.Test;
using Coursework.Models.Classes.User.Information;
using Coursework.Models;

namespace Coursework.ViewModel.ViewModels.PagesVM.Tests.TestTypes
{
    class OptionType : Event
    {
        private const int AmointButtons = 4;
        private const int CoinsForRightResult = 3;

        private TestManager _owner;
        private User _user;
        private OneSessionStatistics _oneSessionStatistics;
        private ObservableCollection<OneCollection> _collections;
        private OneWordPair _rightWordPair;
        private SolidColorBrush _outputForeground;

        private Visibility _timerVisibility;

        private string _topLeft;
        private string _topRight;
        private string _bottomLeft;
        private string _bottomRight;
        private string _rightOutput;

        private string _word;
        private string _translate;

        private bool _waiting;
        private bool _translateWordButtonEnabled;
        private bool _usingHelp;

        private RelayCommand _backButton;
        private RelayCommand _checkResult;
        private RelayCommand _translateWordButton;
        private RelayCommand _listenWord;


        public OptionType(ObservableCollection<OneCollection> userCollections, TestManager owner, 
            OneSessionStatistics oneSessionStatistics, User user)
        {
            _usingHelp = false;
            _user = user;
            _timerVisibility = Visibility.Hidden;
            _waiting = true;
            _oneSessionStatistics = oneSessionStatistics;
            _collections = userCollections;
            _owner = owner;
            TranslateWordButtonEnabled = true;
            UpdateTest();
        }


        public int AmountCoins => _owner.UserCoins;
        public int AmountExp => _owner.UserExp;
        public int TranslateWordCost => _owner.TranslateConst;
        public int AudioValue => _owner.AudioValue;
        public string CurrentTest => _owner.CurrentTestString;
        public TestManager GetOwner => _owner;
        

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
        public string OutputResult
        {
            get => _rightOutput;
            set
            {
                _rightOutput = value;
                OnPropertyChanged("OutputResult");
            }
        }
        public SolidColorBrush OutputForeground
        {
            get => _outputForeground;
            set
            {
                _outputForeground = value;
                OnPropertyChanged("OutputForeground");
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
        public bool TranslateWordButtonEnabled
        {
            get
            {
                if(AmountCoins < TranslateWordCost )
                {
                    return false;
                }
                else
                {
                    return _translateWordButtonEnabled;
                }
            }
            set
            {
                _translateWordButtonEnabled = value;
                OnPropertyChanged("TranslateWordButtonEnabled");
            }
        }


        public RelayCommand CheckResult
        {
            get => _checkResult ??
                (_checkResult = new RelayCommand(obj =>
                 {
                     string result = obj as string;
                     Waiting = false;
                     if (result == _translate)
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
        public RelayCommand TranslateWordButton
        {
            get
            {
                return _translateWordButton ??
                    ( _translateWordButton = new RelayCommand(obj =>
                     {
                         if(AmountCoins >= TranslateWordCost )
                         {
                             _usingHelp = true;
                             Word = _translate;
                             _user.Information.GetCoins.Subtract(TranslateWordCost);
                             TranslateWordButtonEnabled = false;
                             OnPropertyChanged("AmountCoins");
                         }
                     }) );
            }
        }
        public RelayCommand ListenWord
        {
            get
            {
                return _listenWord ??
                    ( _listenWord = new RelayCommand(obj =>
                     {
                         SpeechSynth.Speech(Word, AudioValue);
                     }) );
            }
        }


        private void WrongResult()
        {
            OutputResult = _owner.WrongOutput();
            TimerVisibility = Visibility.Visible;
            
            OutputForeground = new SolidColorBrush(Colors.Red);

            _oneSessionStatistics.AddAmountErrors();
            _rightWordPair.AddAmountErrors();
            _user.Statistics.AddWrongs(1);

            _owner.StartTimerToNextTest();
        }
        private void RightResult()
        {
            OutputResult = _owner.RightOutput();
            TimerVisibility = Visibility.Visible;

            if(!_usingHelp )
            {
                _user.Statistics.AddExpForRightWord(Word, _translate);
                _user.Information.GetCoins.Add(CoinsForRightResult);
            }
            _rightWordPair.AddAmountRepetition();
            _user.Statistics.AddRepitedWords(1);
            _oneSessionStatistics.UpdateOneWordStatistic(_rightWordPair);
            _oneSessionStatistics.AddExp(_user.Statistics.ExpFormula(Word, _translate));
            

            OutputForeground = new SolidColorBrush(Colors.Green);
            _owner.StartTimerToNextTest();
        }
        private void UpdateTest()
        {
            OneCollection _currentCollection = Test.SetRandomCurrentCollection(_collections);
            List<OneWordPair> WordPairs = Test.GetWordPairs(AmointButtons, _currentCollection);
            _rightWordPair = Test.GetRandomAnswer(WordPairs);
            if (_user.Information.FullTest && Test.GetSwap())
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
    }
}
