using Cysharp.Threading.Tasks;

namespace MineSweeperRogue.GridSystem.Cell.Components;

public interface IFlag
{
    UniTask LiftAsync();
    UniTask PutDownAsync();
}