using Cysharp.Threading.Tasks;
using SnekPlugin.MineSweeper.Cell.Components;
using SnekPlugin.MineSweeper.Grid;

namespace SnekPlugin.MineSweeper.Cell;

public interface IHumbleCell
{
    IFlag Flag { get; }
    ICover Cover { get; }
    
    UniTask<bool> Reveal();
    UniTask<bool> SwitchFlag();

    void SetHighlight(bool isHighlight);
    void Init(GridIndex gridIndex, int neighborBombCount);
}