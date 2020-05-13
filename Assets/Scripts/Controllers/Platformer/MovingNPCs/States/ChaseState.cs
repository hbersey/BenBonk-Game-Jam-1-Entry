using UnityEngine;

namespace Controllers.Platformer.MovingNPCs.States
{
    public class ChaseState : MoveState
    {
        public delegate void OnLeaveRadius();

        private GameObject _target;
        private float _minChaseX;
        private float _maxChaseX;
        private OnLeaveRadius _onLeaveRadius;

        public ChaseState(PlatformerMovingNpc npc, GameObject target, float minChaseX, float maxChaseX, OnLeaveRadius onLeaveRadius) : base(npc, 0)
        {
            _target = target;
            _minChaseX = minChaseX;
            _maxChaseX = maxChaseX;
            _onLeaveRadius = onLeaveRadius;
        }

        public override void PhysicsUpdate()
        {
            if (Npc.transform.position.x < _minChaseX || Npc.transform.position.x > _maxChaseX) _onLeaveRadius();

            XDirection = _target.transform.position.x > Npc.transform.position.x ? 1 : -1;
            base.PhysicsUpdate();
        }
    }
}