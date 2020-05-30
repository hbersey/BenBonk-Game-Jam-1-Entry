using Game;
using UnityEngine;

namespace Item
{
    [RequireComponent(typeof(Collider2D))]
    public class ItemController : MonoBehaviour
    {
        internal GameManager Game;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.gameObject.tag.Equals("Player")) return;

            Destroy(gameObject);
            if (Game.State is RoundState state) state.RoundScorer.IncrementScore();
        }
    }
}