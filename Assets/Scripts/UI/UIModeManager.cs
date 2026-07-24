using TMPro;
using UnityEngine;

namespace UI
{
    public class UIModeManager : MonoBehaviour
    {
        [SerializeField] private UIModeView view;

        public void UpdateLevel(string level)
        {
            view.UpdateLevelText(level);
        }
        
        public void UpdateTime(float time)
        {
            view.UpdateTimeText(time);
        }
    }
}