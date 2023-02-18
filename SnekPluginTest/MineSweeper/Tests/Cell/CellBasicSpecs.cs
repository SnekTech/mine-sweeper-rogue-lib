using FluentAssertions;
using NSubstitute;
using SnekPlugin.MineSweeper.Cell;
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
        BombMatrix bombMatrixOne = A.BombMatrix.WithOnlyOneCellThat().HasBomb();
        var humbleGrid = A.MockHumbleGridBuilder
            .WithHumbleCellProvider(() => mockCell).Build();
        Grid grid = A.Grid
            .WithBombMatrix(bombMatrixOne)
            .WithHumbleGrid(humbleGrid);

        // Act


        // Assert
        mockCell.Received().Init(Arg.Any<GridIndex>(), Arg.Any<int>());
    }

    [Test]
    public void should_be_covered_when_created()
    {
        // Arrange
        BombMatrix parentMatrix = A.BombMatrix
            .WithOnlyOneCellThat()
            .HasBomb();
        
        Cell cell = A.Cell
            .WithParentMatrix(parentMatrix)
            .At(new GridIndex(0, 0));

        // Act


        // Assert
        cell.IsCovered.Should().BeTrue();
    }

    [Test]
    public void covered_cell_should_be_flagged_when_switchFlag_completed()
    {
        BombMatrix parentMatrix = A.BombMatrix
            .WithOnlyOneCellThat()
            .HasBomb();
        Cell cell = A.Cell.WithParentMatrix(parentMatrix)
            .At(new GridIndex(0, 0));

        throw new NotImplementedException();
    }
}