using System;
using UnityEngine;

namespace MVVM
{
    class WalletViewModel : ViewModel
    {
        [Model] private Wallet _wallet = new Wallet(); //need a DI

        [Project] public int Coins => _wallet.Coins;

        [Command]
        public void AddCoin() => _wallet.AddCoin();
    }
}
