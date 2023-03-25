using Cysharp.Threading.Tasks;

namespace SnekTech.MineSweeperRogue.Core.FSM;

public interface IAsyncState
{
    UniTask OnEnter();
    UniTask OnExit();
}