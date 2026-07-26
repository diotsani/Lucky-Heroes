using System;
using Enums;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace Database.Upgrade
{
    [CreateAssetMenu(fileName = "UpgradeData", menuName = "Database/Core/UpgradeData")]
    public class UpgradeData : ScriptableObject
    {
        [Header("Detail")]
        public string upgradeName;
        public string upgradeDescription;
        public Sprite upgradeIcon;
        
        [Header("Stats")]
        public UpgradeRarityType upgradeRarity;
        [Range(0,100)] public int upgradeWeight;
        public StatType upgradeStat;
        public UpgradeValueType upgradeValueType;
        public int upgradeValue;

        public string UpgradeValueString()
        {
            var value = $"+{upgradeValue}";
            if (upgradeValueType == UpgradeValueType.Percentage)
            {
                value += "%";
            }
            return value;
        }

#if UNITY_EDITOR
        [ContextMenu("Rename")]

        private void Rename()
        {
            string path = AssetDatabase.GetAssetPath(Selection.activeObject);
            string newName = $"{upgradeRarity} {upgradeName} Upgrade";
            string error = AssetDatabase.RenameAsset(path, newName);
            if (!string.IsNullOrEmpty(error))
            {
                Debug.LogError($"Gagal mengubah nama: {error}");
            }
            else
            {
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
        }
#endif
    }
}