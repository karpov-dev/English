﻿using System;
using System.Collections.ObjectModel;
using Coursework.Models;
using Coursework.ViewModel.NavigateBase;
using Coursework.ViewModel.ViewModels.PagesVM.Tests.TestTypes;
using Coursework.Models.Classes.Commands;
using Coursework.Models.Classes.User.WordCollections;
using Coursework.ViewModel.MangerOfNavigate;
using Coursework.Models.Classes.Test;

namespace Coursework.ViewModel.ViewModels.PagesVM.Tests.TestsManager
{
    class TestManager : NavigateVM
    {
        private const int AmointTests = 10;
        private const int AmountTypes = 2;

        private User _currentUser;
        private Random _random;
        private OneSessionStatistics _oneSessionStatistics;

        private ObservableCollection<OneCollection> _selectedCollections;
        private ObservableCollection<object> _tests;
        private ObservableCollection<string> _rightOutputs;
        private int _currentTest;

        private RelayCommand _backButton;
        private RelayCommand _optionTypeButton;
        private RelayCommand _spellingTypeButton;
        private RelayCommand _mixedTypeButton;


        public TestManager(User user, NavigateManager navigateManager) : base(navigateManager)
        {
            SetRightOutputs();
            _currentUser = user;
            _currentTest = 0;
            _selectedCollections = GetSelectedCollections(_currentUser.Collections.GetCollections());
        }

        public void NextTest()
        {
            if(_tests.Count > _currentTest)
            {
                Manager.CurrentViewModel = _tests[_currentTest];
                _currentTest++;
            }
            else
            {
                Manager.CurrentViewModel = new SessionResultPage(_oneSessionStatistics, _currentUser.Statistics, this);
                _currentTest = 0;
                _tests = null;
                _oneSessionStatistics = null;
            }
        }
        public void BackAndClean()
        {
            _tests = null;
            _oneSessionStatistics = null;
            _currentTest = 0;
            Manager.CurrentViewModel = this;
        }
        public string RightOutput()
        {
            int index = _random.Next(_rightOutputs.Count);
            return _rightOutputs[index];
        }

        public RelayCommand BackButton
        {
            get
            {
                return _backButton ??
                    ( _backButton = new RelayCommand(obj =>
                    {
                        Manager.GoTo("MainPageVM");
                    }) );
            }
        }
        public RelayCommand OptionTypeButton
        {
            get
            {
                return _optionTypeButton ??
                    ( _optionTypeButton = new RelayCommand(obj =>
                     {
                         _tests = new ObservableCollection<object>();
                         _oneSessionStatistics = new OneSessionStatistics();
                         for ( int i = 0; i < AmointTests; i++ )
                         {
                             _random = new Random(DateTime.Now.Millisecond + i);
                             _tests.Add(new OptionType(_random, _selectedCollections, this, _oneSessionStatistics));
                         }
                         NextTest();
                     }));
            }
        }
        public RelayCommand SpellingTypeButton
        {
            get
            {
                return _spellingTypeButton ??
                    (_spellingTypeButton = new RelayCommand(obj =>
                     {
                         _tests = new ObservableCollection<object>();
                         _oneSessionStatistics = new OneSessionStatistics();
                         for (int i = 0; i < AmointTests; i++ )
                         {
                             _random = new Random(DateTime.Now.Millisecond + i);
                             _tests.Add(new SpellingType(_random, _selectedCollections, this, _oneSessionStatistics));
                         }
                         NextTest();
                     }));
            }
        }
        public RelayCommand MixedTypeButton
        {
            get
            {
                return _mixedTypeButton ??
                    (_mixedTypeButton = new RelayCommand(obj =>
                     {
                         _tests = new ObservableCollection<object>();
                         _oneSessionStatistics = new OneSessionStatistics();
                         for (int i = 0; i < AmointTests; i++ )
                         {
                             _random = new Random(DateTime.Now.Millisecond + i);
                             int type = _random.Next(AmountTypes);
                             switch(type)
                             {
                                 case 0:
                                     {
                                         _tests.Add(new OptionType(_random, _selectedCollections, this, _oneSessionStatistics));
                                         break;
                                     }
                                 case 1:
                                     {
                                         _tests.Add(new SpellingType(_random, _selectedCollections, this, _oneSessionStatistics));
                                         break;
                                     }
                             }
                         }
                         NextTest();
                     }));
            }
        }


        private ObservableCollection<OneCollection> GetSelectedCollections(ObservableCollection<OneCollection> userCollections)
        {
            ObservableCollection<OneCollection> selectedCollections = new ObservableCollection<OneCollection>();
            for(int i = 0; i < userCollections.Count; i++ )
            {
                if(userCollections[i].IsChecked == true)
                {
                    selectedCollections.Add(userCollections[i]);
                }
            }
            return selectedCollections;
        }
        private void SetRightOutputs()
        {
            _rightOutputs = new ObservableCollection<string>();
            _rightOutputs.Add(Properties.Resources.Good);
            _rightOutputs.Add(Properties.Resources.GoodJob);
            _rightOutputs.Add(Properties.Resources.VeryGood);
            _rightOutputs.Add(Properties.Resources.KeepItUp);
            _rightOutputs.Add(Properties.Resources.Delightfully);
        }
    }
}