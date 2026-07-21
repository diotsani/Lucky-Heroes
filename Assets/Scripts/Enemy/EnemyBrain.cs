using Database.Character;
using UnityEngine;

namespace Enemy
{
    public class EnemyBrain : MonoBehaviour
    {
        [SerializeField] private EnemyStats stats;
        public RuntimeStats GetRuntimeStats()
        {
            return stats.RuntimeStats;
        }
    }
}