using System;
using UnityEngine;

namespace MVC
{
    public class Initable<T> : MonoBehaviour
    {
        public T Model { get; private set; }

        private bool _inited;

        public void Init(T model)
        {
            if (_inited)
                throw new InvalidOperationException();

            Model = model;
            _inited = true;
            enabled = true;
        }

        private void OnEnable()
        {
            if (_inited == false)
            {
                enabled = false;
                return;
            }

            OnInitedEnable();
        }

        private void OnDisable()
        {
            if (_inited)
                OnInitedDisable();
        }

        protected virtual void OnInitedEnable() { }
        protected virtual void OnInitedDisable() { }
    }
}
