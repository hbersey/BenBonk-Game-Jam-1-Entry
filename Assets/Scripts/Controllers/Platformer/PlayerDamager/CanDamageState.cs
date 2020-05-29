using UnityEngine;

namespace Controllers.Platformer.PlayerDamager
{
    public class CanDamageState : PlayerDamagerState
    {
        public CanDamageState(PlayerDamager playerDamager) : base(playerDamager)
        {
            
        }

        public override void OnCollision(Collider2D other)
        {
            base.OnCollision(other);
            Debug.Log("BASH!");
        }
    }
}