using Context;
using Database.Character;
using Database.Weapon;
using Enemy;
using Interfaces;
using UnityEngine;

namespace Weapons
{
    public abstract class WeaponBase : MonoBehaviour
    {
        public WeaponData Data { get; set; }
        
        public abstract void Attack(AttackContext attackContext, IDamageable damageable);
    }
}