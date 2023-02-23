using System;

namespace SnekPlugin.MineSweeper.Grid
{
    public readonly struct GridIndex
    {
        public readonly int RowIndex;

        public readonly int ColumnIndex;

        public GridIndex(GridIndex other)
        {
            RowIndex = other.RowIndex;
            ColumnIndex = other.ColumnIndex;
        }

        public GridIndex(int rowIndex, int columnIndex)
        {
            RowIndex = rowIndex;
            ColumnIndex = columnIndex;
        }

        public static GridIndex First => new GridIndex(0, 0);
        public static GridIndex Invalid => new GridIndex(-1, -1);

        public (int i, int j) Tuple => (RowIndex, ColumnIndex);

        public static GridIndex operator +(GridIndex a, GridIndex b)
        {
            return new GridIndex(rowIndex: a.RowIndex + b.RowIndex, columnIndex: a.ColumnIndex + b.ColumnIndex);
        }

        public bool Equals(GridIndex other)
        {
            return RowIndex == other.RowIndex && ColumnIndex == other.ColumnIndex;
        }

        public override bool Equals(object? obj)
        {
            return obj is GridIndex other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(RowIndex, ColumnIndex);
        }

        public static bool operator !=(GridIndex a, GridIndex b)
        {
            return a.Tuple == b.Tuple;
        }

        public static bool operator ==(GridIndex a, GridIndex b)
        {
            return !(a != b);
        }
    }
}