using System.Collections.Generic;
using Interfaces;
using UnityEngine;

namespace Pool
{
    public class ObjectPool<T> : IObjectPool where T : Component
    {
        private readonly Queue<T> _available = new Queue<T>();
        private readonly List<T> _all = new List<T>();

        public Transform Parent;
        public int InitialSize = 30;
        public int ExpandSize = 10;
        public int MaxSize = 300;
        
        Component IObjectPool.Get(Component prefab)
        {
            return Get(prefab);
        }
        
        void IObjectPool.Release(Component obj)
        {
            Release((T)obj);
        }
        
        private T Get(Component  prefab)
        {
            if (_available.Count == 0)
            {
                Expand(prefab);
            }

            if (_available.Count == 0) return null;
            
            var obj = _available.Dequeue();
            return obj;
        }

        void Expand(Component prefab)
        {
            int count = Mathf.Min(ExpandSize, MaxSize - _all.Count);

            for (int i = 0; i < count; i++)
            {
                var obj = Object.Instantiate(prefab, Parent);
                obj.gameObject.SetActive(false);

                _available.Enqueue(obj as T);
                _all.Add(obj as T);
            }
        }

        private void Release(T obj)
        {
            obj.gameObject.SetActive(false);
            _available.Enqueue(obj);
        }
        
        public void Prewarm(int count)
        {
            
        }
    }
}