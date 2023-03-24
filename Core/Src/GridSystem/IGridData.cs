namespace MineSweeperRogue.GridSystem;

public interface IGridData
{
    GridSize GridSize { get; }
    float BombProbability { get; }
}