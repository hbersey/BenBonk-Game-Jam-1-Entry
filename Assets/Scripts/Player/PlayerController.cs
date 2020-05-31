using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D), typeof(Collider2D), typeof(Animator))]
    public class PlayerController: StateMachine.StateMachine<PlayerState>
    {
        [SerializeField] protected internal float movementSpeed = 10f;
    
        protected internal Rigidbody2D Rigidbody;
        protected internal Animator Animator;
        private void Start()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
            Animator = GetComponent<Animator>();
            
            SetState(new PlayerMoveState(this));   
        }
    }
}