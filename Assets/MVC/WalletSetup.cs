using System.Collections.Generic;
using UnityEngine;

namespace MVC
{
    public class WalletSetup : MonoBehaviour
    {
        [SerializeField] private List<WalletView> _views;

        private Wallet _wallet;
        private WalletController _controller;

        private void Awake()
        {
            _wallet = new Wallet();
            _controller = Controllers.Create<WalletController>(_wallet);
            _views.ForEach(view => view.Init(_wallet));
        }
    }
}