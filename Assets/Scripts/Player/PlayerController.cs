using System;
using Game;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Animator))]
    public class PlayerController: StateMachine.StateMachine<PlayerState>
    {
        [SerializeField] protected internal float movementSpeed = 10f;
    
        protected internal Rigidbody2D Rigidbody;
        protected internal Animator Animator;
        protected internal SpriteRenderer Renderer;
        private void Start()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
            Animator = GetComponent<Animator>();
            Renderer = GetComponent<SpriteRenderer>();
            
            SetState(new PlayerMoveState(this));   
        }
    }
}