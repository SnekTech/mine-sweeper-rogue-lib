using Cysharp.Threading.Tasks;

namespace MineSweeperRogue.GridSystem.CellSystem.Components;

public interface ICover
{
    UniTask RevealAsync();
    UniTask PutCoverAsync();
}