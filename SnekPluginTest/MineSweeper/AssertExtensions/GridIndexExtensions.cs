using FluentAssertions;
using FluentAssertions.Execution;
using SnekPlugin.MineSweeper.Grid;

namespace SnekPluginTest.MineSweeper.AssertExtensions;

public static class GridIndexExtensions
{
    public static GridIndexAssertions Should(this GridIndex gridIndex)
    {
        return new GridIndexAssertions(gridIndex);
    }
}

public class GridIndexAssertions
{
    public GridIndexAssertions(GridIndex gridIndex)
    {
        Subject = gridIndex;
    }
    
    public GridIndex Subject { get; }

    [CustomAssertion]
    public AndConstraint<GridIndexAssertions> BeValidIndexOf(IGrid grid)
    {
        Execute.Assertion
            .ForCondition(grid.IsValid(Subject))
            .FailWith("Expected {0} to be a valid index for {1}, but it's invalid.", Subject, grid.Size);

        return new AndConstraint<GridIndexAssertions>(this);
    }

    [CustomAssertion]
    public AndConstraint<GridIndexAssertions> BeInvalidIndexOf(IGrid grid)
    {
        Execute.Assertion
            .ForCondition(!grid.IsValid(Subject))
            .FailWith("Expected {0} to an invalid index for {1}, but it's valid.", Subject, grid.Size);

        return new AndConstraint<GridIndexAssertions>(this);
    }
}