using SnekPlugin.MineSweeper.Cell;
using SnekPlugin.MineSweeper.Grid;

namespace SnekPluginTest.MineSweeper.Builders;

public class CellBuilder
    :
        ISetCellBuilderFirstStage,
        ISetGridIndexStage
{
    private BombMatrix _bombMatrix = A.BombMatrix.WithOnlyOneCellThat(true);
    private GridIndex _gridIndex = new GridIndex(0, 0);

    public CellBuilder WithOneCellParentGridThat(bool hasBomb)
    {
        _bombMatrix = A.BombMatrix.WithOnlyOneCellThat(hasBomb);
        
        return this;
    }

    public ISetGridIndexStage WithParentMatrix(BombMatrix bombMatrix)
    {
        _bombMatrix = bombMatrix;
        return this;
    }

    public CellBuilder At(GridIndex gridIndex)
    {
        _gridIndex = gridIndex;
        return this;
    }

    public Cell Build()
    {
        Grid parent = A.Grid.WithBombMatrix(_bombMatrix);
        var cell = parent.GetCellAt(_gridIndex);

        return (Cell) cell;
    }

    public static implicit operator Cell(CellBuilder builder)
    {
        return builder.Build();
    }
}

public interface ISetCellBuilderFirstStage
{
    CellBuilder WithOneCellParentGridThat(bool hasBomb);
    ISetGridIndexStage WithParentMatrix(BombMatrix bombMatrix);
}

public interface ISetGridIndexStage
{
    CellBuilder At(GridIndex gridIndex);
}