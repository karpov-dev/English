using System;
using Coursework.Models.Classes.Events;

namespace Coursework.Models.Classes.User.WordCollections.WordPair
{
    [Serializable]
    class OneWordPair : Event
    {
        private string _word;
        private string _translation;

        public string Word
        {
            get => _word;
            set
            {
                _word = value;
                OnPropertyChanged("Word");
            }
        }
        public string Translation
        {
            get => _translation;
            set
            {
                _translation = value;
                OnPropertyChanged("Translation");
            }
        }
    }
}
