using Database.Character;
using Enemy;
using UnityEngine;

namespace Database.Weapon
{
    public abstract class WeaponData : ScriptableObject
    {
        public string WeaponName;
        public float AttackDamage;
        public float AttackInterval;
        public Vector2 AttackSize;
    }
}