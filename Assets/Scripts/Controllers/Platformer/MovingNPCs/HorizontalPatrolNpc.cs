using Controllers.Platformer.MovingNPCs.States;
using UnityEngine;

namespace Controllers.Platformer.MovingNPCs
{
    public class HorizontalPatrolNpc : PlatformerMovingNpc
    {
        [SerializeField] private Transform pointA;
        [SerializeField] private Transform pointB;

        [SerializeField] private bool destroyPoints;

        protected float[] _points;

        public override void Start()
        {
            base.Start();

            _points = new[] {pointA.position.x, pointB.position.x};

            if (destroyPoints)
            {
                Destroy(pointA.gameObject);
                Destroy(pointB.gameObject);
            }

            SetState(new HorizontalPatrolState(this, _points));
        }
    }
}