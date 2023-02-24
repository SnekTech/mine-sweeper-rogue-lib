using FluentAssertions;
using SnekPlugin.MineSweeper.Grid;
using SnekPluginTest.MineSweeper.Builders;
using SnekPluginTest.MineSweeper.AssertExtensions;

namespace SnekPluginTest.MineSweeper.Tests;

public class GridRevealMechanismSpecs
{
    public static readonly GridRevealTestCase[] RevealAtCases =
    {
        new GridRevealTestCase(
            new[]
            {
                // 🔳, ⛳, 💢, 💣,
                "💣💣💣",
                "💣💢💣",
                "💣💣💣",
            },
            new[]
            {
                // 🔳, ⛳, 💢, 💣,
                "🔳🔳🔳",
                "🔳🔳🔳",
                "🔳🔳🔳",
            },
            new[]
            {
                // 🔳, ⛳, 💢, 💣,
                "🔳🔳🔳",
                "🔳💢🔳",
                "🔳🔳🔳",
            },
            new GridIndex(1, 1)
        ),
        new GridRevealTestCase(
            new[]
            {
                // 🔳, ⛳, 💢, 💣,
                "💢💢💢",
                "💢💢💢",
                "💢💢💢",
            },
            new[]
            {
                "🔳🔳🔳",
                "🔳🔳🔳",
                "🔳🔳🔳",
            },
            new[]
            {
                "💢💢💢",
                "💢💢💢",
                "💢💢💢",
            },
            new GridIndex(1, 1)
        ),
        new GridRevealTestCase(
            new[]
            {
                // 🔳, ⛳, 💢, 💣,
                "💣💢💢💢💢",
                "💣💢💢💢💢",
                "💣💢💢💢💢",
                "💣💣💣💣💣",
                "💣💣💣💣💣",
            },
            new[]
            {
                // 🔳, ⛳, 💢, 💣,
                "🔳🔳🔳🔳🔳",
                "🔳🔳🔳🔳🔳",
                "🔳🔳🔳🔳🔳",
                "🔳🔳🔳🔳🔳",
                "🔳🔳🔳🔳🔳",
            },
            new[]
            {
                // 🔳, ⛳, 💢, 💣,
                "🔳💢💢💢💢",
                "🔳💢💢💢💢",
                "🔳💢💢💢💢",
                "🔳🔳🔳🔳🔳",
                "🔳🔳🔳🔳🔳",
            },
            new GridIndex(1, 3)
        ),
        new GridRevealTestCase(
            new[]
            {
                // 🔳, ⛳, 💢, 💣,
                "💢💢💢💢💢",
                "💢💢💢💢💢",
                "💢💢💣💢💢",
                "💢💢💢💢💢",
                "💢💢💢💢💢",
            },
            new[]
            {
                // 🔳, ⛳, 💢, 💣,
                "🔳🔳🔳🔳🔳",
                "🔳🔳🔳🔳🔳",
                "🔳🔳🔳🔳🔳",
                "🔳🔳🔳🔳🔳",
                "🔳🔳🔳🔳🔳",
            },
            new[]
            {
                // 🔳, ⛳, 💢, 💣,
                "💢💢💢💢💢",
                "💢💢💢💢💢",
                "💢💢🔳💢💢",
                "💢💢💢💢💢",
                "💢💢💢💢💢",
            },
            new GridIndex(0, 0)
        ),
    };

    public static readonly GridRevealTestCase[] RevealAroundCases =
    {
        new GridRevealTestCase(
            // 🔳, ⛳, 💢, 💣,
            new[]
            {
                "💣💣💣",
                "💣💢💣",
                "💣💣💣",
            },
            new[]
            {
                "🔳🔳🔳",
                "🔳💢🔳",
                "🔳🔳🔳",
            },
            new[]
            {
                "🔳🔳🔳",
                "🔳💢🔳",
                "🔳🔳🔳",
            },
            new GridIndex(1, 1),
            "no flag, no effect"
        ),
        new GridRevealTestCase(
            new[]
            {
                "💢💢💢",
                "💣💢💢",
                "💣💢💢",
            },
            new[]
            {
                "🔳🔳🔳",
                "⛳💢🔳",
                "⛳🔳🔳",
            },
            new[]
            {
                "💢💢💢",
                "⛳💢💢",
                "⛳💢💢",
            },
            new GridIndex(1, 1),
            "flag count matches unrevealed bomb count(2), can reveal around target, leave the 2 flagged untouched"
        ),
    };

    [Test]
    public async Task revealAtCall_shouldNot_TakeEffect_on_revealed_cell()
    {
        // Arrange
        var testCase = new GridRevealTestCase(
            // 🔳, ⛳, 💢, 💣,
            new[]
            {
                "💣💣💣",
                "💣💢💣",
                "💣💣💣",
            },
            new[]
            {
                "🔳🔳🔳",
                "🔳💢🔳",
                "🔳🔳🔳",
            },
            new[]
            {
                "🔳🔳🔳",
                "🔳💢🔳",
                "🔳🔳🔳",
            },
            new GridIndex(1, 1),
            "no flag, no effect"
        );
        
        var grid = await A.GridBuilder.WithBombMatrix(testCase.HasBombMatrix).Build();

        await grid.ResetStateAsync(testCase.OriginalStateMatrix);
        var targetCell = grid.GetCellAt(new GridIndex(1, 1));

        // Act
        await grid.RevealAtAsync(targetCell.Index);

        // Assert
        targetCell.IsRevealed.Should().BeTrue();
        var actualStateMatrix = grid.GridStateMatrix();
        actualStateMatrix.Should().BeEqualTo(testCase.ExpectedStateMatrix, "the grid state should not change");
    }

    [TestCaseSource(nameof(RevealAtCases))]
    public async Task reveal_at_recursively(GridRevealTestCase testCase)
    {
        // Arrange
        var grid = await A.GridBuilder.WithBombMatrix(testCase.HasBombMatrix).Build();
        await grid.ResetStateAsync(testCase.OriginalStateMatrix);

        // Act
        await grid.RevealAtAsync(testCase.TargetCellIndex);

        // Assert
        var actualStateMatrix = grid.GridStateMatrix();
        actualStateMatrix.Should().BeEqualTo(testCase.ExpectedStateMatrix,
            $"the cell at{testCase.TargetCellIndex.Tuple} should be revealed");
    }

    [Test]
    public async Task reveal_around_should_not_affect_covered_cell()
    {
    }

    [TestCaseSource(nameof(RevealAroundCases))]
    public async Task reveal_around_recursively(GridRevealTestCase testCase)
    {
        // Arrange
        var grid = await A.GridBuilder.WithBombMatrix(testCase.HasBombMatrix).Build();
        await grid.ResetStateAsync(testCase.OriginalStateMatrix);

        // Act
        await grid.RevealAroundAsync(testCase.TargetCellIndex);

        // Assert
        var actualStateMatrix = grid.GridStateMatrix();
        actualStateMatrix.Should().BeEqualTo(testCase.ExpectedStateMatrix);
    }
}