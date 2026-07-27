using System;
using Character;
using UnityEngine;

namespace UI
{
    public class UIStatusManager : MonoBehaviour
    {
        [SerializeField] private UIStatsManager stats;
        [SerializeField] private UIStatusView view;
        private CharacterBrain _character;

        private void OnEnable()
        {
            
        }
        
        private void OnDisable()
        {
            _character.Stats.OnHealthChanged -= UpdateHp;
            _character.Level.OnGainExp -= UpdateExp;
            _character.Resources.OnGoldChanged -= UpdateGold;
            
            view.OnStatsClicked -= StatsClicked;
        }

        private void Start()
        {
            if(_character == null) _character = Services.ServiceLocator.Get<CharacterBrain>();
            _character.Stats.OnHealthChanged += UpdateHp;
            _character.Level.OnGainExp += UpdateExp;
            _character.Resources.OnGoldChanged += UpdateGold;

            view.OnStatsClicked += StatsClicked;
        }

        private void StatsClicked()
        {
            stats.OpenUI();
        }

        private void UpdateHp(float value)
        {
            view.UpdateHpFill(value);
        }

        private void UpdateExp(float value)
        {
            view.UpdateExpFill(value);
        }

        public void UpdateMp(float value)
        {
            view.UpdateMpFill(value);
        }

        public void UpdateStamina(float value)
        {
            view.UpdateStaminaFill(value);
        }

        private void UpdateGold(int value)
        {
            view.UpdateGoldText(value);
        }
    }
}