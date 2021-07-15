using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MVC
{
    public class WalletHeapView : WalletView
    {
        [SerializeField] private Transform _coinsHeapParent;
        [SerializeField] private GameObject _coinInHeapTemplate;

        protected override void OnInitedEnable()
        {
            try
            {
                Validate();
            }
            catch (Exception e)
            {
                enabled = false;
                throw e;
            }

            Model.CoinsChanged += OnCoinsChanged;
        }

        protected override void OnInitedDisable()
        {
            Model.CoinsChanged -= OnCoinsChanged;
        }

        private void Validate()
        {
            if (_coinsHeapParent == null)
                throw new InvalidOperationException();

            if (_coinInHeapTemplate == null)
                throw new InvalidOperationException();         
        }

        public void OnCoinsChanged()
        {
            //магия какая-нибудь с подгонкой кучи монеток
            GameObject newCoin = Instantiate(_coinInHeapTemplate, _coinsHeapParent);
            newCoin.transform.Translate(Vector3.up * 10);
        }
    }
}
