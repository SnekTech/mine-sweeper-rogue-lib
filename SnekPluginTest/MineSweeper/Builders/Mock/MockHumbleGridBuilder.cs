using NSubstitute;
using NSubstitute.Core;
using SnekPlugin.MineSweeper.Cell;
using SnekPlugin.MineSweeper.Grid;

namespace SnekPluginTest.MineSweeper.Builders.Mock;

public class MockHumbleGridBuilder
{
    private List<IHumbleCell> GetHumbleCells(int n)
    {
        var humbleCells = new List<IHumbleCell>();
        for (var i = 0; i < n; i++)
        {
            var mockHumbleCellBuilder = new MockHumbleCellBuilder();
            humbleCells.Add(mockHumbleCellBuilder.Build());
        }

        return humbleCells;
    }

    public IHumbleGrid Build()
    {
        var mock = Substitute.For<IHumbleGrid>();


        mock.InstantiateHumbleCells(Arg.Any<int>()).Returns(info => GetHumbleCells(info.Arg<int>()));

        return mock;
    }
}