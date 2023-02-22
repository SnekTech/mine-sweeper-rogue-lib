using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;
using SnekPlugin.Core.CustomExtensions;
using SnekPlugin.MineSweeper;
using SnekPlugin.MineSweeper.Grid;

namespace SnekPluginTest.MineSweeper.AssertExtensions;

public static class BombMatrixExtensions
{
    public static BombMatrixAssertions Should(this BombMatrix bombMatrix)
    {
        return new BombMatrixAssertions(bombMatrix);
    }
}

public class BombMatrixAssertions :
    ReferenceTypeAssertions<BombMatrix, BombMatrixAssertions>
{
    public BombMatrixAssertions(BombMatrix instance)
        : base(instance)
    {
    }

    protected override string Identifier => nameof(BombMatrix);

    [CustomAssertion]
    public AndConstraint<BombMatrixAssertions> Match(string[] bombRows, string because = "", params object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .Given<(string[] bombRows, GridSize size)>(() => (bombRows, new GridSize(bombRows.Length, bombRows[0].Length)))
            .ForCondition(pair => Subject.GridSize == pair.size)
            .FailWith($"subject bombMatrix and expected bombRows has different sizes")
            .Then
            .Given(_ => IsEquivalent(Subject, bombRows))
            .ForCondition(missPos => missPos == (-1, -1))
            .FailWith(
                "Expect {context} to match original bombRows {reason}, but found miss match at position: {0}",
                missPos => missPos);

        return new AndConstraint<BombMatrixAssertions>(this);
    }

    private static (int i, int j) IsEquivalent(BombMatrix bombMatrix, string[] bombRows)
    {
        var targetMatrix = BoolMatrix.From(bombRows);
        return bombMatrix.Matrix.FindMissMatch(targetMatrix);
    }
}