using System.Collections.Generic;
using SnekTech.MineSweeperRogue.GridSystem.CellSystem;

namespace SnekTech.MineSweeperRogue.GridSystem;

public interface IHumbleGrid
{
    IGrid Grid { get; }
    List<IHumbleCell> InstantiateHumbleCells(int count);
}