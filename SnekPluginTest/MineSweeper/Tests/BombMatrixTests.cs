using FluentAssertions;
using SnekPlugin.MineSweeper;
using SnekPlugin.MineSweeper.Grid;
using SnekPluginTest.MineSweeper.Builders;

namespace SnekPluginTest.MineSweeper.Tests;

[TestFixture]
public class BombMatrixTests
{
    private static readonly int[,] arr1 =
    {
        {1, 0, 1},
        {1, 0, 1},
        {1, 1, 1},
    };

    [Test]
    public void size_should_match_the_origin_matrix()
    {
        BombMatrix bombMatrix = A.BombMatrix.WithBombIntMatrix(arr1);
        
        bombMatrix.Size.Should().BeEquivalentTo(arr1.Size());
    }

    [Test]
    public void every_element_should_match_the_origin_matrix()
    {
        BombMatrix bombMatrix = A.BombMatrix.WithBombIntMatrix(arr1);

        var size = bombMatrix.Size;
        for (var i = 0; i < size.RowCount; i++)
        {
            for (var j = 0; j < size.ColumnCount; j++)
            {
                var hasBombOriginal = arr1[i, j] != 0;
                bombMatrix[i, j].Should().Be(hasBombOriginal);
            }
        }
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

        for (var i = 0; i < rowCount; i++)
        {
            for (var j = 0; j < columnCount; j++)
            {
                bombMatrix[i, j].Should().Be(false);
            }
        }
    }
}