using Controllers.Platformer.MovingNPCs;
using UnityEngine;

namespace Controllers.Platformer.Player
{
    public class Player: PlatformerMovingNpc
    {
        [SerializeField] internal float jumpSpeed;
        [SerializeField] private LayerMask groundLayer; 
        
        
        public override void Start()
        {
            base.Start();
            
            SetState(new PlayerMoveState(this, groundLayer));
        }
    }
}