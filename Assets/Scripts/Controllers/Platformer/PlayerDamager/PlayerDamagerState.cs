using UnityEngine;

namespace Controllers.Platformer.PlayerDamager
{
    public abstract class PlayerDamagerState: StateMachine.State
    {
        private PlayerDamager _playerDamager;
        
        public PlayerDamagerState(PlayerDamager playerDamager)
        {
            _playerDamager = playerDamager;
        }

        public virtual void OnCollision(Collider2D other)
        {
            
        }
        
    }
}