using System;
using System.Collections.ObjectModel;
using System.Windows.Threading;
using Coursework.Models;
using Coursework.ViewModel.NavigateBase;
using Coursework.ViewModel.ViewModels.PagesVM.Tests.TestTypes;
using Coursework.Models.Classes.Commands;
using Coursework.Models.Classes.User.WordCollections;
using Coursework.ViewModel.MangerOfNavigate;
using Coursework.Models.Classes.Test;
using Coursework.Models.Classes.User.WordCollections.WordPair;

namespace Coursework.ViewModel.ViewModels.PagesVM.Tests.TestsManager
{
    class TestManager : NavigateVM
    {
        private const int AmountTypes = 2;

        private User _currentUser;
        private Random _random;
        private OneSessionStatistics _oneSessionStatistics;
        private DispatcherTimer _timer;

        private ObservableCollection<OneCollection> _selectedCollections;
        private ObservableCollection<object> _tests;
        private int _currentTest;
        private int _amountTests;
        private int _currentTestForString;
        private int _timerValue;

        private RelayCommand _backButton;
        private RelayCommand _optionTypeButton;
        private RelayCommand _spellingTypeButton;
        private RelayCommand _mixedTypeButton;
        private RelayCommand _cardTypeButton;


        public TestManager(User user, NavigateManager navigateManager) : base(navigateManager)
        {
            _currentUser = user;
            _currentTest = 0;
            _amountTests = user.Information.AmountTests;
            _selectedCollections = GetSelectedCollections(_currentUser.Collections.GetCollections());
            if(_selectedCollections.Count < 1)
            {
                TestsEnabled = false;
            }
            else
            {
                TestsEnabled = true;
            }

        }

        public void StartTimerToNextTest()
        {
            TimerValue = _currentUser.Information.NextTestTime;
            _timer = new DispatcherTimer();
            _timer.Tick += new EventHandler(TimerTick);
            _timer.Interval = new TimeSpan(0, 0, 1);
            _timer.Start();
        }
        public void SetNextTest()
        {
            if ( _tests.Count > _currentTest )
            {
                _currentTestForString = _currentTest;
                Manager.CurrentViewModel = _tests[_currentTest];
                _currentTest++;
            }
            else
            {
                Manager.CurrentViewModel = new SessionResultPage(_oneSessionStatistics, this);
                _currentTest = 0;
                _tests = null;
                _oneSessionStatistics = null;
            }
        }
        public void AddTest(object test)
        {
            _tests.Add(test);
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
            if(_currentUser.Information.GoodReactions.Count > 0)
            {
                int index = _random.Next(_currentUser.Information.GoodReactions.Count);
                return _currentUser.Information.GoodReactions[index].Reaction;
            }
            return "-1";
        }
        public string WrongOutput()
        {
            if ( _currentUser.Information.BadReaction.Count > 0 )
            {
                int index = _random.Next(_currentUser.Information.BadReaction.Count);
                return _currentUser.Information.BadReaction[index].Reaction;
            }
            return "-1";
        }


        public string CurrentTestString
        {
            get
            {
                string stringCurrentTest = Convert.ToString(_currentTestForString + 1);
                string stringMaxNumberOfTest = Convert.ToString(_tests.Count);
                string result = stringCurrentTest + " / " + stringMaxNumberOfTest;
                return result;
            }
        }
        public int TimerValue
        {
            get => _timerValue;
            set
            {
                _timerValue = value;
                OnPropertyChanged("TimerValue");
            }
        }


        public int UserCoins => _currentUser.Information.GetCoins.Count;
        public int UserExp => _currentUser.Statistics.TotalExperience;
        public int TranslateConst => _currentUser.Information.TranslateCost;
        public int SpellingTranslateCost => _currentUser.Information.SpellingTranslateCost;
        public int AudioValue => _currentUser.Information.AudioValue;
        public bool TestsEnabled { get; set; }


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
                         for ( int i = 0; i < _amountTests; i++ )
                         {
                             _random = new Random(DateTime.Now.Millisecond + i);
                             AddOptionTypeTest();
                         }
                         SetNextTest();
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
                         for (int i = 0; i < _amountTests; i++ )
                         {
                             _random = new Random(DateTime.Now.Millisecond + i);
                             AddSpellingTypeTest();
                         }
                         SetNextTest();
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
                         for (int i = 0; i < _amountTests; i++ )
                         {
                             _random = new Random(DateTime.Now.Millisecond + i);
                             int type = _random.Next(AmountTypes);
                             switch(type)
                             {
                                 case 0:
                                     {
                                         AddOptionTypeTest();
                                         break;
                                     }
                                 case 1:
                                     {
                                         AddSpellingTypeTest();
                                         break;
                                     }
                             }
                         }
                         SetNextTest();
                     }));
            }
        }
        public RelayCommand CardTypeButton
        {
            get
            {
                return _cardTypeButton ??
                    ( _cardTypeButton = new RelayCommand(obj =>
                     {
                         _tests = new ObservableCollection<object>();
                         _oneSessionStatistics = new OneSessionStatistics();
                         for(int i = 0; i < _amountTests; i++ )
                         {
                             _random = new Random(DateTime.Now.Millisecond + i);
                             AddCardTypeTest();
                         }
                         SetNextTest();
                     }) );
            }
        }


        private ObservableCollection<OneCollection> GetSelectedCollections(ObservableCollection<OneCollection> userCollections)
        {
            ObservableCollection<OneCollection> selectedCollections = new ObservableCollection<OneCollection>();
            for(int i = 0; i < userCollections.Count; i++ )
            {
                if(userCollections[i].IsChecked == true && userCollections[i].WordPair.Count > 5)
                {
                    selectedCollections.Add(userCollections[i]);
                }
            }
            return selectedCollections;
        }
        private void AddSpellingTypeTest()
        {
            _tests.Add(new SpellingType(_selectedCollections, this, _oneSessionStatistics, _currentUser));
        }
        private void AddOptionTypeTest()
        {
            _tests.Add(new OptionType( _selectedCollections, this, _oneSessionStatistics, _currentUser));
        }
        private void AddCardTypeTest()
        {
            _tests.Add(new CardsType(_selectedCollections, this, _oneSessionStatistics, _currentUser));
        }
        private void TimerTick(object sender, EventArgs e)
        {
            TimerValue--;
            if ( TimerValue < 0 )
            {
                _timer.Stop();
                SetNextTest();
            }
        }
    }
}
