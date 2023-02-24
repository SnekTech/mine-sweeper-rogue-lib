using System;
using System.Linq;
using System.Text;
using SnekPlugin.Core.CustomExtensions;
using SnekPlugin.MineSweeper.Cell;
using SnekPlugin.MineSweeper.Grid;

namespace SnekPlugin.MineSweeper;

public class BoolMatrix
{
    public readonly bool[,] Matrix;

    public BoolMatrix(bool[,] matrix)
    {
        Matrix = matrix;
    }

    public int TrueValueCount => Matrix.Values().Count(isTrue => isTrue);
    
    public bool this[int i, int j] => Matrix[i, j];

    public override string ToString()
    {
        return ToStringBinary(this);
    }

    private static string ToStringBinary(BoolMatrix matrix, char truthyChar = '✅', char falsyChar = '❌')
    {
        var sBuilder = new StringBuilder("\n");
        var (rows, columns) = matrix.Matrix.Size();
        
        for (var i = 0; i < rows; i++)
        {
            sBuilder.Append($"{i}");
            for (var j = 0; j < columns; j++)
            {
                sBuilder.Append(matrix[i, j] ? truthyChar : falsyChar);
            }

            sBuilder.Append('\n');
        }

        return sBuilder.ToString();
    }

    public static bool[,] CreateBoolMatrix(string[] bombRows, string trueUnit = CellExtensions.BombEmoji)
    {
        var (rows, columns) = GridExtensions.GetDimension(bombRows);
        if (rows == 0 || columns == 0)
        {
            throw new ArgumentException("cant generate boolMatrix from empty string[]");
        }
        
        var result = new bool[rows, columns];

        for (var i = 0; i < rows; i++)
        {
            var bombRow = bombRows[i];
            var bombEmojis = GridExtensions.SplitEmojiRow(bombRow);
            for (var j = 0; j < columns; j++)
            {
                var hasBomb = bombEmojis[j] == trueUnit;
                result[i, j] = hasBomb;
            }
        }

        return result;
    }
}