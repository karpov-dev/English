using System.Collections.ObjectModel;
using System;
using Coursework.Models.Classes.User.WordCollections.WordPair;
using Coursework.Models.Classes.Events;

namespace Coursework.Models.Classes.User.WordCollections
{
    [Serializable]
    class OneCollection : Event
    {
        private string _title;
        private string _description;
        private bool _isChecked;

        public OneCollection()
        {
            WordPair = new ObservableCollection<OneWordPair>();
        }

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
        public bool IsChecked
        {
            get => _isChecked;
            set
            {
                _isChecked = value;
                OnPropertyChanged("IsChecked");
            }
        }
        public ObservableCollection<OneWordPair> WordPair { get; set; }
        public int AmountWords => WordPair.Count;
        public int AmountWrongs
        {
            get
            {
                int wrongs = 0;
                for(int i = 0; i < WordPair.Count; i++ )
                {
                    wrongs += WordPair[i].AmountErrors;
                }
                return wrongs;
            }
        }
        public int AmountRepetition
        {
            get
            {
                int repetition = 0;
                for(int i = 0; i < WordPair.Count; i++ )
                {
                    repetition += WordPair[i].AmountRepetiotion;
                }
                return repetition;
            }
        }
    }
}
