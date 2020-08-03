namespace StateMachine
{
    public abstract class State<T>
    {
        private T _controlling;

        protected State(T controlling)
        {
            _controlling = controlling;
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