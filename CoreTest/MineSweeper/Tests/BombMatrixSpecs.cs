using FluentAssertions;
using MineSweeperRogue;
using MineSweeperRogue.Grid;
using SnekPluginTest.MineSweeper.AssertExtensions;
using SnekPluginTest.MineSweeper.Builders;

namespace SnekPluginTest.MineSweeper.Tests;

public class BombMatrixSpecs
{
    private static readonly string[] empty = Array.Empty<string>();
    
    private static readonly string[] square1 =
    {
        "💣💢💣",
        "💣💢💣",
        "💣💣💣",
    };

    private static readonly string[] rect1 =
    {
        "💣💣💣",
        "💢💣💢",
    };

    private static readonly string[][] AllBombMatrices =
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
        1.Invoking(_ => (BombMatrix)A.BombMatrix.WithBombRows(empty))
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

    [TestCaseSource(nameof(AllBombMatrices))]
    public void size_should_match_the_origin_matrix(string[] bombRows)
    {
        // Arrange
        BombMatrix bombMatrix = A.BombMatrix.WithBombRows(bombRows);

        // Assert
        bombMatrix.GridSize.Tuple.Should().BeEquivalentTo(GridExtensions.GetDimension(bombRows));
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

        // Assert
        bombMatrix.BombCount.Should().Be(0);
    }

    [Test]
    public void should_have_4_bomb_when_created_from_rect1()
    {
        BombMatrix matrixWith4Bombs = A.BombMatrix.WithBombRows(rect1);

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
        bombCount.Should().Be(bombMatrix.GridSize.TotalCount);
    }
}