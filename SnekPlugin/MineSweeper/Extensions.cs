using System.Collections.Generic;
using SnekPlugin.MineSweeper.Grid;

namespace SnekPlugin.MineSweeper;

public static class Extensions
{
    public static GridSize GridSize<T>(this T[,] matrix)
    {
        return new GridSize(matrix.GetLength(0), matrix.GetLength(1));
    }

    public static IEnumerable<T> Values<T>(this T[,] matrix)
    {
        if (matrix.Length == 0) yield break;
        
        var enumerator = matrix.GetEnumerator();

        while (enumerator.MoveNext())
        {
            yield return ((T) enumerator.Current)!;
        }
    }

    public static IEnumerable<GridIndex> GridIndices<T>(this T[,] matrix)
    {
        var size = matrix.GridSize();
        for (var i = 0; i < size.RowCount; i++)
        {
            for (var j = 0; j < size.ColumnCount; j++)
            {
                yield return new GridIndex(i, j);
            }
        }
    }
}