using System;
using UnityEngine;
using UnityEngine.UI;

namespace MVP_PassiveView
{
    //В MVP View можно рассматривать не как прямую проекцию модели
    //Wallet -> WalletUIView 
    //Но и как самостоятельный UI компонент
    //Например Bag (стандартный компонент в котором текст обьединяется с цифрой)
    //Ну или как тут
    public class ClampedAmountWithIcon : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private Text _text;
        [SerializeField] private Button _clickSource;

        public event Action Click;

        private void OnEnable()
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

            _clickSource.onClick.AddListener(OnClick);
        }

        private void OnDisable()
        {
            _clickSource?.onClick.RemoveListener(OnClick);
        }

        private void Validate()
        {
            if (_icon == null)
                throw new InvalidOperationException();

            if (_text == null)
                throw new InvalidOperationException();
        }

        public void SetAmount(int amount, int maxAmount)
        {
            _text.text = $"{amount}";
            _icon.color = Color.Lerp(Color.white, Color.red, amount / maxAmount);
        }
        
        private void OnClick()
        {
            Click?.Invoke();
        }
    }
}
