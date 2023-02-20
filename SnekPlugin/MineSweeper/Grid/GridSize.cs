using System;

namespace SnekPlugin.MineSweeper.Grid;

[System.Serializable]
public struct GridSize
{
    public readonly int RowCount;
    public readonly int ColumnCount;

    public int TotalCount => RowCount * ColumnCount;

    public GridSize(int rowCount, int columnCount)
    {
        ColumnCount = columnCount;
        RowCount = rowCount;
    }
    
    public bool Equals(GridSize other)
    {
        return RowCount == other.RowCount && ColumnCount == other.ColumnCount;
    }

    public override bool Equals(object? obj)
    {
        return obj is GridSize other && Equals(other);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            return (RowCount * 397) ^ ColumnCount;
        }
    }

    public static bool operator != (GridSize a, GridSize b)
    {
        return a.ColumnCount != b.ColumnCount || a.RowCount != b.RowCount;
    }

    public static bool operator ==(GridSize a, GridSize b)
    {
        return !(a != b);
    }
}