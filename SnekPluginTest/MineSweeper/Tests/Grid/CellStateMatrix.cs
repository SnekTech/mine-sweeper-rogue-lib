using SnekPlugin.MineSweeper.Cell.StateMachine;

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

    private static CellStateMatrix From(string[] gridRows)
    {
        return new CellStateMatrix(GridTestExtensions.CreateStateMatrix(gridRows));
    }
}