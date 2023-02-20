﻿using NSubstitute;
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
        Grid grid = A.Grid
            .WithBombMatrix(bombMatrixOne)
            .WithHumbleGrid(humbleGrid);

        // Act


        // Assert
        mockCell.Received().Init(Arg.Any<GridIndex>(), Arg.Any<int>());
    }
}