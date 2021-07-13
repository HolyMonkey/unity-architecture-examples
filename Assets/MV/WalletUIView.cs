using System;
using UnityEngine;
using UnityEngine.UI;

namespace MV
{
    public class WalletUIView : MonoBehaviour
    {
        [SerializeField] private Wallet _wallet;
        [SerializeField] private Image _coinsIcon;
        [SerializeField] private Text _coinsText;

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
            if (_coinsIcon == null)
                throw new InvalidOperationException();

            if (_coinsText == null)
                throw new InvalidOperationException();
        }

        private void OnCoinAdded()
        {
            _coinsText.text = $"{_wallet.Coins}";
            _coinsIcon.color = Color.Lerp(Color.white, Color.red, _wallet.Coins / Wallet.MaxCoins);
        }
    }
}
