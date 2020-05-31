using Game;
using Player;
using UnityEngine;

namespace Item
{
    [RequireComponent(typeof(CapsuleCollider2D))]
    public class SanitizerController : MonoBehaviour
    {
        [SerializeField] private GameManager game;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.tag.Equals("Player")) return;
            if (!(game.State is RoundState state)) return;
            if (!(game.player.State is PlayerMoveState)) return;

            if (game.Health >= 3)
                game.AddScore(state.PointsPerItem * 5);
            else game.PickupHealth();

            Destroy(gameObject);
        }
    }
}