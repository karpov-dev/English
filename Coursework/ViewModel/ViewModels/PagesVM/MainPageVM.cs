using System.Collections.ObjectModel;
using System.Windows;
using Coursework.Models;
using Coursework.ViewModel.NavigateBase;
using Coursework.ViewModel.MangerOfNavigate;
using Coursework.Models.Classes.Commands;
using Coursework.Models.Classes.User.WordCollections;

namespace Coursework.ViewModel.ViewModels.PagesVM
{
    class MainPageVM : NavigateVM
    {
        private User _currentUser;
        private RelayCommand _addNewCollectionCommand;
        private RelayCommand _deleteCollectionCommand;
        private RelayCommand _editCollectionCommand;
        private OneCollection _selectedCollection;

        public MainPageVM(string viewModelName, NavigateManager manager) : base(viewModelName, manager)
        {
            _currentUser = new User();
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
        public bool EditButtonEnabled
        {
            get
            {
                if(_selectedCollection != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
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
    }
}
