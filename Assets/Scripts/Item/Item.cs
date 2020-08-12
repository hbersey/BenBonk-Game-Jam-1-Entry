using System;
using StateMachine;
using UnityEngine;

namespace Item
{
    [RequireComponent(typeof(SpriteRenderer), typeof(Collider2D))]
    public class Item : MonoBehaviour, IHasStateMachine<Item>
    {
        [SerializeField] public ItemScriptableObject item;

        private readonly StateMachine<Item> _stateMachine = new StateMachine<Item>();

        private void Start() => _stateMachine.SetState(new ItemNotCollectedState(this));

        public bool CanCollect() => _stateMachine.State is ItemNotCollectedState;

        public void Collect() => _stateMachine.SetState(new ItemCollectedState(this));

        
        
        
        public StateMachine<Item> GetStateMachine() => _stateMachine;
    }
}