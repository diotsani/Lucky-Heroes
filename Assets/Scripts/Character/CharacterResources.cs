using System;
using UnityEngine;

namespace Character
{
    public class CharacterResources : MonoBehaviour
    {
        public int Gold { get; private set; }
        public Action<int> OnGoldChanged;

        public void GainGold(int gold)
        {
            Gold += gold;
            OnGoldChanged?.Invoke(Gold);
        }
        
        public void ReduceGold(int gold)
        {
            Gold -= gold;
            OnGoldChanged?.Invoke(Gold);
        }
    }
}