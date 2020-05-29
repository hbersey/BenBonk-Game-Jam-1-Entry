using System;
using Controllers.Platformer.MovingNPCs;
using UnityEngine;
using Util.Health;

namespace Controllers.Platformer.Player
{
    public class Player : PlatformerMovingNpc, IHasHealth, IDamageable
    {
        [SerializeField] internal float jumpSpeed;
        [SerializeField] private LayerMask groundLayer;

        private float _health;

        public override void Start() 
        {
            base.Start();

            SetState(new PlayerMoveState(this, groundLayer));
        }

        public float GetHealth() => _health;

        public void SetHealth(float health) => _health = health;

        public bool CanTakeDamage() => ((IPlayerState) State).CanTakeDamage();

        public void TakeDamage(float amount)
        {
            _health -= amount;
            if (_health <= 0f)
                SetState(new PlayerDeathState(this));
        }
    }
}