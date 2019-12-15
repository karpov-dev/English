using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    class OptionType : TestBase
    {
        private const int AmointButtons = 4;

        private TestManager _owner;
        private UserStatistics _statistic;

        private string _word;
        private string _translation;
        private string _topLeft;
        private string _topRight;
        private string _bottomLeft;
        private string _bottomRight;

        private RelayCommand _backButton;
        private RelayCommand _checkResult;


        public OptionType (UserStatistics userStatistics, ObservableCollection<OneCollection> userCollections, TestManager owner)
            : base(userCollections)
        {
            _owner = owner;
            _statistic = userStatistics;
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


        public RelayCommand CheckResult
        {
            get => _checkResult ??
                (_checkResult = new RelayCommand(obj =>
                 {
                     string result = obj as string;
                     if(result == _translation)
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
                     _owner.Manager.CurrentViewModel = _owner;
                 }));
        }


        private void RightResult()
        {
            int amountExp = _translation.Length;
            _statistic.LearnedWords();
            _statistic.Experience(amountExp);
            UpdateTest();
        }
        private void WrongResult()
        {

        }
        private void UpdateTest()
        {
            List<OneWordPair> WordPairs;
            OneWordPair _rightWordPair;
            _currentCollection = SetRandomCurrentCollection(_collections);
            WordPairs = GetWordPairs(AmointButtons, _currentCollection);
            _rightWordPair = GetRandomAnswer(WordPairs);
            _translation = _rightWordPair.Translation;
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
    }
}
