using Game;
using UnityEngine;

namespace Item
{
    [RequireComponent(typeof(Collider2D))]
    public class ItemController : MonoBehaviour
    {
        [SerializeField] private GameManager game;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.gameObject.tag.Equals("Player")) return;

            Destroy(gameObject);
            if (game.State is RoundState state) state.RoundScorer.IncrementScore();
        }
    }
}