using UnityEngine;
using UnityEngine.Animations;
using Util;

namespace NPC
{
    public class NpcMoveState : NpcState
    {
        private Vector2 _startPosition;
        private Vector2 _destination;

        public NpcMoveState(NpcController npc, Vector2 destination) : base(npc)
        {
            _startPosition = npc.transform.position;
            _destination = destination;
        }

        public override void Begin()
        {
            Vector2.Distance(Npc.transform.position, _destination);
        }

        public override void PhysicsUpdate()
        {
            Npc.Rigidbody.velocity = (_destination - (Vector2) Npc.transform.position).normalized * Npc.speed;
            
            if (Vector2.Distance(_startPosition, Npc.transform.position) >=
                Vector2.Distance(_startPosition, _destination)) {
                Npc.SetState(new NpcDestinationReachedState(Npc, _destination));
            }
        }
    }
}