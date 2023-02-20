using SnekPlugin.MineSweeper.Grid;

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

    private Grid Build()
    {
        _humbleGrid ??= A.MockHumbleGridBuilder.Build();
        
        return new Grid(_bombMatrix, _humbleGrid);
    }

    public static implicit operator Grid(GridBuilder builder)
    {
        return builder.Build();
    }
}