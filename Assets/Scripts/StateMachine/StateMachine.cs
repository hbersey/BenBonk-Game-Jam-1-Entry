using JetBrains.Annotations;

namespace StateMachine
{
    public class StateMachine<T>
    {
        [CanBeNull] private State<T> _state;

        public StateMachine(State<T> initialState = null) => _state = initialState;

        public void SetState(State<T> state)
        {
            _state?.OnEnd();
            _state = state;
            _state?.OnBegin();
        }

        public void Tick() => _state?.OnTick();

        public void PhysicsTick() => _state?.OnPhysicsTick();
    }
}