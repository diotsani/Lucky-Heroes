using System;
using Database;

namespace Interfaces
{
    public interface IStats
    {
        public RuntimeStats RuntimeStats { get; }
        public Action OnDeath { get; set; }
        void InitializeStats();
        void ReduceHealth(float amount);
    }
}