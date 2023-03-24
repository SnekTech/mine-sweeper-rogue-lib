using MineSweeperRogue.GridSystem;
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
        string[] hasBomb,
        CellStateMatrix originalStateMatrix,
        CellStateMatrix expectedStateMatrix,
        GridIndex targetCellIndex,
        string because = ""
    )
    {
        HasBombMatrix = A.BombMatrix.WithBombRows(hasBomb);
        OriginalStateMatrix = originalStateMatrix;
        ExpectedStateMatrix = expectedStateMatrix;
        TargetCellIndex = targetCellIndex;
        Because = because;
    }


}