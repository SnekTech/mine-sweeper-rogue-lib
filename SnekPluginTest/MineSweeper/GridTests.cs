using NSubstitute;
using SnekPlugin.MineSweeper.Grid;
using SnekPluginTest.MineSweeper.Builders;

namespace SnekPluginTest.MineSweeper;

[TestFixture]
public class GridTests
{
    private static readonly int[,] matrix1 =
    {
        {0, 0, 0},
        {0, 0, 0},
    };

    [Test]
    public void grid_can_be_initiated_with_2d_array()
    {
        var data = A.MockGridDataBuilder
            .WithSize(new GridSize(10, 10))
            .WithBombProbability(0)
            .Build();
        var bombGenerator = A.MockBombGeneratorBuilder
            .Build();

    }
}