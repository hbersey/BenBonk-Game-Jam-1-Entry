using UnityEngine;
using Util;

namespace Controllers.Platformer.MovingNPCs.States
{
    public class HorizontalPatrolState : HorizontalMoveState
    {
        private readonly float[] _stops;

        private int _current;

        public HorizontalPatrolState(PlatformerMovingNpc npc, float[] stops) : base(npc, 0)
        {
            _stops = stops;
        }

        public override void Begin()
        {
            var x = Npc.transform.position.x;

            _current = 0;
            var minDist = float.MaxValue;
            for (var i = 0; i < _stops.Length; i++)
            {
                var dist = Mathf.Abs(x - _stops[i]);
                if (!(dist < minDist)) continue;

                minDist = dist;
                _current = i;
            }

            XDirection = _stops[_current] > Npc.transform.position.x ? 1 : -1;
        }

        public override void PhysicsUpdate()
        {
            if (ReachedStop())
            {
                _current = MathUtil.Wrap(_current + 1, 0, _stops.Length - 1);
                XDirection = _stops[_current] > Npc.transform.position.x ? 1 : -1;
            }

            base.PhysicsUpdate();
        }


        private bool ReachedStop()
        {
            if (_stops[_current] > 0)
                return Npc.transform.position.x >= _stops[_current];
            return  Npc.transform.position.x <= _stops[_current];

        }
    }
}