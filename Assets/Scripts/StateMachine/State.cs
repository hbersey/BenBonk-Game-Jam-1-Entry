namespace StateMachine
{
    public abstract class State<T> where T : IHasStateMachine<T>
    {
        internal readonly T Controlling;

        protected State(T controlling)
        {
            Controlling = controlling;
        }

        public virtual void OnBegin()
        {
        }

        public virtual void OnEnd()
        {
        }

        public virtual void OnTick()
        {
        }

        public virtual void OnPhysicsTick()
        {
        }

        public void SetState(State<T> state)
        {
            Controlling.GetStateMachine().SetState(state);
        }
    }
}