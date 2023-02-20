using SnekPlugin.MineSweeper;
using SnekPlugin.MineSweeper.Grid;

namespace SnekPluginTest.MineSweeper.Builders;

public class BombMatrixBuilder
    : 
        IChooseBetweenArrayOrGridDataStage, 
        ISetBombGeneratorStage
{
    private int[,] _bombMatrixInt = Constants.DefaultArray2D;
    private IGridData? _gridData;
    private IBombGenerator _bombGenerator = A.MockBombGeneratorBuilder.WithConstBool(true).Build();

    public BombMatrixBuilder WithArray2D(int[,] array2D)
    {
        _bombMatrixInt = array2D;
        return this;
    }

    public BombMatrixBuilder WithOnlyOneCellThat(bool hasBomb)
    {
        return this;
    }

    public BombMatrixBuilder HasBomb()
    {
        _bombMatrixInt = new[,] {{1}};
        return this;
    }

    public BombMatrixBuilder IsEmpty()
    {
        _bombMatrixInt = new[,] {{1}};
        return this;
    }

    public ISetBombGeneratorStage WithGridData(IGridData gridData)
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
        if (_gridData != null)
        {
            return new BombMatrix(_gridData, _bombGenerator);
        }

        return new BombMatrix(_bombMatrixInt);
    }

    public static implicit operator BombMatrix(BombMatrixBuilder builder)
    {
        return builder.Build();
    }
}

public interface IChooseBetweenArrayOrGridDataStage
{
    BombMatrixBuilder WithArray2D(int[,] array2D);
    ISetBombGeneratorStage WithGridData(IGridData gridData);

    BombMatrixBuilder WithOnlyOneCellThat(bool hasBomb);
}

public interface ISetBombGeneratorStage
{
    BombMatrixBuilder WithBombGenerator(IBombGenerator bombGenerator);
}