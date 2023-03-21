using Cysharp.Threading.Tasks;

namespace MineSweeperRogue.Grid.Cell.StateMachine
{
    public class FlaggedState : CellState
    {
        public FlaggedState(CellStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override CellStateValue Value => CellStateValue.Flagged;
        public override UniTask OnEnter()
        {
            return Cell.Flag.LiftAsync();
        }

        public override UniTask OnExit()
        {
            return Cell.Flag.PutDownAsync();
        }

        public override UniTask OnReveal()
        {
            return UniTask.CompletedTask;
        }

        public override UniTask OnSwitchFlag()
        {
            return StateMachine.ChangeState(StateMachine.CachedCoveredState);
        }
    }
}