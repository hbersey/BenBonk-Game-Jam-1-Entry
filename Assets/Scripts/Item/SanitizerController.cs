using System;
using System.Collections;
using Game;
using Player;
using UnityEngine;

namespace Item
{
    [RequireComponent(typeof(CapsuleCollider2D), typeof(AudioSource))]
    public class SanitizerController : MonoBehaviour
    {
        internal GameManager Game;

        private AudioSource _audioSource;
        private void Start() => _audioSource = GetComponent<AudioSource>();
         

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.tag.Equals("Player")) return;
            if (!(Game.State is RoundState state)) return;
            if (!(Game.player.State is PlayerMoveState)) return;

            StartCoroutine(Collect(state));
        }

        private IEnumerator Collect(RoundState state)
        {
            if (Game.Health >= 3)
                Game.AddScore(state.PointsPerItem * 5);
            else Game.PickupHealth();

            _audioSource.Play();
            yield return new WaitForSeconds(_audioSource.clip.length);
            
            Destroy(gameObject);
        }
    }
}