using System;
using System.Collections;
using Core.GameMode;
using Database.Core;
using Enums;
using Interfaces;
using UnityEngine;

namespace Core
{
    public class WaveModeManager : GameModeManager
    {
        [SerializeField] private LevelWaveData levelWave;
        public int CurrentWave { get; private set; }

        public WaveData CurrentWaveData { get; private set; }
        
        private float _currentTime = 0;
        private Coroutine _waveCoroutine;

        public override void Setup()
        {
            
        }

        public override void NextMode()
        {
            CurrentWave++;
            
            if (CurrentWave > levelWave.TotalWave)
            {
                OnCompleteMode?.Invoke();
                return;
            }
            
            CurrentWaveData = levelWave.GetWaveData(CurrentWave - 1);
            
            StartMode();
        }

        protected override void StartMode()
        {
            OnStartMode?.Invoke();
            _waveCoroutine = StartCoroutine(StartTimerCor());
            
            ui.UpdateLevel($"Wave {CurrentWave}");
        }

        protected override void EndMode()
        {
            OnEndMode?.Invoke();
            _waveCoroutine = null;
            
            ui.UpdateTime(0);
        }

        public override EnemyType GetEnemyType()
        {
            return CurrentWaveData.GetEnemyType();
        }

        IEnumerator StartTimerCor()
        {
            ElapsedGameTime += CurrentWaveData.Duration;
            _currentTime = CurrentWaveData.Duration;
            
            while (_currentTime > 0)
            {
                ui.UpdateTime(_currentTime);
                _currentTime -= Time.deltaTime;
                yield return null;
            }
            
            EndMode();
        }
    }
}