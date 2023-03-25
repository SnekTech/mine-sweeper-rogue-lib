using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using SnekTech.MineSweeperRogue.Core.CustomExtensions;
using SnekTech.MineSweeperRogue.GridSystem.CellSystem;

namespace SnekTech.MineSweeperRogue.GridSystem;

public class Grid : IGrid
{
    private readonly IHumbleGrid _humbleGrid;
    private ICell[,] _cellMatrix = null !;
    private BombMatrix _bombMatrix = null !;

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

    public Grid(IHumbleGrid humbleGrid)
    {
        _humbleGrid = humbleGrid;
    }

    public GridSize Size => _bombMatrix.GridSize;
    public int BombCount => _bombMatrix.BombCount;
    public int FlaggedCellCount => _cellMatrix.Values().Count(cell => cell.IsFlagged);
    public int RevealedCellCount => _cellMatrix.Values().Count(cell => cell.IsRevealed);
    public bool IsCleared => _cellMatrix.Values().Where(cell => !cell.HasBomb).All(cell => cell.IsRevealed);
    public List<ICell> Cells => _cellMatrix.Values().ToList();

    private bool IsValid(GridIndex gridIndex)
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
            var cell = new Cell(cellIndex, this, hasBomb, humbleCell);
            _cellMatrix[i, j] = cell;
        }

        var initCellTasks = _cellMatrix.Values().Select(cell => cell.Init());
        await UniTask.WhenAll(initCellTasks);

        await _humbleGrid.OnInitComplete();
    }

    public IEnumerable<ICell> GetNeighborsOf(ICell cell)
    {
        return from offset in NeighborOffsets
            select cell.Index + offset
            into cellIndex
            where IsValid(cellIndex)
            select GetCellAt(cellIndex);
    }

    public async UniTask RevealAtAsync(GridIndex gridIndex)
    {
        var cellsToReveal = new List<ICell>();
        FindCellsToReveal(gridIndex, cellsToReveal);

        var revealTasks = cellsToReveal.Select(cell => cell.RevealAsync());
        await UniTask.WhenAll(revealTasks);
    }

    public async UniTask RevealAroundAsync(GridIndex gridIndex)
    {
        var cell = GetCellAt(gridIndex);
        var canRevealAround = cell is {IsRevealed: true, HasBomb: false};
        if (!canRevealAround)
            return;

        var neighbors = GetNeighborsOf(cell).ToList();
        var flaggedNeighborCount = neighbors.Count(neighbor => neighbor.IsFlagged);
        if (flaggedNeighborCount != cell.NeighborBombCount)
        {
            return;
        }

        var cellsToReveal = new HashSet<ICell>();
        foreach (var neighbor in neighbors)
        {
            FindCellsToReveal(neighbor.Index, cellsToReveal);
        }

        var revealTasks = cellsToReveal.Select(c => c.RevealAsync());
        await UniTask.WhenAll(revealTasks);
    }

    private void FindCellsToReveal(GridIndex gridIndex, ICollection<ICell> cellsToReveal)
    {
        if (!IsValid(gridIndex)) return;

        var cell = GetCellAt(gridIndex);

        var visited = cellsToReveal.Contains(cell);
        if (visited) return;

        if (!cell.IsCovered) return;

        cellsToReveal.Add(cell);

        if (cell.NeighborBombCount > 0) return;

        foreach (var neighborCell in GetNeighborsOf(cell))
        {
            FindCellsToReveal(neighborCell.Index, cellsToReveal);
        }
    }
}