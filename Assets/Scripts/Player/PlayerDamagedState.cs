using UnityEngine;

namespace Player
{
    public class PlayerDamagedState : PlayerState
    {
        private float _continueAt;
        private static readonly int Horizontal = Animator.StringToHash("Horizontal");
        private static readonly int Vertical = Animator.StringToHash("Vertical");
        private static readonly int Moving = Animator.StringToHash("Moving");
        
        private float _horizontal;
        private float _vertical;
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
            _horizontal = Input.GetAxisRaw("Horizontal");
            _vertical = Input.GetAxisRaw("Vertical");

            Player.Animator.SetFloat(Horizontal, _horizontal);
            Player.Animator.SetFloat(Vertical, _vertical);
        
            if (Time.time >= _continueAt)
            {
                Player.Renderer.color = Color.white;
                Player.SetState(new PlayerMoveState(Player));
            }
        }
        
        public override void PhysicsUpdate()
        {
            Player.Rigidbody.velocity = new Vector2(_horizontal, _vertical) * Player.movementSpeed;
            Player.Animator.SetBool(Moving, Mathf.Abs(_horizontal) > 0.1f || Mathf.Abs(_vertical) > 0.1f);
        }
    }
}