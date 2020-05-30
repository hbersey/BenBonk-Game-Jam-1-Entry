using System.Collections.Generic;
using Player;
using StateMachine;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.XR.WSA.Input;

namespace Game
{
    public class GameManager : StateMachine<GameState>
    {
        [SerializeField] internal float basePointsPerItem = 100f;
        [SerializeField] internal int baseItemsPerRound = 2;

        [SerializeField] internal PlayerController player;
        
        [SerializeField] internal Transform startPoint;
        [SerializeField] internal Item.ItemScriptableObject[] allItems;
        [SerializeField] internal Transform[] itemSpawnPoints;

        [SerializeField] internal Text dayWeekText;
        [SerializeField] internal Image toFindImage;

        internal int RoundNumber;
        internal List<GameObject> SpawnedItems;
        private void Start()
        {
            SpawnedItems = new List<GameObject>();
            SetState(NextRound());
        }

        public RoundState NextRound()
        {
            RoundNumber++;
            var items = new List<Item.ItemScriptableObject>();
            for (var i = 0; i < Mathf.Min(baseItemsPerRound + (int) (Mathf.Pow(1.125f, RoundNumber) - 1), itemSpawnPoints.Length); i++)
                items.Add(allItems[Random.Range(0, allItems.Length)]);
            // ReSharper disable once Unity.IncorrectMonoBehaviourInstantiation
            return new RoundState(this, items);
            
        }
    }
}