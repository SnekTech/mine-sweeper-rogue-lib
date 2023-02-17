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
        _bombMatrix = new bool[size.RowCount, size.ColumnCount];
        for (var i = 0; i < size.RowCount; i++)
        {
            for (var j = 0; j < size.RowCount; j++)
            {
                _bombMatrix[i, j] = bombMatrix[i, j] != 0;
            }
        }
    }

    public bool this[int i, int j] => _bombMatrix[i, j];
    public GridSize Size => _bombMatrix.Size();
}