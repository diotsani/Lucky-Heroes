using System;
using UnityEngine;

namespace Drop
{
    public class PickupDetector : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Pickup pickup))
            {
                pickup.Trigger();
            }
        }
    }
}