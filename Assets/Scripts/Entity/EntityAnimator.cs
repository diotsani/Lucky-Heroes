using System;
using Enums;
using Events;
using UnityEngine;

namespace Entity
{
    public class EntityAnimator : MonoBehaviour
    {
        [SerializeField] private EntityAnimationType animationType;
        [SerializeField] private Animator animator;
        [SerializeField] private WeaponAnimationEvent weaponAnimationEvent;
        public WeaponAnimationEvent WeaponEvent => weaponAnimationEvent;
        
        public readonly int Speed = Animator.StringToHash("Speed");
        public readonly int Run = Animator.StringToHash("Run");
        public readonly int Attack = Animator.StringToHash("Attack");
        public readonly int Death = Animator.StringToHash("Death");
        public readonly int ASpd = Animator.StringToHash("ASpd");

        public void SetFloat(int id, float value)
        {
            animator.SetFloat(id, value);
        }

        public void SetBool(int id, bool value)
        {
            animator.SetBool(id, value);
        }

        public void SetTrigger(int id)
        {
            animator.SetTrigger(id);
        }
    }
}