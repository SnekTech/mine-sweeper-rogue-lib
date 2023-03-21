using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using MineSweeperRogue.Grid.Cell;

namespace MineSweeperRogue.Grid;

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
}