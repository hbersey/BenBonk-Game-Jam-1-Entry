using System;
using Game;
using StateMachine;
using UnityEngine;
using Util;

namespace NPC
{
    [RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
    public class NpcController : StateMachine.StateMachine<NpcState>
    {
        [SerializeField] internal GameManager game;
        [SerializeField] internal float speed = 5f;

        internal Rigidbody2D Rigidbody;
        internal Collider2D Collider;

        private void Start()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
            Collider = GetComponent<Collider2D>();

            // ReSharper disable once Unity.IncorrectMonoBehaviourInstantiation
            SetState(new NpcDestinationReachedState(this, null));
        }
    }
}