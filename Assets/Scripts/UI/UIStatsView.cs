using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIStatsView : MonoBehaviour
    {
        [Header("Bar")]
        [SerializeField] private Image hpFillImage;
        [SerializeField] private Image mpFillImage;
        [SerializeField] private Image staminaFillImage;
        [Header("Stats")]
        [SerializeField] private GameObject statsPanel;
        [SerializeField] private TMP_Text statsText;
        
        public void UpdateHpFill(float value)
        {
            hpFillImage.fillAmount = value;
        }

        public void UpdateMpFill(float value)
        {
            mpFillImage.fillAmount = value;
        }

        public void UpdateStaminaFill(float value)
        {
            staminaFillImage.fillAmount = value;
        }
    }
}