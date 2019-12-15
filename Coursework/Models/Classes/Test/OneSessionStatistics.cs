using Coursework.Models.Classes.Events;
using Coursework.Models.Classes.User.WordCollections.WordPair;

namespace Coursework.Models.Classes.Test
{
    class OneSessionStatistics : Event
    {
        public int LearnedWords { get; private set; }
        public int RepitedWords { get; private set;}
        public int TotalExp { get; private set; }
        public int AmountErrors { get; private set; }

        public void UpdateOneWordStatistic(OneWordPair word)
        {
            if(word.Learned)
            {
                LearnedWords++;
            }
            else
            {
                RepitedWords++;
            }
        }
        public void AddExp(OneWordPair word)
        {
            if(word.Learned == true)
            {
                TotalExp += ( ( word.Translation.Length + word.Word.Length ) * 2 );
            }
            else
            {
                TotalExp += word.Translation.Length + word.Word.Length;
            }
        }
        public void AddAmountErrors()
        {
            AmountErrors++;
        }
    }
}
