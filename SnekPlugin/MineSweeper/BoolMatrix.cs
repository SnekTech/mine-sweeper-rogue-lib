using System;
using SnekPlugin.Core.CustomExtensions;

namespace SnekPlugin.MineSweeper;

public class BoolMatrix
{
    public readonly bool[,] Matrix;

    public BoolMatrix(bool[,] matrix)
    {
        Matrix = matrix;
    }
    
    public BoolMatrix(string[] source)
    {
        Matrix = From(source);
    }
    
    public static implicit operator BoolMatrix(string[] source)
    {
        return new BoolMatrix(source);
    }
    
    public bool this[int i, int j] => Matrix[i, j];

    public static bool[,] From(string[] bombRows, char zeroChar = '0')
    {
        if (bombRows.Length == 0)
        {
            throw new ArgumentException("can't create matrix from empty source");
        }
        var rows = bombRows.Length;
        var columns = bombRows[0].Length;
        var result = new bool[rows, columns];

        for (var i = 0; i < rows; i++)
        {
            var bombRow = bombRows[i];
            for (var j = 0; j < columns; j++)
            {
                result[i, j] = bombRow[j] != zeroChar;
            }
        }

        return result;
    }
}