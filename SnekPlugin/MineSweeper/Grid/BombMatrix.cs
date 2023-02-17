using System.Collections;
using System.Collections.Generic;

namespace SnekPlugin.MineSweeper.Grid;

public class BombMatrix
{
    private readonly bool[,] _bombMatrix;

    public BombMatrix(IGridData gridData, IBombGenerator bombGenerator)
    {
        var size = gridData.GridSize;
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
}