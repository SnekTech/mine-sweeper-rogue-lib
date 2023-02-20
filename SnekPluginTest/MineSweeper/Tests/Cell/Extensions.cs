using SnekPlugin.MineSweeper.Cell.StateMachine;
using SnekPlugin.MineSweeper.Grid;

namespace SnekPluginTest.MineSweeper.Tests;

public static class Extensions
{
    public static int[,] GetIsCoveredMatrix(this IGrid grid)
    {
        var (rowCount, columnCount) = (grid.Size.RowCount, grid.Size.ColumnCount);

        var result = new int[rowCount, columnCount];

        for (var i = 0; i < rowCount; i++)
        {
            for (var j = 0; j < columnCount; j++)
            {
                var cell = grid.GetCellAt(new GridIndex(i, j));
                var isCovered = cell.CurrentState == CellStateValue.Covered;
                result[i, j] = isCovered ? 1 : 0;
            }
        }

        return result;
    }
}