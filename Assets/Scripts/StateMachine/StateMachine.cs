using UnityEngine;

namespace StateMachine
{
    public abstract class StateMachine<T>: MonoBehaviour  where T : State
    {
        protected internal T State;

        private void Update()
        {
            State.Update();
        }

        private void FixedUpdate()
        {
            State.PhysicsUpdate();
        }

        protected internal void SetState(T state)
        {
            if (State != null) State.End();
            State = state;
            State.Begin();
        }
    }
}