using System;
using System.Text;
using SnekPlugin.MineSweeper;
using SnekPlugin.MineSweeper.Grid;

namespace SnekPlugin.Core.CustomExtensions;

public static class MatrixExtensions
{
    public static (int rows, int columns) SizeTuple<T>(this T[,] matrix)
    {
        return (matrix.GetLength(0), matrix.GetLength(1));
    }
    
    public static GridIndex FindMissMatch<T>(this T[,] self, T[,] target) where T : IComparable<T>
    {
        var (sizeA, sizeB) = (self.Size(), target.Size());
        if (sizeA != sizeB)
        {
            throw new InvalidOperationException("can't find miss match between matrices with different sizes");
        }

        for (var i = 0; i < sizeA.RowCount; i++)
        {
            for (var j = 0; j < sizeA.ColumnCount; j++)
            {
                if (!self[i, j].Equals(target[i, j]))
                {
                    return new GridIndex(i, j);
                }
            }
        }

        return GridIndex.Invalid;
    }

    public static string ToStringBinary(this int[,] matrix, char nonZero = 'x', char zero = 'o')
    {
        var sBuilder = new StringBuilder("\n");
        var (rows, columns) = matrix.SizeTuple();

        sBuilder.Append("  ");
        
        for (var i = 0; i < columns; i++)
        {
            sBuilder.Append(i);
        }
        sBuilder.Append("\n");
        
        
        for (var i = 0; i < rows; i++)
        {
            sBuilder.Append($"{i} ");
            for (var j = 0; j < columns; j++)
            {
                sBuilder.Append(matrix[i, j] == 0 ? zero : nonZero);
            }

            sBuilder.Append('\n');
        }

        return sBuilder.ToString();
    }
}