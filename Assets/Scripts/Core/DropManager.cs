using System.Collections.Generic;
using Drop;
using Enums;
using Pool;
using UnityEngine;

namespace Core
{
    public class DropManager : MonoBehaviour
    {
        [SerializeField] private GameManager game;
        [SerializeField] private float dropRadius = 0.5f;
        private readonly List<Pickup> _activePickups = new List<Pickup>();
        private PoolManager _pool;
        
        private void Start()
        {
            _pool = Services.ServiceLocator.Get<PoolManager>();
        }
        
        public void SpawnExp(int value, Vector2 position)
        {
            var exp = _pool.GetPickup(PickupType.Exp);
            _activePickups.Add(exp);
            exp.Spawn(this, value, DropPosition(position));
        }

        public void SpawnGold(int value, Vector2 position)
        {
            var gold = _pool.GetPickup(PickupType.Gold);
            _activePickups.Add(gold);
            gold.Spawn(this, value, DropPosition(position));
        }

        public void Remove(Pickup pickup)
        {
            _activePickups.Remove(pickup);
        }

        public void DespawnAll()
        {
            foreach (var pickup in _activePickups)
            {
                pickup.ForceDespawn();
            }
            
            _activePickups.Clear();
        }

        Vector2 DropPosition(Vector2 pos)
        {
            return new Vector2(Random.Range(pos.x - dropRadius, pos.x + dropRadius), Random.Range(pos.y - dropRadius, pos.y + dropRadius));
        }
    }
}