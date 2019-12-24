using System.Windows;
using Coursework.ViewModel.MangerOfNavigate;
using Coursework.ViewModel.NavigateBase;
using Coursework.Models;
using Coursework.Models.Classes.Commands;
using Coursework.Models.Classes.User.Information;
using Coursework.ViewModel.ViewModels.PagesVM.Develop;
using Coursework.Models.Classes.Enternet;

namespace Coursework.ViewModel.ViewModels.PagesVM.ApplictionSettings
{
    class AppSettings : NavigateVM
    {
        private User _user;
        private RelayCommand _deleteAllCollections;
        private RelayCommand _deleteStatistics;
        private RelayCommand _deleteProgress;
        private RelayCommand _resetSettings;
        private RelayCommand _backButton;
        private RelayCommand _setMode;
        private RelayCommand _editGoodReaction;
        private RelayCommand _editBadReaction;
        private RelayCommand _checkEnternetConnection;

        public AppSettings(string viewModelName, NavigateManager navigateManager, User user) : base(viewModelName, navigateManager)
        {
            _user = user;
        }

        public Settings GetSettings => _user.Information;
        public string Mode
        {
            get
            {
                if(_user.Administrator)
                {
                    return Properties.Resources.SetUserMode;
                }
                else
                {
                    return Properties.Resources.SetGodMode;
                }
            }
        }


        public RelayCommand DeleteAllCollections
        {
            get
            {
                return _deleteAllCollections ??
                    ( _deleteAllCollections = new RelayCommand(obj =>
                     {
                         _user.Collections.DeleteAllCollections();
                         OnPropertyChanged("DeleteAllCollectionsEnabled");
                     }) );
            }
        }
        public RelayCommand DeleteStatistics
        {
            get
            {
                return _deleteStatistics ??
                    (_deleteStatistics = new RelayCommand(obj =>
                     {
                         _user.Statistics.ClearStatistics();
                     }));
            }
        }
        public RelayCommand DeleteProgress
        {
            get
            {
                return _deleteProgress ??
                    ( _deleteProgress = new RelayCommand(obj =>
                    {
                        _user.Clear();
                        Manager.CurrentViewModel = new HelloPageVM(_user.Information, "", Manager);
                    }) );
            }
        }
        public RelayCommand ResetSettings
        {
            get
            {
                return _resetSettings ??
                    ( _resetSettings = new RelayCommand(obj =>
                     {
                         _user.Information.Reset();
                     }) );
            }
        }
        public RelayCommand SetMode
        {
            get
            {
                return _setMode ??
                    ( _setMode = new RelayCommand(obj =>
                     {
                         if(_user.Administrator)
                         {
                             _user.Administrator = false;
                         }
                         else
                         {
                             _user.Administrator = true;
                         }
                         OnPropertyChanged("Mode");
                         OnPropertyChanged("DeveloperFunctionsVisibility");
                     }) );
            }
        }
        public RelayCommand EditGoodReaction
        {
            get
            {
                return _editGoodReaction ??
                    ( _editGoodReaction = new RelayCommand(obj =>
                     {
                         Manager.CurrentViewModel = new TestReactions(Manager, _user.Information.GoodReactions, "Good Reaction Edit");
                     }) );
            }
        }
        public RelayCommand EditBadReaction
        {
            get
            {
                return _editBadReaction ??
                    ( _editBadReaction = new RelayCommand(obj =>
                    {
                        Manager.CurrentViewModel = new TestReactions(Manager, _user.Information.BadReaction, "Bad Reaction Edit");
                    }) );
            }
        }
        public RelayCommand CheckEnternetConnection
        {
            get
            {
                return _checkEnternetConnection ??
                    (_checkEnternetConnection = new RelayCommand(obj =>
                     {
                         _user.Information.UpdateInternetConnection();
                     }));
            }
        }
        public RelayCommand BackButton
        {
            get
            {
                return _backButton ??
                    ( _backButton = new RelayCommand(obj =>
                    {
                        if(_user.Information.Name == "")
                        {
                            ErrorMessage(Properties.Resources.NameCantBeEmpty);
                        }
                        else
                        {
                            Manager.GoTo("MainPageVM");
                        }
                    }) );
            }
        }

        public bool DeleteAllCollectionsEnabled
        {
            get
            {
                if ( _user.Collections.Count >= 1 )
                    return true;
                else
                    return false;
            }
        }
        public Visibility DeveloperFunctionsVisibility
        {
            get
            {
                if(_user.Administrator)
                {
                    return Visibility.Visible;
                }
                else
                {
                    return Visibility.Collapsed;
                }
            }
        }
    }
}
