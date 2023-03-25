using System;

namespace SnekTech.MineSweeperRogue.GridSystem.CellSystem;

public interface IHumbleCell
{
    ICell Cell { get; }
    IFlag Flag { get; }
    ICover Cover { get; }

    // void SetHighlight(bool isHighlight);
    void Init(ICell cell);
}