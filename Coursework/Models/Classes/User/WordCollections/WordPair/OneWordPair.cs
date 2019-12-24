using System;
using Coursework.Models.Classes.Events;

namespace Coursework.Models.Classes.User.WordCollections.WordPair
{
    [Serializable]
    class OneWordPair : Event, ICloneable
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
            private set
            {
                _amountRepetiotion = value;
                OnPropertyChanged("AmountRepetiotion");
            }
        }
        public int AmountErrors
        {
            get => _amountErrors;
            private set
            {
                _amountErrors = value;
                OnPropertyChanged("AmountErrors");
            }
        }
        public bool Learned
        {
            get => _learned;
            private set
            {
                _learned = value;
                OnPropertyChanged("Learned");
            }
        }


        public void AddAmountRepetition()
        {
            AmountRepetiotion++;
            if(AmountRepetiotion > 10)
            {
                Learned = true;
            }
        }
        public void AddAmountErrors()
        {
            AmountErrors++;
        }
        public void RightTest()
        {
            AddAmountRepetition();
        }
        public void WrongTest()
        {
            AddAmountErrors();
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
        public void Swap()
        {
            string temp = Translation;
            Translation = Word;
            Word = temp;
        }
    }
}
