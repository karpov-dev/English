using System.Windows;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Coursework.ViewModel.NavigateBase;
using Coursework.ViewModel.MangerOfNavigate;
using Coursework.ViewModel.ViewModels.VM;
using Coursework.Models;
using Coursework.Models.Classes.Commands;
using Coursework.Models.Classes.User.WordCollections;
using Coursework.Models.Classes.User.WordCollections.WordPair;
using Coursework.Models.Classes.User.Information;

namespace Coursework.ViewModel.ViewModels.PagesVM
{
    class AddingNewCollectionPageVM : NavigateVM
    {
        private RelayCommand _addNewPairCommand;
        private RelayCommand _saveCommand;
        private RelayCommand _deleteSelectedItems;
        private RelayCommand _cancelCommand;
        private RelayCommand _translateSelected;
        private RelayCommand _cleanTranslation;
        private RelayCommand _cleanAllWordPairs;

        private WordCollection _userWordCollection;
        private Settings _settings;

        public AddingNewCollectionPageVM(Settings settings, WordCollection wordCollection, string viewModelName, NavigateManager manager) : base(viewModelName, manager)
        {
            WordPairsCollection = new WordPairs(_settings);
            _userWordCollection = wordCollection;
            _settings = settings;
            _settings.UpdateInternetConnection();
        }


        public bool OnlineTranslationAvailable => _settings.OnlineTranslationAvailable;
        public bool SelectedTranslationButtonEnabled
        {
            get
            {
                bool enabled = OnlineTranslationAvailable;
                if(OnlineTranslationAvailable)
                {
                    if(OnlineTranslation)
                    {
                        enabled = false;
                    }
                }
                return enabled;
            }
        }
        public string TitleWordCollectionBox { get; set; }
        public string DescriptionBox { get; set; }
        public bool IsCheckedBox { get; set; }
        public bool OnlineTranslation
        {
            get => _settings.OnlineTranslation;
            set
            {
                _settings.OnlineTranslation = value;
                if(_settings.OnlineTranslation)
                {
                    WordPairsCollection.SetOnlineTranslation();
                }
                else
                {
                    WordPairsCollection.DeleteOnlineTranslations();
                }
                OnPropertyChanged("OnlineTranslation");
                OnPropertyChanged("SelectedTranslationButtonEnabled");
            }
        }
        public WordPairs WordPairsCollection { get; set; }
        public ObservableCollection<TranslatePairVM> Pairs => WordPairsCollection.GetWordPairs;

        public RelayCommand AddNewPairCommand
        {
            get
            {
                return _addNewPairCommand ??
                    (_addNewPairCommand = new RelayCommand(obj =>
                    {
                        WordPairsCollection.AddWordPair(_settings.OnlineTranslation);
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
                        for(int i = 0; i < WordPairsCollection.Count; i++)
                        {
                            if(WordPairsCollection.GetOneWordPair(i).IsCheck == true)
                            {
                                WordPairsCollection.RemoveAt(i);
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
        public RelayCommand TranslateSelected
        {
            get
            {
                return _translateSelected ??
                    ( _translateSelected = new RelayCommand(obj =>
                     {
                         List<int> indexes = new List<int>();
                         for(int i = 0; i < WordPairsCollection.Count; i++ )
                         {
                             if(WordPairsCollection.GetOneWordPair(i).IsCheck)
                             {
                                 indexes.Add(i);
                             }
                         }
                         WordPairsCollection.DeleteAllTranslations();
                         WordPairsCollection.UserOnlineTranslationForChecked(indexes);
                     }) );
            }
        }
        public RelayCommand CleanTranslation
        {
            get
            {
                return _cleanTranslation ??
                    ( _cleanTranslation = new RelayCommand(obj =>
                     {
                         WordPairsCollection.DeleteAllTranslations();
                     }) );
            }
        }
        public RelayCommand CleanAllWordPairs
        {
            get
            {
                return _cleanAllWordPairs ??
                    ( _cleanAllWordPairs = new RelayCommand(obj =>
                     {
                         WordPairsCollection.Clear();
                     }) );
            }
        }


        private void CopyWordPairs(OneCollection userCollection)
        {
            for (int i = 0; i < WordPairsCollection.Count; i++)
            {
                userCollection.WordPair.Add(new OneWordPair {Word = WordPairsCollection.GetOneWordPair(i).Word, 
                    Translation = WordPairsCollection.GetOneWordPair(i).Translation });
            }
        }
        private bool CheckEmptyFields()
        {
            if (WordPairsCollection.Count != 0 && TitleWordCollectionBox != null)
            {
                for(int i = 0; i < WordPairsCollection.Count; i++)
                {
                    if(WordPairsCollection.GetOneWordPair(i).Translation == null || WordPairsCollection.GetOneWordPair(i).Word == null)
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
