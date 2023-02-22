using SnekPlugin.MineSweeper;
using SnekPlugin.MineSweeper.Grid;
using SnekPluginTest.MineSweeper.Builders;

namespace SnekPluginTest.MineSweeper.Tests;

public struct GridRevealTestCase
{
    public readonly BombMatrix BombMatrix;
    public readonly bool[,] IsCoveredBefore;
    public readonly bool[,] IsCoveredAfter;
    public readonly GridIndex CellIndex;

    public GridRevealTestCase(BoolMatrix bomb, BoolMatrix isCoveredBefore, BoolMatrix isCoveredAfter,
        GridIndex cellIndex)
    {
        BombMatrix = A.BombMatrix.WithBoolMatrix(bomb.Matrix);
        IsCoveredBefore = isCoveredBefore.Matrix;
        IsCoveredAfter = isCoveredAfter.Matrix;
        CellIndex = cellIndex;
    }
}
