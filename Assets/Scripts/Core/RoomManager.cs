using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core
{
    public class RoomManager : MonoBehaviour
    {
        [SerializeField] private ArenaBound arena;
        [SerializeField] private Transform[] points;
        [SerializeField] private SpawnPoint[] spawnPoints;
        private SpawnPoint[] _activeSpawnPoints;
        
        public ArenaBound Arena => arena;

        public void Setup()
        {
            //Do you need to take the active one?
            //Spawn environment obstacle?
            _activeSpawnPoints = spawnPoints;
        }

        public Vector2 GetSpawnPointPosition()
        {
            int random = Random.Range(0, _activeSpawnPoints.Length);
            Vector2 point = _activeSpawnPoints[random].point.position;
            float radius = _activeSpawnPoints[random].radius;
            
            Vector2 get = new Vector2
            {
                x = Random.Range(point.x - radius, point.x + radius),
                y = Random.Range(point.y - radius, point.y + radius)
            };
            return get;
        }
#if UNITY_EDITOR
        [ContextMenu("Setup Points")]
        private void SetupPoints()
        {
            for (int i = 0; i < points.Length; i++)
            {
                spawnPoints[i].id = i;
                spawnPoints[i].point = points[i];
                spawnPoints[i].radius = 0.5f;
            }
        }
#endif
    }

    [Serializable]
    public class SpawnPoint
    {
        public int id;
        public Transform point;
        public float radius;
    }
}