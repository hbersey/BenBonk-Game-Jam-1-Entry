using JetBrains.Annotations;

namespace StateMachine
{
    public class StateMachine<T> where T: IHasStateMachine<T>
    {
        [CanBeNull] internal State<T> State;

        public StateMachine(State<T> initialState = null) => State = initialState;

        public void SetState(State<T> state)
        {
            State?.OnEnd();
            State = state;
            State?.OnBegin();
        }

        public void Tick() => State?.OnTick();

        public void PhysicsTick() => State?.OnPhysicsTick();
    }
}