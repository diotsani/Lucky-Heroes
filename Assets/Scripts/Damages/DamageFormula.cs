using System.Linq;

namespace Damages
{
    public static class DamageFormula
    {
        public static float CalculateDamage(DamageFormulaContext context)
        {
            float final = 0;
            final = BaseDamage(context.DamageScale, context.DamageMultiplier) * Defense(context.OwnerLevel, context.TargetLevel);
            final += LuckDamage(context.LuckScale, context.OwnerLevel);
            return final;
        }

        static float BaseDamage(float scale, float multiplier)
        {
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
            return (characterLevel + 100) / (characterLevel + enemyLevel + 200);
        }
    }

    public class DamageFormulaContext
    {
        public float DamageScale;
        public float DamageMultiplier;
        public float[] DamageBonus;
        public float LuckScale;
        public int OwnerLevel;
        public int TargetLevel;
    }
}