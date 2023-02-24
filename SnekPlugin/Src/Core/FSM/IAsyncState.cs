using Cysharp.Threading.Tasks;

namespace SnekPlugin.Core.FSM;

public interface IAsyncState
{
    UniTask OnEnter();
    UniTask OnExit();
}