namespace SnekPlugin.MineSweeper.Grid;

public interface IGridData
{
    GridSize GridSize { get; }
    float BombProbability { get; }
}