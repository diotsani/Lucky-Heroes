using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI
{
    public class UIUpgradeView : MonoBehaviour
    {
        [FormerlySerializedAs("upViews")] [SerializeField] private UIUpgrade[] uiUpgrades;
        [SerializeField] private Button nextButton;
        public Action OnNextButtonClicked;
        
        public void Open()
        {
            gameObject.SetActive(true);
            nextButton.gameObject.SetActive(false);
            nextButton.onClick.AddListener(() => OnNextButtonClicked?.Invoke());
            foreach (UIUpgrade ui in uiUpgrades)
            {
                ui.gameObject.SetActive(true);
            }
        }

        public void Close()
        {
            gameObject.SetActive(false);
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
    }
}