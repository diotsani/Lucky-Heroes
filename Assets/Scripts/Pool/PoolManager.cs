using System;
using System.Collections.Generic;
using System.Linq;
using Drop;
using Enemy;
using Enums;
using Interfaces;
using Services;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Pool
{
    public class PoolManager : MonoBehaviour
    {
        [Header("Enemy")]
        [SerializeField] private EnemyBrain[] enemies;
        [SerializeField] private Transform enemiesParent;
        
        [Header("Pickup")]
        [SerializeField] private Pickup[] pickups;
        [SerializeField] private Transform pickupsParent;
        
        private readonly Dictionary<GameObject, IObjectPool> _pools = new ();

        private void Awake()
        {
            ServiceLocator.Register(this);
            
            foreach (var e in enemies)
            {
                _pools.Add(e.gameObject, new ObjectPool<EnemyBrain>()
                {
                    Parent = enemiesParent,
                });
            }

            foreach (var p in pickups)
            {
                _pools.Add(p.gameObject, new ObjectPool<Pickup>()
                {
                    Parent = pickupsParent,
                });
            }
        }
        
        private void OnDestroy()
        {
            ServiceLocator.Unregister(this);
        }

        public EnemyBrain GetEnemy(EnemyType type)
        {
            EnemyBrain enemy = enemies.First(e => e.Type == type);
            return Get(enemy);
        }

        public Pickup GetPickup(PickupType type)
        {
            Pickup pickup = pickups.First(p => p.Type == type);
            return Get(pickup);
        }

        public void Release(EnemyBrain brain)
        {
            EnemyBrain enemy = enemies.First(e => e.Type == brain.Type);
            Release(enemy, brain);
        }

        public void Release(Pickup pick)
        {
            Pickup pickup = pickups.First(p => p.Type == pick.Type);
            Release(pickup, pick);
        }

        private T Get<T>(T prefab) where T : Component
        {
            return (T)_pools[prefab.gameObject].Get(prefab);
        }

        private void Release(Component c, Component obj)
        {
            _pools[c.gameObject].Release(obj);
        }
    }
}