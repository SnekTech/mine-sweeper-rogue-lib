using SnekPlugin.MineSweeper;
using SnekPlugin.MineSweeper.Grid;
using SnekPluginTest.MineSweeper.Builders;

namespace SnekPluginTest.MineSweeper.Tests;

public struct GridRevealTestCase
{
    public readonly BombMatrix HasBombMatrix;
    public readonly CellStateMatrix OriginalStateMatrix;
    public readonly CellStateMatrix ExpectedStateMatrix;
    public readonly GridIndex TargetCellIndex;
    public readonly string Because;

    public GridRevealTestCase(
        BoolMatrix hasBomb,
        CellStateMatrix originalStateMatrix,
        CellStateMatrix expectedStateMatrix,
        GridIndex targetCellIndex,
        string because = ""
    )
    {
        HasBombMatrix = A.BombMatrix.WithBoolMatrix(hasBomb);
        OriginalStateMatrix = originalStateMatrix;
        ExpectedStateMatrix = expectedStateMatrix;
        TargetCellIndex = targetCellIndex;
        Because = because;
    }


}