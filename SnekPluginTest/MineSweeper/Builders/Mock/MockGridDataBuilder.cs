using NSubstitute;
using SnekPlugin.MineSweeper.Grid;

namespace SnekPluginTest.MineSweeper.Builders.Mock;

public class MockGridDataBuilder
{
    private GridSize _gridSize;
    private float _bombProbability;

    public MockGridDataBuilder WithSize(int rowCount, int columnCount)
    {
        _gridSize = new GridSize(rowCount, columnCount);
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