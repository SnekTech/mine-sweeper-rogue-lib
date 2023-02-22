using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using SnekPlugin.Core.CustomExtensions;
using SnekPlugin.MineSweeper.Cell;
using SnekPlugin.MineSweeper.Cell.StateMachine;

namespace SnekPlugin.MineSweeper.Grid;

public class Grid : IGrid
{
    private readonly IHumbleGrid _humbleGrid;
    private ICell[,] _cellMatrix;
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

        _cellMatrix = new ICell[Size.RowCount, Size.ColumnCount];
    }

    public GridSize Size => _bombMatrix.GridSize;
    public int BombCount => _cellMatrix.Values().Count(cell => cell.HasBomb);
    public int FlaggedCellCount => _cellMatrix.Values().Count(cell => cell.CurrentState == CellStateValue.Flagged);
    public int RevealedCellCount => _cellMatrix.Values().Count(cell => cell.CurrentState == CellStateValue.Revealed);

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

        return _cellMatrix[gridIndex.RowIndex, gridIndex.ColumnIndex];
    }

    public async UniTask InitCells(BombMatrix bombMatrix)
    {
        _bombMatrix = bombMatrix;
        var (rowCount, columnCount) = Size.Tuple;
        _cellMatrix = new ICell[rowCount, columnCount];

        var humbleCells = _humbleGrid.InstantiateHumbleCells(rowCount * columnCount);

        foreach (var (i, j) in _cellMatrix.Indices())
        {
            var humbleCell = humbleCells[i * columnCount + j];
            var hasBomb = _bombMatrix[i, j];

            var cellIndex = new GridIndex(i, j);
            var cell = new Cell.Cell(cellIndex, this, hasBomb, humbleCell);
            _cellMatrix[i, j] = cell;
        }

        var initTasks = _cellMatrix.Values().Select(cell => cell.Init());
        await UniTask.WhenAll(initTasks);
    }

    public IEnumerable<ICell> GetNeighborsOf(ICell cell)
    {
        return from offset in NeighborOffsets select cell.Index + offset into cellIndex where IsValid(cellIndex) select GetCellAt(cellIndex);
    }

    public UniTask RevealCellAsync(GridIndex gridIndex)
    {
        return UniTask.FromResult(true);
    }
}