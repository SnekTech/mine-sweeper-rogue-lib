using Cysharp.Threading.Tasks;
using MineSweeperRogue.GridSystem.CellSystem.Components;
using MineSweeperRogue.GridSystem.CellSystem.StateMachine;

namespace MineSweeperRogue.GridSystem.CellSystem;

public interface ICell
{
    GridIndex Index { get; }
    IGrid Parent { get; }

    ICover Cover { get; }
    IFlag Flag { get; }
    
    UniTask Init();

    UniTask RevealAsync();
    UniTask SwitchFlagAsync();
    
    bool HasBomb { get; }
    
    int NeighborBombCount { get; }
    
    CellStateValue State { get; }
    bool IsFlagged { get; }
    bool IsCovered { get; }
    bool IsRevealed { get; }
}