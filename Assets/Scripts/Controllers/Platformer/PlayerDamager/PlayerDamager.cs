using System;
using UnityEngine;

namespace Controllers.Platformer.PlayerDamager
{
    [RequireComponent(typeof(Collider2D))]
    public class PlayerDamager : StateMachine.StateMachine<PlayerDamagerState>
    {
        [SerializeField] private float amount;

        private void OnCollisionEnter2D(Collision2D other)
        {
            
        }
    }
}