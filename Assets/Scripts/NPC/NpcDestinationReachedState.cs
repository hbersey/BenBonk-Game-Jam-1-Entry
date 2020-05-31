using UnityEngine;
using Debug = System.Diagnostics.Debug;

namespace NPC
{
    public class NpcDestinationReachedState : NpcState
    {
        private Vector2? _previousDestination;

        public NpcDestinationReachedState(NpcController npc, Vector2? previousDestination) : base(npc)
        {
            _previousDestination = previousDestination;
        }

        public override void Begin()
        {
            if (_previousDestination == null)
            {
                _previousDestination = Npc.game.NpcWaypoints[0];
                var min = Mathf.Infinity;
                for (var i = 1; i < Npc.game.NpcWaypoints.Count; i++)
                {
                    var distance = Vector2.Distance(Npc.game.NpcWaypoints[i], Npc.transform.position);
                    if (distance >= min) continue;
                    _previousDestination = Npc.game.NpcWaypoints[i];
                    min = distance;
                }
            }
            Npc.transform.position = (Vector3) _previousDestination;

            Debug.Assert(_previousDestination != null, nameof(_previousDestination) + " != null");
            var possibleWaypoints = Npc.game.NpcWaypoints.FindAll(waypoint => waypoint != _previousDestination &&
                                                                              (waypoint.x.Equals(
                                                                                   _previousDestination?.x) ||
                                                                               waypoint.y.Equals(
                                                                                   _previousDestination?.y)));

            var destination = possibleWaypoints[Random.Range(0, possibleWaypoints.Count - 1)];
            Npc.SetState(new NpcMoveState(Npc, destination));
        }
    }
}