using Cysharp.Threading.Tasks;
using MineSweeperRogue.GridSystem;
using MineSweeperRogue.GridSystem.Cell;

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

    public async UniTask<Cell> Build()
    {
        var parent = await A.GridBuilder.WithBombMatrix(_bombMatrix).Build();
        var cell = parent.GetCellAt(_gridIndex);

        return (Cell) cell;
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