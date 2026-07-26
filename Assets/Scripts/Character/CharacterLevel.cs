using System;
using Core;
using UnityEngine;

namespace Character
{
    public class CharacterLevel : MonoBehaviour
    {
        [SerializeField] private CharacterBrain brain;
        [Header("Exp")]
        [SerializeField] private int baseExp = 10;
        [SerializeField] private float growth = 1.10f;
        private int _requiredExp;
        private int _lastLevel = 0;
        
        public Action<float> OnGainExp { get; set; }
        private Action OnLevelUp;

        private void Start()
        {
            _requiredExp = baseExp;
        }

        public void GainExp(int amount)
        {
            brain.Stats.RuntimeStats.Experience += amount;
            OnGainExp?.Invoke(Exp());
            while (brain.Stats.RuntimeStats.Experience >= _requiredExp)
            {
                brain.Stats.RuntimeStats.Experience -= _requiredExp;
                LevelUp();
            }
        }

        private void LevelUp()
        {
            Debug.Log("Character level up");
            brain.Stats.RuntimeStats.Level++;
            _requiredExp = RequiredExp();
            OnLevelUp?.Invoke();
        }

        public int LevelUpgrade()
        {
            int up = brain.Stats.RuntimeStats.Level -  _lastLevel;
            _lastLevel = brain.Stats.RuntimeStats.Level;
            return up;
        }

        private int RequiredExp()
        {
            return Mathf.RoundToInt(baseExp * Mathf.Pow(growth, brain.Stats.RuntimeStats.Level - 1));
        }

        public float Exp()
        {
            return (float)brain.Stats.RuntimeStats.Experience / _requiredExp;
        }
    }
}