using System;
using Enums;
using Events;
using UnityEngine;

namespace Character
{
    public class CharacterAnimator : MonoBehaviour
    {
        [SerializeField] private CharacterAnimationType animationType;
        [SerializeField] private Animator animator;
        [SerializeField] private WeaponAnimationEvent weaponAnimationEvent;
        public WeaponAnimationEvent WeaponEvent => weaponAnimationEvent;

        private bool _hold = false;

        public void PlayAnimation(CharacterAnimationType type)
        {
            if(_hold)return;
            animationType = type;
            animator.Play(type.ToString());
        }

        public void Continue(CharacterStateType type)
        {
            _hold = false;
            animationType = type switch
            {
                CharacterStateType.Idle => CharacterAnimationType.Idle,
                CharacterStateType.Walk => CharacterAnimationType.Walk,
                CharacterStateType.Run => CharacterAnimationType.Run,
                CharacterStateType.Dead => CharacterAnimationType.Death,
                _ => animationType
            };
            PlayAnimation(animationType);
        }

        public void Hold()
        {
            _hold = true;
        }
    }
}