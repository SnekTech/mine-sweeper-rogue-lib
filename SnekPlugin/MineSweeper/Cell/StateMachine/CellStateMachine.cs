using Cysharp.Threading.Tasks;
using SnekPlugin.Core.FSM;

namespace SnekPlugin.MineSweeper.Cell.StateMachine
{
    public class CellStateMachine
    {
        public readonly CoveredState CachedCoveredState;
        public readonly FlaggedState CachedFlaggedState;
        public readonly RevealedState CachedRevealedState;
        
        public CellState Current = null !;

        
        public CellStateMachine(ICell cell)
        {
            Cell = cell;
            
            CachedCoveredState = new CoveredState(this);
            CachedFlaggedState = new FlaggedState(this);
            CachedRevealedState = new RevealedState(this);
            
            SetInitState(CachedCoveredState);
        }
        
        public ICell Cell { get; }

        public bool IsTransitioning { get; set; }
        public CellStateValue CurrentStateValue => Current.Value;

        private void SetInitState(CellState initState)
        {
            Current = initState;
            Current.OnEnter();
        }

        public async UniTask ChangeState(CellState newState)
        {
            // Debug.Log($"{typeof(T).Name} {Current.GetType().Name} ---> {newState.GetType().Name}");
            if (newState == Current) return;
            if (IsTransitioning) return;

            IsTransitioning = true;
            await Current.OnExit();
            
            Current = newState;
            
            await Current.OnEnter();
            IsTransitioning = false;
        }
    }
}