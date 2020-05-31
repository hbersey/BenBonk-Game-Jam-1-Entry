using Game;
using Player;
using UnityEngine;

namespace Item
{
    [RequireComponent(typeof(CapsuleCollider2D))]
    public class SanitizerController : MonoBehaviour
    {
        internal GameManager Game;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.tag.Equals("Player")) return;
            if (!(Game.State is RoundState state)) return;
            if (!(Game.player.State is PlayerMoveState)) return;

            if (Game.Health >= 3)
                Game.AddScore(state.PointsPerItem * 5);
            else Game.PickupHealth();

            Destroy(gameObject);
        }
    }
}