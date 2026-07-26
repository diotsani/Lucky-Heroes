using System;
using Enums;
using UnityEngine;

namespace Database
{
    [CreateAssetMenu(fileName = "UI Stats Data", menuName = "Database/UI Stats Data")]
    public class UIStatsData : ScriptableObject
    {
        [SerializeField] private UIStats[] uiStats;
        public UIStats[] UIStats => uiStats;
    }

    [Serializable]
    public struct UIStats
    {
        public string Label;
        public string Description;
        public Sprite Icon;
        public StatType StatType;
    }
}