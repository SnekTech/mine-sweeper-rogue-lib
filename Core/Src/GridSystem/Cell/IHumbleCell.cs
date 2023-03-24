using Cysharp.Threading.Tasks;
using MineSweeperRogue.GridSystem.Cell.Components;

namespace MineSweeperRogue.GridSystem.Cell;

public interface IHumbleCell
{
    IFlag Flag { get; }
    ICover Cover { get; }
    
    UniTask<bool> RevealAsync();
    UniTask<bool> SwitchFlagAsync();

    void SetHighlight(bool isHighlight);
    void Init(GridIndex gridIndex, int neighborBombCount);
}