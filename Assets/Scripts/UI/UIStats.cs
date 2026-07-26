using System.Globalization;
using Enums;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIStats : MonoBehaviour
    {
        [SerializeField] private StatType statType;
        [SerializeField] private Image image;
        [SerializeField] private TMP_Text labelText;
        [SerializeField] private TMP_Text descriptionText;
        [SerializeField] private TMP_Text valueText;
        [SerializeField] private Button clickButton;

        public void Setup(StatType type, Sprite icon, string label, string description, string value)
        {
            statType = type;
            image.sprite = icon;
            labelText.text = label;
            descriptionText.text = description;
            UpdateValue(value);
        }

        public void UpdateValue(string value)
        {
            valueText.text = value;
        }
    }
}