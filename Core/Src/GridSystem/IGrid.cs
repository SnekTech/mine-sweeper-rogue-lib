using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using SnekTech.MineSweeperRogue.GridSystem.CellSystem;

namespace SnekTech.MineSweeperRogue.GridSystem;

public interface IGrid
{
    ICell GetCellAt(GridIndex gridIndex);
    UniTask InitCells(BombMatrix bombMatrix);
    UniTask RevealAtAsync(GridIndex gridIndex);
    UniTask RevealAroundAsync(GridIndex gridIndex);

    IEnumerable<ICell> GetNeighborsOf(ICell cell);
    
    GridSize Size { get; }
    int BombCount { get; }
    int RevealedCellCount { get; }
    int FlaggedCellCount { get; }
    bool IsCleared { get; }
    List<ICell> Cells { get; }
}