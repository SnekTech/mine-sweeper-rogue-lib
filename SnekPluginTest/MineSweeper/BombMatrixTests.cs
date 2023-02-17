using FluentAssertions;
using SnekPlugin.MineSweeper;
using SnekPlugin.MineSweeper.Grid;
using SnekPluginTest.MineSweeper.Builders;

namespace SnekPluginTest.MineSweeper;

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
}