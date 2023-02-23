using Cysharp.Threading.Tasks;
using SnekPlugin.Core.CustomExtensions;
using SnekPlugin.MineSweeper.Grid;
using SnekPluginTest.MineSweeper.AssertExtensions;
using SnekPluginTest.MineSweeper.Builders;

namespace SnekPluginTest.MineSweeper.Tests;

public class GridRevealMechanismSpecs
{
    private static GridRevealTestCase[] s_matricesToRevealWithoutRecursion =
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
        };

    [TestCaseSource(nameof(s_matricesToRevealWithoutRecursion))]
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
        
        await grid.RevealCellAsync(testCase.CellIndex);


        // Assert
        var actualIsCoveredAfter = grid.GetIsCoveredMatrix();
        actualIsCoveredAfter.Should().BeEquivalentTo(testCase.IsCoveredAfter, $"the grid expects to be like this after revealed cell at{testCase.CellIndex.Tuple}");
    }
}