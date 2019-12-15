using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Coursework.Models;
using Coursework.Models.Classes.Events;
using Coursework.Models.Classes.Commands;
using Coursework.ViewModel.ViewModels.PagesVM.Tests.TestsManager;
using Coursework.Models.Classes.User.WordCollections.WordPair;
using Coursework.Models.Classes.User.WordCollections;
using Coursework.ViewModel.ViewModels.VM;
using Coursework.Models.Classes.User.Statistics;

namespace Coursework.ViewModel.ViewModels.PagesVM.Tests.TestTypes
{
    abstract class TestBase : Event
    {
        protected Random _random;
        protected OneCollection _currentCollection;
        protected ObservableCollection<OneCollection> _collections;

        public TestBase(ObservableCollection<OneCollection> userCollections)
        {
            _random = new Random();
            _collections = userCollections;
        }

        protected OneCollection SetRandomCurrentCollection(ObservableCollection<OneCollection> collections)
        {
            OneCollection randomCollection;
            randomCollection = collections[_random.Next(0, collections.Count)];
            return randomCollection;
        }
        protected OneWordPair GetWordPair(OneCollection collections)
        {
            int amountWords = _currentCollection.AmountWords;
            int randomWordPairIndex = _random.Next(0, amountWords);
            return _currentCollection.WordPair[randomWordPairIndex];
        }
        protected List<OneWordPair> GetWordPairs(int amount, OneCollection collection)
        {
            List<OneWordPair> listOfWords = new List<OneWordPair>();
            for ( int i = 0; i < amount; i++ )
            {
                bool checkRepetition = false;
                OneWordPair wordPair;
                do
                {
                    checkRepetition = false;
                    wordPair = GetWordPair(collection);
                    for ( int j = 0; j < listOfWords.Count; j++ )
                    {
                        if ( wordPair == listOfWords[j] )
                        {
                            checkRepetition = true;
                            break;
                        }
                    }
                } while ( checkRepetition );
                listOfWords.Add(wordPair);
            }
            return listOfWords;
        }
        protected OneWordPair GetRandomAnswer(List<OneWordPair> wordPairs)
        {
            OneWordPair RightAnswer;
            RightAnswer = wordPairs[_random.Next(0, wordPairs.Count)];
            return RightAnswer;
        }
    }
}
