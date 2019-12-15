using System.Collections.ObjectModel;
using Coursework.Models;
using Coursework.ViewModel.NavigateBase;
using Coursework.ViewModel.ViewModels.PagesVM.Tests.TestTypes;
using Coursework.Models.Classes.Commands;
using Coursework.Models.Classes.User.WordCollections;
using Coursework.Models.Classes.User.WordCollections.WordPair;
using Coursework.ViewModel.MangerOfNavigate;

namespace Coursework.ViewModel.ViewModels.PagesVM.Tests.TestsManager
{
    class TestManager : NavigateVM
    {
        private User _currentUser;

        private ObservableCollection<OneCollection> _selectedCollections;

        private RelayCommand _backButton;
        private RelayCommand _optionTypeButton;
        private RelayCommand _spellingTypeButton;


        public TestManager(User user, NavigateManager navigateManager) : base(navigateManager)
        {
            _currentUser = user;
            _selectedCollections = GetSelectedCollections(_currentUser.Collections.GetCollections());
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
                         Manager.CurrentViewModel = new OptionType(_currentUser.Statistics, _selectedCollections, this);
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
                         Manager.CurrentViewModel = new SpellingType(_currentUser.Statistics, _selectedCollections, this);
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

    }
}
