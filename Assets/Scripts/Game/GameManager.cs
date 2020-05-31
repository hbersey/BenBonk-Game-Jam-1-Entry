using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Item;
using Map;
using NPC;
using Player;
using StateMachine;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Util;
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
        [SerializeField] internal GameObject[] npcPrefabs;

        [SerializeField] private Text scoreText;
        [SerializeField] private Text highScoreText;
        [SerializeField] internal Text dayWeekText;
        [SerializeField] internal Image toFindImage;
        [SerializeField] internal GameObject health1;
        [SerializeField] internal GameObject health2;
        [SerializeField] internal GameObject health3;

        [SerializeField] private GameObject inGameGui;
        [SerializeField] private GameObject endOfDayGui;
        [SerializeField] private GameObject endOfWeekGui;

        [FormerlySerializedAs("endOfDayGuiDayWeekText")] [SerializeField]
        private Text endOfDayTitle;

        [SerializeField] private Text endOfWeekTitle;

        internal int RoundNumber;
        internal Vector2 StartPoint;
        internal List<Vector2> ItemSpawnPoints;
        internal List<GameObject> SpawnedItems;
        internal List<Vector2> NpcWaypoints;
        internal int Health;

        private int _currentMapExtent = 1;
        private GameObject[,] _map = new GameObject[1, 1];
        private ScoreAndHighScoreManager _scorer;
        private List<int> _usedNpcSpawns;
        private List<GameObject> _spawnedNpcs;

        private void Start()
        {
            ItemSpawnPoints = new List<Vector2>();
            SpawnedItems = new List<GameObject>();
            NpcWaypoints = new List<Vector2>();
            _spawnedNpcs = new List<GameObject>();
            _scorer = new ScoreAndHighScoreManager(prefix: "GAME", doAutoSave: true, doTryLoad: true);
            AddScore(0f); // Renders Text
            inGameGui.SetActive(true);
            endOfDayGui.SetActive(false);
            endOfWeekGui.SetActive(false);
            SetHealth(3);
            SetState(NextRound(true));
        }

        public RoundState NextRound(bool isFirst = false)
        {
            foreach (var item in SpawnedItems)
                Destroy(item);
            foreach (var npc in _spawnedNpcs) Destroy(npc);

            SpawnedItems = new List<GameObject>();
            _spawnedNpcs = new List<GameObject>();

            RoundNumber++;

            if ((RoundNumber - 1) % 7 == 0) AddNewFragment(isFirst);

            var items = new List<ItemScriptableObject>();
            for (var i = 0;
                i < Mathf.Min(baseItemsPerRound + (int) (Mathf.Pow(1.125f, RoundNumber) - 1), ItemSpawnPoints.Count);
                i++)
                items.Add(allItems[Random.Range(0, allItems.Length)]);


            _usedNpcSpawns = new List<int>();
            for (var i = 0; i < Mathf.Min(RoundNumber / 4, NpcWaypoints.Count); i++)
            {
                GameObject o;
                o = _spawnedNpcs.Count >= i
                    ? Instantiate(npcPrefabs[Random.Range(0, npcPrefabs.Length)])
                    : _spawnedNpcs[i];

                var npc = o.GetComponent<NpcController>();
                npc.Game = this;

                int waypointIndex;
                while (true)
                {
                    waypointIndex = Random.Range(0, NpcWaypoints.Count);
                    if (!_usedNpcSpawns.Contains(waypointIndex))
                        break;
                }

                o.transform.position = NpcWaypoints[waypointIndex];
                npc.SetState(new NpcDestinationReachedState(npc, NpcWaypoints[waypointIndex]));

                _spawnedNpcs.Add(o);
                _usedNpcSpawns.Add(waypointIndex);
            }

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
                            if (mapX - 1 >= 0 && _map[mapX - 1, mapY] != null)
                            {
                                if (fragment.left != null) Destroy(fragment.left);
                                var otherFragment = _map[mapX - 1, mapY].GetComponent<MapFragmentController>();
                                if (otherFragment.right != null) Destroy(otherFragment.right);
                            }
                            else if (mapX + 1 < _currentMapExtent && _map[mapX + 1, mapY] != null)
                            {
                                if (fragment.right != null) Destroy(fragment.right);
                                var otherFragment = _map[mapX + 1, mapY].GetComponent<MapFragmentController>();
                                if (otherFragment.left != null) Destroy(otherFragment.left);
                            }

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
                        fragment.npcWaypoints.ForEach(npcWaypoint => NpcWaypoints.Add(npcWaypoint.transform.position));

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
            scoreText.text = MathUtil.Abbreviate((int) _scorer.Score);
            highScoreText.text = $"Best: {MathUtil.Abbreviate((int) _scorer.HighScore)}";
        }

        public void EndOfDay()
        {
            inGameGui.SetActive(false);

            if (RoundNumber % 7 == 0)
            {
                endOfDayGui.SetActive(false);
                endOfWeekGui.SetActive(true);
                endOfWeekTitle.text = $"Week {RoundNumber / 7} Completed!";
            }
            else
            {
                endOfDayGui.SetActive(true);
                endOfWeekGui.SetActive(false);
                endOfDayTitle.text =
                    $"{Contants.Days[(RoundNumber - 1) % 7]}, Week {RoundNumber / 7 + 1} Completed!";
            }
        }

        public void Continue()
        {
            inGameGui.SetActive(true);
            endOfDayGui.SetActive(false);
            endOfWeekGui.SetActive(false);
            SetState(NextRound());
        }

        private void GameOver()
        {
        }

        private void SetHealth(int health)
        {
            if (health <= 0)
                GameOver();

            health1.SetActive(health >= 1);
            health2.SetActive(health >= 2);
            health3.SetActive(health >= 3);

            Health = health;
        }

        public void PickupHealth() => SetHealth(Health + 1);

        public void LooseHealth() => SetHealth(Health - 1);
    }
}