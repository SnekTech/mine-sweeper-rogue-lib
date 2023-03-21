namespace MineSweeperRogue.Grid;

public interface IGridData
{
    GridSize GridSize { get; }
    float BombProbability { get; }
}