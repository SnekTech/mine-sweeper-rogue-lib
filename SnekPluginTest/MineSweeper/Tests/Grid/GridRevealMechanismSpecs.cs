using Cysharp.Threading.Tasks;
using FluentAssertions;
using SnekPlugin.MineSweeper;
using SnekPlugin.MineSweeper.Cell.StateMachine;
using SnekPlugin.MineSweeper.Grid;
using SnekPluginTest.MineSweeper.AssertExtensions;
using SnekPluginTest.MineSweeper.Builders;

namespace SnekPluginTest.MineSweeper.Tests;

public class GridRevealMechanismSpecs
{
    private static GridRevealTestCase[] s_matricesToRevealWithoutRecursion =
        {
            new GridRevealTestCase(
                new[,]
                {
                    {1, 1, 1},
                    {1, 0, 1},
                    {1, 1, 1},
                },
                new[,]
                {
                    {1, 1, 1},
                    {1, 1, 1},
                    {1, 1, 1},
                },
                new[,]
                {
                    {1, 1, 1},
                    {1, 0, 1},
                    {1, 1, 1},
                },
                new GridIndex(1, 1)
            ),
        };

    [TestCaseSource(nameof(s_matricesToRevealWithoutRecursion))]
    public async Task reveal_Without_Recursion(GridRevealTestCase testCase)
    {
        // Arrange
        var grid = await A.GridBuilder.WithBombMatrix(testCase.BombMatrix).Build();

        // Act
        var restoreRevealedCellTasks = testCase.IsCoveredBefore.GridIndices()
            .Select(gridIndex =>
            {
                var cell = grid.GetCellAt(gridIndex);
                var shouldBeCovered = testCase.IsCoveredBefore[gridIndex.I, gridIndex.J] != 0;
                return shouldBeCovered ? UniTask.CompletedTask : cell.RevealAsync();
            });
        
        await UniTask.WhenAll(restoreRevealedCellTasks);
        
        await grid.RevealCellAsync(testCase.CellIndex);


        // Assert
        var actualIsCoveredAfter = grid.GetIsCoveredMatrix();
        actualIsCoveredAfter.Should().BeEquivalentTo(testCase.IsCoveredAfter);
    }
}