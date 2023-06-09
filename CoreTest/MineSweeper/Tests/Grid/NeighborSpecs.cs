﻿using FluentAssertions;
using SnekPluginTest.MineSweeper.Builders;
using SnekTech.MineSweeperRogue.GridSystem;

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
        var bombRows = new[]
        {
            "10",
            "00",
        };
        BombMatrix bombMatrix = A.BombMatrix.WithBombRows(bombRows);
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
        var bombRows = new[]
        {
            "010",
            "000",
        };
        
        BombMatrix bombMatrix = A.BombMatrix.WithBombRows(bombRows);
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
        var bombRows = new[]
        {
            "000",
            "010",
            "000",
        };
        
        BombMatrix bombMatrix = A.BombMatrix.WithBombRows(bombRows);
        
        var centerCell = await A.CellBuilder
            .WithParentMatrix(bombMatrix)
            .At(new GridIndex(1, 1)).Build();

        // Act
        var neighbors = centerCell.Parent.GetNeighborsOf(centerCell);

        // Assert
        neighbors.Should().HaveCount(8);
    }

    [Test]
    public async Task should_have_6_neighbor_bombs()
    {
        // Arrange
        var bombRows = new[]
        {
            // 🔳, ⛳, 💢, 💣,
            "💣💢💣",
            "💣💢💣",
            "💣💢💣",
        };
        BombMatrix bombMatrix = A.BombMatrix
            .WithBombRows(bombRows);

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
            .BeNegative(because: $"cell at {cellIndex.Tuple} has a bomb");
    }
}