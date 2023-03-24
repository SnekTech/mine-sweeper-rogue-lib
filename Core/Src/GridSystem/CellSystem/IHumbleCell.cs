using MineSweeperRogue.GridSystem.CellSystem.Components;

namespace MineSweeperRogue.GridSystem.CellSystem;

public interface IHumbleCell
{
    IFlag Flag { get; }
    ICover Cover { get; }

    // void SetHighlight(bool isHighlight);
    void Init(GridIndex gridIndex, int neighborBombCount);
}