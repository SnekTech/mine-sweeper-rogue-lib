using FluentAssertions;
using SnekPlugin.MineSweeper.Cell;
using SnekPlugin.MineSweeper.Cell.StateMachine;
using SnekPlugin.MineSweeper.Grid;
using SnekPluginTest.MineSweeper.Builders;

namespace SnekPluginTest.MineSweeper.Tests;

public class CellStateTransitionSpecs
{
    [Test]
    public void should_be_covered_when_created()
    {
        // Arrange

        BombMatrix parentMatrix = A.BombMatrix
            .WithOnlyOneCellThat()
            .HasBomb();

        Cell cell = A.Cell
            .WithParentMatrix(parentMatrix)
            .At(GridIndex.First);

        // Act


        // Assert
        cell.CurrentState.Should().Be(CellStateValue.Covered);
    }

    [Test]
    public async Task covered_cell_should_be_flagged_when_switchFlag_complete()
    {
        // Arrange
        BombMatrix parentMatrix = A.BombMatrix
            .WithOnlyOneCellThat()
            .HasBomb();
        Cell cell = A.Cell.WithParentMatrix(parentMatrix)
            .At(GridIndex.First);

        // Act
        await cell.SwitchFlagAsync();

        // Assert
        cell.CurrentState.Should().Be(CellStateValue.Flagged);
    }

    [Test]
    public async Task flaggedCell_should_be_covered_When_switchFlag_complete()
    {
        // Arrange
        BombMatrix parent = A.BombMatrix.WithOnlyOneCellThat().HasBomb();
        Cell cell = A.Cell.WithParentMatrix(parent).At(GridIndex.First);
        
        // Act
        await cell.SwitchFlagAsync();
        await cell.SwitchFlagAsync();

        // Assert
        cell.CurrentState.Should().Be(CellStateValue.Covered);
    }

    [Test]
    public async Task coveredCell_should_be_revealed_when_reveal_completed()
    {
        // Arrange
        BombMatrix parent = A.BombMatrix.WithOnlyOneCellThat().HasBomb();
        Cell cell = A.Cell.WithParentMatrix(parent).At(GridIndex.First);
        
        // Act
        await cell.RevealAsync();

        // Assert
        cell.CurrentState.Should().Be(CellStateValue.Revealed);
    }
}