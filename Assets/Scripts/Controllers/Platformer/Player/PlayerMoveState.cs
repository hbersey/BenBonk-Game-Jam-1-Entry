using Controllers.Platformer.MovingNPCs;
using UnityEngine;

namespace Controllers.Platformer.Player
{
    public class PlayerMoveState : MovingNPCs.States.HorizontalMoveState, IPlayerState
    {
        private const float GroundDistance = 0.8f;
        
        private readonly Player _player;
        private readonly LayerMask _groundLayer;

        public PlayerMoveState(Player player, LayerMask groundLayer) : base(player, 0)
        {
            _player = player;
            _groundLayer = groundLayer;
        }

        public override void PhysicsUpdate()
        {
            XDirection = Input.GetAxisRaw("Horizontal") ;

            if (IsGrounded() && Input.GetAxisRaw("Vertical") > 0)
            {
                _player.Rigidbody.velocity = new Vector2(_player.Rigidbody.velocity.x, _player.jumpSpeed);
            }
            
            base.PhysicsUpdate();
        }

        private bool IsGrounded()
        {
            Vector2 position = _player.transform.position;

            var hit = Physics2D.Raycast(position, Vector2.down, GroundDistance, _groundLayer);
            return hit.collider;
        }

        public bool CanTakeDamage() => true;
    }
}