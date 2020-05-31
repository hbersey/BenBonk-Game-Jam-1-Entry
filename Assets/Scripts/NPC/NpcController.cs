using System;
using Game;
using StateMachine;
using UnityEngine;
using Util;

namespace NPC
{
    [RequireComponent(typeof(Rigidbody2D), typeof(Collider2D), typeof(Animator))]
    public class NpcController : StateMachine.StateMachine<NpcState>
    {
        [SerializeField] internal float speed = 2.5f;

        internal GameManager Game;
        internal Rigidbody2D Rigidbody;
        internal Animator Animator ;

        private void Start()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
            Animator = GetComponent<Animator>();
            
            // ReSharper disable once Unity.IncorrectMonoBehaviourInstantiation
            SetState(new NpcDestinationReachedState(this, null));
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.collider.tag.Equals("Player")) return;
if(!(Game.player.State is ))
            
            
        }
    }
}