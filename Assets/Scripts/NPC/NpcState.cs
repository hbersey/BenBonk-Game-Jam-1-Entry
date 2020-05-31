namespace NPC
{
    public abstract class NpcState: StateMachine.State
    {
        internal NpcController Npc;

        public NpcState(NpcController npc)
        {
            Npc = npc;
        }
    }
}