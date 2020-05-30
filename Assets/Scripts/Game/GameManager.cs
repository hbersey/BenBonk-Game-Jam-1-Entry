using System.Collections.Generic;
using StateMachine;
using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;

namespace Game
{
    public class GameManager : StateMachine<GameState>
    {
        [SerializeField] internal float basePointsPerItem = 100f;
        [SerializeField] internal int baseItemsPerRound = 2;

        [FormerlySerializedAs("items")] [SerializeField]
        internal Item.ItemScriptableObject[] allItems;
        [SerializeField] internal Transform[] spawnLocations;    
        
        private int _roundNumber;
        internal List<GameObject> SpawnedItems;

        private Random _random;

        private void Start()
        {
            SpawnedItems = new List<GameObject>();
            _random = new Random();

            SetState(NextRound());
        }

        private RoundState NextRound()
        {
            _roundNumber++;
            var items = new List<Item.ItemScriptableObject>();
            for (var i = 0; i < baseItemsPerRound + (int) (1 - Mathf.Pow(1.15f, +_roundNumber)); i++)
                items.Add(allItems[_random.Next(allItems.Length - 1)]);
            // ReSharper disable once Unity.IncorrectMonoBehaviourInstantiation
            return new RoundState(this, _roundNumber, items);
        }
    }
}