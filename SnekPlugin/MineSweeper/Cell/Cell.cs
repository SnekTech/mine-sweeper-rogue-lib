﻿using System.Linq;
using Cysharp.Threading.Tasks;
using SnekPlugin.MineSweeper.Cell.Components;
using SnekPlugin.MineSweeper.Grid;

namespace SnekPlugin.MineSweeper.Cell;

public class Cell : ICell
{
    private readonly IHumbleCell _humbleCell;

    public Cell(GridIndex index, IGrid parent, bool hasBomb, IHumbleCell humbleCell)
    {
        Index = index;
        Parent = parent;
        HasBomb = hasBomb;
        _humbleCell = humbleCell;

        Cover = _humbleCell.Cover;
        Flag = _humbleCell.Flag;
    }

    public GridIndex Index { get; }
    public IGrid Parent { get; }
    public ICover Cover { get; }
    public IFlag Flag { get; }

    public int NeighborBombCount => HasBomb ? -1 :
        Parent.GetNeighborsOf(this).Count(neighbor => neighbor.HasBomb);

    public void Init()
    {
        _humbleCell.Init(Index, NeighborBombCount);
    }

    public UniTask<bool> Reveal() => _humbleCell.Reveal();

    public UniTask<bool> SwitchFlag() => _humbleCell.SwitchFlag();

    public bool HasBomb { get; }
    public bool IsFlagged { get; }
    public bool IsCovered { get; }
    public bool IsRevealed { get; }
}