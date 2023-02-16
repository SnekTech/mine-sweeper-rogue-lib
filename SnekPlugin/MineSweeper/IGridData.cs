namespace SnekPlugin.MineSweeper;

public interface IGridData
{
    GridSize GridSize { get; }
    float BombProbability { get; }
}