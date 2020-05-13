using UnityEngine;

namespace Controllers.Platformer.MovingNPCs.States
{
    public class HorizontalPatrolAndDetectState: HorizontalPatrolState
    {
        public delegate void OnDetect();

        private GameObject _target;
        private float _minDetectX;
        private float _maxDetectX;
        private OnDetect _onDetect;

        public HorizontalPatrolAndDetectState(PlatformerMovingNpc npc, float[] stops, GameObject target, float minDetectX, float maxDetectX, OnDetect onDetect) : base(npc, stops)
        {
            _target = target;
            _minDetectX = minDetectX;
            _maxDetectX = maxDetectX;
            _onDetect = onDetect;
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            var targetX = _target.transform.position.x;

            if (targetX > _minDetectX && targetX < _maxDetectX)
                _onDetect();
        }
    }
}