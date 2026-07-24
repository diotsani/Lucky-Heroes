using System;
using Pool;
using UnityEngine;

namespace Services
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private PoolManager poolManager;

        private void Awake()
        {
            Services.Register(poolManager);
        }
    }
}