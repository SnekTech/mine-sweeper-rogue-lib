using MineSweeperRogue.Grid.Cell;
using MineSweeperRogue.Grid.Cell.Components;
using NSubstitute;

namespace SnekPluginTest.MineSweeper.Builders.Mock;

public class MockHumbleCellBuilder
{
    private ICover _cover = Substitute.For<ICover>();
    private IFlag _flag = Substitute.For<IFlag>();

    public MockHumbleCellBuilder WithCover(ICover cover)
    {
        _cover = cover;
        return this;
    }

    public MockHumbleCellBuilder WithFlag(IFlag flag)
    {
        _flag = flag;
        return this;
    }
    
    public IHumbleCell Build()
    {
        var mockHumbleCell = Substitute.For<IHumbleCell>();
        mockHumbleCell.Cover.Returns(_cover);
        mockHumbleCell.Flag.Returns(_flag);

        return mockHumbleCell;
    }
}