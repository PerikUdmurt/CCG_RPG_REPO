using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace CollectionCardGame.Infrastructure
{
    public class CustomPool<T> where T: MonoBehaviour
    {
        private T _prefab;
        private List<T> _objects;
        private PlaceholderFactory<T> _factory;
        public CustomPool(T prefab, int prepareObjects, PlaceholderFactory<T> placeholderFactory)
        {
            _prefab = prefab;
            _objects = new List<T>();
            _factory = placeholderFactory;

            for (int i = 0; i < prepareObjects; i++)
            {
                Create(prefab);
            }
        }


        public T Get()
        {
            var obj = _objects.FirstOrDefault(x => !x.isActiveAndEnabled);

            if (obj == null)
            {
                obj = Create(_prefab);
            }

            obj.gameObject.SetActive(true);
            return obj;
        }

        private T Create(T prefab)
        {
            var obj = _factory.Create();
            obj.gameObject.SetActive(false);
            _objects.Add(obj);
            return obj;
        }

        public void Release(T obj)
        {
            obj.gameObject.SetActive(false);
        }
    }
}

public interface ICreator
{ }
