using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace MVC
{
    class WalletController
    {
        private readonly Wallet _wallet;

        public WalletController(Wallet wallet)
        {
            _wallet = wallet;
        }

        [MVCEvent]
        public void OnCoinsClick()
        {
            //Поставлять до контроллера события от UI Unity
            //без лишних зависимостей можно только за счёт модифицкации инфраструктуры
            //Сами мы подписываться не можем иначе сломаем абстракцию View

            _wallet.AddCoin();
        }

        [MVCEvent]
        public void OnClickUpLevelButton()
        {
            _wallet.UpLevel();
        }
    }
}
