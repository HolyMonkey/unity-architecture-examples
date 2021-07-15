using System;
using UnityEngine;

namespace MVC
{
    public class Wallet
    {
        public int MaxCoins { get; private set; } = 100;
        public int Coins { get; private set; }

        public event Action CoinsChanged;

        public void AddCoin()
        {
            if (Coins + 1 > MaxCoins)
                throw new InvalidOperationException();

            Coins += 1;

            CoinsChanged?.Invoke();
        }

        public void UpLevel()
        {
            if (Coins < MaxCoins)
                throw new InvalidOperationException();

            MaxCoins *= 2;
            Coins = 0;
        }
    }
}