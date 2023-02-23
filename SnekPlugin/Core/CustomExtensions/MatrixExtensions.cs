using System;
using System.Collections.Generic;
using System.Text;
using SnekPlugin.MineSweeper;
using SnekPlugin.MineSweeper.Grid;

namespace SnekPlugin.Core.CustomExtensions;

public static class MatrixExtensions
{
    public static (int rows, int columns) Size<T>(this T[,] matrix)
    {
        return (matrix.GetLength(0), matrix.GetLength(1));
    }

    public static bool IsEmpty<T>(this T[,] matrix) => matrix.Length == 0;

    public static IEnumerable<(int i, int j)> Indices<T>(this T[,] matrix)
    {
        var (rows, columns) = matrix.Size();
        for (var i = 0; i < rows; i++)
        {
            for (var j = 0; j < columns; j++)
            {
                yield return (i, j);
            }
        }
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

    public static (int i, int j) FindMissMatch<T>(this T[,] self, T[,] target) where T : IComparable<T>
    {
        var (sizeA, sizeB) = (self.Size(), target.Size());
        if (sizeA != sizeB)
        {
            throw new InvalidOperationException("can't find miss match between matrices with different sizes");
        }

        foreach (var (i, j) in self.Indices())
        {
            if (!self[i, j].Equals(target[i, j]))
            {
                return (i, j);
            }
        }

        return (-1, -1);
    }

}