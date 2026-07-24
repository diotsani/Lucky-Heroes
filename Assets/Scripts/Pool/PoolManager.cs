using System;
using System.Collections.Generic;
using System.Linq;
using Enemy;
using Enums;
using Interfaces;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Pool
{
    public class PoolManager : MonoBehaviour
    {
        [SerializeField] private EnemyBrain[] enemies;
        [SerializeField] private Transform enemiesParent;
        
        private readonly Dictionary<GameObject, IObjectPool> _pools = new ();

        private void Awake()
        {
            Services.Services.Register(this);
            
            foreach (var t in enemies)
            {
                _pools.Add(t.gameObject, new ObjectPool<EnemyBrain>()
                {
                    Parent = enemiesParent,
                });
            }
        }

        public EnemyBrain GetEnemy(EnemyType type)
        {
            EnemyBrain enemy = enemies.First(e => e.Type == type);
            return Get(enemy);
        }

        private T Get<T>(T prefab) where T : Component
        {
            return (T)_pools[prefab.gameObject].Get(prefab);
        }
    }
}