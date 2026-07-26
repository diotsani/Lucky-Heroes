using Core;
using Database.Upgrade;
using UnityEngine;

namespace UI
{
    public class UIUpgradeManager : MonoBehaviour
    {
        [SerializeField] private UpgradeManager manager;
        [SerializeField] private UIUpgradeView view;

        public void OpenUI()
        {
            view.OnNextButtonClicked += OnNext;
            view.Open();
        }

        public void CloseUI()
        {
            view.OnNextButtonClicked -= OnNext;
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