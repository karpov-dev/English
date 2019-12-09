using System.Collections.ObjectModel;
using Coursework.Models.Classes.User.WordCollections.WordPair;
using Coursework.Models.Classes.Events;

namespace Coursework.Models.Classes.User.WordCollections
{
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
    }
}
