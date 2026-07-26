using System;
using Enums;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Database.Core
{
    [CreateAssetMenu(fileName = "Loot Table", menuName = "Database/Loot Table")]
    public class LootTableData : ScriptableObject
    {
        public LootEntry[] entries;

        public LootData[] RollData()
        {
            LootData[] rollData = new LootData[entries.Length];
            for (int i = 0; i < entries.Length; i++)
            {
                rollData[i] = new LootData
                {
                    LootType = entries[i].LootType,
                    Amount = entries[i].Amount
                };
            }
            return rollData;
        }
    }

    [Serializable]
    public class LootEntry
    {
        public LootType LootType;
        
        public int MinAmount;
        public int MaxAmount;
        
        [HideInInspector] public float DropChance;
        [HideInInspector] public int Weight;
        
        public int Amount => Random.Range(MinAmount,  MaxAmount + 1);
    }

    [Serializable]
    public class LootData
    {
        public LootType LootType;
        public int Amount;
    }
}