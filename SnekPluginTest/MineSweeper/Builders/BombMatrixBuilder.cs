using SnekPlugin.MineSweeper;
using SnekPlugin.MineSweeper.Cell;
using SnekPlugin.MineSweeper.Grid;

namespace SnekPluginTest.MineSweeper.Builders;

public class BombMatrixBuilder
    :
        IChooseBetweenArrayOrGridDataStage,
        ISetBombGeneratorStage
{
    private bool[,] _bombMatrix = BoolMatrix.CreateBoolMatrix(new[] {"1"});
    private IGridData? _gridData;
    private IBombGenerator _bombGenerator = A.MockBombGeneratorBuilder.WithConstBool(true).Build();

    public BombMatrixBuilder WithBombRows(string[] bombRows)
    {
        _bombMatrix = BoolMatrix.CreateBoolMatrix(bombRows);
        return this;
    }

    public BombMatrixBuilder WithBoolMatrix(BoolMatrix hasBombMatrix)
    {
        _bombMatrix = hasBombMatrix.Matrix;
        return this;
    }

    public BombMatrixBuilder WithOnlyOneCellThat(bool hasBomb)
    {
        _bombMatrix = BoolMatrix.CreateBoolMatrix(new[] {hasBomb ? CellExtensions.BombEmoji : "0"});
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

        return new BombMatrix(_bombMatrix);
    }

    public static implicit operator BombMatrix(BombMatrixBuilder builder)
    {
        return builder.Build();
    }
}

public interface IChooseBetweenArrayOrGridDataStage
{
    BombMatrixBuilder WithBombRows(string[] bombRows);
    BombMatrixBuilder WithBoolMatrix(BoolMatrix hasBombMatrix);
    ISetBombGeneratorStage WithGridData(IGridData gridData);

    BombMatrixBuilder WithOnlyOneCellThat(bool hasBomb);
}

public interface ISetBombGeneratorStage
{
    BombMatrixBuilder WithBombGenerator(IBombGenerator bombGenerator);
}