namespace SnekPlugin.Core.FSM
{
    public abstract class FiniteStateMachine<T> where T : class, IState
    {
        public T Current = null !;

        public void SetInitState(T initState)
        {
            Current = initState;
            Current.OnEnter();
        }

        public virtual void ChangeState(T newState)
        {
            // Debug.Log($"{typeof(T).Name} {Current.GetType().Name} ---> {newState.GetType().Name}");
            if (newState == Current) return;
            Current.OnExit();
            Current = newState;
            Current.OnEnter();
        }
    }
}