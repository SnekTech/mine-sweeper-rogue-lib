using MineSweeperRogue;
using MineSweeperRogue.Grid;
using MineSweeperRogue.Grid.Cell;

namespace SnekPluginTest.MineSweeper.Builders;

public class BombMatrixBuilder
    :
        IChooseBetweenBoolArrayOrGridDataStage,
        ISetBombGeneratorStage
{
    private bool[,] _bombMatrix = BombMatrix.From(new[] {"1"});
    private IGridData? _gridData;
    private IBombGenerator _bombGenerator = A.MockBombGeneratorBuilder.WithConstBool(true).Build();

    public BombMatrixBuilder WithBombRows(string[] bombRows)
    {
        _bombMatrix = BombMatrix.From(bombRows);
        return this;
    }

    public BombMatrixBuilder WithOnlyOneCellThat(bool hasBomb)
    {
        _bombMatrix = BombMatrix.From(new[] {hasBomb ? CellExtensions.Emoji.Bomb : "0"});
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
        _bombMatrix = BombMatrix.From(_gridData!, _bombGenerator);
        return this;
    }

    private BombMatrix Build()
    {
        return new BombMatrix(_bombMatrix);
    }

    public static implicit operator BombMatrix(BombMatrixBuilder builder)
    {
        return builder.Build();
    }
}

public interface IChooseBetweenBoolArrayOrGridDataStage
{
    BombMatrixBuilder WithBombRows(string[] bombRows);
    ISetBombGeneratorStage WithGridData(IGridData gridData);

    BombMatrixBuilder WithOnlyOneCellThat(bool hasBomb);
}

public interface ISetBombGeneratorStage
{
    BombMatrixBuilder WithBombGenerator(IBombGenerator bombGenerator);
}