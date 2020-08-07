using System;
using UnityEngine;

namespace Entity
{
    internal class EntityMoveState : BaseEntityState
    {
        public delegate void OnDestinationReached(EntityMoveState state);

        private readonly Vector2 _destination;
        private readonly Rigidbody2D _rigidbody;
        private readonly OnDestinationReached _onDestinationReached;
        private readonly float _threshold;

        private static readonly OnDestinationReached DefaultOnDestinationReached = state =>
        {
            state._rigidbody.velocity = Vector2.zero;
            state.SetState(new EntityIdleState(state.Controlling));
        };

        public EntityMoveState(Entity controlling, Vector2 destination,
            OnDestinationReached onDestinationReached = null,
            float threshold = 0.1f) : base(
            controlling)
        {
            _destination = destination;
            _rigidbody = Controlling.GetComponent<Rigidbody2D>();
            _onDestinationReached = onDestinationReached ?? DefaultOnDestinationReached;
            _threshold = threshold;
        }

        public override void OnPhysicsTick()
        {
            Vector2 current = Controlling.transform.position;
            _rigidbody.velocity = (_destination - current).normalized * Controlling.speed;
            if ((_destination - current).sqrMagnitude <= _threshold)
                _onDestinationReached?.Invoke(this);
        }
    }
}