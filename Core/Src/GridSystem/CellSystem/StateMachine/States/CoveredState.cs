using Cysharp.Threading.Tasks;

namespace SnekTech.MineSweeperRogue.GridSystem.CellSystem
{
    public class CoveredState : CellState
    {
        public CoveredState(CellStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override CellStateValue Value => CellStateValue.Covered;

        public override UniTask OnEnter() => UniTask.CompletedTask;

        public override UniTask OnExit() => UniTask.CompletedTask;

        public override UniTask OnReveal()
        {
            return StateMachine.ChangeState(StateMachine.CachedRevealedState);
        }

        public override UniTask OnSwitchFlag()
        {
            return StateMachine.ChangeState(StateMachine.CachedFlaggedState);
        }
    }
}