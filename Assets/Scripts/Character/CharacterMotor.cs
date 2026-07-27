using System;
using Core;
using UnityEngine;
using UnityEngine.Serialization;

namespace Character
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class CharacterMotor : MonoBehaviour
    {
        private CharacterBrain _brain;
        private ArenaBound _arena;

        public void Initialize(CharacterBrain brain, ArenaBound arena)
        {
            _brain = brain;
            _arena = arena;
        }

        public void Move(Vector2 move, float speed)
        {
            if (move.sqrMagnitude < 0.01f) return;
            
            move.Normalize();
            
            Vector2 nextPosition = (Vector2)transform.position + move * speed * Time.deltaTime;

            if (_arena == null || _arena.IsInside(nextPosition))
            {
                transform.position = nextPosition;
            }
            
            Rotate(move);
        }

        private void Rotate(Vector2 direction)
        {
            if (Mathf.Abs(direction.x) < 0.01f) return;
            transform.localScale = new Vector3(direction.x > 0 ? 1 : -1, 1, 1);
        }

        public void Stop()
        {
            
        }
    }
}