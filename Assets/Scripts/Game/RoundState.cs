using System.Collections.Generic;
using System.Linq;
using Item;
using UnityEngine;
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
            RoundScorer = new ScoreOnlyManager();
            _remainingItems = items;
            _random = new Random();
        }

        public override void Begin()
        {
           Game.SpawnedItems.ForEach(Destroy);
           Game.SpawnedItems.Clear();

           var itemsToSpawn = new Queue<ItemScriptableObject>(_remainingItems);
           var unusedSpawnLocations = new List<Transform>(Game.spawnLocations);

           while (itemsToSpawn.Count > 0)
           {
               var location = unusedSpawnLocations[_random.Next(unusedSpawnLocations.Count - 1)];
               Instantiate(itemsToSpawn.First().itemPrefab, location.position, location.rotation);
               Game.SpawnedItems.Add(itemsToSpawn.First());
               unusedSpawnLocations.Remove(location);
               
           }

        }
    }
}