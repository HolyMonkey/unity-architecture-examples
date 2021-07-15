using System;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

namespace MVC
{
    public class ClickRoutingRecord : MonoBehaviour
    {
        [SerializeField] private Button _clickable;
        [HideInInspector] [SerializeField] private string _target;

        private void OnEnable()
        {
            _clickable.onClick.AddListener(OnClick);
        }

        private void OnDisable()
        {
            _clickable.onClick.RemoveListener(OnClick);
        }

        private void OnClick()
        {
            CallTarget(_target);
        }

        private static void CallTarget(string target)
        {
            //код только чтобы проверить работоспособность концепта
            string methodName = target.Split(':')[0];
            string fullTypeName = target.Split(':')[1];

            var type = Assembly.GetAssembly(typeof(MVCEventAttribute)).GetTypes()
                            .First(t => t.FullName == fullTypeName);

            var method = type.GetMethods()
                            .Where(m => m.GetCustomAttributes(typeof(MVCEventAttribute), false).Length > 0)
                            .First(m => m.Name == methodName);

            var instance = Controllers.Get(type);

            method.Invoke(instance, null);
        }
    }
}
