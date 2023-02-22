using System;
using System.Linq;
using SnekPlugin.Core.CustomExtensions;

namespace SnekPlugin.MineSweeper.Grid;

public class BombMatrix : BoolMatrix
{
    public BombMatrix(IGridData gridData, IBombGenerator bombGenerator) : base(From(gridData, bombGenerator))
    {
    }

    public BombMatrix(bool[,] bombMatrix) : base(bombMatrix)
    {
        if (bombMatrix.IsEmpty())
        {
            throw new ArgumentException("can't create bombMatrix object with an empty 2d array");
        }
    }

    public GridSize GridSize => new GridSize(Matrix.Size());
    public int BombCount => TrueCount; // t.element -> hasBomb

    private static bool[,] From(IGridData gridData, IBombGenerator bombGenerator)
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
}