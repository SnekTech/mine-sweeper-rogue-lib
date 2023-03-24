using MineSweeperRogue.Core.FSM;

namespace MineSweeperRogue.GridSystem.CellSystem.StateMachine
{
    public class CellStateMachine : AsyncFsm<CellState, ICell>
    {
        public readonly CoveredState CachedCoveredState;
        public readonly FlaggedState CachedFlaggedState;
        public readonly RevealedState CachedRevealedState;
        
        
        public CellStateMachine(ICell cell) : base(cell)
        {
            CachedCoveredState = new CoveredState(this);
            CachedFlaggedState = new FlaggedState(this);
            CachedRevealedState = new RevealedState(this);
        }

        public CellStateValue CurrentStateValue => CurrentState.Value;
    }
}