using Controllers.Platformer.MovingNPCs.States;
using UnityEngine;
using UnityEngine.UIElements;

namespace Controllers.Platformer.MovingNPCs
{
    public class HorizontalPatrolAndChaseNpc : HorizontalPatrolNpc
    {
        private float _maxChaseX;
        private float _maxDetectX;
        private float _minChaseX;

        private float _minDetectX;
        [SerializeField] private float chaseRadius;

        [SerializeField] private float detectRadius;
        [SerializeField] private GameObject target;

        private static HorizontalPatrolAndDetectState.OnDetect OnPatrolDetect(HorizontalPatrolAndChaseNpc npc)
        {
            return () =>
                npc.SetState(new ChaseState(npc, npc.target, npc._minChaseX, npc._maxChaseX, SetPatrolState(npc)));
        }

        private static ChaseState.OnLeaveRadius SetPatrolState(HorizontalPatrolAndChaseNpc npc)
        {
            return () => npc.SetState(new HorizontalPatrolAndDetectState(npc, npc._points, npc.target, npc._minDetectX,
                npc._maxDetectX, OnPatrolDetect(npc)));
        }

        public override void Start()
        {
            var x = transform.position.x;
            _minDetectX = x - detectRadius;
            _maxDetectX = x + detectRadius;
            _minChaseX = x - chaseRadius;
            _maxChaseX = x + chaseRadius;

            base.Start();
            SetState(new HorizontalPatrolAndDetectState(this, _points, target, _minDetectX, _maxDetectX,
                OnPatrolDetect(this)));
        }
    }
}