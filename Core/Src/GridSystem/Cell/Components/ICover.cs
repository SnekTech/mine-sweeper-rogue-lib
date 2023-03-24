using Cysharp.Threading.Tasks;

namespace MineSweeperRogue.GridSystem.Cell.Components;

public interface ICover
{
    UniTask RevealAsync();
    UniTask PutCoverAsync();
}