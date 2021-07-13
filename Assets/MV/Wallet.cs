using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MV
{
    public class Wallet : MonoBehaviour
    {
        public const int MaxCoins = 100;

        public int Coins { get; private set; }

        public event Action CoinAdded;

        public void AddCoin()
        {
            if (Coins + 1 > MaxCoins)
                throw new InvalidOperationException();

            Coins += 1;

            CoinAdded?.Invoke();
        }
    }
}
