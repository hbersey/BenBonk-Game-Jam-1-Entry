using UnityEngine;

namespace StateMachine
{
    public abstract class StateMachine<T> : MonoBehaviour where T : State
    {
        protected internal State State;

        private void Update()
        {
            State.Update();
        }

        private void FixedUpdate()
        {
            State.PhysicsUpdate();
        }

        protected void SetState(State state)
        {
            State?.End();
            State = state;
            State.Begin();
        }
    }
}