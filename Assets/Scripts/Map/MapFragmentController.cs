using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class MapFragmentController : MonoBehaviour
    {
        private static readonly float SpawnClearThreshold = 3f;

        [SerializeField] internal GameObject top;
        [SerializeField] internal GameObject left;
        [SerializeField] internal GameObject right;
        [SerializeField] internal GameObject bottom;

        [SerializeField] internal List<Transform> itemSpawnPoints;
        [SerializeField] internal List<Transform> npcWaypoints;

        public (MapFragmentController, Vector2) StartFragment()
        {
            var startPoint = itemSpawnPoints[Random.Range(0, itemSpawnPoints.Count)].position;

            var toRemove = new List<int>();
            for (var i = 0; i < itemSpawnPoints.Count; i++)
                if (Vector2.Distance(startPoint, itemSpawnPoints[i].position) <= SpawnClearThreshold)
                    toRemove.Add(i);
            var j = 0;
            toRemove.ForEach(i =>
            {
                itemSpawnPoints.RemoveAt(i - j);
                j++;
            });
            return (this, startPoint);
        }
    }
}