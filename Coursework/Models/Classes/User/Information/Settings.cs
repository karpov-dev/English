using System;
using Coursework.Models.Classes.Events;
using System.Collections.ObjectModel;
using Coursework.Models.Classes.Enternet;
using Coursework.ViewModel.ViewModels.VM;

namespace Coursework.Models.Classes.User.Information
{
    [Serializable]
    class Settings : Event
    {
        private const int DeafultAmountTests = 10;
        private const int DefaultTestTime = 1;
        private const bool DefaultFullTest = true;
        private const bool DefaultOnlineTranslation = true;

        private string _name;
        private int _amountTests;
        private int _testTime;
        private bool _fullTest;
        private bool _onlineTranslation;

        public Settings()
        {
            GoodReactions = new ObservableCollection<ReactionVM>();
            BadReaction = new ObservableCollection<ReactionVM>();
        }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }
        public int AmountTests
        {
            get => _amountTests;
            set
            {
                _amountTests = value;
                OnPropertyChanged("AmountTests");
            }
        }
        public int NextTestTime
        {
            get => _testTime;
            set
            {
                _testTime = value;
                OnPropertyChanged("NextTestTime");
            }
        }
        public bool FullTest
        {
            get => _fullTest;
            set
            {
                _fullTest = value;
                OnPropertyChanged("FullTest");
            }
        }
        public bool OnlineTranslation
        {
            get => _onlineTranslation;
            set
            {
                _onlineTranslation = value;
                OnPropertyChanged("GiveTranslation");
            }
        }
        public bool OnlineTranslationAvailable => Connection.IsInternetAvailable();
        public ObservableCollection<ReactionVM> GoodReactions { get; private set; }
        public ObservableCollection<ReactionVM> BadReaction { get; private set; }
        

        public void Reset()
        {
            AmountTests = DeafultAmountTests;
            NextTestTime = DefaultTestTime;
            FullTest = DefaultFullTest;
            OnlineTranslation = DefaultOnlineTranslation;
        }
        public void UpdateInternetConnection()
        {
            OnPropertyChanged("OnlineTranslation");
            OnPropertyChanged("OnlineTranslationAvailable");
        }
    }
}
