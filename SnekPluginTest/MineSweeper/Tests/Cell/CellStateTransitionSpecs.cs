﻿using FluentAssertions;
using SnekPlugin.MineSweeper.Cell;
using SnekPlugin.MineSweeper.Cell.StateMachine;
using SnekPlugin.MineSweeper.Grid;
using SnekPluginTest.MineSweeper.Builders;

namespace SnekPluginTest.MineSweeper.Tests;

public class CellStateTransitionSpecs
{
    [Test]
    public async Task should_be_covered_when_created()
    {
        // Arrange

        BombMatrix parentMatrix = A.BombMatrix
            .WithOnlyOneCellThat(true);

        var cell = await A.CellBuilder
            .WithParentMatrix(parentMatrix)
            .At(GridIndex.First).Build();

        // Act


        // Assert
        cell.State.Should().Be(CellStateValue.Covered);
    }

    [Test]
    public async Task covered_cell_should_be_flagged_when_switchFlag_complete()
    {
        // Arrange
        BombMatrix parentMatrix = A.BombMatrix
            .WithOnlyOneCellThat(true);
        var cell = await A.CellBuilder.WithParentMatrix(parentMatrix)
            .At(GridIndex.First).Build();

        // Act
        await cell.SwitchFlagAsync();

        // Assert
        cell.State.Should().Be(CellStateValue.Flagged);
    }

    [Test]
    public async Task flaggedCell_should_be_covered_When_switchFlag_complete()
    {
        // Arrange
        BombMatrix parent = A.BombMatrix.WithOnlyOneCellThat(true);
        var cell = await A.CellBuilder.WithParentMatrix(parent).At(GridIndex.First).Build();
        
        // Act
        await cell.SwitchFlagAsync();
        await cell.SwitchFlagAsync();

        // Assert
        cell.State.Should().Be(CellStateValue.Covered);
    }

    [Test]
    public async Task coveredCell_should_be_revealed_when_reveal_completed()
    {
        // Arrange
        BombMatrix parent = A.BombMatrix.WithOnlyOneCellThat(true);
        var cell = await A.CellBuilder.WithParentMatrix(parent).At(GridIndex.First).Build();
        
        // Act
        await cell.RevealAsync();

        // Assert
        cell.State.Should().Be(CellStateValue.Revealed);
    }

    [Test]
    public async Task revealedCell_should_Not_change_when_call_reveal()
    {
        
    }
}