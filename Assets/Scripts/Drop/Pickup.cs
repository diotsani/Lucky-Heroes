using Character;
using Enums;
using Pool;
using UnityEngine;

namespace Drop
{
    public abstract class Pickup : MonoBehaviour
    {
        [SerializeField] private PickupType type;
        [SerializeField] private PickupState state;
        public PickupType Type => type;

        [Header("Magnet")]
        //[SerializeField] private float collectRadius = 1f;
        [SerializeField] private float moveSpeed = 3f;
        public abstract int Value { get; set; }
        protected CharacterBrain Player;
        
        public void Spawn(int value, Vector2 position)
        {
            state = PickupState.Idle;
            if(Player == null)Player = Services.Services.Get<CharacterBrain>();
            transform.position = position;
            Value = value;
            gameObject.SetActive(true);
        }

        public void Trigger()
        {
            state = PickupState.Magnet;
        }

        protected virtual void Update()
        {
            if (state == PickupState.Magnet)
            {
                OnMagnet();
            }
        }

        private void OnIdle()
        {
            
        }

        private void OnMagnet()
        {
            var playerPos = Player.transform.position;
            transform.position = Vector2.MoveTowards(transform.position, playerPos, moveSpeed * Time.deltaTime);
            
            float sqrDistance = (playerPos - transform.position).sqrMagnitude;

            if (sqrDistance <= 0.05f)
            {
                Collect();
            }
        }

        private void Collect()
        {
            state = PickupState.Collected;
            OnCollected();
            Services.Services.Get<PoolManager>().Release(this);
        }
        
        public void ForceDespawn()
        {
            Services.Services.Get<PoolManager>().Release(this);
        }
        
        protected abstract void OnCollected();
    }
}