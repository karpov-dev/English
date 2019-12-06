using Coursework.Models.Classes.User;
using Coursework.Models.Classes.NotifyPropertyEvent;
using System.Collections.ObjectModel;

namespace Coursework.Models
{
    class User : NotifyPropertyChangedEvent
    {
        private string _name;
        private UserStatistics _statistics;
        private Achievements _achievements;
        private ObservableCollection<OneCollection> _wordCollections;


        public User()
        {
            _statistics = new UserStatistics();
            _achievements = new Achievements();
            _wordCollections = new ObservableCollection<OneCollection>();
        }


        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }
        public ObservableCollection<OneCollection> WordCollections
        {
            get => _wordCollections;
            set
            {
                _wordCollections = value;
                OnPropertyChanged("WordCollections");
            }
        }


        public void AddCollection(OneCollection addedCollection)
        {
            _wordCollections.Add(addedCollection);
        }
        public void DeleteCollection(OneCollection deletedCollection)
        {
            _wordCollections.Remove(deletedCollection);
        }
        public void DeleteCollection(int[] collectionsIndexes)
        {
            for (int i = 0; i < collectionsIndexes.Length; i++)
            {
                _wordCollections.RemoveAt(collectionsIndexes[i]);
            }
        }
    }
}
