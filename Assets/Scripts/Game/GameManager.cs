﻿using System.Collections.Generic;
using System.Globalization;
using Item;
using Map;
using Player;
using StateMachine;
using UnityEngine;
using UnityEngine.UI;
using Util.Scoring;

namespace Game
{
    public class GameManager : StateMachine<GameState>
    {
        private const float MapFragmentSize = 8f;

        [SerializeField] internal float basePointsPerItem = 100f;
        [SerializeField] internal int baseItemsPerRound = 2;

        [SerializeField] internal PlayerController player;

        [SerializeField] internal GameObject[] mapFragmentPrefabs;
        [SerializeField] internal ItemScriptableObject[] allItems;

        [SerializeField] private Text scoreText;
        [SerializeField] private Text highScoreText;
        [SerializeField] internal Text dayWeekText;
        [SerializeField] internal Image toFindImage;

        internal int RoundNumber;
        internal Vector2 StartPoint;
        internal List<Vector2> ItemSpawnPoints;
        internal List<GameObject> SpawnedItems;

        private int _currentMapExtent = 1;
        private GameObject[,] _map = new GameObject[1, 1];
        private ScoreAndHighScoreManager _scorer;

        private void Start()
        {
            ItemSpawnPoints = new List<Vector2>();
            SpawnedItems = new List<GameObject>();
            _scorer = new ScoreAndHighScoreManager(prefix: "GAME", doAutoSave: true, doTryLoad: true);
            AddScore(0f); // Renders Text
            SetState(NextRound(true));
        }

        public RoundState NextRound(bool isFirst = false)
        {
            foreach (var item in SpawnedItems)
                Destroy(item);
            SpawnedItems = new List<GameObject>();

            RoundNumber++;

            if ((RoundNumber - 1) % 7 == 0) AddNewFragment(isFirst);

            var items = new List<ItemScriptableObject>();
            for (var i = 0;
                i < Mathf.Min(baseItemsPerRound + (int) (Mathf.Pow(1.125f, RoundNumber) - 1), ItemSpawnPoints.Count);
                i++)
                items.Add(allItems[Random.Range(0, allItems.Length)]);
            // ReSharper disable once Unity.IncorrectMonoBehaviourInstantiation
            return new RoundState(this, items);
        }

        private void AddNewFragment(bool isFirst)
        {
            while (true)
            {
                var failed = true;
                var shouldBreak = false;
                for (var mapX = 0; mapX < _map.GetLength(0); mapX++)
                {
                    for (var mapY = 0; mapY < _map.GetLength(1); mapY++)
                    {
                        var isDone = _map[mapX, mapY];
                        if (isDone) continue;

                        var fragmentInstance =
                            Instantiate(mapFragmentPrefabs[Random.Range(0, mapFragmentPrefabs.Length)],
                                new Vector2(mapX * MapFragmentSize, mapY * MapFragmentSize),
                                Quaternion.Euler(Vector3.zero));
                        var fragment = fragmentInstance.GetComponent<MapFragmentController>();
                        if (isFirst) (fragment, StartPoint) = fragment.StartFragment();
                        else
                        {
                            if (mapY - 1 >= 0 && _map[mapX, mapY - 1] != null)
                            {
                                if (fragment.bottom != null) Destroy(fragment.bottom);
                                var otherFragment = _map[mapX, mapY - 1].GetComponent<MapFragmentController>();
                                if (otherFragment.top != null) Destroy(otherFragment.top);
                            }
                            else if (mapY + 1 < _currentMapExtent && _map[mapX, mapY + 1] != null)
                            {
                                if (fragment.top != null) Destroy(fragment.top);
                                var otherFragment = _map[mapX, mapY + 1].GetComponent<MapFragmentController>();
                                if (otherFragment.bottom != null) Destroy(otherFragment.bottom);
                            }
                        }

                        fragment.itemSpawnPoints.ForEach(point =>
                        {
                            Vector2 pointPosition;
                            ItemSpawnPoints.Add(new Vector2((pointPosition = point.position).x,
                                pointPosition.y));
                        });

                        _map[mapX, mapY] = fragmentInstance;
                        failed = false;
                        shouldBreak = true;
                        break;
                    }

                    if (shouldBreak) break;
                }

                if (failed)
                {
                    _currentMapExtent++;
                    var oldMap = (GameObject[,]) _map.Clone();
                    _map = new GameObject[_currentMapExtent, _currentMapExtent];
                    for (var x = 0; x < _currentMapExtent - 1; x++)
                    for (var y = 0; y < _currentMapExtent - 1; y++)
                        _map[x, y] = oldMap[x, y];

                    continue;
                }

                break;
            }
        }

        public void AddScore(float amount)
        {
            _scorer.IncrementScore(amount);
            scoreText.text = _scorer.Score.ToString(CultureInfo.CurrentCulture);
            highScoreText.text = $"Best: {_scorer.HighScore.ToString(CultureInfo.CurrentCulture)}";
        }
    }
}