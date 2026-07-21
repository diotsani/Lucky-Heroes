using Database.Character;

namespace Interfaces
{
    public interface IDamageable
    {
        void TakeDamage(float damage);
        void Death();
        public RuntimeStats GetRuntimeStats();
    }
}