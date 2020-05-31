using UnityEngine;

namespace StateMachine
{
    public abstract class State: MonoBehaviour
    {

        public virtual void Begin()
        {
        }

        public virtual void Update()
        {
            
        }

        public virtual void PhysicsUpdate()
        {
        }
    }
}