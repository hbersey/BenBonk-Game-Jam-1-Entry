using System;
using UnityEngine;

namespace Player
{
    public class PlayerMoveState : PlayerState
    {
        private static readonly int Horizontal = Animator.StringToHash("Horizontal");
        private static readonly int Vertical = Animator.StringToHash("Vertical");
        private static readonly int Moving = Animator.StringToHash("Moving");
        
        private float _horizontal;
        private float _vertical;

        public PlayerMoveState(PlayerController player) : base(player)
        {
        }

        public override void Update()
        {
            _horizontal = Input.GetAxisRaw("Horizontal");
            _vertical = Input.GetAxisRaw("Vertical");

            Player.Animator.SetFloat(Horizontal, _horizontal);
            Player.Animator.SetFloat(Vertical, _vertical);
        }

        public override void PhysicsUpdate()
        {
            Player.Rigidbody.velocity = new Vector2(_horizontal, _vertical) * Player.movementSpeed;
            Player.Animator.SetBool(Moving, Math.Abs(_horizontal) > 0.1f || Math.Abs(_vertical) > 0.1f);
        }
    }
}