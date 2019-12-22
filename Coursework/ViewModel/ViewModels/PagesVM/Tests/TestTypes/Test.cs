using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Coursework.Models.Classes.User.WordCollections.WordPair;
using Coursework.Models.Classes.User.WordCollections;
using System.Security.Cryptography;

namespace Coursework.ViewModel.ViewModels.PagesVM.Tests.TestTypes
{
    static class Test
    {
        static public OneCollection SetRandomCurrentCollection(ObservableCollection<OneCollection> collections)
        {
            OneCollection randomCollection;
            randomCollection = collections[GetRandomNumber(0, (collections.Count - 1))];
            return randomCollection;
        }
        static public OneWordPair GetWordPair(OneCollection collection)
        {
            int amountWords = collection.AmountWords;
            int randomWordPairIndex = GetRandomNumber(0, (amountWords - 1));
            return collection.WordPair[randomWordPairIndex];
        }
        static public List<OneWordPair> GetWordPairs(int amount, OneCollection collection)
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
                        if ( (wordPair.Translation == listOfWords[j].Translation) && (wordPair.Word == listOfWords[j].Word ))
                        {
                            checkRepetition = true;
                            break;
                        }
                    }
                } while ( checkRepetition );
                listOfWords.Add(wordPair.Clone() as OneWordPair);
            }
            return listOfWords;
        }
        static public OneWordPair GetRandomAnswer(List<OneWordPair> wordPairs)
        {
            OneWordPair RightAnswer;
            RightAnswer = wordPairs[GetRandomNumber(0, (wordPairs.Count - 1))];
            return RightAnswer.Clone() as OneWordPair;
        }
        static public bool GetSwap()
        {
            int isSwap = GetRandomNumber(0, 1);
            if ( isSwap > 0 )
                return true;
            return false;
        }
        static public int GetRandomNumber(int minValue, int maxValue)
        {
            RNGCryptoServiceProvider rnd = new RNGCryptoServiceProvider();
            int result = 0;
            do
            {
                byte[] randombyte = new byte[1];
                rnd.GetBytes(randombyte); //получаем случайный байт 
                double random_multiplyer = ( randombyte[0] / 255d );
                int difference = maxValue - minValue + 1; //находим разницу между максимальным и минимальным значением 
                result = (int) ( minValue + Math.Floor(random_multiplyer * difference) );  //прибавляем к минимальному значение число от 0 до difference
            } while ( result > maxValue || result < minValue );
            return result;
        }
    }
}
