using Cysharp.Threading.Tasks;
using SnekPlugin.MineSweeper.Cell.Components;
using SnekPlugin.MineSweeper.Cell.StateMachine;
using SnekPlugin.MineSweeper.Grid;

namespace SnekPlugin.MineSweeper.Cell;

public interface ICell
{
    GridIndex Index { get; }
    IGrid Parent { get; }

    ICover Cover { get; }
    IFlag Flag { get; }
    
    void Init();

    UniTask RevealAsync();
    UniTask SwitchFlagAsync();
    
    bool HasBomb { get; }
    
    int NeighborBombCount { get; }
    
    CellStateValue CurrentState { get; }
}