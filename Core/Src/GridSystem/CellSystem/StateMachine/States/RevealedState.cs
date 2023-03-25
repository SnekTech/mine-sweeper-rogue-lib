using Cysharp.Threading.Tasks;

namespace SnekTech.MineSweeperRogue.GridSystem.CellSystem
{
    public class RevealedState : CellState
    {
        public RevealedState(CellStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override CellStateValue Value => CellStateValue.Revealed;
        public override async UniTask OnEnter()
        {
            await Cell.Cover.RevealAsync();
        }

        public override UniTask OnExit() => UniTask.CompletedTask;

        public override UniTask OnReveal() => UniTask.CompletedTask;

        public override UniTask OnSwitchFlag() => UniTask.CompletedTask;
    }
}