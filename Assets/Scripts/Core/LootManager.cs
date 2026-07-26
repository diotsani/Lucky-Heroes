using System;
using Database.Core;
using Enums;
using UnityEngine;

namespace Core
{
    public class LootManager : MonoBehaviour
    {
        [SerializeField] private GameManager game;
        public void Roll(LootTableData tableData, Vector2 position)
        {
            foreach (var loot in tableData.RollData())
            {
                int value = Mathf.RoundToInt(loot.Amount * game.Difficulty.LootMultiplier);
                switch (loot.LootType)
                {
                    case LootType.Exp:
                        game.Drop.SpawnExp(value, position);
                        break;
                    case LootType.Gold:
                        game.Drop.SpawnGold(value, position);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}