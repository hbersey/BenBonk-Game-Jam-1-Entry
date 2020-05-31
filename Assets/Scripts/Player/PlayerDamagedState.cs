using UnityEngine;

namespace Player
{
    public class PlayerDamagedState : PlayerState
    {
        private float _continueAt;

        public PlayerDamagedState(PlayerController player) : base(player)
        {
        }

        public override void Begin()
        {
            _continueAt = Time.time + 2;
            Player.Renderer.color = Color.red;
        }

        public override void Update()
        {
            if (Time.time >= _continueAt)
            {
                Player.Renderer.color = Color.white;
                Player.SetState(new PlayerMoveState(Player));
            }
        }
    }
}