using Database.Character;
using UnityEngine;

namespace Character
{
    public class CharacterStats : MonoBehaviour
    {
        [SerializeField] private RuntimeStats runtimeStats;
        
        public RuntimeStats RuntimeStats => runtimeStats;
    }
}