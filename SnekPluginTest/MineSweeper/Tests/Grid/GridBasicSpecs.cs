using FluentAssertions;
using FluentAssertions.Execution;
using SnekPlugin.MineSweeper.Grid;
using SnekPluginTest.MineSweeper.AssertExtensions;
using SnekPluginTest.MineSweeper.Builders;

namespace SnekPluginTest.MineSweeper.Tests;

[TestFixture]
public class GridBasicSpecs
{
    private static readonly string[] Zero1 =
    {
        "000",
        "000",
    };

    private static readonly string[] One1 =
    {
        "111",
        "111",
    };

    private static readonly string[] m1 =
    {
        "111",
        "111",
        "111",
    };

    private static readonly BombMatrix bombMatrix1 = A.BombMatrix.WithBombRows(Zero1);
    private static readonly BombMatrix bombMatrix2 = A.BombMatrix.WithBombRows(One1);
    private static readonly BombMatrix bombMatrix3 = A.BombMatrix.WithBombRows(m1);

    private static readonly BombMatrix[] AllBombMatrices =
    {
        bombMatrix1,
        bombMatrix2,
    };

    [TestCaseSource(nameof(AllBombMatrices))]
    public async Task should_match_bombCount_with_original_bombMatrix(BombMatrix bombMatrix)
    {
        // Arrange
        var grid = await A.GridBuilder
            .WithBombMatrix(bombMatrix).Build();

        // Act
        var expectedBombCount = bombMatrix.BombCount;

        // Assert
        grid.BombCount.Should().Be(expectedBombCount);
    }

    [Test]
    public async Task should_match_bombCount_When_initCells_with_another_bombMatrix()
    {
        // Arrange
        var originalBombMatrix = bombMatrix1;
        
        var grid = await A.GridBuilder
            .WithBombMatrix(originalBombMatrix).Build();

        var anotherBombMatrix = bombMatrix3;
        anotherBombMatrix.GridSize.Should().NotBeEquivalentTo(originalBombMatrix.GridSize);
        
        // Act
        await grid.InitCells(anotherBombMatrix);

        // Assert
        grid.BombCount.Should().Be(anotherBombMatrix.BombCount);
    }

    [Test]
    public async Task negative_gridIndex_shouldBe_invalid()
    {
        // Arrange
        var bombMatrix = bombMatrix1;
        var grid = await A.GridBuilder
            .WithBombMatrix(bombMatrix).Build();
        var badGridIndex = new GridIndex(-1, -1);

        // Act

        // Assert
        1.Invoking(_ => grid.GetCellAt(badGridIndex))
            .Should().Throw<ArgumentOutOfRangeException>();
        
        for (var i = 0; i < grid.Size.RowCount; i++)
        {
            for (var j = 0; j < grid.Size.ColumnCount; j++)
            {
                var index = new GridIndex(i, j);
                1.Invoking(_ => grid.GetCellAt(index))
                    .Should().NotThrow();
            }
        }
    }

    [Test]
    public async Task gridIndex_that_exceeds_boundary_shouldBe_invalid()
    {
        // Arrange
        BombMatrix smallest = A.BombMatrix
            .WithOnlyOneCellThat(true);
        var grid = await A.GridBuilder
            .WithBombMatrix(smallest).Build();
        var exceededGridIndex = new GridIndex(1, 2);

        // Act
        

        // Assert
        Execute.Assertion
            .Invoking(_ => grid.GetCellAt(exceededGridIndex))
            .Should().Throw<ArgumentOutOfRangeException>();
    }

    [TestCaseSource(nameof(AllBombMatrices))]
    public async Task gridIndex_in_grid_shouldBe_valid(BombMatrix bombMatrix)
    {
        // Arrange
        var grid = await A.GridBuilder
            .WithBombMatrix(bombMatrix).Build();

        // Act

        // Assert
        for (var i = 0; i < grid.Size.RowCount; i++)
        {
            for (var j = 0; j < grid.Size.ColumnCount; j++)
            {
                var index = new GridIndex(i, j);
                Execute.Assertion
                    .Invoking(_ => grid.GetCellAt(index))
                    .Should().NotThrow();
            }
        }
    }
}