using Cysharp.Threading.Tasks;

namespace MineSweeperRogue.Grid.Cell.Components;

public interface ICover
{
    UniTask RevealAsync();
    UniTask PutCoverAsync();
}