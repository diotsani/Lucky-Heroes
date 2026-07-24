using Database.Character;
using Enemy;
using UnityEngine;
using UnityEngine.Serialization;

namespace Database.Weapon
{
    public abstract class WeaponData : ScriptableObject
    {
        public string WeaponName;
        [Tooltip("Flat bonus attack")]
        public float Attack;
        [Tooltip("Damage in percentage")]
        public float AttackDamage;
        public float AttackCooldown;
        public AnimationClip AttackClip;
        public Vector2 AttackSize;
    }
}