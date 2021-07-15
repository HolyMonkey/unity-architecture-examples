using System;
using UnityEngine;
using UnityEngine.UI;

namespace MVC
{
    public class WalletUIView : WalletView
    {
        [SerializeField] private Image _coinsIcon;
        [SerializeField] private Text _coinsText;

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
            if (_coinsIcon == null)
                throw new InvalidOperationException();

            if (_coinsText == null)
                throw new InvalidOperationException();
        }

        public void OnCoinsChanged()
        {
            _coinsText.text = $"{Model.Coins}";
            _coinsIcon.color = Color.Lerp(Color.white, Color.red, Model.Coins / Model.MaxCoins);
        }
    }
}
