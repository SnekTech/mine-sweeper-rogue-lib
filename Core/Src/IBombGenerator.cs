namespace MineSweeperRogue.MineSweeper;

public interface IBombGenerator
{
    bool NextHasBomb(float probability);
}