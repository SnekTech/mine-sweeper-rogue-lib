using System.Collections.Generic;
using MineSweeperRogue.GridSystem.CellSystem;

namespace MineSweeperRogue.GridSystem;

public interface IHumbleGrid
{
    IGrid Grid { get; }
    List<IHumbleCell> InstantiateHumbleCells(int count);
}