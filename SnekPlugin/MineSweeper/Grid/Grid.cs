﻿using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using SnekPlugin.MineSweeper.Cell;
using SnekPlugin.MineSweeper.Cell.StateMachine;

namespace SnekPlugin.MineSweeper.Grid;

public class Grid : IGrid
{
    private readonly IHumbleGrid _humbleGrid;
    private readonly List2D<ICell> _cellMatrix = new List2D<ICell>();
    private BombMatrix _bombMatrix;

    private static readonly GridIndex[] NeighborOffsets =
    {
        new GridIndex(-1, -1),
        new GridIndex(0, -1),
        new GridIndex(1, -1),
        new GridIndex(-1, 0),
        new GridIndex(1, 0),
        new GridIndex(-1, 1),
        new GridIndex(0, 1),
        new GridIndex(1, 1),
    };

    public Grid(BombMatrix bombMatrix, IHumbleGrid humbleGrid)
    {
        _bombMatrix = bombMatrix;
        _humbleGrid = humbleGrid;
    }

    public GridSize Size => _bombMatrix.GridSize;
    public int BombCount => _cellMatrix.Count(cell => cell.HasBomb);
    public int FlaggedCellCount => _cellMatrix.Count(cell => cell.CurrentState == CellStateValue.Flagged);
    public int RevealedCellCount => _cellMatrix.Count(cell => cell.CurrentState == CellStateValue.Revealed);

    public bool IsValid(GridIndex gridIndex)
    {
        var rowIndex = gridIndex.RowIndex;
        var columnIndex = gridIndex.ColumnIndex;
        
        return rowIndex >= 0 && rowIndex < Size.RowCount &&
               columnIndex >= 0 && columnIndex < Size.ColumnCount;
    }

    public ICell GetCellAt(GridIndex gridIndex)
    {
        if (!IsValid(gridIndex))
        {
            throw new ArgumentOutOfRangeException(nameof(gridIndex));
        }
        
        return _cellMatrix[gridIndex.RowIndex][gridIndex.ColumnIndex];
    }

    public async UniTask InitCells(BombMatrix bombMatrix)
    {
        _bombMatrix = bombMatrix;
        await InitCells();
    }

    private async UniTask InitCells()
    {
        var rowCount = Size.RowCount;
        var columnCount = Size.ColumnCount;
        var humbleCells = _humbleGrid.InstantiateHumbleCells(rowCount * columnCount);

        for (var i = 0; i < rowCount; i++)
        {
            var aRowOfCells = new List<ICell>();

            for (var j = 0; j < columnCount; j++)
            {
                var humbleCell = humbleCells[i * columnCount + j];
                var cellIndex = new GridIndex(i, j);
                var hasBomb = _bombMatrix[i, j];

                var cell = new Cell.Cell(cellIndex, this, hasBomb, humbleCell);
                aRowOfCells.Add(cell);
            }

            _cellMatrix.AddRow(aRowOfCells);
        }
        
        var initTasks = _cellMatrix.Select(cell => cell.Init());
        await UniTask.WhenAll(initTasks);
    }

    public List<ICell> GetNeighborsOf(ICell cell)
    {
        var neighbors = new List<ICell>();
        
        foreach (var offset in NeighborOffsets)
        {
            var cellIndex = cell.Index + offset;
            if (IsValid(cellIndex))
            {
                neighbors.Add(GetCellAt(cellIndex));
            }
        }

        return neighbors;
    }

    public UniTask RevealCellAsync(GridIndex gridIndex)
    {
        return UniTask.FromResult(true);
    }
}