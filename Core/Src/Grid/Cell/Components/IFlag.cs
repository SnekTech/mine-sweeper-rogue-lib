using Cysharp.Threading.Tasks;

namespace MineSweeperRogue.Grid.Cell.Components;

public interface IFlag
{
    UniTask LiftAsync();
    UniTask PutDownAsync();
}