using NSubstitute;
using SnekPlugin.MineSweeper.Grid;
using SnekPluginTest.MineSweeper.Builders;

namespace SnekPluginTest.MineSweeper.Tests;

public class CellBasicSpecs
{
    [Test]
    public void should_init_humble_cell_when_parent_grid_finish_cells_generation()
    {
        // Arrange
        var mockCell = A.MockHumbleCellBuilder.Build();
        BombMatrix bombMatrixOne = A.BombMatrix.WithOnlyOneCellThat(true);
        var humbleGrid = A.MockHumbleGridBuilder
            .WithHumbleCellProvider(() => mockCell).Build();
        var grid = A.GridBuilder
            .WithBombMatrix(bombMatrixOne)
            .WithHumbleGrid(humbleGrid).Build();

        // Act


        // Assert
        mockCell.Received().Init(Arg.Any<GridIndex>(), Arg.Any<int>());
    }
}