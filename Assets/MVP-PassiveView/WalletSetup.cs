using System;
using UnityEngine;

namespace MVP_PassiveView
{
    public class WalletSetup : MonoBehaviour
    {
        [SerializeField] private ClampedAmountWithIcon _view;

        private WalletPresenter _presenter;
        private Wallet _model;

        private void Awake()
        {
            _model = new Wallet();
            _presenter = new WalletPresenter(_view, _model);
        }

        private void OnEnable()
        {
            _presenter.Enable();
        }

        private void OnDisable()
        {
            _presenter.Disable();
        }
    }
}
