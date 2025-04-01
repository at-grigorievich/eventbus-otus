using System;
using System.Collections.Generic;

namespace DefaultNamespace
{
    public sealed class Entity
    {
        private readonly Dictionary<Type, IComponent> _components = new();
        
        public Entity AddComponent(IComponent component)
        {
            Type cType = component.GetType();

            _components.TryAdd(cType, component);

            return this;
        }
        
        
        public bool TryRemoveComponent<T>() where T: IComponent
        {
            Type cType = typeof(T);

            if (_components.ContainsKey(cType) == false)
            {
                //throw new ArgumentException($"Component {component.GetType()} is not existed");
                return false;
            }
            
            _components.Remove(cType);

            return true;
        }

        public bool TryGetComponent<T>(out T component) where T: IComponent
        {
            Type cType = typeof(T);

            if (_components.TryGetValue(cType, out IComponent result) == false)
            {
                component = default;
                return false;
            }
            
            component = (T)result;
            
            return true;
        }
    }
}