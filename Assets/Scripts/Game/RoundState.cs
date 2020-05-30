using System.Collections.Generic;
using System.Linq;
using Item;
using UnityEngine;
using UnityEngine.UIElements;
using Util.Scoring;
using Random = System.Random;

namespace Game
{
    public class RoundState : GameState
    {
        protected internal ScoreOnlyManager RoundScorer;
        private List<ItemScriptableObject> _remainingItems;

        private Random _random;

        public RoundState(GameManager game, int roundNumber, List<ItemScriptableObject> items) : base(game)
        {
            RoundScorer = new ScoreOnlyManager {_incrementAmount = game.basePointsPerItem * roundNumber};
            _remainingItems = items;
            _random = new Random();
        }

        public override void Begin()
        {
            Game.SpawnedItems.ForEach(Destroy);
            Game.SpawnedItems.Clear();

            var itemsToSpawn = new List<ItemScriptableObject>(_remainingItems);
            var unusedSpawnLocations = new List<Transform>(Game.spawnLocations);
            
            foreach (var item in itemsToSpawn)
            {
                var locationIndex = _random.Next(unusedSpawnLocations.Count - 1);
                var o = Instantiate(item.itemPrefab, unusedSpawnLocations[locationIndex].position,
                    unusedSpawnLocations[locationIndex].rotation);
                unusedSpawnLocations.RemoveAt(locationIndex);
                Game.SpawnedItems.Add(o);
            }

            foreach (var o in from location in unusedSpawnLocations let item = Game.allItems[_random.Next(Game.allItems.Length)] select Instantiate(item.itemPrefab, location.position, location.rotation))
            {
                Game.SpawnedItems.Add(o);
            }
            unusedSpawnLocations.Clear();
            
            Game.SpawnedItems.ForEach(o => o.GetComponent<ItemController>().Game = Game);
            
        }
    }
}