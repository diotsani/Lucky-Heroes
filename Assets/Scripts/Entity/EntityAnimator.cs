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

        [SerializeField] private bool _hold = false;
        private static readonly int ASpd = Animator.StringToHash("ASpd");

        public virtual void PlayAnimation(EntityAnimationType type)
        {
            if(_hold)return;
            //if(animationType == type)return;
            animationType = type;
            animator.Play(type.ToString());
        }

        public virtual void Continue(CharacterStateType type)
        {
            Debug.Log("Hold Attack False");
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
            Debug.Log("Hold Attack True");
            _hold = true;
        }

        public void UpdateASpdValue(float value)
        {
            animator.SetFloat(ASpd, value);
        }
    }
}