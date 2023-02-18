using FluentAssertions;
using SnekPlugin.MineSweeper;
using SnekPlugin.MineSweeper.Grid;
using SnekPluginTest.MineSweeper.AssertExtensions;
using SnekPluginTest.MineSweeper.Builders;

namespace SnekPluginTest.MineSweeper.Tests;

[TestFixture]
public class BombMatrixSpecs
{
    private static readonly int[,] empty = { };
    
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

    private static readonly IBombGenerator FalseBombGenerator = A.MockBombGeneratorBuilder
        .WithConstBool(false).Build();
    private static readonly IBombGenerator TrueBombGenerator = A.MockBombGeneratorBuilder
        .WithConstBool(true).Build();

    [Test]
    public void should_throw_given_empty_2d_array()
    {
        1.Invoking(_ => (BombMatrix)A.BombMatrix.WithArray2D(empty))
            .Should().Throw<ArgumentException>();
    }

    [Test]
    public void should_throw_When_given_empty_sized_gridData()
    {
        var emptySizedGridData = A.MockGridDataBuilder
            .WithSize(new GridSize(0, 0)).Build();

        1.Invoking(_ => 
                (BombMatrix) A.BombMatrix
                    .WithGridData(emptySizedGridData)
                    .WithBombGenerator(FalseBombGenerator))
            .Should().Throw<ArgumentException>();
    }

    [TestCaseSource(nameof(AllArrays))]
    public void size_should_match_the_origin_matrix(int[,] array2D)
    {
        // Arrange
        var originalSize = array2D.Size();
        BombMatrix bombMatrix = A.BombMatrix.WithArray2D(array2D);

        // Assert
        bombMatrix.Size.Should().BeEquivalentTo(originalSize);
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
        // Arrange
        var gridData = A.MockGridDataBuilder
            .WithSize(new GridSize(10, 10)).Build();
        
        BombMatrix bombMatrix = A.BombMatrix
            .WithGridData(gridData)
            .WithBombGenerator(FalseBombGenerator);
        
        // Act
        var bombCount = bombMatrix.Count(hasBomb => hasBomb);
        

        // Assert
        bombCount.Should().Be(0);
    }

    [Test]
    public void should_have_4_bomb_when_created_from_rect1()
    {
        BombMatrix matrixWith4Bombs = A.BombMatrix.WithArray2D(rect1);

        matrixWith4Bombs.BombCount.Should().Be(4);
    }

    [Test]
    public void should_all_have_bomb_whenUsing_constTrueBombGenerator()
    {
        // Arrange
        var gridData = A.MockGridDataBuilder
            .WithSize(new GridSize(10, 10)).Build();
        BombMatrix bombMatrix = A.BombMatrix
            .WithGridData(gridData)
            .WithBombGenerator(TrueBombGenerator);

        // Act
        var bombCount = bombMatrix.BombCount;

        // Assert
        bombCount.Should().Be(bombMatrix.Size.TotalCount);
    }
}