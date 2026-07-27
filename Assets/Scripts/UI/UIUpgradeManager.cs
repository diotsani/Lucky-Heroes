using System;
using System.Collections.Generic;
using Core;
using Database.Upgrade;
using UnityEngine;

namespace UI
{
    public class UIUpgradeManager : MonoBehaviour
    {
        [SerializeField] private UpgradeManager manager;
        [SerializeField] private UIUpgradeView view;

        private void OnEnable()
        {
            view.OnNextButtonClicked += OnNext;
        }

        private void OnDisable()
        {
            view.OnNextButtonClicked -= OnNext;
        }

        public void OpenUI()
        {
            view.Open();
        }

        public void OpenUIViewOnly(List<UpgradeData> upgrades)
        {
            view.OpenViewOnly();
            for (int i = 0; i < upgrades.Count; i++)
            {
                var data = upgrades[i];
                string desc = $"{data.UpgradeValueString()} {data.upgradeStat}";
                view.InitializeViewOnly(data.upgradeIcon, data.upgradeName, desc);
            }
        }

        public void CloseUI()
        {
            view.Close();
        }
        
        public void RefreshUI(UpgradeData[] upgrades)
        {
            for (int i = 0; i < upgrades.Length; i++)
            {
                var data = upgrades[i];
                string desc = $"{data.UpgradeValueString()} {data.upgradeStat}";
                view.Initialize(i, data.upgradeIcon, data.upgradeName, desc, () =>
                {
                    OnChoose(data);
                });
            }
        }

        public void Next()
        {
            view.Next();
        }

        private void OnChoose(UpgradeData upgrade)
        {
            manager.Choose(upgrade);
        }

        private void OnNext()
        {
            manager.Next();
        }
    }
}