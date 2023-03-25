using Cysharp.Threading.Tasks;

namespace SnekTech.MineSweeperRogue.GridSystem.CellSystem;

public interface ICover
{
    UniTask RevealAsync();
    UniTask PutCoverAsync();
}