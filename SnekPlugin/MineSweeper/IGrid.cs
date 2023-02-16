using Cysharp.Threading.Tasks;

namespace SnekPlugin.MineSweeper;

public interface IGrid
{
    ICell GetCellAt(GridIndex gridIndex);
    IGridData GridData { get; }
    void InitCells(IGridData gridData);
    UniTask RevealCellAsync(GridIndex gridIndex);
}