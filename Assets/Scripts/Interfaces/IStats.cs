using System;
using Database;

namespace Interfaces
{
    public interface IStats
    {
        public RuntimeStats RuntimeStats { get; }
        void InitializeStats();
        void ReduceHealth(float amount);
    }
}