using Cysharp.Threading.Tasks;

namespace MineSweeperRogue.GridSystem.CellSystem.Components;

public interface IFlag
{
    UniTask LiftAsync();
    UniTask PutDownAsync();
}