using SnekPlugin.Core.CustomExtensions;
using SnekPlugin.MineSweeper;
using SnekPlugin.MineSweeper.Cell.StateMachine;
using SnekPlugin.MineSweeper.Grid;

namespace SnekPluginTest.MineSweeper.Tests;

public static class GridTestExtensions
{
    public static BoolMatrix GetIsCoveredMatrix(this IGrid grid)
    {
        var (rowCount, columnCount) = grid.Size.Tuple;

        var isCoveredMatrix = new bool[rowCount, columnCount];

        foreach (var (i, j) in isCoveredMatrix.Indices())
        {
            var cell = grid.GetCellAt(new GridIndex(i, j));
            isCoveredMatrix[i, j] = cell.State == CellStateValue.Covered;
        }

        return new BoolMatrix(isCoveredMatrix);
    }
}