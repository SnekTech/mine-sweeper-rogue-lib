namespace SnekPlugin.MineSweeper
{
    public struct GridIndex
    {
        public int RowIndex;

        public int ColumnIndex;

        public static GridIndex Zero = new GridIndex
        {
            RowIndex = 0,
            ColumnIndex = 0,
        };

        public GridIndex(GridIndex other)
        {
            RowIndex = other.RowIndex;
            ColumnIndex = other.ColumnIndex;
        }

        public static GridIndex operator +(GridIndex a, GridIndex b)
        {
            return new GridIndex
            {
                RowIndex = a.RowIndex + b.RowIndex,
                ColumnIndex = a.ColumnIndex + b.ColumnIndex,
            };
        }
    }
}