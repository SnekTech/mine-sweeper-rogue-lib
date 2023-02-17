using FluentAssertions;
using FluentAssertions.Execution;
using SnekPlugin.MineSweeper;
using SnekPlugin.MineSweeper.Grid;
using SnekPluginTest.MineSweeper.AssertExtensions;
using SnekPluginTest.MineSweeper.Builders;

namespace SnekPluginTest.MineSweeper.Tests;

[TestFixture]
public class BombMatrixTests
{
    private static readonly int[,] square1 =
    {
        {1, 0, 1},
        {1, 0, 1},
        {1, 1, 1},
    };

    private static readonly int[,] rect1 =
    {
        {1, 1, 1},
        {0, 1, 0},
    };

    private static readonly int[][,] AllArrays =
    {
        square1,
        rect1,
    };

    [Test]
    public void size_should_match_the_origin_matrix()
    {
        BombMatrix bombMatrix = A.BombMatrix.WithArray2D(square1);

        bombMatrix.Size.Should().BeEquivalentTo(square1.Size());
    }

    [TestCaseSource(nameof(AllArrays))]
    public void every_element_should_match_the_origin_matrix(int[,] array2D)
    {
        BombMatrix bombMatrix = A.BombMatrix.WithArray2D(array2D);

        bombMatrix.Should().Match(array2D);
    }

    [Test]
    public void should_have_no_bomb_when_using_const_false_bomb_generator()
    {
        const int rowCount = 10;
        const int columnCount = 10;
        var gridData = A.MockGridDataBuilder
            .WithSize(new GridSize(rowCount, columnCount)).Build();
        var falseBombGenerator = A.MockBombGeneratorBuilder
            .WithConstBool(false).Build();
        BombMatrix bombMatrix = A.BombMatrix
            .WithGridData(gridData)
            .WithBombGenerator(falseBombGenerator);

        var bombCount = bombMatrix.Count(hasBomb => hasBomb);

        bombCount.Should().Be(0);
    }
}