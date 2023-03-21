using Cysharp.Threading.Tasks;
using MineSweeperRogue.Grid.Cell.Components;

namespace MineSweeperRogue.Grid.Cell;

public interface IHumbleCell
{
    IFlag Flag { get; }
    ICover Cover { get; }
    
    UniTask<bool> RevealAsync();
    UniTask<bool> SwitchFlagAsync();

    void SetHighlight(bool isHighlight);
    void Init(GridIndex gridIndex, int neighborBombCount);
}