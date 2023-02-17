using FluentAssertions;
using SnekPlugin.MineSweeper.Grid;
using SnekPluginTest.MineSweeper.Builders;

namespace SnekPluginTest.MineSweeper.Tests;

public class CellBasicTests
{
    private static readonly IHumbleGrid SimpleHumbleGrid = A.MockHumbleGridBuilder.Build();

    [Test]
    public void can_access_parent()
    {
        // Arrange
        BombMatrix bombMatrix = A.BombMatrix
            .WithArray2D(new[,] {{1}});
        Grid grid = A.Grid
            .WithBombMatrix(bombMatrix)
            .WithHumbleGrid(SimpleHumbleGrid);

        // Act
        var cell = grid.GetCellAt(new GridIndex(0, 0));

        // Assert
        cell.Parent.Should().BeSameAs(grid);
    }

}