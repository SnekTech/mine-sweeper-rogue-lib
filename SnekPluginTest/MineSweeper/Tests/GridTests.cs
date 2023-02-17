using FluentAssertions;
using SnekPlugin.MineSweeper;
using SnekPlugin.MineSweeper.Grid;
using SnekPluginTest.MineSweeper.Builders;

namespace SnekPluginTest.MineSweeper.Tests;

[TestFixture]
public class GridTests
{
    private static readonly int[,] Zero1 =
    {
        {0, 0, 0},
        {0, 0, 0},
    };

    private static readonly int[,] One1 =
    {
        {1, 1, 1},
        {1, 1, 1},
    };

    [Test]
    public void should_have_no_bomb_when_initialized_with_zero_bomb_matrix()
    {
        BombMatrix bombMatrix = A.BombMatrix.WithArray2D(Zero1);
        var humbleGrid = A.MockHumbleGridBuilder
            .WithGridSize(bombMatrix.Size).Build();
        Grid grid = A.Grid
            .WithBombMatrix(bombMatrix)
            .WithHumbleGrid(humbleGrid);

        grid.BombCount.Should().Be(0, because: $"no bombs in int matrix {nameof(Zero1)}");
    }

    [Test]
    public void should_match_bombCount_with_original_matrix()
    {
        BombMatrix bombMatrix = A.BombMatrix.WithArray2D(One1);
        var humbleGrid = A.MockHumbleGridBuilder
            .WithGridSize(bombMatrix.Size).Build();
        Grid grid = A.Grid
            .WithBombMatrix(bombMatrix)
            .WithHumbleGrid(humbleGrid);
        var expectedBombCount = One1.Length;

        grid.BombCount.Should().Be(expectedBombCount, because: $"int matrix has {expectedBombCount} bombs");
    }
}