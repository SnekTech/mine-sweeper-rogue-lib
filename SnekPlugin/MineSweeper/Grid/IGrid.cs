using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using SnekPlugin.MineSweeper.Cell;

namespace SnekPlugin.MineSweeper.Grid;

public interface IGrid
{
    ICell GetCellAt(GridIndex gridIndex);
    void InitCells(BombMatrix bombMatrix);
    UniTask RevealCellAsync(GridIndex gridIndex);

    bool IsValid(GridIndex gridIndex);
    List<ICell> GetNeighborsOf(ICell cell);
    
    GridSize Size { get; }
    int CellCount { get; }
    int BombCount { get; }
    int RevealedCellCount { get; }
    int FlaggedCellCount { get; }
}