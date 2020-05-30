using Game;
using UnityEngine;

namespace Item
{
    [RequireComponent(typeof(Collider2D))]
    public class ItemController : MonoBehaviour
    {
        internal string Id;
        internal GameManager Game;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.gameObject.tag.Equals("Player")) return;

            if (!(Game.State is RoundState state) || state.CurrentItem() == null) return;
            // ReSharper disable once PossibleNullReferenceException
            if (state.CurrentItem() != null && !state.CurrentItem().id.Equals(Id))
                return;

            state.RoundScorer.IncrementScore();
            gameObject.SetActive(false);

            state.NextItem();
        }
    }
}