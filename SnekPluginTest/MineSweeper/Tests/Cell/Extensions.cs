using SnekPlugin.MineSweeper.Cell.StateMachine;
using SnekPlugin.MineSweeper.Grid;

namespace SnekPluginTest.MineSweeper.Tests;

public static class Extensions
{
    public static bool[,] GetIsCoveredMatrix(this IGrid grid)
    {
        var (rowCount, columnCount) = (grid.Size.RowCount, grid.Size.ColumnCount);

        var result = new bool[rowCount, columnCount];

        for (var i = 0; i < rowCount; i++)
        {
            for (var j = 0; j < columnCount; j++)
            {
                var cell = grid.GetCellAt(new GridIndex(i, j));
                result[i, j] = cell.CurrentState == CellStateValue.Covered;
            }
        }

        return result;
    }
}