using Cysharp.Threading.Tasks;
using SnekPlugin.Core.CustomExtensions;
using SnekPlugin.MineSweeper.Grid;
using SnekPluginTest.MineSweeper.AssertExtensions;
using SnekPluginTest.MineSweeper.Builders;

namespace SnekPluginTest.MineSweeper.Tests;

public class GridRevealMechanismSpecs
{
    private static GridRevealTestCase[] s_cases =
        {
            new GridRevealTestCase(
                new[]
                {
                    "111",
                    "101",
                    "111",
                },
                new[]
                {
                    "111",
                    "111",
                    "111",
                },
                new[]
                {
                    "111",
                    "101",
                    "111",
                },
                new GridIndex(1, 1)
            ),
            new GridRevealTestCase(
                new[]
                {
                    "000",
                    "000",
                    "000",
                },
                new[]
                {
                    "111",
                    "111",
                    "111",
                },
                new[]
                {
                    "000",
                    "000",
                    "000",
                },
                new GridIndex(1, 1)
            ),
            new GridRevealTestCase(
                new[]
                {
                    "10000",
                    "10000",
                    "10000",
                    "11111",
                    "11111",
                },
                new[]
                {
                    "11111",
                    "11111",
                    "11111",
                    "11111",
                    "11111",
                },
                new[]
                {
                    "10000",
                    "10000",
                    "10000",
                    "11111",
                    "11111",
                },
                new GridIndex(1, 3)
            ),
            new GridRevealTestCase(
                new[]
                {
                    "00000",
                    "00000",
                    "00100",
                    "00000",
                    "00000",
                },
                new[]
                {
                    "11111",
                    "11111",
                    "11111",
                    "11111",
                    "11111",
                },
                new[]
                {
                    "00000",
                    "00000",
                    "00100",
                    "00000",
                    "00000",
                },
                new GridIndex(0, 0)
            ),
        };

    [TestCaseSource(nameof(s_cases))]
    public async Task reveal_With_Recursion(GridRevealTestCase testCase)
    {
        // Arrange
        var grid = await A.GridBuilder.WithBombMatrix(testCase.HasBombMatrix).Build();

        // Act
        var restoreRevealedCellTasks = testCase.IsCoveredBefore.Matrix.Indices()
            .Select(gridIndex =>
            {
                var (i, j) = gridIndex;
                var cell = grid.GetCellAt(new GridIndex(i, j));
                var shouldBeRevealed = ! testCase.IsCoveredBefore[i, j];
                return shouldBeRevealed ? cell.RevealAsync() : UniTask.CompletedTask;
            });
        
        await UniTask.WhenAll(restoreRevealedCellTasks);
        
        await grid.RevealAtAsync(testCase.CellIndex);


        // Assert
        var actualIsCoveredAfter = grid.GetIsCoveredMatrix();
        actualIsCoveredAfter.Should().BeEquivalentTo(testCase.IsCoveredAfter, $"the cell at{testCase.CellIndex.Tuple} should be revealed");
    }
}