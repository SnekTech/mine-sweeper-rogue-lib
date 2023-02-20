using System.Linq;
using Cysharp.Threading.Tasks;
using SnekPlugin.MineSweeper.Cell.Components;
using SnekPlugin.MineSweeper.Cell.StateMachine;
using SnekPlugin.MineSweeper.Grid;

namespace SnekPlugin.MineSweeper.Cell;

public class Cell : ICell
{
    private readonly IHumbleCell _humbleCell;
    
    private readonly CellStateMachine _stateMachine;

    public Cell(GridIndex index, IGrid parent, bool hasBomb, IHumbleCell humbleCell)
    {
        Index = index;
        Parent = parent;
        HasBomb = hasBomb;
        _humbleCell = humbleCell;

        Cover = _humbleCell.Cover;
        Flag = _humbleCell.Flag;

        _stateMachine = new CellStateMachine(this);
    }

    public GridIndex Index { get; }
    public IGrid Parent { get; }
    public ICover Cover { get; }
    public IFlag Flag { get; }

    public CellStateValue CurrentState => _stateMachine.CurrentStateValue;

    public int NeighborBombCount => HasBomb ? -1 :
        Parent.GetNeighborsOf(this).Count(neighbor => neighbor.HasBomb);

    public async UniTask Init()
    {
        await _stateMachine.SetInitState(_stateMachine.CachedCoveredState);
        _humbleCell.Init(Index, NeighborBombCount);
    }

    public UniTask RevealAsync()
    {
        return _stateMachine.CurrentState.OnReveal();
    }

    public UniTask SwitchFlagAsync() => _stateMachine.CurrentState.OnSwitchFlag();

    public bool HasBomb { get; }
}