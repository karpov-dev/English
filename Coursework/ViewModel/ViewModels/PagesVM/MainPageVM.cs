using System.Collections.ObjectModel;
using Coursework.Models;
using Coursework.ViewModel.NavigateBase;
using Coursework.ViewModel.MangerOfNavigate;
using Coursework.Models.Classes.Commands;
using Coursework.Models.Classes.User.WordCollections;
using Coursework.ViewModel.ViewModels.PagesVM.Tests.TestsManager;
using Coursework.Models.Classes.User.Statistics;
using Coursework.ViewModel.ViewModels.PagesVM.Notification;
using Coursework.Views.Notification;
using Coursework.ViewModel.ViewModels.PagesVM.ApplictionSettings;

namespace Coursework.ViewModel.ViewModels.PagesVM
{
    class MainPageVM : NavigateVM
    {
        private User _currentUser;
        private RelayCommand _addNewCollectionCommand;
        private RelayCommand _deleteCollectionCommand;
        private RelayCommand _editCollectionCommand;
        private RelayCommand _goToTest;
        private RelayCommand _settingsCommand;
        private OneCollection _selectedCollection;

        public MainPageVM(string viewModelName, NavigateManager manager, User user) : base(viewModelName, manager)
        {
            _currentUser = user;
            if(_currentUser.Information.Name == null)
            {
                Manager.CurrentViewModel = new HelloPageVM(_currentUser.Information, "HelloPageVM", manager);
            }
        }


        public ObservableCollection<OneCollection> UserCollections => _currentUser.Collections.GetCollections();
        public OneCollection SelectedCollection
        {
            get => _selectedCollection;
            set
            {
                _selectedCollection = value;
                OnPropertyChanged("SelectedCollection");
                OnPropertyChanged("EditButtonEnabled");
            }
        }


        public string UserName => _currentUser.Information.Name;
        public UserStatistics TotalUserStatistic => _currentUser.Statistics;
        public StatisticsForTheDay CurrentDayStatistics => _currentUser.Statistics.DayStatistics();
        public string UserImagePath
        {
            get
            {
                int numberOfImage = 0;
                string imagePath = null;
                if ( _currentUser.Statistics.CurrentLevel > 0 && _currentUser.Statistics.CurrentLevel <= 3 )
                    numberOfImage = 1;
                if ( _currentUser.Statistics.CurrentLevel > 3 && _currentUser.Statistics.CurrentLevel <= 7 )
                    numberOfImage = 2;
                if ( _currentUser.Statistics.CurrentLevel > 7 && _currentUser.Statistics.CurrentLevel <= 10 )
                    numberOfImage = 3;
                if ( _currentUser.Statistics.CurrentLevel > 10 && _currentUser.Statistics.CurrentLevel <= 15 )
                    numberOfImage = 4;
                if ( _currentUser.Statistics.CurrentLevel > 15 && _currentUser.Statistics.CurrentLevel <= 25 )
                    numberOfImage = 5;
                if ( _currentUser.Statistics.CurrentLevel > 25 && _currentUser.Statistics.CurrentLevel <= 30 )
                    numberOfImage = 6;
                
                switch(numberOfImage)
                {
                    case 1:
                        {
                            imagePath = "pack://application:,,,/Resources/scout.png";
                            break;
                        }
                    case 2:
                        {
                            imagePath = "pack://application:,,,/Resources/explorer.png";
                            break;
                        }
                    case 3:
                        {
                            imagePath = "pack://application:,,,/Resources/adventurer.png";
                            break;
                        }
                    case 4:
                        {
                            imagePath = "pack://application:,,,/Resources/mountaineer.png";
                            break;
                        }
                    case 5:
                        {
                            imagePath = "pack://application:,,,/Resources/expeditioner.png";
                            break;
                        }
                    case 6:
                        {
                            imagePath = "pack://application:,,,/Resources/ranger.png";
                            break;
                        }
                    default:
                        {
                            imagePath = "pack://application:,,,/Resources/scout.png";
                            break;
                        }
                }
                return imagePath;
            }
        }


        public RelayCommand AddNewCollection
        {
            get
            {
                return _addNewCollectionCommand ??
                    (_addNewCollectionCommand = new RelayCommand(obj =>
                    {
                        Manager.CurrentViewModel = new AddingNewCollectionPageVM(_currentUser.Collections, "AddingNewCollectionPageVM", Manager);
                    }));
            }
        }
        public RelayCommand DeleteCollection
        {
            get
            {
                return _deleteCollectionCommand ??
                    (_deleteCollectionCommand = new RelayCommand(obj =>
                    {
                        UserCollections.Remove(obj as OneCollection);
                    }));
            }
        }
        public RelayCommand EditCollectionCommand
        {
            get
            {
                return _editCollectionCommand ??
                    (_editCollectionCommand = new RelayCommand(obj =>
                    {
                        OneCollection collection = obj as OneCollection;
                        Manager.CurrentViewModel = new EditCollectionPageVM(collection, Manager);
                    }));
            }
        }
        public RelayCommand GoToTest
        {
            get
            {
                return _goToTest ??
                    (_goToTest = new RelayCommand(obj =>
                    {
                        if(CheckCollectionsForChecked(_currentUser.Collections.GetCollections()) 
                        && CheckCollectionsForNull(_currentUser.Collections.GetCollections()) )
                        {
                            Manager.CurrentViewModel = new TestManager(_currentUser, Manager);
                        }
                        else
                        {
                            InformationMessage(Properties.Resources.WithoutCheckedCollections);
                        }
                    }));
            }
        }
        public RelayCommand Settings
        {
            get
            {
                return _settingsCommand ??
                    (_settingsCommand = new RelayCommand(obj =>
                     {
                         Manager.GoTo("Settings");
                     }));
            }
        }


        public bool EditButtonEnabled
        {
            get
            {
                if ( _selectedCollection != null )
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }


        private bool CheckCollectionsForNull(ObservableCollection<OneCollection> collections)
        {
            if ( collections == null )
            {
                return false;
            }
            return true;
        }
        private bool CheckCollectionsForChecked(ObservableCollection<OneCollection> collections)
        {
            for ( int i = 0; i < collections.Count; i++ )
            {
                if ( collections[i].IsChecked )
                {
                    return true;
                }
            }
            return false;
        }
    }
}
