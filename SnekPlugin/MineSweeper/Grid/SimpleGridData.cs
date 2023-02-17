namespace SnekPlugin.MineSweeper.Grid;

public class SimpleGridData : IGridData
{
    public SimpleGridData(GridSize gridSize, float bombProbability)
    {
        GridSize = gridSize;
        BombProbability = bombProbability;
    }

    public GridSize GridSize { get; }
    public float BombProbability { get; }
}