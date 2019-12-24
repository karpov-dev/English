using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework.Models.Classes.User.Information
{
    [Serializable]
    class Coins
    {
        private int _coins;

        public Coins()
        {
            _coins = 0;
        }

        public int Count => _coins;
        public void Add(int amount)
        {
            if(amount > 0)
            {
                _coins += amount;
            }
        }
        public void Subtract(int amount)
        {
            if(amount > 0)
            {
                _coins -= amount;
            }
        }
        public void Reset() => _coins = 0;
    }
}
