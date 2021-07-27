using System;
using UnityEngine;

namespace MVVM
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
    }
}