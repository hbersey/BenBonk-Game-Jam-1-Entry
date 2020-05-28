using UnityEngine;

namespace Controllers.Platformer.MovingNPCs.States
{
    public class HorizontalMoveState : BaseState
    {
        protected float XDirection;

        protected HorizontalMoveState(PlatformerMovingNpc npc, float xDirection) : base(npc)
        {
            XDirection = xDirection;
        }

        public override void PhysicsUpdate()
        {
            Npc.Rigidbody.velocity = new Vector2(XDirection * Npc.speed, Npc.Rigidbody.velocity.y);
        }
    }
}