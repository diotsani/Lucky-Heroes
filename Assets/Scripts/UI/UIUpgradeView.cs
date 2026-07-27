using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI
{
    public class UIUpgradeView : MonoBehaviour
    {
        [Header("Parent")]
        [SerializeField] private GameObject upgrade;
        [SerializeField] private GameObject upgradeViewOnly;
        [SerializeField] private Transform viewOnlyParent;
        [Header("Upgrade")]
        [SerializeField] private UIUpgrade uiUpgradePrefab;
        [SerializeField] private UIUpgrade[] uiUpgrades;
        [Header("Button")]
        [SerializeField] private Button nextButton;
        public Action OnNextButtonClicked;
        
        public void Open()
        {
            gameObject.SetActive(true);
            upgrade.SetActive(true);
            upgradeViewOnly.SetActive(false);
            nextButton.gameObject.SetActive(false);
            nextButton.onClick.AddListener(() => OnNextButtonClicked?.Invoke());
            foreach (UIUpgrade ui in uiUpgrades)
            {
                ui.gameObject.SetActive(true);
            }
        }

        public void OpenViewOnly()
        {
            gameObject.SetActive(true);
            upgrade.SetActive(false);
            upgradeViewOnly.SetActive(true);
            nextButton.gameObject.SetActive(false);
        }

        public void Close()
        {
            gameObject.SetActive(false);
            upgrade.SetActive(false);
            upgradeViewOnly.SetActive(false);
            nextButton.onClick.RemoveAllListeners();
        }

        public void Next()
        {
            nextButton.gameObject.SetActive(true);
            foreach (UIUpgrade ui in uiUpgrades)
            {
                ui.gameObject.SetActive(false);
            }
        }
        
        public void Initialize(int i, Sprite icon, string title, string description, Action choose)
        {
            uiUpgrades[i].Initialize(icon, title, description, choose);
        }

        public void InitializeViewOnly(Sprite icon, string title, string description)
        {
            var ui = Instantiate(uiUpgradePrefab, viewOnlyParent);
            ui.Initialize(icon, title, description, null);
            ui.SetViewOnly();
            ui.gameObject.SetActive(true);
        }
    }
}