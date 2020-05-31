using UnityEngine;

namespace Player
{
    public class PlayerDamagedState: PlayerState
    {
        private float _continueAt;
        
        public PlayerDamagedState(PlayerController player) : base(player)
        {
            
        }

        public override void Begin()
        {
            _continueAt = Time.time + 2;
        }

        public override void Update()
        {
            if (Time.time.Equals(_continueAt))
                Player.SetState(new PlayerMoveState(Player));
        }
    }
}