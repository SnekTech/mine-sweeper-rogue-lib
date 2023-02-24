using System.Collections.Generic;
using SnekPlugin.MineSweeper.Cell;

namespace SnekPlugin.MineSweeper.Grid;

public interface IHumbleGrid
{
    List<IHumbleCell> InstantiateHumbleCells(int count);
}