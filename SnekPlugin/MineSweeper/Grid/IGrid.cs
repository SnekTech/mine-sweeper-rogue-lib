using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using SnekPlugin.MineSweeper.Cell;

namespace SnekPlugin.MineSweeper.Grid;

public interface IGrid
{
    ICell GetCellAt(GridIndex gridIndex);
    GridSize Size { get; }
    void InitCells(IGridData gridData);
    UniTask RevealCellAsync(GridIndex gridIndex);

    bool IsValid(GridIndex gridIndex);
    List<ICell> GetNeighborsOf(ICell cell);
}