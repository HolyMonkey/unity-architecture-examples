using System;
using UnityEngine;
using UnityEngine.UI;

public class Wallet : MonoBehaviour
{
    private const int MaxCoins = 100;

    [SerializeField] private int _coins;
    [SerializeField] private Image _coinsIcon;
    [SerializeField] private Text _coinsText;
    [SerializeField] private Transform _coinsHeapParent;
    [SerializeField] private GameObject _coinInHeapTemplate;
    [SerializeField] private bool _presentInUi;
    [SerializeField] private bool _presentInHeap;

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
    }

    private void Validate()
    {
        if (_presentInUi)
        {
            if (_coinsIcon == null)
                throw new InvalidOperationException();

            if (_coinsText == null)
                throw new InvalidOperationException();
        }

        if (_presentInHeap)
        {
            if (_coinsHeapParent == null)
                throw new InvalidOperationException();

            if (_coinInHeapTemplate == null)
                throw new InvalidOperationException();
        }
    }

    public void AddCoin()
    {
        _coins = Mathf.Clamp(_coins + 1, 0, MaxCoins);

        if (_presentInUi)
        {
            _coinsText.text = $"{_coins}";
            _coinsIcon.color = Color.Lerp(Color.white, Color.red, _coins / MaxCoins);
        }

        if (_presentInHeap)
        {
            GameObject newCoin = Instantiate(_coinInHeapTemplate, _coinsHeapParent);
            newCoin.transform.Translate(Vector3.up * 10);
        }
    }
}
