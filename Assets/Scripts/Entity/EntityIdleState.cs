using UnityEngine;

namespace Entity
{
    internal class EntityIdleState : BaseEntityState
    {
        public EntityIdleState(Entity controlling) : base(controlling)
        {
        }

        public override void OnBegin()
        {
            Debug.Log("IDLE");
        }
    }
}