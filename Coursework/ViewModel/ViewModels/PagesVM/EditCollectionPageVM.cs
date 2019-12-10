using System.Collections.ObjectModel;
using System.Windows;
using Coursework.ViewModel.MangerOfNavigate;
using Coursework.ViewModel.NavigateBase;
using Coursework.Models.Classes.Commands;
using Coursework.Models.Classes.User.WordCollections;
using Coursework.ViewModel.ViewModels.VM;
using Coursework.Models.Classes.User.WordCollections.WordPair;


namespace Coursework.ViewModel.ViewModels.PagesVM
{
    class EditCollectionPageVM : NavigateVM
    {
        private OneCollection _userCollection;
        private RelayCommand _saveCommand;
        private RelayCommand _addWordPair;
        private RelayCommand _deleteSelectedWordPairs;
        private RelayCommand _cancelCommand;
        private string _title;
        private string _description;

        public EditCollectionPageVM(OneCollection userCollection, NavigateManager navigateManager) : base(navigateManager)
        {
            _userCollection = userCollection;
            _title = _userCollection.Title;
            _description = _userCollection.Description;
            WordPairs = CopyWordPairs(_userCollection.WordPair);
        }

        public ObservableCollection<TranslatePairVM> WordPairs { get; set; }
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged("Title");
            }
        }
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged("Description");
            }
        }

        public RelayCommand SaveCommand
        {
            get
            {
                return _saveCommand ??
                    (_saveCommand = new RelayCommand(obj =>
                    {
                        _userCollection.Title = Title;
                        _userCollection.Description = Description;
                        _userCollection.WordPair = CopyWordPairs(WordPairs);
                        Manager.GoTo("MainPageVM");
                    }));
            }
        }
        public RelayCommand CancelCommand
        {
            get
            {
                return _cancelCommand ??
                    (_cancelCommand = new RelayCommand(obj =>
                    {
                        InformationMessage("Вы не сохранили подборку, в прочем, уже поздно");
                        Manager.GoTo("MainPageVM");
                    }));
            }
        }
        public RelayCommand AddNewWordPair
        {
            get
            {
                return _addWordPair ??
                    (_addWordPair = new RelayCommand(obj =>
                    {
                        WordPairs.Add(new TranslatePairVM());
                    }));
            }
        }
        public RelayCommand DeleteSeceltedWordPairs
        {
            get
            {
                return _deleteSelectedWordPairs ??
                    (_deleteSelectedWordPairs = new RelayCommand(obj =>
                    {
                        for(int i = 0; i < WordPairs.Count; i++)
                        {
                            if(WordPairs[i].IsCheck)
                            {
                                WordPairs.RemoveAt(i);
                                i--;
                            }
                        }
                    }));
            }
        }

        private ObservableCollection<TranslatePairVM> CopyWordPairs(ObservableCollection<OneWordPair> userWordPairs)
        {
            ObservableCollection<TranslatePairVM> newWordPairs = new ObservableCollection<TranslatePairVM>();
            for(int i = 0; i < userWordPairs.Count; i++)
            {
                newWordPairs.Add(new TranslatePairVM { Word = userWordPairs[i].Word, Translation = userWordPairs[i].Translation, IsCheck = false });
            }
            return newWordPairs;
        }
        private ObservableCollection<OneWordPair> CopyWordPairs(ObservableCollection<TranslatePairVM> pageWordPairs)
        {
            ObservableCollection<OneWordPair> newWordPair = new ObservableCollection<OneWordPair>();
            for(int i = 0; i < WordPairs.Count; i++)
            {
                newWordPair.Add(new OneWordPair { Word = WordPairs[i].Word, Translation = WordPairs[i].Translation });
            }
            return newWordPair;
        }
    }
}
