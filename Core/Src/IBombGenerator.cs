namespace SnekTech.MineSweeperRogue;

public interface IBombGenerator
{
    bool NextHasBomb(float probability);
}