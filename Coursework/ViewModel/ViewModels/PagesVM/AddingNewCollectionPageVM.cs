using System.Windows;
using System.Collections.ObjectModel;
using Coursework.ViewModel.NavigateBase;
using Coursework.ViewModel.MangerOfNavigate;
using Coursework.ViewModel.ViewModels.VM;
using Coursework.Models.Classes.Commands;
using Coursework.Models.Classes.User.WordCollections;
using Coursework.Models.Classes.User.WordCollections.WordPair;

namespace Coursework.ViewModel.ViewModels.PagesVM
{
    class AddingNewCollectionPageVM : NavigateVM
    {
        private RelayCommand _addNewPairCommand;
        private RelayCommand _saveCommand;
        private RelayCommand _deleteSelectedItems;
        private RelayCommand _cancelCommand;
        private WordCollection _userWordCollection;

        public AddingNewCollectionPageVM(WordCollection wordCollection, string viewModelName, NavigateManager manager) : base(viewModelName, manager)
        {
            WordPairs = new ObservableCollection<TranslatePairVM>();
            _userWordCollection = wordCollection;
        }


        public string TitleWordCollectionBox { get; set; }
        public string DescriptionBox { get; set; }
        public bool IsCheckedBox { get; set; }
        public ObservableCollection<TranslatePairVM> WordPairs { get; set; }

        public RelayCommand AddNewPairCommand
        {
            get
            {
                return _addNewPairCommand ??
                    (_addNewPairCommand = new RelayCommand(obj =>
                    {
                        WordPairs.Add(new TranslatePairVM());
                    }));
            }
        }
        public RelayCommand SaveCollectionCommand
        {
            get
            {
                return _saveCommand ??
                    (_saveCommand = new RelayCommand(obj =>
                    {
                        if (CheckEmptyFields())
                        {
                            OneCollection addingCollection = new OneCollection();
                            addingCollection.Title = TitleWordCollectionBox;
                            addingCollection.Description = DescriptionBox;
                            addingCollection.IsChecked = IsCheckedBox;
                            CopyWordPairs(addingCollection);
                            _userWordCollection.AddCollection(addingCollection);
                            Manager.GoTo("MainPageVM");
                        }
                        else
                        {
                            ErrorMessage(Properties.Resources.EmptyFieldsError);
                        }
                    }));
            }
        }
        public RelayCommand DeleteSelectedItems
        {
            get
            {
                return _deleteSelectedItems ??
                    (_deleteSelectedItems = new RelayCommand(obj =>
                    {
                        int amountWordsPairs = WordPairs.Count;
                        for(int i = 0; i < WordPairs.Count; i++)
                        {
                            if(WordPairs[i].IsCheck == true)
                            {
                                WordPairs.RemoveAt(i);
                                i--;
                            }
                        }
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
                        if(CheckEmptyFields() == true)
                        {

                        }
                    }));
            }
        }


        private void CopyWordPairs(OneCollection userCollection)
        {
            for (int i = 0; i < WordPairs.Count; i++)
            {
                userCollection.WordPair.Add(new OneWordPair {Word = WordPairs[i].Word, Translation = WordPairs[i].Translation });
            }
        }
        private bool CheckEmptyFields()
        {
            if (WordPairs.Count != 0 && TitleWordCollectionBox != null && DescriptionBox != null)
            {
                for(int i = 0; i < WordPairs.Count; i++)
                {
                    if(WordPairs[i].Translation == null || WordPairs[i].Word == null)
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
