using UnityEngine;
using UnityEngine.Animations;
using Util;

namespace NPC
{
    public class NpcMoveState : NpcState
    {
        private static readonly int Horizontal = Animator.StringToHash("Horizontal");
        private static readonly int Vertical = Animator.StringToHash("Vertical");
        private static readonly int Moving = Animator.StringToHash("Moving");
        
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

            Npc.Animator.SetFloat(Horizontal, Npc.Rigidbody.velocity.normalized.x);
            Npc.Animator.SetFloat(Vertical, Npc.Rigidbody.velocity.normalized.y);
            Npc.Animator.SetBool(Moving,
                Mathf.Abs(Npc.Rigidbody.velocity.x) > 0.1f || Mathf.Abs(Npc.Rigidbody.velocity.y) > 0.1f);

            if (Vector2.Distance(_startPosition, Npc.transform.position) >=
                Vector2.Distance(_startPosition, _destination))
            {
                Npc.SetState(new NpcDestinationReachedState(Npc, _destination));
            }
        }
    }
}