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
    class CardsType : Event
    {
        private ObservableCollection<OneCollection> _collections;
        private TestManager _owner;
        private OneSessionStatistics _oneSessionStatistics;
        private User _user;

        private RelayCommand _backButton;
        private RelayCommand _know;
        private RelayCommand _dontKnow;
        private RelayCommand _translateWord;

        private string _word;
        private string _tempWord;
        private string _translation;

        public CardsType(ObservableCollection<OneCollection> userCollections, TestManager owner,
            OneSessionStatistics oneSessionStatistics, User user)
        {
            _collections = userCollections;
            _owner = owner;
            _oneSessionStatistics = oneSessionStatistics;
            _user = user;
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


        public RelayCommand BackButton
        {
            get => _backButton ??
                ( _backButton = new RelayCommand(obj =>
                {
                    _owner.BackAndClean();
                }) );
        }
        public RelayCommand DontKnow
        {
            get
            {
                return _dontKnow ??
                    ( _dontKnow = new RelayCommand(obj =>
                    {
                        _owner.AddTest(this);
                        _owner.SetNextTest();
                    }) );
            }
        }
        public RelayCommand Know
        {
            get
            {
                return _know ??
                    ( _know = new RelayCommand(obj =>
                     {
                         _owner.SetNextTest();
                     }) );
            }
        }
        public RelayCommand TranslateWord
        {
            get
            {
                return _translateWord ??
                    ( _translateWord = new RelayCommand(obj =>
                     {
                         if(_translation != Word)
                         {
                             Word = _translation;
                         }
                         else
                         {
                             Word = _tempWord;
                         }

                     }) );
            }
        }


        private void UpdateTest()
        {
            OneWordPair pair = Test.GetWordPair(Test.SetRandomCurrentCollection(_collections));
            if(_user.Information.FullTest && Test.GetSwap())
            {
                Word = pair.Translation;
                _translation = pair.Word;
            }
            else
            {
                Word = pair.Word;
                _translation = pair.Translation;
            }
            _tempWord = Word;
        }
    }
}
