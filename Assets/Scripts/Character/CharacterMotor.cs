using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Character
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class CharacterMotor : MonoBehaviour
    {
        [SerializeField] private CharacterBrain brain;
        [SerializeField] private Rigidbody2D rb;
        
        public void Move(Vector2 move, bool run)
        {
            Vector2 moveDirection = move;
            
            if(moveDirection.sqrMagnitude < 0.01f)return;
            
            moveDirection.Normalize();
            
            float speed = run ? brain.GetStats().RunSpeed : brain.GetStats().WalkSpeed;
            rb.linearVelocity = moveDirection * speed;
            
            Rotate(moveDirection);
        }

        private void Rotate(Vector2 direction)
        {
            if(direction.x == 0)return;
            transform.localScale = new Vector3(direction.x > 0 ? 1 : -1, 1, 1);
        }

        public void Stop()
        {
            rb.linearVelocity = Vector2.zero;
        }
    }
}