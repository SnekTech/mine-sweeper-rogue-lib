using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SnekPlugin.MineSweeper.Grid;

public class BombMatrix : IEnumerable<bool>
{
    private readonly bool[,] _bombMatrix;

    public BombMatrix(IGridData gridData, IBombGenerator bombGenerator)
    {
        var size = gridData.GridSize;
        if (size.TotalCount == 0)
        {
            throw new ArgumentException("can't create bombMatrix object with an empty 2d array");
        }
        
        _bombMatrix = new bool[size.RowCount, size.ColumnCount];
        for (var i = 0; i < size.RowCount; i++)
        {
            for (var j = 0; j < size.ColumnCount; j++)
            {
                _bombMatrix[i, j] = bombGenerator.NextHasBomb(gridData.BombProbability);
            }
        }
    }

    public BombMatrix(int[,] bombMatrix)
    {
        var size = bombMatrix.Size();
        if (size.TotalCount == 0)
        {
            throw new ArgumentException("can't create bombMatrix object with an empty 2d array");
        }
        
        var rowCount = size.RowCount;
        var columnCount = size.ColumnCount;
        _bombMatrix = new bool[rowCount, columnCount];
        for (var i = 0; i < rowCount; i++)
        {
            for (var j = 0; j < columnCount; j++)
            {
                _bombMatrix[i, j] = bombMatrix[i, j] != 0;
            }
        }
    }

    public bool this[int i, int j] => _bombMatrix[i, j];
    public GridSize Size => _bombMatrix.Size();
    public int BombCount => this.Count(hasBomb => hasBomb);

    public IEnumerator<bool> GetEnumerator()
    {
        var rowCount = Size.RowCount;
        var columnCount = Size.ColumnCount;

        for (var i = 0; i < rowCount; i++)
        {
            for (var j = 0; j < columnCount; j++)
            {
                yield return _bombMatrix[i, j];
            }
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}