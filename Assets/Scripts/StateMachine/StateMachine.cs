using UnityEngine;

namespace StateMachine
{
    public abstract class StateMachine<T>: MonoBehaviour  where T : State
    {
        protected internal T State;

        private protected void Update()
        {
            State.Update();
        }

        private void FixedUpdate()
        {
            State.PhysicsUpdate();
        }

        protected internal void SetState(T state)
        {
            State = state;
            State.Begin();
        }
    }
}