using Cysharp.Threading.Tasks;
using SnekPlugin.Core.FSM;
using SnekPlugin.MineSweeper.Cell.Components;

namespace SnekPlugin.MineSweeper.Cell.StateMachine
{
    public abstract class CellState : IAsyncState
    {
        protected readonly CellStateMachine StateMachine;
        protected ICell Cell => StateMachine.Cell;

        protected CellState(CellStateMachine stateMachine)
        {
            StateMachine = stateMachine;
        }
        
        public abstract CellStateValue Value { get; }

        public abstract UniTask OnEnter();
        public abstract UniTask OnExit();
        public abstract UniTask OnReveal();
        public abstract UniTask OnSwitchFlag();

    }

    public enum CellStateValue
    {
        Covered,
        Flagged,
        Revealed,
    }
}
