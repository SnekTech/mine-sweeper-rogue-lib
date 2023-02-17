using FluentAssertions;
using SnekPlugin.MineSweeper.Grid;
using SnekPluginTest.MineSweeper.AssertExtensions;
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

    private static readonly int[,] m1 =
    {
        {1, 1, 1},
        {1, 1, 1},
        {1, 1, 1},
    };

    private static readonly IHumbleGrid basicHumbleGrid = A.MockHumbleGridBuilder.Build();

    private static readonly BombMatrix bombMatrix1 = A.BombMatrix.WithArray2D(Zero1);
    private static readonly BombMatrix bombMatrix2 = A.BombMatrix.WithArray2D(One1);
    private static readonly BombMatrix bombMatrix3 = A.BombMatrix.WithArray2D(m1);

    private static readonly BombMatrix[] AllBombMatrices =
    {
        bombMatrix1,
        bombMatrix2,
    };

    [TestCaseSource(nameof(AllBombMatrices))]
    public void should_match_bombCount_with_original_bombMatrix(BombMatrix bombMatrix)
    {
        // Arrange
        SnekPlugin.MineSweeper.Grid.Grid grid = A.Grid
            .WithBombMatrix(bombMatrix)
            .WithHumbleGrid(basicHumbleGrid);

        var expectedBombCount = bombMatrix.BombCount;

        grid.BombCount.Should().Be(expectedBombCount, because: $"{nameof(bombMatrix)} has {expectedBombCount} bombs");
    }

    [Test]
    public void should_match_bombCount_after_initCells_with_another_bombMatrix()
    {
        // Arrange
        var originalBombMatrix = bombMatrix1;
        
        SnekPlugin.MineSweeper.Grid.Grid grid = A.Grid
            .WithBombMatrix(originalBombMatrix)
            .WithHumbleGrid(basicHumbleGrid);

        // Act
        var anotherBombMatrix = bombMatrix3;
        anotherBombMatrix.Size.Should().NotBeEquivalentTo(originalBombMatrix.Size);
        
        grid.InitCells(anotherBombMatrix);

        // Assert
        grid.BombCount.Should().Be(anotherBombMatrix.BombCount);
    }

    [Test]
    public void can_validate_gridIndex()
    {
        // Arrange
        var bombMatrix = bombMatrix1;
        SnekPlugin.MineSweeper.Grid.Grid grid = A.Grid
            .WithBombMatrix(bombMatrix)
            .WithHumbleGrid(basicHumbleGrid);
        var badGridIndex = new GridIndex(-1, -1);

        // Act

        // Assert
        badGridIndex.Should().BeInvalidIndexOf(grid);
        
        for (var i = 0; i < grid.Size.RowCount; i++)
        {
            for (var j = 0; j < grid.Size.ColumnCount; j++)
            {
                var index = new GridIndex(i, j);
                index.Should().BeValidIndexOf(grid);
            }
        }
    }
}