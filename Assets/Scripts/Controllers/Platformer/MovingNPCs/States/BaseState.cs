using StateMachine;

namespace Controllers.Platformer.MovingNPCs.States
{
    public class BaseState: State
    {
        protected PlatformerMovingNpc Npc;

        protected BaseState(PlatformerMovingNpc npc)
        {
            Npc = npc;
        }
    }
}