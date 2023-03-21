using Cysharp.Threading.Tasks;

namespace MineSweeperRogue.Core.FSM;

public interface IAsyncState
{
    UniTask OnEnter();
    UniTask OnExit();
}