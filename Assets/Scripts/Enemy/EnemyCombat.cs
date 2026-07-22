using Context;
using UnityEngine;
using Weapons;

namespace Enemy
{
    public class EnemyCombat : MonoBehaviour
    {
        [SerializeField] private EnemyBrain brain;
        [SerializeField] private WeaponBase weapon;
        
        private void Start()
        {
            weapon.Data = brain.GetWeaponData();
        }
        
        public void ManualAttack()
        {
            
        }
        
        public void WeaponPlay()
        {
            AttackContext ctx = new AttackContext()
            {
                Attack = brain.GetRuntimeStats().Attack,
                Luck = brain.GetRuntimeStats().Luck,
                Level = brain.GetRuntimeStats().Level,
            };
            weapon.Attack(ctx, null);
        }
    }
}