using System.Collections.ObjectModel;
using System;
using Coursework.Models.Classes.User.WordCollections.WordPair;
using Coursework.Models.Classes.Events;

namespace Coursework.Models.Classes.User.WordCollections
{
    [Serializable]
    class OneCollection : Event, ICloneable
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
        
        public object Clone()
        {
            ObservableCollection<OneWordPair> wordsPair = new ObservableCollection<OneWordPair>();
            OneCollection collection = new OneCollection();
            for(int i = 0; i < WordPair.Count; i++)
            {
                wordsPair.Add(new OneWordPair { Word = WordPair[i].Word, Translation = WordPair[i].Translation });
            }
            collection.WordPair = wordsPair;
            collection.Title = this.Title;
            collection.Description = this.Description;
            collection.IsChecked = this.IsChecked;
            return collection;
        }
    }
}
