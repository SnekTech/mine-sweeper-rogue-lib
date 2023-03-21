using System.Collections.Generic;
using MineSweeperRogue.Grid.Cell;

namespace MineSweeperRogue.Grid;

public interface IHumbleGrid
{
    List<IHumbleCell> InstantiateHumbleCells(int count);
}