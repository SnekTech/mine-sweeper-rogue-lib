using FluentAssertions;
using SnekPlugin.MineSweeper;
using SnekPlugin.MineSweeper.Grid;

namespace SnekPluginTest.MineSweeper.Tests;

[TestFixture]
public class List2DSpecs
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

        count.Should().Be(arr1.Length,
            "list2d count which calculated using linq(IEnumerable), should match the original 2d array count");
    }

    [Test]
    public void can_be_created_with_2d_array()
    {
        var list2D = new List2D<int>(arr1);

        list2D[0][2].Should().Be(arr1[0,2]);
    }

    [Test]
    public void can_be_created_using_add_row()
    {
        var list2D = new List2D<int>();
        var row = new List<int> {1, 2, 3};
        
        list2D.AddRow(row);

        list2D[0][1].Should().Be(row[1]);
    }

    [Test]
    public void should_return_GridSize_through_extension_method()
    {
        var matrixSize = arr1.Size();
        matrixSize.Should().BeEquivalentTo(new GridSize(2, 3));
    }
}