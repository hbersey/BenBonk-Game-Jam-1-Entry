﻿using JetBrains.Annotations;
using StateMachine;
using UnityEngine;

namespace Player
{
    public class Player : Entity.Entity, IHasStateMachine<Player>
    {
        [SerializeField] internal float speed = 20f;

        private static Player _instance;

        internal Rigidbody2D Rigidbody;
        private readonly StateMachine<Player> _stateMachine = new StateMachine<Player>();

        public bool IsMoving() => _stateMachine.State?.GetType().IsInstanceOfType(typeof(PlayerMoveState)) ?? false;

        private void Start()
        {
            _instance = this;
            Rigidbody = GetComponent<Rigidbody2D>();
            _stateMachine.SetState(new PlayerIdleState(this));
        }

        private void Update() => _stateMachine.Tick();

        private void FixedUpdate() => _stateMachine.PhysicsTick();

        public StateMachine<Player> GetStateMachine() => _stateMachine;

        [CanBeNull]
        public Player GetPlayer() => _instance;
    }
}