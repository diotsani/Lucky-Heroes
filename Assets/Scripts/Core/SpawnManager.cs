using System;
using System.Collections.Generic;
using Enemy;
using Pool;
using UnityEngine;

namespace Core
{
    public class SpawnManager : MonoBehaviour
    {
        [SerializeField] private GameManager game;
        private PoolManager _pool;
        
        private float _baseSpawnInterval;
        private float _spawnInterval;
        private int _baseSpawnCount;
        private int _spawnCount;
        private bool _isSpawning;
        
        public void Setup()
        {
            
        }

        private void Start()
        {
            _pool = Services.Services.Get<PoolManager>();
        }

        public void StartSpawn(float spawnInterval, int spawnCount)
        {
            _baseSpawnInterval = spawnInterval;
            _baseSpawnCount = spawnCount;
            _spawnInterval = _baseSpawnInterval * game.RandomMultiplier;
            _spawnCount = Mathf.RoundToInt(_baseSpawnCount * game.RandomMultiplier);
            _isSpawning = true;
        }

        public void EndSpawn()
        {
            _isSpawning = false;
        }

        public void CompleteSpawn()
        {
            _isSpawning = false;
        }

        private void Update()
        {
            if(!_isSpawning)return;
            _spawnInterval -= Time.deltaTime;

            if (_spawnInterval <= 0)
            {
                for (int i = 0; i < _spawnCount; i++)
                {
                    var enemy = _pool.GetEnemy(game.EnemyType);
                    enemy.Initialize(game.SpawnPosition);
                }
                _spawnInterval = _baseSpawnInterval * game.RandomMultiplier;
                _spawnCount = Mathf.RoundToInt(_baseSpawnCount * game.RandomMultiplier);
            }
        }
    }
}