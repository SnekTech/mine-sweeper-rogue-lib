using System.Text;
using MineSweeperRogue.Core.CustomExtensions;
using MineSweeperRogue.Grid.Cell;
using MineSweeperRogue.Grid.Cell.StateMachine;

namespace SnekPluginTest.MineSweeper.Tests;

public class CellStateMatrix
{
    public readonly CellStateValue[,] Matrix;

    public CellStateMatrix(CellStateValue[,] matrix)
    {
        Matrix = matrix;
    }

    public static implicit operator CellStateMatrix(string[] gridRows)
    {
        return From(gridRows);
    }

    public CellStateValue this[int i, int j] => Matrix[i, j];
    public (int rows, int columns) Size => Matrix.Size();

    public IEnumerable<(int i, int j, CellStateValue stateValue)> GetEnumerable()
    {
        foreach (var (i, j) in Matrix.Indices())
        {
            yield return (i, j, Matrix[i, j]);
        }
    }

    public string Format()
    {
        var sBuilder = new StringBuilder("\n");
        var (rows, columns) = Matrix.Size();

        for (var i = 0; i < rows; i++)
        {
            sBuilder.Append($"{i}");
            for (var j = 0; j < columns; j++)
            {
                var state = Matrix[i, j];
                sBuilder.Append(state.ToEmoji());
            }

            sBuilder.Append('\n');
        }

        return sBuilder.ToString();
    }

    private static CellStateMatrix From(string[] gridRows)
    {
        return new CellStateMatrix(GridTestExtensions.CreateStateMatrix(gridRows));
    }
}