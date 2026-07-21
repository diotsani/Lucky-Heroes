using Database.Character;
using UnityEngine;

namespace Enemy
{
    public class EnemyStats : MonoBehaviour
    {
        [SerializeField] private RuntimeStats stats;
        public RuntimeStats RuntimeStats => stats;
    }
}