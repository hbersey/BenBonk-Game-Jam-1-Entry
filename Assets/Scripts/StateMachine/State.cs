namespace StateMachine
{
    public abstract class State<T>
    {
        private T _machine;

        protected State(T machine)
        {
            _machine = machine;
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
    }
}