using System;
using StateMachine;
using UnityEngine;

namespace Entity
{
    public class Entity : MonoBehaviour
    {
        private readonly StateMachine<Entity> _stateMachine = new StateMachine<Entity>();

        private void Start()
        {
            _stateMachine.SetState(new EntityIdleState(this));
        }
    }

    internal abstract class BaseEntityState : State<Entity>
    {
        protected BaseEntityState(Entity controlling) : base(controlling)
        {
        }
    }
}