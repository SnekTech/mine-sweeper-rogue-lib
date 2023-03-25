namespace SnekTech.MineSweeperRogue.GridSystem.CellSystem;

public interface IHumbleCell
{
    IFlag Flag { get; }
    ICover Cover { get; }

    // void SetHighlight(bool isHighlight);
    void Init(ICell cell);
}