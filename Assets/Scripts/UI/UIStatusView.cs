using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIStatusView : MonoBehaviour
    {
        [Header("Bar")]
        [SerializeField] private Image hpFillImage;
        [SerializeField] private Image expFillImage;
        [SerializeField] private Image mpFillImage;
        [SerializeField] private Image staminaFillImage;
        [Header("Resources")]
        [SerializeField] private TMP_Text goldText;
        [Header("Button")]
        [SerializeField] private Button statsButton;

        public Action OnStatsClicked;

        private void OnEnable()
        {
            statsButton.onClick.AddListener(() => OnStatsClicked?.Invoke());
        }

        private void OnDisable()
        {
            statsButton.onClick.RemoveAllListeners();
        }

        public void UpdateHpFill(float value)
        {
            hpFillImage.fillAmount = value;
        }
        
        public void UpdateExpFill(float value)
        {
            expFillImage.fillAmount = value;
        }

        public void UpdateMpFill(float value)
        {
            mpFillImage.fillAmount = value;
        }

        public void UpdateStaminaFill(float value)
        {
            staminaFillImage.fillAmount = value;
        }
        
        public void UpdateGoldText(int value)
        {
            goldText.text = value.ToString();
        }
    }
}