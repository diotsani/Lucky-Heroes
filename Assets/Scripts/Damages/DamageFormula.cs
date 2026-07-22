using System.Linq;
using Context;
using UnityEngine;

namespace Damages
{
    public static class DamageFormula
    {
        public static float CalculateDamage(DamageContext context)
        {
            float final = 0;
            final = BaseDamage(context.DamageScale, context.DamageMultiplier) * Defense(context.OwnerLevel, context.TargetLevel);
            final += LuckDamage(context.LuckScale, context.OwnerLevel);
            return final;
        }

        static float BaseDamage(float scale, float multiplier)
        {
            Debug.Log($"Base Dmg: {scale * (multiplier / 100)}");
            return scale * (multiplier / 100);
        }

        static float DamageBonus(float[] bonuses)
        {
            return (1 + (bonuses.Sum() / 100)) * 1000;
        }

        static float LuckDamage(float scale, int characterLevel)
        {
            return scale / characterLevel;
        }

        static float Critical(float critDamage)
        {
            return 1 + critDamage / 100;
        }

        static float Defense(int characterLevel, int enemyLevel)
        {
            Debug.Log($"{characterLevel}, {enemyLevel} > Defense: {(characterLevel + 100) / (characterLevel + enemyLevel + 200)}");
            return (characterLevel + 100f) / (characterLevel + enemyLevel + 200f);
        }
    }
}