using StateMachine;
using UnityEngine;

namespace Item
{
    internal class ItemCollectedState : State<Item>
    {
        public ItemCollectedState(Item controlling) : base(controlling)
        {
        }

        public override void OnBegin()
        {
            Object.Destroy(Controlling.gameObject);
        }
    }
}