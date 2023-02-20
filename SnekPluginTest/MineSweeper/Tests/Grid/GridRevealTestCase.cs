using SnekPlugin.MineSweeper.Grid;
using SnekPluginTest.MineSweeper.Builders;

namespace SnekPluginTest.MineSweeper.Tests;

public struct GridRevealTestCase
{
    public readonly BombMatrix BombMatrix;
    public readonly int[,] IsCoveredBefore;
    public readonly int[,] IsCoveredAfter;
    public readonly GridIndex CellIndex;

    public GridRevealTestCase(int[,] bombMatrix, int[,] isCoveredBefore, int[,] isCoveredAfter, GridIndex cellIndex)
    {
        BombMatrix = A.BombMatrix.WithArray2D(bombMatrix);
        IsCoveredBefore = isCoveredBefore;
        IsCoveredAfter = isCoveredAfter;
        CellIndex = cellIndex;
    }
}