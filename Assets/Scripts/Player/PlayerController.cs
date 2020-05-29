using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
    public class PlayerController: StateMachine.StateMachine<PlayerState>
    {
        [SerializeField] protected internal float speed = 10f;
    
        protected internal Rigidbody2D Rigidbody; 
        
        private void Start()
        {
            Rigidbody = GetComponent<Rigidbody2D>();

            SetState(new PlayerMoveState(this));   
        }
    }
}