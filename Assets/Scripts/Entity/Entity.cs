using System;
using System.Collections.Generic;
using UnityEngine;

namespace Entity
{
    public class Entity : MonoBehaviour
    {
        [SerializeField]
        private readonly List<object> components = new();
        public T Get<T>()
        {
            for (int i = 0, count = this.components.Count; i < count; i++)
            {
                var component = this.components[i];
                if (component is T result)
                {
                    return result;
                }
            }

            throw new Exception($"Component of type {typeof(T).Name} is not found!");
        }

        public bool TryGet<T>(out T result)
        {
            for (int i = 0, count = this.components.Count; i < count; i++)
            {
                var component = this.components[i];
                if (component is T tComponent)
                {
                    result = tComponent;
                    return true;
                }
            }

            result = default;
            return false;
        }

        public void Add(object component)
        {
            components.Add(component);
        }
        
        public void Remove(object component)
        {
            if(components.Contains(component))
                components.Remove(component);
        }
    }
}
