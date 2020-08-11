namespace StateMachine
{
    public interface IHasStateMachine<T> where T: IHasStateMachine<T>
    {
        StateMachine<T> GetStateMachine();
    }
}