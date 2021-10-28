using System;
using UnityEngine;

namespace MVP_PassiveView
{
    class WalletPresenter
    {
        private ClampedAmountWithIcon _view;
        private Wallet _model;

        public WalletPresenter(ClampedAmountWithIcon view, Wallet model)
        {
            _view = view;
            _model = model;
        }

        public void Enable()
        {
            _model.CoinsChanged += OnCoinsChanged;
            _view.Click += OnViewClick;
        }

        public void Disable()
        {
            _model.CoinsChanged -= OnCoinsChanged;
            _view.Click -= OnViewClick;
        }

        private void OnCoinsChanged()
        {
            _view.SetAmount(_model.Coins, _model.MaxCoins);
        }

        private void OnViewClick()
        {
            _model.AddCoin();
            OnCoinsChanged();
        }
    }
}
