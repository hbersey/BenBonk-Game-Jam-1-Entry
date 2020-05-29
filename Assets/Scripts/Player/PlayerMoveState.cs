using UnityEngine;

namespace Player
{
    public class PlayerMoveState : PlayerState
    {
        public PlayerMoveState(PlayerController player) : base(player)
        {
        }

        public override void PhysicsUpdate()
        {
            var horizontalMovement = Input.GetAxisRaw("Horizontal") * Player.speed;
            var verticalMovement = Input.GetAxisRaw("Vertical") * Player.speed;
            
            Player.Rigidbody.velocity = new Vector2(horizontalMovement, verticalMovement);
        }
    }
}