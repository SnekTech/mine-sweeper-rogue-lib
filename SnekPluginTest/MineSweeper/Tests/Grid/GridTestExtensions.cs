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

        for (var i = 0; i < rowCount; i++)
        {
            for (var j = 0; j < columnCount; j++)
            {
                var cell = grid.GetCellAt(new GridIndex(i, j));
                isCoveredMatrix[i, j] = cell.CurrentState == CellStateValue.Covered;
            }
        }

        return new BoolMatrix(isCoveredMatrix);
    }
}