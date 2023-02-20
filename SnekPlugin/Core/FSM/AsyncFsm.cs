using System;
using Cysharp.Threading.Tasks;

namespace SnekPlugin.Core.FSM;

public abstract class AsyncFsm<TState, TContext> where TState : class, IAsyncState
{
    public TState CurrentState = null!;

    protected AsyncFsm(TContext context)
    {
        Context = context;
    }

    public TContext Context { get; }
    private bool _isTransitioning;

    public virtual async UniTask SetInitState(TState initState)
    {
        CurrentState = initState;
        
        _isTransitioning = true;
        await CurrentState.OnEnter();
        _isTransitioning = false;
    }

    public virtual async UniTask ChangeState(TState newState)
    {
        // Debug.Log($"{typeof(T).Name} {Current.GetType().Name} ---> {newState.GetType().Name}");
        if (CurrentState == null)
        {
            throw new InvalidOperationException("can not change state when initial state not set");
        }

        if (_isTransitioning) return;
        if (newState == CurrentState) return;

        _isTransitioning = true;

        await CurrentState.OnExit();
        CurrentState = newState;
        await CurrentState.OnEnter();

        _isTransitioning = false;
    }
}