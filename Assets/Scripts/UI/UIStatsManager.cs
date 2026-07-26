using System;
using System.Globalization;
using Core;
using Database;
using Enums;
using UnityEngine;

namespace UI
{
    public class UIStatsManager : MonoBehaviour
    {
        [SerializeField] private UIStatsData data;
        [SerializeField] private UIStatsView view;
        private GameManager _game;

        private void OnEnable()
        {
            view.OnCloseClicked += CloseUI;
        }

        private void OnDisable()
        {
            view.OnCloseClicked -= CloseUI;
        }

        private void Start()
        {
            _game = Services.Services.Get<GameManager>();
            for (int i = 0; i < data.UIStats.Length; i++)
            {
                var stat = data.UIStats[i];
                view.Setup(i, stat.StatType, stat.Icon, stat.Label, stat.Description, GetValueString(stat.StatType));
            }
        }

        public void OpenUI()
        {
            view.Open();
            for (int i = 0; i < data.UIStats.Length; i++)
            {
                var stat = data.UIStats[i];
                view.UpdateValue(i, GetValueString(stat.StatType));
            }
        }

        public void UpdateSpecificValue(StatType type)
        {
            view.UpdateValue((int)type, GetValueString(type));
        }

        public void CloseUI()
        {
            view.Close();
        }

        private string GetValueString(StatType type)
        {
            return _game.Character.Stats.GetValue(type).ToString(CultureInfo.InvariantCulture);
        }
    }
}