using System;
using StateMachine;
using UnityEngine;

namespace Entity
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Entity : MonoBehaviour, IHasStateMachine<Entity>
    {
        [SerializeField] internal float speed = 5f;

        private readonly StateMachine<Entity> _stateMachine = new StateMachine<Entity>();

        private void Start()
        {
            // _stateMachine.SetState(new EntityIdleState(this));
            _stateMachine.SetState(new EntityMoveState(this, transform.position + new Vector3(-10, -10),
                state => state.SetState(new EntityIdleState(this))));
        }

        private void FixedUpdate()
        {
            _stateMachine.PhysicsTick();
        }

        public StateMachine<Entity> GetStateMachine()
        {
            return _stateMachine;
        }
    }

    internal abstract class BaseEntityState : State<Entity>
    {
        protected BaseEntityState(Entity controlling) : base(controlling)
        {
        }
    }
}