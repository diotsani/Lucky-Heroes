using System;
using Enums;
using UI;
using UnityEngine;

namespace Core.GameMode
{
    public class GameModeManager : MonoBehaviour
    {
        [SerializeField] protected UIModeManager ui;
        public Action OnStartMode;
        public Action OnEndMode;
        public Action OnCompleteMode;

        public float ElapsedGameTime { get; protected set; }
        public float Progress => ElapsedGameTime;
        
        public virtual void Setup()
        {
            
        }

        public virtual void NextMode()
        {
            
        }

        protected virtual void StartMode()
        {
            
        }

        protected virtual void EndMode()
        {
            
        }

        public virtual EnemyType GetEnemyType()
        {
            return EnemyType.Orc1;
        }
    }
}