using Controllers.Platformer.MovingNPCs;
using Controllers.Platformer.MovingNPCs.States;

namespace Controllers.Platformer.Player
{
    public class PlayerDeathState: BaseState, IPlayerState
    {
        public PlayerDeathState(PlatformerMovingNpc npc) : base(npc)
        {
            
        }

        public bool CanTakeDamage() => false;
    }
}