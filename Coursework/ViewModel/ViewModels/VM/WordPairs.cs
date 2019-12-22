using Coursework.Models.Classes.Events;
using Coursework.Models.Classes.User.WordCollections;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using Coursework.Models.Classes.User.Information;

namespace Coursework.ViewModel.ViewModels.VM
{
    class WordPairs
    {
        private ObservableCollection<TranslatePairVM> _pairs;
        private Translator _translator;
        private Settings _setting;

        public WordPairs(Settings setting)
        {
            _pairs = new ObservableCollection<TranslatePairVM>();
            _translator = new Translator();
            _setting = setting;
        }

        public int Count => _pairs.Count;
        public ObservableCollection<TranslatePairVM> GetWordPairs => _pairs;
        public void AddWordPair(bool UseOnlainTranslation)
        {
            _pairs.Add(new TranslatePairVM(_translator, UseOnlainTranslation));
        }
        public void RemoveWordPair(TranslatePairVM pair) => _pairs.Remove(pair);
        public void RemoveAt(int index)
        {
            if(index < _pairs.Count)
            {
                _pairs.RemoveAt(index);
            }
        }
        public TranslatePairVM GetOneWordPair(int index)
        {
            if(index < _pairs.Count )
            {
                return _pairs[index];
            }
            else
            {
                return null;
            }
        }
        public void SetOnlineTranslation()
        {
            if(_pairs != null && _pairs.Count > 0)
            {
                for(int i = 0; i < _pairs.Count; i++ )
                {
                    _pairs[i].UseOnlineTranslate = true;
                    _pairs[i].Translate();
                    _pairs[i].UseOnlineTranslate = true;
                }
            }
        }
        public void DeleteOnlineTranslations()
        {
            if(_pairs != null && _pairs.Count > 0)
            {
                for(int i = 0; i < _pairs.Count; i++ )
                {
                    if(_pairs[i].IsOnlineTranslation)
                    {
                        _pairs[i].Translation = "";
                        _pairs[i].IsOnlineTranslation = false;
                        _pairs[i].UseOnlineTranslate = false;
                    }
                }
            }
        }
        public void DeleteAllTranslations()
        {
            if(_pairs != null || _pairs.Count > 0)
            {
                for(int i = 0; i < _pairs.Count; i++ )
                {
                    _pairs[i].Translation = "";
                    _pairs[i].IsOnlineTranslation = false;
                    _pairs[i].UseOnlineTranslate = false;
                }
            }
        }
        public void UseForAllPairsOnlineTranslations()
        {
            if(_pairs != null && _pairs.Count > 0)
            {
                DeleteAllTranslations();
                for (int i = 0; i < _pairs.Count; i++ )
                {
                    _pairs[i].Translate();
                }
            }
        }
        public void UserOnlineTranslationForChecked(List<int> indexes)
        {
            if(indexes.Count <= _pairs.Count)
            {
                for(int i = 0; i < indexes.Count; i++ )
                {
                    if(indexes[i] < _pairs.Count)
                    {
                        _pairs[indexes[i]].UseOnlineTranslate = true;
                        _pairs[indexes[i]].IsOnlineTranslation = true;
                        _pairs[indexes[i]].Translate();
                    }
                }
            }
        }
        public void Clear()
        {
            _pairs.Clear();
        }
    }
}
