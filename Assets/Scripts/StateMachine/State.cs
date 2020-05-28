namespace StateMachine
{
    public abstract class State
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

        public virtual void End()
        {
        }
    }
}