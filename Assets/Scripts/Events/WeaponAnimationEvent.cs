using System;
using UnityEngine;

namespace Events
{
    public class WeaponAnimationEvent : MonoBehaviour
    {
        public Action PlayAction;
        public Action StopAction;
        public Action EndAction;
        
        public void PlayEvent()
        {
            PlayAction?.Invoke();
        }

        public void StopEvent()
        {
            StopAction?.Invoke();
        }

        public void EndEvent()
        {
            EndAction?.Invoke();
        }
    }
}