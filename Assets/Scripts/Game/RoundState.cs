using System.Collections.Generic;
using System.Linq;
using Item;
using JetBrains.Annotations;
using UnityEngine;
using Util.Scoring;

namespace Game
{
    public class RoundState : GameState
    {
        private static readonly string[] Days =
            {"Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday"};

        protected internal readonly ScoreOnlyManager RoundScorer;
        private readonly List<ItemScriptableObject> _remainingItems;

        public RoundState(GameManager game, List<ItemScriptableObject> items) : base(game)
        {
            RoundScorer = new ScoreOnlyManager {_incrementAmount = game.basePointsPerItem * game.RoundNumber};
            _remainingItems = items;
        }

        public override void Begin()
        {
            Game.SpawnedItems.ForEach(Destroy);
            Game.SpawnedItems.Clear();

            var itemsToSpawn = new List<ItemScriptableObject>(_remainingItems);
            var unusedSpawnLocations = new List<Transform>(Game.itemSpawnPoints);

            foreach (var item in itemsToSpawn)
            {
                var locationIndex = Random.Range(0, unusedSpawnLocations.Count);
                var o = Instantiate(item.pickupPrefab, unusedSpawnLocations[locationIndex].position,
                    unusedSpawnLocations[locationIndex].rotation);
                o.GetComponent<ItemController>().Id = item.id;
                unusedSpawnLocations.RemoveAt(locationIndex);
                Game.SpawnedItems.Add(o);
            }

            foreach (var location in unusedSpawnLocations)
            {
                var item = Game.allItems[Random.Range(0, Game.allItems.Length)];
                var o = Instantiate(item.pickupPrefab, location.position, location.rotation);
                o.GetComponent<ItemController>().Id = item.id;
                Game.SpawnedItems.Add(o);
            }

            unusedSpawnLocations.Clear();

            Game.SpawnedItems.ForEach(o => o.GetComponent<ItemController>().Game = Game);

            Game.player.gameObject.transform.position = Game.startPoint.position;

            Render();
        }

        private void Render()
        {
            var day = Days[(Game.RoundNumber - 1) % 7];
            var week = (Game.RoundNumber - 1) / 7 + 1;

            Game.dayWeekText.text = $"{day}, Week {week}";
            Game.toFindImage.sprite = CurrentItem()?.toFindSprite;
        }

        [CanBeNull]
        public ItemScriptableObject CurrentItem()
        {
            return _remainingItems.Count <= 0 ? null : _remainingItems[0];
        }


        public void NextItem()
        {
            _remainingItems.RemoveAt(0);
            if (_remainingItems.Count == 0)
                Game.SetState(new EndGameState(Game));
            else 
                Render();
        }
    }
}