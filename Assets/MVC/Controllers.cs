using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace MVC
{
    public class Controllers
    {
        private static Dictionary<Type, object> _controllers = new Dictionary<Type, object>();

        //Реализация просто чтобы показать концепт, требует серьёзных доработок
        public static T Create<T>(object model)
        {
            if (_controllers.ContainsKey(typeof(T)))
                throw new InvalidOperationException($"{nameof(T)} alredy created");

            T instance = (T)Activator.CreateInstance(typeof(T), new object[] { model });
            _controllers.Add(typeof(T), instance);
            return instance;
        }   

        public static object Get(Type type)
        {
            return _controllers[type];
        }
    }

    public class MVCEventAttribute : Attribute { }
}
