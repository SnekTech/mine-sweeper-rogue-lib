using SnekPlugin.MineSweeper;
using SnekPlugin.MineSweeper.Grid;

namespace SnekPluginTest.MineSweeper.Builders;

public class BombMatrixBuilder
{
    private int[,]? _bombMatrixInt;
    private IGridData? _gridData;
    private IBombGenerator? _bombGenerator;

    public BombMatrixBuilder WithBombIntMatrix(int[,] matrix)
    {
        _bombMatrixInt = matrix;
        return this;
    }

    public BombMatrixBuilder WithGridData(IGridData gridData)
    {
        _gridData = gridData;
        return this;
    }

    public BombMatrixBuilder WithBombGenerator(IBombGenerator bombGenerator)
    {
        _bombGenerator = bombGenerator;
        return this;
    }

    private BombMatrix Build()
    {
        if (_bombMatrixInt != null)
        {
            return new BombMatrix(_bombMatrixInt);
        }

        return new BombMatrix(_gridData!, _bombGenerator!);
    }

    public static implicit operator BombMatrix(BombMatrixBuilder builder)
    {
        return builder.Build();
    }
}