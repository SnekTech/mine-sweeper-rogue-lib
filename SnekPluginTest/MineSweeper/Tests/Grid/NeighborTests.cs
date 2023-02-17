using FluentAssertions;
using SnekPlugin.MineSweeper.Grid;
using SnekPluginTest.MineSweeper.Builders;

namespace SnekPluginTest.MineSweeper.Tests;

public class NeighborTests
{
    private static readonly IHumbleGrid SimpleHumbleGrid = A.MockHumbleGridBuilder.Build();

    [Test]
    public void grid_with_1_cell_with_1_bomb_should_have_zero_neighbor()
    {
        // Arrange
        BombMatrix oneBombCell = A.BombMatrix.WithArray2D(new[,]{{1}});
        Grid grid = A.Grid
            .WithBombMatrix(oneBombCell)
            .WithHumbleGrid(SimpleHumbleGrid);

        // Act
        var cell = grid.GetCellAt(new GridIndex(0, 0));
        var neighbors = grid.GetNeighborsOf(cell);

        // Assert
        neighbors.Should().BeEmpty();
    }

    [Test]
    public void corner_cell_should_have_3_neighbors()
    {
        // Arrange
        var array2D = new[,]
        {
            {1, 0},
            {0, 0},
        };
        BombMatrix bombMatrix = A.BombMatrix.WithArray2D(array2D);
        Grid grid = A.Grid
            .WithHumbleGrid(SimpleHumbleGrid)
            .WithBombMatrix(bombMatrix);

        // Act
        var cornerCell = grid.GetCellAt(new GridIndex(0, 0));
        var neighbors = grid.GetNeighborsOf(cornerCell);

        // Assert
        neighbors.Should().HaveCount(3);
    }

    [Test]
    public void side_cell_should_have_5_neighbors()
    {
        // Arrange
        var array2D = new[,]
        {
            {0, 1, 0},
            {0, 0, 0},
        };
        BombMatrix bombMatrix = A.BombMatrix.WithArray2D(array2D);
        Grid grid = A.Grid
            .WithHumbleGrid(SimpleHumbleGrid)
            .WithBombMatrix(bombMatrix);
        var sideCell = grid.GetCellAt(new GridIndex(0, 1));

        // Act
        var neighbors = grid.GetNeighborsOf(sideCell);

        // Assert
        neighbors.Should().HaveCount(5);
    }

    [Test]
    public void center_cell_should_have_8_neighbors()
    {
        // Arrange
        var array2D = new[,]
        {
            {0, 0, 0},
            {0, 1, 0},
            {0, 0, 0},
        };
        BombMatrix bombMatrix = A.BombMatrix.WithArray2D(array2D);
        Grid grid = A.Grid
            .WithHumbleGrid(SimpleHumbleGrid)
            .WithBombMatrix(bombMatrix);
        var centerCell = grid.GetCellAt(new GridIndex(1, 1));

        // Act
        var neighbors = grid.GetNeighborsOf(centerCell);

        // Assert
        neighbors.Should().HaveCount(8);
    }

    [Test]
    public void can_calculate_neighbor_bombCount()
    {
        // Arrange
        var arr = new[,]
        {
            {1,0,1},
            {1,0,1},
            {1,0,1},
        };
        BombMatrix bombMatrix = A.BombMatrix
            .WithArray2D(arr);
        Grid grid = A.Grid
            .WithBombMatrix(bombMatrix)
            .WithHumbleGrid(SimpleHumbleGrid);
        var centerCell = grid.GetCellAt(new GridIndex(1, 1));

        // Act
        

        // Assert
        centerCell.NeighborBombCount.Should().Be(6);
    }

    [Test]
    public void should_return_negative_when_calculating_neighborBombCount_on_a_bomb()
    {
        // Arrange
        var arr2D = new[,]
        {
            {1},
        };
        BombMatrix bombMatrix = A.BombMatrix.WithArray2D(arr2D);
        Grid grid = A.Grid
            .WithBombMatrix(bombMatrix)
            .WithHumbleGrid(SimpleHumbleGrid);
        var bombCell = grid.GetCellAt(new GridIndex(0, 0));


        // Act


        // Assert
        var cellIndex = bombCell.Index;
        bombCell.NeighborBombCount.Should()
            .BeNegative(because: "cell at ({0}, {1}) has a bomb", cellIndex.RowIndex, cellIndex.ColumnIndex);
    }
}