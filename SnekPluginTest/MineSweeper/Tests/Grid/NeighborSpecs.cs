using FluentAssertions;
using SnekPlugin.MineSweeper.Cell;
using SnekPlugin.MineSweeper.Grid;
using SnekPluginTest.MineSweeper.Builders;

namespace SnekPluginTest.MineSweeper.Tests;

public class NeighborSpecs
{
    [Test]
    public async Task should_have_no_neighbor_when_it_is_the_only_1_cell_in_grid()
    {
        // Arrange
        BombMatrix smallest = A.BombMatrix
            .WithOnlyOneCellThat(true);
        var cell = await A.CellBuilder
            .WithParentMatrix(smallest)
            .At(new GridIndex(0, 0)).Build();

        // Act
        var neighbors = cell.Parent.GetNeighborsOf(cell);

        // Assert
        neighbors.Should().BeEmpty();
    }

    [Test]
    public async Task corner_cell_should_have_3_neighbors()
    {
        // Arrange
        var array2D = new[,]
        {
            {1, 0},
            {0, 0},
        };
        BombMatrix bombMatrix = A.BombMatrix.WithArray2D(array2D);
        var cornerCell = await A.CellBuilder
            .WithParentMatrix(bombMatrix)
            .At(new GridIndex(0, 0)).Build();

        // Act
        var neighbors = cornerCell.Parent.GetNeighborsOf(cornerCell);

        // Assert
        neighbors.Should().HaveCount(3);
    }

    [Test]
    public async Task side_cell_should_have_5_neighbors()
    {
        // Arrange
        var array2D = new[,]
        {
            {0, 1, 0},
            {0, 0, 0},
        };
        
        BombMatrix bombMatrix = A.BombMatrix.WithArray2D(array2D);
        var sideCell = await A.CellBuilder
            .WithParentMatrix(bombMatrix)
            .At(new GridIndex(0, 1)).Build();

        // Act
        var neighbors = sideCell.Parent.GetNeighborsOf(sideCell);

        // Assert
        neighbors.Should().HaveCount(5);
    }

    [Test]
    public async Task center_cell_should_have_8_neighbors()
    {
        // Arrange
        var array2D = new[,]
        {
            {0, 0, 0},
            {0, 1, 0},
            {0, 0, 0},
        };
        
        BombMatrix bombMatrix = A.BombMatrix.WithArray2D(array2D);
        
        var centerCell = await A.CellBuilder
            .WithParentMatrix(bombMatrix)
            .At(new GridIndex(1, 1)).Build();

        // Act
        var neighbors = centerCell.Parent.GetNeighborsOf(centerCell);

        // Assert
        neighbors.Should().HaveCount(8);
    }

    [Test]
    public async Task subject_center_cell_should_have_6_neighbor_bombs_when_grid_be_like_below()
    {
        // Arrange
        var arr = new[,]
        {
            {1, 0, 1},
            {1, 0, 1}, // cell at (1, 1) has 6 neighborBombs, obviously
            {1, 0, 1},
        };
        BombMatrix bombMatrix = A.BombMatrix
            .WithArray2D(arr);

        var centerCell = await A.CellBuilder
            .WithParentMatrix(bombMatrix)
            .At(new GridIndex(1, 1)).Build();

        // Act


        // Assert
        centerCell.NeighborBombCount.Should().Be(6);
    }

    [Test]
    public async Task should_return_negative_when_calculating_neighborBombCount_on_a_bomb()
    {
        BombMatrix bombMatrix = A.BombMatrix
            .WithOnlyOneCellThat(true);
        var bombCell = await A.CellBuilder
            .WithParentMatrix(bombMatrix)
            .At(GridIndex.First).Build();

        // Act


        // Assert
        var cellIndex = bombCell.Index;
        bombCell.NeighborBombCount.Should()
            .BeNegative(because: "cell at ({0}, {1}) has a bomb", cellIndex.RowIndex, cellIndex.ColumnIndex);
    }
}