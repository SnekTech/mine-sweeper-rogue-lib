using Cysharp.Threading.Tasks;
using SnekTech.MineSweeperRogue.GridSystem;

namespace SnekPluginTest.MineSweeper.Builders;

public class GridBuilder
{
    private BombMatrix _bombMatrix = A.BombMatrix.WithOnlyOneCellThat(true);
    private IHumbleGrid? _humbleGrid;

    public GridBuilder WithBombMatrix(BombMatrix bombMatrix)
    {
        _bombMatrix = bombMatrix;
        
        return this;
    }

    public GridBuilder WithHumbleGrid(IHumbleGrid humbleGrid)
    {
        _humbleGrid = humbleGrid;
        return this;
    }

    public async UniTask<Grid> Build()
    {
        _humbleGrid ??= A.MockHumbleGridBuilder.Build();

        var grid = new Grid(_humbleGrid);
        await grid.InitCells(_bombMatrix);
        
        return grid;
    }
}