using UnityEngine;

namespace UI
{
    public class UIStatsManager : MonoBehaviour
    {
        [SerializeField] private UIStatsView view;

        public void UpdateHp(float value)
        {
            view.UpdateHpFill(value);
        }

        public void UpdateMp(float value)
        {
            view.UpdateMpFill(value);
        }

        public void UpdateStamina(float value)
        {
            view.UpdateStaminaFill(value);
        }
    }
}