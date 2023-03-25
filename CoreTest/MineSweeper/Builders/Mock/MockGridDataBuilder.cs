using NSubstitute;
using SnekTech.MineSweeperRogue.GridSystem;

namespace SnekPluginTest.MineSweeper.Builders.Mock;

public class MockGridDataBuilder
{
    private GridSize _gridSize;
    private float _bombProbability;

    public MockGridDataBuilder WithSize(GridSize gridSize)
    {
        _gridSize = gridSize;
        return this;
    }

    public MockGridDataBuilder WithBombProbability(float probability)
    {
        _bombProbability = probability;
        return this;
    }

    public IGridData Build()
    {
        var mockGridData = Substitute.For<IGridData>();
        mockGridData.GridSize.Returns(_gridSize);
        mockGridData.BombProbability.Returns(_bombProbability);

        return mockGridData;
    }
}