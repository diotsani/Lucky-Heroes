using TMPro;
using UnityEngine;

namespace UI
{
    public class UIModeView : MonoBehaviour
    {
        [SerializeField] private TMP_Text levelText;
        [SerializeField] private TMP_Text timeText;
        
        public void UpdateLevelText(string text)
        {
            levelText.text = text;
        }

        public void UpdateTimeText(float time)
        {
            int minutes = Mathf.FloorToInt(time / 60);
            int seconds = Mathf.FloorToInt(time % 60);
            timeText.text = $"{minutes:00}:{seconds:00}";
        }
    }
}