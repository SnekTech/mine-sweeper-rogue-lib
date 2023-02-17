using SnekPlugin.MineSweeper.Grid;

namespace SnekPluginTest.MineSweeper.Builders;

public class BombMatrixBuilder
{
    private int[,]? _bombMatrixInt;

    public BombMatrixBuilder WithBombIntMatrix(int[,] matrix)
    {
        _bombMatrixInt = matrix;
        return this;
    }

    private BombMatrix Build()
    {
        return new BombMatrix(_bombMatrixInt!);
    }

    public static implicit operator BombMatrix(BombMatrixBuilder builder)
    {
        return builder.Build();
    }
}