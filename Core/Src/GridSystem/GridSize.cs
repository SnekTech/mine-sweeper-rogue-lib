namespace SnekTech.MineSweeperRogue.GridSystem;

[System.Serializable]
public struct GridSize
{
    public int RowCount;
    public int ColumnCount;

    public int TotalCount => RowCount * ColumnCount;

    public GridSize(int rowCount, int columnCount)
    {
        ColumnCount = columnCount;
        RowCount = rowCount;
    }
    
    public GridSize((int rows, int columns) pair) : this(pair.rows, pair.columns)
    {}
    
    public (int rowCount, int columnCount) Tuple => (RowCount, ColumnCount);
    
    #region equalty stuff
    
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
    
    #endregion
}