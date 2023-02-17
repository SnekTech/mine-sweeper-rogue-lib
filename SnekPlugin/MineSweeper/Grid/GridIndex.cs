namespace SnekPlugin.MineSweeper.Grid
{
    public struct GridIndex
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

        public static GridIndex operator +(GridIndex a, GridIndex b)
        {
            return new GridIndex(rowIndex: a.RowIndex + b.RowIndex, columnIndex: a.ColumnIndex + b.ColumnIndex);
        }
    }
}