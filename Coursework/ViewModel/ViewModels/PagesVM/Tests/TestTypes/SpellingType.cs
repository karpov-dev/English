using System;
using System.Windows;
using System.Globalization;
using System.Collections.ObjectModel;
using Coursework.Models.Classes.Events;
using Coursework.Models.Classes.Commands;
using Coursework.Models;
using Coursework.ViewModel.ViewModels.PagesVM.Tests.TestsManager;
using Coursework.Models.Classes.User.WordCollections.WordPair;
using Coursework.Models.Classes.User.WordCollections;
using Coursework.Models.Classes.Test;


namespace Coursework.ViewModel.ViewModels.PagesVM.Tests.TestTypes
{
    class SpellingType : Event
    {
        private const int CoinsForRightResult = 5;

        private User _user;
        private OneSessionStatistics _oneSessionStatistics;
        private TestManager _owner;
        private OneWordPair _wordPair;
        private OneCollection _currentCollection;
        private ObservableCollection<OneCollection> _userCollections;
        private Visibility _timerVisibility;

        private string _userInput;
        private string _textResult;
        
        private int _amountRightSymbols;
        private bool _expectation;
        private bool _translateWordButtonEnabled;

        private string _translation;
        private string _word;

        private RelayCommand _backButton;
        private RelayCommand _translateWordButton;
        private RelayCommand _listenWord;
        

        public SpellingType(ObservableCollection<OneCollection> userCollections, 
            TestManager owner, OneSessionStatistics oneSessionStatistics, User user)
        {
            _user = user;
            _translateWordButtonEnabled = true;
            _oneSessionStatistics = oneSessionStatistics;
            _timerVisibility = Visibility.Hidden;
            _expectation = true;
            _userCollections = userCollections;
            _owner = owner;
            UpdateTest();
        }


        public int AmountCoins => _owner.UserCoins;
        public int AmountExp => _owner.UserExp;
        public int TranslateWordCost => _owner.SpellingTranslateCost;
        public int AudioValue => _owner.AudioValue;
        public string CurrentTest => _owner.CurrentTestString;
        public TestManager GetOwner => _owner;


        public int TranslationLenght => _translation.Length;
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
        public bool TranslateWordButtonEnabled
        {
            get
            {
                if(AmountCoins < TranslateWordCost)
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
        public RelayCommand TranslateWordButton
        {
            get
            {
                return _translateWordButton ??
                    ( _translateWordButton = new RelayCommand(obj =>
                    {
                        if(AmountCoins >= TranslateWordCost)
                        {
                            _user.Information.GetCoins.Subtract(TranslateWordCost);
                            if(_translation.Length >= _amountRightSymbols)
                            {
                                UserInput += Convert.ToString(_translation[_amountRightSymbols]);
                            }
                            OnPropertyChanged("AmountCoins");
                            if( AmountCoins < TranslateWordCost )
                            {
                                TranslateWordButtonEnabled = false;
                            }
                        }
                        else
                        {
                            TranslateWordButtonEnabled = false;
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


        private void UpdateTest()
        {
            _currentCollection = Test.SetRandomCurrentCollection(_userCollections);
            _wordPair = Test.GetWordPair(_currentCollection);
            if(_user.Information.FullTest && Test.GetSwap() )
            {
                Word = _wordPair.Translation;
                _translation = _wordPair.Word;
            }
            else
            {
                Word = _wordPair.Word;
                _translation = _wordPair.Translation;
            }
            Expectation = true;
        }
        private void CheckInput()
        {
            string translationToLower = _translation.ToLower();
            string wordToLower = UserInput.ToLower();
            if( translationToLower.Contains(wordToLower) )
            {
                AmountRightSymbols = wordToLower.Length;
            }
            else
            {
                AmountRightSymbols = 0;
            }

            if( translationToLower == wordToLower )
            {
                Expectation = false;
                RightResult();
            }
        }
        private void RightResult()
        {
            _user.Statistics.AddExpForRightWord(Word, _translation);
            _user.Statistics.AddRepitedWords(1);
            _wordPair.AddAmountRepetition();
            _oneSessionStatistics.UpdateOneWordStatistic(_wordPair);
            _oneSessionStatistics.AddExp(_user.Statistics.ExpFormula(Word, _translation));
            _user.Information.GetCoins.Add(CoinsForRightResult);

            TextResult = _owner.RightOutput();
            TimerVisibility = Visibility.Visible;
            Expectation = false;
            _owner.StartTimerToNextTest();
        }
    }
}
