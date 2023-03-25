using System;
using System.Linq;
using System.Text;
using SnekTech.MineSweeperRogue.Core.CustomExtensions;
using SnekTech.MineSweeperRogue.GridSystem.CellSystem;

namespace SnekTech.MineSweeperRogue.GridSystem;

public class BombMatrix
{
    private readonly bool[,] _matrix;

    public BombMatrix(IGridData gridData, IBombGenerator bombGenerator)
    {
        var bombMatrix = From(gridData, bombGenerator);
        if (bombMatrix.IsEmpty())
        {
            throw new ArgumentException("can't create bombMatrix object with an empty 2d array");
        }

        _matrix = bombMatrix;
    }

    public BombMatrix(bool[,] bombMatrix)
    {
        if (bombMatrix.IsEmpty())
        {
            throw new ArgumentException("can't create bombMatrix object with an empty 2d array");
        }

        _matrix = bombMatrix;
    }

    public GridSize GridSize => new GridSize(_matrix.Size());
    public int BombCount => _matrix.Values().Count(hasBomb => hasBomb);
    public bool this[int i, int j] => _matrix[i, j];

    public string Format()
    {
        var sBuilder = new StringBuilder("\n");
        var (rows, columns) = _matrix.Size();

        for (var i = 0; i < rows; i++)
        {
            sBuilder.Append($"{i}");
            for (var j = 0; j < columns; j++)
            {
                sBuilder.Append(this[i, j] ? 
                    CellExtensions.Emoji.Bomb : CellExtensions.Emoji.Revealed);
            }

            sBuilder.Append('\n');
        }

        return sBuilder.ToString();
    }

    public static bool[,] From(IGridData gridData, IBombGenerator bombGenerator)
    {
        var size = gridData.GridSize;
        if (size.TotalCount == 0)
        {
            throw new ArgumentException("can't create bombMatrix object with an empty 2d array");
        }

        var (rows, columns) = (size.RowCount, size.ColumnCount);

        var result = new bool[rows, columns];
        foreach (var (i, j) in result.Indices())
        {
            result[i, j] = bombGenerator.NextHasBomb(gridData.BombProbability);
        }

        return result;
    }
    
    public static bool[,] From(string[] bombRows, string bombUnit = CellExtensions.Emoji.Bomb)
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
                var hasBomb = bombEmojis[j] == bombUnit;
                result[i, j] = hasBomb;
            }
        }

        return result;
    }
}