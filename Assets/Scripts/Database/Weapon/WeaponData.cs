using Database.Character;
using Enemy;
using UnityEngine;

namespace Database.Weapon
{
    public abstract class WeaponData : ScriptableObject
    {
        public string WeaponName;
        [Tooltip("Flat bonus attack")]
        public float Attack;
        [Tooltip("Damage in percentage")]
        public float AttackDamage;
        public float AttackInterval;
        public Vector2 AttackSize;
    }
}