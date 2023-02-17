using FluentAssertions;
using SnekPlugin.MineSweeper;
using SnekPlugin.MineSweeper.Grid;

namespace SnekPluginTest;

[TestFixture]
public class List2DTests
{
    private readonly List2D<int> _list2D = new List2D<int>();

    private static readonly int[,] arr1 = {
        {1, 2, 3},
        {4, 5, 6},
    };

    [SetUp]
    public void Setup()
    {
        _list2D.ResetWith(arr1);
    }

    [TestCase(0, 0, 1)]
    [TestCase(1, 1, 5)]
    public void can_Retrieve_AtIndex(int rowIndex, int columnIndex, int value)
    {
        _list2D.ResetWith(arr1);

        var result = _list2D[rowIndex][columnIndex];

        result.Should().Be(value);
    }

    [Test]
    public void is_enumerable()
    {
        _list2D.ResetWith(arr1);

        var count = _list2D.Count();

        count.Should().Be(6);
    }

    [Test]
    public void can_init_with_2d_array()
    {
        var list2D = new List2D<int>(new[,]
        {
            {1, 2, 3},
            {4, 5, 6},
        });

        list2D[0][2].Should().Be(3);
    }

    [Test]
    public void should_return_GridSize_through_extension_method()
    {
        var matrixSize = arr1.Size();
        matrixSize.Should().BeEquivalentTo(new GridSize(2, 3));
    }
}