using SnekPlugin.MineSweeper.Grid;

namespace SnekPlugin.MineSweeper;

public interface IBombGenerator
{
    bool NextHasBomb(float probability);
}