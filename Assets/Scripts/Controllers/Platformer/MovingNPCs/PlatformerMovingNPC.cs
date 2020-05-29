using Controllers.Platformer.MovingNPCs.States;
using UnityEngine;

namespace Controllers.Platformer.MovingNPCs
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class PlatformerMovingNpc : StateMachine.StateMachine<BaseState>
    {
        [SerializeField, Min(0)] internal float speed;

        internal Rigidbody2D Rigidbody;
        
        public virtual void Start()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
        }
    }
}