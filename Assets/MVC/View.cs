using System;
using System.Dynamic;
using UnityEngine;

namespace MVC
{
    public abstract class View<T> : Initable<T>
    {

    }

    //Hint for unity inspector
    public abstract class WalletView : View<Wallet> { }
}
