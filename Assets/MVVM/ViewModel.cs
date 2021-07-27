using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using UnityEngine;

namespace MVVM
{
    /// <summary>
    /// Код чисто proof of concept, написан слабо и нужен только чтобы показать принцип работа
    /// Можете отрефакторить в качестве ДЗ ;)
    /// </summary>
    public class ViewModel : MonoBehaviour
    {
        private static readonly Dictionary<PropertyInfo, Property> _properties = new Dictionary<PropertyInfo, Property>();
        private static readonly Dictionary<MethodInfo, Command> _commands = new Dictionary<MethodInfo, Command>();

        public IEnumerable<IBindable> GetProperties()
        {
            Type type = GetType();

            foreach(var property in type.GetProperties())
            {
                if (_properties.TryGetValue(property, out Property domainProperty))
                {
                    yield return domainProperty;
                    continue;
                }

                if (property.GetCustomAttributes<ProjectAttribute>().Count() == 0)
                    continue;

                domainProperty = new Property(this, property.GetValue, property.Name);

                AddPropertyChangeEventListener(GetModels(), property, domainProperty.OnChanged);

                _properties.Add(property, domainProperty);
                yield return domainProperty;
            }
        }

        public IEnumerable<IBindable> GetCommands()
        {
            Type type = GetType();

            foreach (var method in type.GetMethods())
            {
                if (_commands.TryGetValue(method, out Command domainCommand))
                {
                    yield return domainCommand;
                    continue;
                }

                if (method.GetCustomAttributes<CommandAttribute>().Count() == 0)
                    continue;

                domainCommand = new Command(this, method.Invoke, method.Name);
                _commands.Add(method, domainCommand);

                yield return domainCommand;
            }
        }

        public IEnumerable<IBindable> GetBindables()
        {
            return GetCommands().Concat(GetProperties());
        }

        private IEnumerable<object> GetModels()
        {
            Type type = GetType();

            foreach (var field in type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance))
            {
                if (field.GetCustomAttributes<ModelAttribute>().Count() == 0)
                    continue;

                yield return field.GetValue(this);
            }
        }

        private void AddPropertyChangeEventListener(IEnumerable<object> eventSources, PropertyInfo property, Action handler)
        {
            foreach (var eventSource in eventSources)
            {
                Type type = eventSource.GetType();
                EventInfo eventInfo = type.GetEvent($"{property.Name}Changed");
                
                if(eventInfo != null)
                {
                    eventInfo.AddEventHandler(eventSource, handler);
                    return;
                }
            }
        }
    }

    public interface IBindable
    {
        string Name { get; }
    }

    public class Command : IBindable
    {
        private ViewModel _target;
        private Func<object, object[], object> _method;

        private static object[] _emptyArguments = new object[0];

        public string Name { get; private set; }

        public Command(ViewModel target, Func<object, object[], object> method, string name)
        {
            _target = target;
            _method = method;
            Name = $"{target.GetType().Name}.{name}";
        }

        public void Do()
        {
            _method.Invoke(_target, _emptyArguments);
        }
    }
    
    public class Property : IBindable
    {
        private ViewModel _target;
        private Func<object, object> _getter;

        public string Name { get; private set; }
        public event Action<Property> Changed;

        public Property(ViewModel target, Func<object, object> getter, string name)
        {
            _target = target;
            _getter = getter;
            Name = $"{target.GetType().Name}.{name}";
        }

        public object Get()
        {
            return _getter.Invoke(_target);
        }

        public void OnChanged()
        {
            Changed.Invoke(this);
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class ProjectAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class CommandAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Field)]
    public class ModelAttribute : Attribute
    {
    }
}
