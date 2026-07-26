using Database.Core;
using Enums;
using UnityEngine;

namespace Database.Enemy
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "Database/EnemyData", order = 2)]
    public class EnemyData : EntityData
    {
        [Header("Enemy")] 
        [SerializeField] private EnemyStateType firstState;
        [SerializeField] private float detectRange;
        [SerializeField] private float detectInterval = 0.2f;
        [SerializeField] private float attackRange;
        [SerializeField] private float attackInterval = 1;
        [SerializeField] private float patrolRange;
        [SerializeField] private float patrolInterval = 2;
        [Header("Loot")]
        [SerializeField] private LootTableData loot;

        public EnemyStateType FirstState => firstState;
        public float DetectRange => detectRange;
        public float DetectInterval => detectInterval;
        public float AttackRange => attackRange;
        public float AttackInterval => attackInterval;
        public float PatrolRange => patrolRange;
        public float PatrolInterval => patrolInterval;
        public LootTableData Loot => loot;
    }
}