using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace MVVM
{
    /// <summary>
    /// Код чисто proof of concept, написан слабо и нужен только чтобы показать принцип работа
    /// Можете отрефакторить в качестве ДЗ ;)
    /// </summary>
    class UIBinder : MonoBehaviour
    {
        [SerializeField] private ViewModel _viewModel;
        [SerializeField] private Transform _root;

        private void OnEnable()
        {
            Bind();
        }

        private void OnDisable()
        {
            Unbind();
        }

        private void Bind()
        {
            foreach(var bindable in _viewModel.GetBindables())
            {
                Transform target = Find(bindable);

                if (bindable is Command command)
                {
                    target.GetComponent<Button>().onClick.AddListener(command.Do);
                }
                else if (bindable is Property property)
                {
                    property.Changed += OnPropertyChanged;
                }
            }
        }

        private void Unbind()
        {
            foreach (var bindable in _viewModel.GetBindables())
            {
                Transform target = Find(bindable);

                if (bindable is Command command)
                {
                    target.GetComponent<Button>().onClick.RemoveListener(command.Do);
                }
                else if (bindable is Property property)
                {
                    property.Changed -= OnPropertyChanged;
                }
            }
        }

        private void OnPropertyChanged(Property property)
        {
            Transform target = Find(property);
            target.GetComponent<Text>().text = property.Get().ToString();
        }

        private Transform Find(IBindable bindable)
        {
            return _root
                .GetComponentsInChildren<Transform>()
                .First(t => t.name == bindable.Name);
        }
    }
}
