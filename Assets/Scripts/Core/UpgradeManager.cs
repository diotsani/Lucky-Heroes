using System;
using System.Collections.Generic;
using Database.Upgrade;
using UI;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core
{
    public class UpgradeManager : MonoBehaviour
    {
        [SerializeField] private GameManager game;
        [Header("Upgrade")]
        [SerializeField] private UpgradeData[] upgrades;
        [SerializeField] private int selectedUpgradeCount = 4;
        private List<UpgradeData> _selectedUpgrades = new List<UpgradeData>();
        private int _upgradeCount;
        private int _upgradedCount;
        
        [Header("UI")]
        [SerializeField] private UIUpgradeManager ui;
        [SerializeField] private UIStatsManager uiStats;
        
        public void Roll(int count)
        {
            _upgradedCount = 0;
            _upgradeCount = count;
            ui.OpenUI();
            uiStats.OpenUI();
            if (count == 0)
            {
                ui.Next();
            }
            else
            {
                ui.RefreshUI(RollUpgrades());
            }
        }

        public void Choose(UpgradeData data)
        {
            game.Character.Stats.Upgrade(data);
            _selectedUpgrades.Add(data);
            uiStats.UpdateSpecificValue(data.upgradeStat);
            _upgradedCount++;
            if (_upgradedCount >= _upgradeCount)
            {
                ui.Next();
            }
            else
            {
                ui.RefreshUI(RollUpgrades());
            }
        }

        public void Next()
        {
            ui.CloseUI();
            uiStats.CloseUI();
            game.StartGame();
        }

        public void OpenUIViewOnly()
        {
            ui.OpenUIViewOnly(_selectedUpgrades);
            uiStats.OpenUI();
        }
        
        private UpgradeData[] RollUpgrades()
        {
            UpgradeData[] u = new UpgradeData[selectedUpgradeCount];

            for (int i = 0; i < selectedUpgradeCount; i++)
            {
                // Temporary can get same upgrade
                u[i] = GetRandomUpgrade();
            }
            
            return u;
        }

        private UpgradeData GetRandomUpgrade()
        {
            int totalWeight = 0;
            foreach (var up in upgrades)
            {
                totalWeight += up.upgradeWeight;
            }
            
            int random = Random.Range(0, totalWeight + 1);

            foreach (var up in upgrades)
            {
                random -= up.upgradeWeight;
                if (random < 0)
                {
                    return up;
                }
            }
            return upgrades[0];
        }
    }
}