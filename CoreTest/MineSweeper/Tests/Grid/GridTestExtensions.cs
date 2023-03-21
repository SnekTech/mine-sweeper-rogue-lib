using Cysharp.Threading.Tasks;
using SnekPlugin.Core.CustomExtensions;
using SnekPlugin.MineSweeper.Cell;
using SnekPlugin.MineSweeper.Cell.StateMachine;
using SnekPlugin.MineSweeper.Grid;

namespace SnekPluginTest.MineSweeper.Tests;

public static class GridTestExtensions
{
    
    public static CellStateMatrix GridStateMatrix(this IGrid grid)
    {
        var (rowCount, columnCount) = grid.Size.Tuple;

        var gridCellState = new CellStateValue[rowCount, columnCount];

        foreach (var (i, j) in gridCellState.Indices())
        {
            var cell = grid.GetCellAt(new GridIndex(i, j));
            gridCellState[i, j] = cell.State;
        }

        return new CellStateMatrix(gridCellState);
    }

    public static CellStateValue[,] CreateStateMatrix(string[] gridRows)
    {
        var (rows, columns) = GridExtensions.GetDimension(gridRows);
        if (gridRows.Length == 0 || gridRows[0].Length == 0)
        {
            throw new ArgumentException("can't create matrix from empty source");
        }

        var result = new CellStateValue[rows, columns];

        for (var i = 0; i < rows; i++)
        {
            var gridRow = gridRows[i];
            var emojiRow = GridExtensions.SplitEmojiRow(gridRow).ToArray();

            for (var j = 0; j < columns; j++)
            {
                var emoji = emojiRow[j];
                
                result[i, j] = CellExtensions.ToCellState(emoji);
            }
        }

        return result;
    }
    
    public static async Task ResetStateAsync(this IGrid grid, CellStateMatrix cellStateMatrix)
    {
        var restoreCellTasks = cellStateMatrix.Matrix.Indices()
            .Select(index =>
            {
                var (i, j) = index;
                var cell = grid.GetCellAt(index);
                var shouldBeState = cellStateMatrix[i, j];

                if (shouldBeState == CellStateValue.Covered)
                {
                    return UniTask.CompletedTask;
                }

                return shouldBeState == CellStateValue.Revealed ? cell.RevealAsync() : cell.SwitchFlagAsync();
            });
        
        await UniTask.WhenAll(restoreCellTasks);
    }
}