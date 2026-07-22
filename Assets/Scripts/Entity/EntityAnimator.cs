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

        private bool _hold = false;

        public void PlayAnimation(EntityAnimationType type)
        {
            if(_hold)return;
            //if(animationType == type)return;
            animationType = type;
            animator.Play(type.ToString());
        }

        public void Continue(CharacterStateType type)
        {
            _hold = false;
            animationType = type switch
            {
                CharacterStateType.Idle => EntityAnimationType.Idle,
                CharacterStateType.Walk => EntityAnimationType.Walk,
                CharacterStateType.Run => EntityAnimationType.Run,
                CharacterStateType.Dead => EntityAnimationType.Death,
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