using UnityEngine;

namespace StateMachine
{
    public abstract class StateMachine : MonoBehaviour
    {
        private State _state;

        private void Update()
        {
            _state.Update();
        }

        private void FixedUpdate()
        {
            _state.PhysicsUpdate();
        }

        protected void SetState(State state)
        {
            _state?.End();
            _state = state;
            _state.Begin();
        }
    }
}