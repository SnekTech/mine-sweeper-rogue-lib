using Cysharp.Threading.Tasks;

namespace SnekTech.MineSweeperRogue.GridSystem.CellSystem;

public interface IFlag
{
    UniTask LiftAsync();
    UniTask PutDownAsync();
}