
using StateMachine;

namespace Item
{
    internal class ItemNotCollectedState : State<Item>
    {
        public ItemNotCollectedState(Item controlling) : base(controlling)
        {
        }
    }
}