using UnityEngine;

namespace Controllers.Platformer.MovingNPCs.States
{
    public class IdleState : BaseState
    {
        public IdleState(PlatformerMovingNpc npc) : base(npc)
        {
        }

        public override void Begin()
        {
            Npc.Rigidbody.velocity = Vector2.zero;
        }
    }
}