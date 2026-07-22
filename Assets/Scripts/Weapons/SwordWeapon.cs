using System;
using Context;
using Damages;
using Database.Character;
using Enemy;
using Interfaces;
using UnityEngine;

namespace Weapons
{
    public class SwordWeapon : WeaponBase
    {
        [SerializeField] private Transform attackPoint;
        [SerializeField] private LayerMask layerMask;
        
        public override void Attack(AttackContext attackContext, IDamageable targetDamageable)
        {
            Collider2D[] hits = Physics2D.OverlapBoxAll(attackPoint.position, Data.AttackSize, attackPoint.eulerAngles.z, layerMask);

            foreach (Collider2D hit in hits)
            {
                if (hit.TryGetComponent(out IDamageable damageable))
                {
                    var ctx = new DamageContext
                    {
                        DamageScale = attackContext.Attack,
                        DamageMultiplier = Data.AttackDamage, 
                        LuckScale = attackContext.Luck, 
                        OwnerLevel = attackContext.Level, 
                        TargetLevel = damageable.GetRuntimeStats().Level,
                    };
                    float dmg = DamageFormula.CalculateDamage(ctx);
                    damageable.TakeDamage(dmg);
                }
            }
        }

#if UNITY_EDITOR
        /*private void OnDrawGizmosSelected()
        {
            if (Data == null)return;
            if (attackPoint == null)return;
            
            Gizmos.color = Color.red;
            Matrix4x4 matrix = Gizmos.matrix;
            
            Gizmos.matrix = Matrix4x4.TRS(attackPoint.position, attackPoint.rotation, Vector3.one);
            
            Gizmos.DrawWireCube(Vector3.zero, Data.AttackSize);
            
            Gizmos.matrix = matrix;
        }*/
#endif
    }
}