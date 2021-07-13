using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MV
{
    public class WalletHeapView : MonoBehaviour
    {
        [SerializeField] private Wallet _wallet;
        [SerializeField] private Transform _coinsHeapParent;
        [SerializeField] private GameObject _coinInHeapTemplate;

        private void OnEnable()
        {
            _wallet.CoinAdded += OnCoinAdded;

            try
            {
                Validate();
            }
            catch (Exception e)
            {
                enabled = false;
                throw e;
            }
        }

        private void Validate()
        {
            if (_coinsHeapParent == null)
                throw new InvalidOperationException();

            if (_coinInHeapTemplate == null)
                throw new InvalidOperationException();         
        }

        private void OnDisable()
        {
            _wallet.CoinAdded -= OnCoinAdded;
        }

        private void OnCoinAdded()
        {
            GameObject newCoin = Instantiate(_coinInHeapTemplate, _coinsHeapParent);
            newCoin.transform.Translate(Vector3.up * 10);
        }
    }
}
