using NSubstitute;
using SnekPlugin.MineSweeper.Cell;
using SnekPlugin.MineSweeper.Grid;

namespace SnekPluginTest.MineSweeper.Builders.Mock;

public class MockHumbleGridBuilder
{
    private int _cellCount;

    public MockHumbleGridBuilder WithGridSize(GridSize gridSize)
    {
        _cellCount = gridSize.TotalCount;
        return this;
    }
    
    public IHumbleGrid Build()
    {
        var mock = Substitute.For<IHumbleGrid>();

        var humbleCells = new List<IHumbleCell>(_cellCount);
        for (var i = 0; i < _cellCount; i++)
        {
            var mockHumbleCellBuilder = new MockHumbleCellBuilder();
            humbleCells.Add(mockHumbleCellBuilder.Build());
        }

        mock.InstantiateHumbleCells(Arg.Any<int>()).Returns(humbleCells);
        
        return mock;
    }
}