using SnekPlugin.MineSweeper;
using SnekPlugin.MineSweeper.Grid;
using SnekPluginTest.MineSweeper.Builders;

namespace SnekPluginTest.MineSweeper.Tests;

public struct GridRevealTestCase
{
    public readonly BombMatrix HasBombMatrix;
    public readonly BoolMatrix IsCoveredBefore;
    public readonly BoolMatrix IsCoveredAfter;
    public readonly GridIndex CellIndex;

    public GridRevealTestCase(BoolMatrix hasBomb, BoolMatrix isCoveredBefore, BoolMatrix isCoveredAfter,
        GridIndex cellIndex)
    {
        HasBombMatrix = A.BombMatrix.WithBoolMatrix(hasBomb);
        IsCoveredBefore = isCoveredBefore;
        IsCoveredAfter = isCoveredAfter;
        CellIndex = cellIndex;
    }
}