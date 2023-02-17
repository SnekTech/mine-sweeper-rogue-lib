using FluentAssertions;
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

    private static readonly BombMatrix bombMatrix1 = A.BombMatrix.WithArray2D(Zero1);
    private static readonly BombMatrix bombMatrix2 = A.BombMatrix.WithArray2D(One1);

    private static readonly BombMatrix[] AllBombMatrices =
    {
        bombMatrix1,
        bombMatrix2,
    };

    [TestCaseSource(nameof(AllBombMatrices))]
    public void should_match_bombCount_with_original_bombMatrix(BombMatrix bombMatrix)
    {
        // Arrange
        var humbleGrid = A.MockHumbleGridBuilder
            .WithGridSize(bombMatrix.Size).Build();
        Grid grid = A.Grid
            .WithBombMatrix(bombMatrix)
            .WithHumbleGrid(humbleGrid);

        var expectedBombCount = bombMatrix.BombCount;

        grid.BombCount.Should().Be(expectedBombCount, because: $"{nameof(bombMatrix)} has {expectedBombCount} bombs");
    }
}