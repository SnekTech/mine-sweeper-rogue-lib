namespace SnekPlugin.Core.FSM
{
    public interface IState
    {
        void OnEnter();

        void OnExit();
    }
}
