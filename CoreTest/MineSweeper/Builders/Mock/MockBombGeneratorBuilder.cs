using NSubstitute;
using SnekTech;
using SnekTech.MineSweeperRogue;

namespace SnekPluginTest.MineSweeper.Builders.Mock;

public class MockBombGeneratorBuilder
{
    private bool _constHasBomb;
    public MockBombGeneratorBuilder WithConstBool(bool constHasBomb)
    {
        _constHasBomb = constHasBomb;
        return this;
    }

    public IBombGenerator Build()
    {
        var mockBombGenerator = Substitute.For<IBombGenerator>();
        mockBombGenerator.NextHasBomb(Arg.Any<float>()).Returns(_constHasBomb);

        return mockBombGenerator;
    }
}