using System;
using JetBrains.Annotations;
using StateMachine;
using UnityEngine;

namespace Game
{
    public class GameManager : MonoBehaviour, IHasStateMachine<GameManager>
    {
        private readonly StateMachine<GameManager> _stateMachine = new StateMachine<GameManager>();

        private static GameManager _instance;

        private void Start()
        {
            _instance = this;
            _stateMachine.SetState(new InGameState(this));
        }

        public StateMachine<GameManager> GetStateMachine() => _stateMachine;

        [CanBeNull]
        public static GameManager GetGameManager() => _instance;
    }
}