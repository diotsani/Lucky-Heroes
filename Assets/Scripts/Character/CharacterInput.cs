using System;
using Inputs;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Character
{
    public class CharacterInput : MonoBehaviour
    {
        private PlayerInputActions _input;
        public Vector2 Move { get; private set; }
        public Vector2 Look { get; private set; }
        
        public bool Running { get; private set; }
        public bool Attacking { get; private set; }

        private bool _enable = true;

        public Action OnAttacked;
        public Action OnInteracted;

        private void Awake()
        {
            _input = new PlayerInputActions();
        }

        private void OnEnable()
        {
            _input.Enable();
        }

        private void OnDisable()
        {
            _input.Disable();
        }

        private void Update()
        {
            if(!_enable)return;
            Move = _input.Player.Move.ReadValue<Vector2>();
            Look = _input.Player.Look.ReadValue<Vector2>();

            Running = _input.Player.Sprint.IsPressed();
            
            if(_input.Player.Attack.IsPressed())
            {
                OnAttacked?.Invoke();
            }

            if (_input.Player.Interact.IsPressed())
            {
                OnInteracted?.Invoke();
            }
        }

        public void Stop()
        {
            _enable = false;
            Move  = Vector2.zero;
            Look = Vector2.zero;
            Running = false;
            Attacking = false;
        }

        public bool Moving()
        {
            return Move != Vector2.zero;
        }
    }
}