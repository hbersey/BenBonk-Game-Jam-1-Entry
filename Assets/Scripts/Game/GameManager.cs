using System;
using System.Runtime.InteropServices;
using JetBrains.Annotations;
using StateMachine;
using UnityEngine;
using World;
using Object = System.Object;
using Random = UnityEngine.Random;

namespace Game
{
    public class GameManager : MonoBehaviour, IHasStateMachine<GameManager>
    {
        [SerializeField] private DepartmentScriptableObject[] allDepartments;
        [SerializeField] private Sprite defaultFloor;
        [SerializeField] private int maxWorldSize = 8;

        private static GameManager _instance;

        private const int WorldGridSize = 16;
        private const float WallWidth = 1f;

        private readonly StateMachine<GameManager> _stateMachine = new StateMachine<GameManager>();
        private Segment[,] _segments;

        private void Start()
        {
            _instance = this;
            _segments = new Segment[maxWorldSize, maxWorldSize];
            _stateMachine.SetState(new InGameState(this));
        }

        public StateMachine<GameManager> GetStateMachine() => _stateMachine;

        [CanBeNull]
        public static GameManager GetGameManager() => _instance;

        private static GameObject HorizontalWall(string goName)
        {
            var go = new GameObject(goName) {layer = LayerMask.NameToLayer("Walls")};
            go.AddComponent<BoxCollider2D>().size = new Vector2(WorldGridSize, WallWidth);
            return go;
        }

        private static GameObject VerticalWall(string goName)
        {
            var go = new GameObject(goName) {layer = LayerMask.NameToLayer("Walls")};
            go.AddComponent<BoxCollider2D>().size = new Vector2(WallWidth, WorldGridSize);
            return go;
        }

        private bool SegmentExists(int x, int y) =>
            x >= 0 && x < WorldGridSize && y >= 0 && y < WorldGridSize &&
            !_segments[x, y].Equals(default(Segment));

        private void NewSegment(int x, int y)
        {
            var department = allDepartments[Random.Range(0, allDepartments.Length)];
            var segment = new GameObject($"Segment ({x}, {y})");

            GameObject topWall = null;
            if (!SegmentExists(x, y + 1))
            {
                topWall = HorizontalWall("Top Wall");
                topWall.transform.parent = segment.transform;
                topWall.transform.position =
                    new Vector3(WorldGridSize * x, WorldGridSize * (y + 0.5f) + WallWidth / 2f);
            }
            else
            {
                var otherSegment = _segments[x, y + 1];
                Destroy(otherSegment.BottomWall);
                otherSegment.BottomWall = null;
            }

            GameObject leftWall = null;
            if (!SegmentExists(x - 1, y))
            {
                leftWall = VerticalWall("Left Wall");
                leftWall.transform.parent = segment.transform;
                leftWall.transform.position =
                    new Vector3(WorldGridSize * (x - 0.5f) - WallWidth / 2f, WorldGridSize * y);
            }
            else
            {
                var otherSegment = _segments[x - 1, y];
                Destroy(otherSegment.RightWall);
                otherSegment.RightWall = null;
            }

            GameObject rightWall = null;
            if (!SegmentExists(x + 1, y))
            {
                rightWall = VerticalWall("Right Wall");
                rightWall.transform.parent = segment.transform;
                rightWall.transform.position =
                    new Vector3(WorldGridSize * (x + 0.5f) + WallWidth / 2f, y * WorldGridSize);
            }
            else
            {
                var otherSegment = _segments[x + 1, y];
                Destroy(otherSegment.LeftWall);
                otherSegment.LeftWall = null;
            }


            GameObject bottomWall = null;
            if (!SegmentExists(x, y - 1))
            {
                bottomWall = HorizontalWall("Bottom Wall");
                bottomWall.transform.parent = segment.transform;
                bottomWall.transform.position = new Vector3(WorldGridSize * x, WorldGridSize * (y - 0.5f) - WallWidth / 2f);
            }
            else
            {
                var otherSegment = _segments[x, y - 1];
                Destroy(otherSegment.TopWall);
                otherSegment.TopWall = null;
            }

            var floor = new GameObject("Floor");
            floor.transform.parent = segment.transform;
            floor.AddComponent<SpriteRenderer>().sprite = defaultFloor;
            floor.transform.position = new Vector3(x * WorldGridSize, y * WorldGridSize);

            _segments[x, y] = new Segment(department, topWall, leftWall, rightWall, bottomWall);
        }
    }
}