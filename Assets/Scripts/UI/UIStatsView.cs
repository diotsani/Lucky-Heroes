using System;
using Enums;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIStatsView : MonoBehaviour
    {
        [SerializeField] private UIStats[] uiStats;
        [SerializeField] private Button closeButton;
        
        public Action OnCloseClicked;

        public void Open()
        {
            gameObject.SetActive(true);
            closeButton.onClick.AddListener(() => OnCloseClicked?.Invoke());
            
        }

        public void Close()
        {
            gameObject.SetActive(false);
            closeButton.onClick.RemoveAllListeners();
        }
        
        public void Setup(int i, StatType type, Sprite icon, string label, string description, string value)
        {
            uiStats[i].Setup(type, icon, label, description, value);
        }
        
        public void UpdateValue(int i, string value)
        {
            uiStats[i].UpdateValue(value);
        }
    }
}