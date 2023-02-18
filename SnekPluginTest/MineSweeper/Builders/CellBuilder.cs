using SnekPlugin.MineSweeper.Cell;
using SnekPlugin.MineSweeper.Grid;

namespace SnekPluginTest.MineSweeper.Builders;

public class CellBuilder
    :
        ISetParentMatrixStage,
        ISetGridIndexStage
{
    private BombMatrix _bombMatrix = A.BombMatrix.WithArray2D(new[,] {{1}});
    private GridIndex _gridIndex = new GridIndex(0, 0);

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

public interface ISetParentMatrixStage
{
    ISetGridIndexStage WithParentMatrix(BombMatrix bombMatrix);
}

public interface ISetGridIndexStage
{
    CellBuilder At(GridIndex gridIndex);
}