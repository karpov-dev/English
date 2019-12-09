using System.Collections.ObjectModel;
using Coursework.Models;
using Coursework.ViewModel.NavigateBase;
using Coursework.ViewModel.MangerOfNavigate;
using Coursework.Models.Classes.Commands;
using Coursework.Models.Classes.User.WordCollections;

namespace Coursework.ViewModel.ViewModels.PagesVM
{
    class MainPageVM : NavigateVM
    {
        private RelayCommand _addNewCollectionCommand;
        private RelayCommand _deleteCollectionCommand;
        private User _currentUser;

        public MainPageVM(string viewModelName, NavigateManager manager) : base(viewModelName, manager)
        {
            _currentUser = new User();
        }

        public OneCollection SelectedOneCollection { get; set; }
        public ObservableCollection<OneCollection> UserCollections => _currentUser.Collections.GetCollections();

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
                        _currentUser.Collections.DeleteCollection(SelectedOneCollection);
                    }));
            }
        }
    }
}
