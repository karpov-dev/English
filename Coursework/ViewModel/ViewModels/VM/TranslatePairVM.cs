using Coursework.Models.Classes.Events;
using Coursework.Models.Classes.User.WordCollections;
using System.Threading;
using System.ComponentModel;

namespace Coursework.ViewModel.ViewModels.VM
{
    class TranslatePairVM : Event
    {
        private string _word;
        private string _translation;
        private Translator _translator;
        private BackgroundWorker _worker;
        private bool _workerLock;
        private bool _line;

        public TranslatePairVM(Translator translator, bool onlineTranslation)
        {
            UseOnlineTranslate = onlineTranslation;
            IsOnlineTranslation = false;
            _translator = translator;
            _worker = new BackgroundWorker();
            _worker.DoWork += Translations;
            _worker.RunWorkerCompleted += UnLockWorker;
            _workerLock = false;
            _line = false;
        }

        public TranslatePairVM()
        {

        }

        public string Word
        {
            get => _word;
            set
            {
                _word = value;
                Translate();
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
        public bool IsCheck { get; set; }
        public bool IsOnlineTranslation { get; set; }
        public bool UseOnlineTranslate { get; set; }

        public void Translate()
        {
            if ( _workerLock == false && UseOnlineTranslate )
            {
                IsOnlineTranslation = true;
                _workerLock = true;
                _worker.RunWorkerAsync();
            }
            else
            {
                _line = true;
            }
        }
        private void Translations(object sender, DoWorkEventArgs e)
        {
            Translation = _translator.Translate(_word, "en-ru");
        }
        private void UnLockWorker(object sender, RunWorkerCompletedEventArgs e)
        {
            _workerLock = false;
            if(_line)
            {
                _worker.RunWorkerAsync();
                _line = false;
                _workerLock = true;
            }
        }
    }
}
