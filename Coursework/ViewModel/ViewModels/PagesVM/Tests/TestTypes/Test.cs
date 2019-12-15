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
    static class Test
    {
        static public OneCollection SetRandomCurrentCollection(Random random, ObservableCollection<OneCollection> collections)
        {
            OneCollection randomCollection;
            randomCollection = collections[random.Next(0, collections.Count)];
            return randomCollection;
        }
        static public OneWordPair GetWordPair(Random random, OneCollection collection)
        {
            int amountWords = collection.AmountWords;
            int randomWordPairIndex = random.Next(0, amountWords);
            return collection.WordPair[randomWordPairIndex];
        }
        static public List<OneWordPair> GetWordPairs(Random random, int amount, OneCollection collection)
        {
            List<OneWordPair> listOfWords = new List<OneWordPair>();
            for ( int i = 0; i < amount; i++ )
            {
                bool checkRepetition = false;
                OneWordPair wordPair;
                do
                {
                    checkRepetition = false;
                    wordPair = GetWordPair(random, collection);
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
        static public OneWordPair GetRandomAnswer(Random random, List<OneWordPair> wordPairs)
        {
            OneWordPair RightAnswer;
            RightAnswer = wordPairs[random.Next(0, wordPairs.Count)];
            return RightAnswer;
        }
    }
}
