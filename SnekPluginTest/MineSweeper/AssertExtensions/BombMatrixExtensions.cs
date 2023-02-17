using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;
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
    public AndConstraint<BombMatrixAssertions> Match(int[,] array2D, string because = "", params object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .ForCondition(Subject.Size == array2D.Size())
            .FailWith($"subject bombMatrix has size: {Subject.Size}, but {nameof(array2D)} has size: {array2D.Size()}")
            .Then
            .Given(() => IsEquivalent(Subject, array2D))
            .ForCondition(missPos => missPos == (-1, -1))
            .FailWith(
                "Expect {context} to match original array2D {reason}, but found element miss match at position: {0}",
                missPos => missPos);

        return new AndConstraint<BombMatrixAssertions>(this);
    }

    private static (int i, int j) IsEquivalent(BombMatrix bombMatrix, int[,] array2D)
    {
        var size = bombMatrix.Size;
        
        for (var i = 0; i < size.RowCount; i++)
        {
            for (var j = 0; j < size.ColumnCount; j++)
            {
                var shouldHaveBomb = array2D[i, j] != 0;
                if (bombMatrix[i, j] != shouldHaveBomb)
                {
                    return (i, j);
                }
            }
        }

        return (-1, -1);
    }
}