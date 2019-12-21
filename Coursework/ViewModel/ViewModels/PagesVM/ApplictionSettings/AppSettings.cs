using Coursework.ViewModel.MangerOfNavigate;
using Coursework.ViewModel.NavigateBase;
using Coursework.Models;
using Coursework.Models.Classes.Commands;
using Coursework.Models.Classes.User.Information;

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

        public AppSettings(string viewModelName, NavigateManager navigateManager, User user) : base(viewModelName, navigateManager)
        {
            _user = user;
        }

        public Settings GetSettings => _user.Information;

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
    }
}
