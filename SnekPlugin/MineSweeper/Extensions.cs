using SnekPlugin.MineSweeper.Grid;

namespace SnekPlugin.MineSweeper;

public static class Extensions
{
    public static GridSize Size<T>(this T[,] matrix)
    {
        return new GridSize(matrix.GetLength(0), matrix.GetLength(1));
    }
}