using System.Collections.Generic;
using StateMachine;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class GameManager : StateMachine<GameState>
    {
        [SerializeField] internal float basePointsPerItem = 100f;
        [SerializeField] internal int baseItemsPerRound = 2;

        [SerializeField] internal Item.ItemScriptableObject[] allItems;
        [SerializeField] internal Transform[] spawnLocations;

        [SerializeField] internal Image toFindImage;
        
        private int _roundNumber;
        internal List<GameObject> SpawnedItems;
        private void Start()
        {
            SpawnedItems = new List<GameObject>();
            SetState(NextRound());
        }

        public RoundState NextRound()
        {
            _roundNumber++;
            var items = new List<Item.ItemScriptableObject>();
            for (var i = 0; i < baseItemsPerRound + (int) (1 - Mathf.Pow(1.15f, +_roundNumber)); i++)
                items.Add(allItems[Random.Range(0, allItems.Length)]);
            // ReSharper disable once Unity.IncorrectMonoBehaviourInstantiation
            return new RoundState(this, _roundNumber, items);
        }
    }
}