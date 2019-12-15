using System;
using Coursework.Models.Classes.Events;

namespace Coursework.Models.Classes.User.WordCollections.WordPair
{
    [Serializable]
    class OneWordPair : Event
    {
        private string _word;
        private string _translation;
        private int _amountRepetiotion;
        private int _amountErrors;
        private bool _learned;

        public OneWordPair()
        {
            _amountErrors = 0;
            _amountRepetiotion = 0;
            _learned = false;
        }

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
        public int AmountRepetiotion
        {
            get => _amountRepetiotion;
            set
            {
                _amountRepetiotion = value;
                if(_amountRepetiotion == 5)
                {
                    Learned = true;
                }
                OnPropertyChanged("AmountRepetiotion");
            }
        }
        public int AmountErrors
        {
            get => _amountErrors;
            set
            {
                _amountErrors = value;
                OnPropertyChanged("AmountErrors");
            }
        }
        public bool Learned
        {
            get => _learned;
            set
            {
                _learned = value;
                OnPropertyChanged("Learned");
            }
        }
    }
}
